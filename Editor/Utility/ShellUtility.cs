using System.Diagnostics;
using System.Text;
using System.Threading;

namespace GameFrameX.Editor
{
    /// <summary>
    /// 提供跨平台执行外部 Shell 命令的工具。
    /// </summary>
    /// <remarks>
    /// Provides utilities for executing external shell commands.
    /// </remarks>
    public static partial class ShellUtility
    {
        /// <summary>
        /// 请求中止命令执行的处理委托。
        /// </summary>
        /// <remarks>
        /// Handler for requesting an abort of a command execution.
        /// </remarks>
        /// <param name="kill">是否立即终止进程；为 false 时尝试礼貌关闭。 / Whether to kill the process immediately; when false, it will ask politely.</param>
        public delegate void ShellRequestAbortEventHandler(bool kill);

        /// <summary>
        /// 通过该接口使用方可以监视命令的输出，甚至中止命令。
        /// 同一个监视器可以连续用于多条命令。
        /// </summary>
        /// <remarks>
        /// Using this interface the user can monitor what is the output of the command and even abort it.
        /// You can use one monitor on multiple commands in a row.
        /// </remarks>
        public interface IShellMonitor
        {
            // NOTE: These methods can be called from a different thread!
            /// <summary>
            /// 追加 Shell 命令及其参数。
            /// </summary>
            /// <remarks>
            /// Appends the shell command and its arguments.
            /// </remarks>
            /// <param name="command">命令名。 / The command name.</param>
            /// <param name="args">命令参数。 / The command arguments.</param>
            void AppendCommand(string command, string args); // Append shell command + arguments.
            /// <summary>
            /// 追加一行输出流内容。
            /// </summary>
            /// <remarks>
            /// Appends a line from the output stream.
            /// </remarks>
            /// <param name="line">输出行。 / The output line.</param>
            void AppendOutputLine(string line); // Append line from the output stream.
            /// <summary>
            /// 追加一行错误流内容。
            /// </summary>
            /// <remarks>
            /// Appends a line from the error stream.
            /// </remarks>
            /// <param name="line">错误行。 / The error line.</param>
            void AppendErrorLine(string line); // Append line from the error stream.

            /// <summary>
            /// 获取是否已请求中止；为 true 时后续所有命令将立即中止。
            /// </summary>
            /// <remarks>
            /// Gets whether an abort has been requested; if true, all subsequent commands will abort immediately.
            /// </remarks>
            bool AbortRequested { get; } // If true, all subsequent commands will abort immediately.

            /// <summary>
            /// 用户请求中止操作时触发的事件。
            /// </summary>
            /// <remarks>
            /// Event invoked when the user requests abort of the operation.
            /// </remarks>
            event ShellRequestAbortEventHandler RequestAbort; // Invoked when user requests abort of the operation.
            // Kill will terminate the process immediately. If false, it will ask politely.
        }

        /// <summary>
        /// Shell 命令执行参数。
        /// </summary>
        /// <remarks>
        /// Arguments for executing a shell command.
        /// </remarks>
        public struct ShellArgs
        {
            /// <summary>
            /// 要执行的命令名。
            /// </summary>
            /// <remarks>
            /// The command to execute.
            /// </remarks>
            public string Command;
            /// <summary>
            /// 命令参数。
            /// </summary>
            /// <remarks>
            /// The command arguments.
            /// </remarks>
            public string Args;
            /// <summary>
            /// 命令的工作目录。
            /// </summary>
            /// <remarks>
            /// The working directory of the command.
            /// </remarks>
            public string WorkingDirectory;
            /// <summary>
            /// 是否等待命令输出结束。
            /// </summary>
            /// <remarks>
            /// Whether to wait for the command output to finish.
            /// </remarks>
            public bool WaitForOutput;

            /// <summary>
            /// 等待超时时间（毫秒）；-1 表示一直等待。仅在 WaitForOutput 为 true 时生效。
            /// </summary>
            /// <remarks>
            /// The wait timeout in milliseconds; -1 means wait forever. Only effective when WaitForOutput is true.
            /// </remarks>
            public int
                WaitTimeout; // WaitTimeout is in milliseconds. -1 means forever. Only if WaitForOutput is true.

            /// <summary>
            /// 命令输出监视器；可为空。
            /// </summary>
            /// <remarks>
            /// The monitor for the command output; may be null.
            /// </remarks>
            public IShellMonitor Monitor;
        }

        /// <summary>
        /// Shell 命令执行结果。
        /// </summary>
        /// <remarks>
        /// The result of a shell command execution.
        /// </remarks>
        public struct ShellResult
        {
            /// <summary>
            /// 已执行的命令名。
            /// </summary>
            /// <remarks>
            /// The executed command name.
            /// </remarks>
            public string Command;
            /// <summary>
            /// 已执行的命令参数。
            /// </summary>
            /// <remarks>
            /// The executed command arguments.
            /// </remarks>
            public string Args;

