using System.Text;
using GameFrameX.Editor;
using NUnit.Framework;
using UnityEngine;

namespace GameFrameX.Tests
{
    [TestFixture]
    public class ShellUtilityTests
    {
        #region 测试用监视器

        private sealed class TestShellMonitor : ShellUtility.IShellMonitor
        {
            private readonly bool m_AbortRequested;

            public TestShellMonitor(bool abortRequested)
            {
                m_AbortRequested = abortRequested;
                Commands = new StringBuilder();
                Output = new StringBuilder();
                Errors = new StringBuilder();
            }

            public StringBuilder Commands { get; }

            public StringBuilder Output { get; }

            public StringBuilder Errors { get; }

            public bool AbortRequested
            {
                get { return m_AbortRequested; }
            }

            public event ShellUtility.ShellRequestAbortEventHandler RequestAbort;

            public void AppendCommand(string command, string args)
            {
                Commands.Append(command).Append(' ').AppendLine(args);
            }

            public void AppendOutputLine(string line)
            {
                Output.AppendLine(line);
            }

            public void AppendErrorLine(string line)
            {
                Errors.AppendLine(line);
            }

            public void RaiseAbort(bool kill)
            {
                if (RequestAbort != null)
                {
                    RequestAbort(kill);
                }
            }
        }

        #endregion

        [Test]
        public void ExecuteCommand_AbortRequested_ReturnsUserAbortedWithoutStarting()
        {
            var monitor = new TestShellMonitor(true);

            var result = ShellUtility.ExecuteCommand("echo", "hello", monitor);

            Assert.IsTrue(result.HasErrors);
            Assert.AreEqual(ShellUtility.USER_ABORTED_LOG, result.Error.Trim());
            Assert.AreEqual(0, monitor.Commands.Length, "中止时不应下发命令到监视器");
        }

        [Test]
        public void ExecuteCommand_UnknownCommand_CapturesErrorWithoutThrowing()
        {
            var result = ShellUtility.ExecuteCommand("gfx_no_such_command_xyz_12345", "");

            Assert.IsTrue(result.HasErrors);
            Assert.IsNotEmpty(result.Error);
        }

        [Test]
        public void ExecuteCommand_Echo_ReturnsOutput()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Assert.Ignore("echo 不是 Windows 可执行文件");
            }

            var result = ShellUtility.ExecuteCommand("echo", "hello");

            Assert.IsFalse(result.HasErrors, result.Error);
            Assert.IsTrue(result.Output.Contains("hello"));
        }

        [Test]
        public void ExecuteCommand_MonitorReceivesCommandAndOutput()
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                Assert.Ignore("echo 不是 Windows 可执行文件");
            }

            var monitor = new TestShellMonitor(false);

            var result = ShellUtility.ExecuteCommand("echo", "monitored", monitor);

            Assert.IsFalse(result.HasErrors, result.Error);
            StringAssert.Contains("echo monitored", monitor.Commands.ToString());
            StringAssert.Contains("monitored", monitor.Output.ToString());
        }
    }
}