            /// <summary>
            /// 标准输出内容。
            /// </summary>
            /// <remarks>
            /// The standard output content.
            /// </remarks>
            public string Output;
            /// <summary>
            /// 错误输出内容。
            /// </summary>
            /// <remarks>
            /// The error output content.
            /// </remarks>
            public string Error;

            /// <summary>
            /// 获取是否发生了错误。
            /// </summary>
            /// <remarks>
            /// Gets whether any error occurred.
            /// </remarks>
            public bool HasErrors => !string.IsNullOrEmpty(Error);
        }

        /// <summary>
        /// 用户中止操作时的日志文案。
        /// </summary>
        /// <remarks>
        /// The log message used when the user aborts the operation.
        /// </remarks>
        public const string USER_ABORTED_LOG = "User aborted the operation...";

        /// <summary>
        /// 执行外部命令并等待其输出结束。
        /// </summary>
        /// <remarks>
        /// Executes an external command and waits for its output.
        /// </remarks>
        /// <param name="command">命令名。 / The command name.</param>
        /// <param name="args">命令参数。 / The command arguments.</param>
        /// <returns>命令执行结果。 / The execution result of the command.</returns>
        public static ShellResult ExecuteCommand(string command, string args)
        {
            return ExecuteCommand(command, args, true);
        }

        /// <summary>
        /// 执行外部命令并等待其输出结束，同时通过监视器监视输出。
        /// </summary>
        /// <remarks>
        /// Executes an external command, waits for its output, and monitors it via the given monitor.
        /// </remarks>
        /// <param name="command">命令名。 / The command name.</param>
        /// <param name="args">命令参数。 / The command arguments.</param>
        /// <param name="monitor">命令输出监视器。 / The monitor for the command output.</param>
        /// <returns>命令执行结果。 / The execution result of the command.</returns>
        public static ShellResult ExecuteCommand(string command, string args, IShellMonitor monitor)
        {
            return ExecuteCommand(command, args, true, monitor);
        }

        /// <summary>
        /// 执行外部命令，可指定是否等待输出结束。
        /// </summary>
        /// <remarks>
        /// Executes an external command, optionally waiting for its output to finish.
        /// </remarks>
        /// <param name="command">命令名。 / The command name.</param>
        /// <param name="args">命令参数。 / The command arguments.</param>
        /// <param name="waitForOutput">是否等待输出结束。 / Whether to wait for the output to finish.</param>
        /// <param name="monitor">命令输出监视器；可为空。 / The monitor for the command output; may be null.</param>
        /// <returns>命令执行结果。 / The execution result of the command.</returns>
        public static ShellResult ExecuteCommand(string command, string args, bool waitForOutput,
            IShellMonitor monitor = null)
        {
            return ExecuteCommand(new ShellArgs()
            {
                Command = command,
                Args = args,
                WaitForOutput = waitForOutput,
                WaitTimeout = -1,
                Monitor = monitor
            });
        }

        /// <summary>
        /// 执行外部命令并等待输出，可指定等待超时时间。
        /// </summary>
        /// <remarks>
        /// Executes an external command and waits for its output, with an optional wait timeout.
        /// </remarks>
        /// <param name="command">命令名。 / The command name.</param>
        /// <param name="args">命令参数。 / The command arguments.</param>
        /// <param name="waitTimeout">等待超时时间（毫秒）；-1 表示一直等待。 / The wait timeout in milliseconds; -1 means wait forever.</param>
        /// <param name="monitor">命令输出监视器；可为空。 / The monitor for the command output; may be null.</param>
        /// <returns>命令执行结果。 / The execution result of the command.</returns>
        public static ShellResult ExecuteCommand(string command, string args, int waitTimeout,
            IShellMonitor monitor = null)
        {
            return ExecuteCommand(new ShellArgs()
            {
                Command = command,
                Args = args,
                WaitForOutput = true,
                WaitTimeout = waitTimeout,
                Monitor = monitor
            });
        }

        /// <summary>
        /// 按给定参数执行外部命令。
        /// </summary>
        /// <remarks>
        /// Executes an external command with the given arguments.
        /// </remarks>
        /// <param name="shellArgs">命令执行参数。 / The arguments for executing the command.</param>
        /// <returns>命令执行结果。 / The execution result of the command.</returns>
        public static ShellResult ExecuteCommand(ShellArgs shellArgs)
        {
            shellArgs.Command = shellArgs.Command ?? string.Empty;
            shellArgs.Args = shellArgs.Args ?? string.Empty;
            shellArgs.WorkingDirectory = shellArgs.WorkingDirectory ?? string.Empty;

            var result = new ShellResult();

            result.Command = shellArgs.Command;
            result.Args = shellArgs.Args;
            result.Output = string.Empty;
            result.Error = string.Empty;

            if (shellArgs.Monitor != null)
            {
                if (shellArgs.Monitor.AbortRequested)
                {
                    result.Error = USER_ABORTED_LOG;
                    return result;
                }

                shellArgs.Monitor.AppendCommand(shellArgs.Command, shellArgs.Args);
            }

            var processStartInfo = new ProcessStartInfo(shellArgs.Command, shellArgs.Args);
            processStartInfo.RedirectStandardOutput = true;
            processStartInfo.RedirectStandardError = true;
            processStartInfo.RedirectStandardInput = true;
            processStartInfo.CreateNoWindow = true;
            processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            processStartInfo.UseShellExecute = false;
            processStartInfo.WorkingDirectory = shellArgs.WorkingDirectory;

            Process process;
            try
            {
                process = Process.Start(processStartInfo);
                // Input is not supported so close it directly. Prevents hangs when CLI prompts the user for something (authentication for example).
                process.StandardInput.Close();
            }
            catch (System.Exception ex)
            {
                // Most probably file not found.
                result.Error = ex.ToString();

                if (shellArgs.Monitor != null)
                {
                    shellArgs.Monitor.AppendErrorLine(result.Error);
                }

                return result;
            }

            // Run and forget...
            // TODO: No Process.Dispose() called - leak?!
            if (!shellArgs.WaitForOutput)
            {
                result.Output = string.Empty;
                result.Error = string.Empty;

                return result;
            }

            //
            // Handle aborting.
            //
            ShellRequestAbortEventHandler abortHandler = null;
            if (shellArgs.Monitor != null)
            {
                abortHandler = (bool kill) =>
                {
                    shellArgs.Monitor?.AppendErrorLine(USER_ABORTED_LOG);

                    // TODO: Is this thread safe?
                    if (kill)
                    {
                        process.Kill();
                    }
                    else
                    {
                        process.CloseMainWindow();
                    }
                };

                shellArgs.Monitor.RequestAbort += abortHandler;
            }

            //
            // Subscribe for standard output.
            //
            var outputBuilder = new StringBuilder();
            DataReceivedEventHandler outputReadLineHandler = null;
            outputReadLineHandler = (sender, args) =>
            {
                // Lock - check the Builder usage at the end.
                lock (outputBuilder)
                {
                    if (args.Data != null)
                    {
                        outputBuilder.AppendLine(args.Data);
                        if (shellArgs.Monitor != null)
                        {
                            shellArgs.Monitor.AppendOutputLine(args.Data);
                        }
                    }
                }
            };
            process.OutputDataReceived += outputReadLineHandler;
            process.BeginOutputReadLine();

            //
            // Subscribe for error output.
            //
            var errorBuilder = new StringBuilder();
            DataReceivedEventHandler errorReadLineHandler = null;
            errorReadLineHandler = (sender, args) =>
            {
                // Lock - check the Builder usage at the end.
                lock (errorBuilder)
                {
                    if (args.Data != null)
                    {
                        errorBuilder.AppendLine(args.Data);
                        if (shellArgs.Monitor != null)
                        {
                            shellArgs.Monitor.AppendErrorLine(args.Data);
                        }
                    }
                }
            };
            process.ErrorDataReceived += errorReadLineHandler;
            process.BeginErrorReadLine();

            if (shellArgs.WaitTimeout < 0)
            {
                process.WaitForExit();
            }
            else
            {
                var stopwatch = new Stopwatch();
                stopwatch.Start();
                while (!process.HasExited && stopwatch.ElapsedMilliseconds < shellArgs.WaitTimeout)
                {
                    Thread.Sleep(20);
                }

                stopwatch.Stop();

                // If process is still running, the timeout kicked in.
                if (!process.HasExited)
                {
                    result.Error += $"Command [{shellArgs.Command} {shellArgs.Args}] timed out.";
                }
            }

            process.OutputDataReceived -= outputReadLineHandler;
            lock (outputBuilder)
            {
                // When the main thread gets here, the process will not be running (unless it timed out),
                // but the OutputDataReceived thread might still be appending the final strings. Lock it!
                result.Output = outputBuilder.ToString();
            }

            process.ErrorDataReceived -= errorReadLineHandler;
            lock (errorBuilder)
            {
                // Same as above. Concat if error was present.
                result.Error += errorBuilder.ToString();
            }

            if (shellArgs.Monitor != null)
            {
                shellArgs.Monitor.RequestAbort -= abortHandler;
            }

            // Dispose() (invoking Close()) will wait for the process to finish.
            // If process is stuck, this will hang Unity on recompile / exit.
            // Not calling Dispose() in that regard will leak some resources / processes, but that shouldn't be the normal case anyway.
            if (process.HasExited)
            {
                process.Dispose();
            }

            return result;
        }
    }
}