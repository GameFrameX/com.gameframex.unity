using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using GameFrameX.Runtime;
using UnityEditor;
using UnityEditor.Build.Reporting;
using UnityEditor.Callbacks;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace GameFrameX.Editor
{
    /// <summary>
    ///  导出和发布产品帮助类
    /// </summary>
    public static class BuildProductHelper
    {
        private static string _buildPath;

        private static List<System.Type> RunHook(System.Type hookType)
        {
            List<System.Type> result = new List<System.Type>();
            var types = Utility.Assembly.GetTypes();
            foreach (var type in types)
            {
                if (type.IsAbstract)
                {
                    continue;
                }

                var isBuilderPreHookHandler = type.IsImplWithInterface(hookType);
                if (!isBuilderPreHookHandler)
                {
                    continue;
                }

                result.Add(type);
            }

            return result;
        }

        private static void RunPreHookBuild()
        {
            var types = RunHook(typeof(IBuilderPreHookHandler));
            List<IBuilderPreHookHandler> result = new List<IBuilderPreHookHandler>();
            foreach (var type in types)
            {
                var instance = (IBuilderPreHookHandler)Activator.CreateInstance(type);
                result.Add(instance);
            }

            result.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            foreach (var handler in result)
            {
                handler.Run(EditorUserBuildSettings.activeBuildTarget, _buildPath);
            }
        }

        private static void RunPostHookBuild()
        {
            var types = RunHook(typeof(IBuilderPostHookHandler));
            var result = new List<IBuilderPostHookHandler>();
            foreach (var type in types)
            {
                var instance = (IBuilderPostHookHandler)Activator.CreateInstance(type);
                result.Add(instance);
            }

            result.Sort((a, b) => a.Priority.CompareTo(b.Priority));
            foreach (var handler in result)
            {
                handler.Run(EditorUserBuildSettings.activeBuildTarget, _buildPath);
            }
        }

        /// <summary>
        /// 发布 当前激活的平台
        /// </summary>
        /*[MenuItem("GameFrameX/Build/Active Build Target", false, 20)]
        public static void BuildPlayerToActiveBuildTarget()
        {
            PlayerSettings.SplashScreen.show = false;
            UpdateBuildTime();

            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSX)
            {
                PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
                PlayerSettings.defaultScreenHeight = 720;
                PlayerSettings.defaultScreenWidth = 1280;
            }

            AssetDatabase.SaveAssets();
            var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, BuildOutputPath(), EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
            if (buildReport.summary.result == BuildResult.Succeeded)
            {
                Debug.LogError("Build Output Path:" + BuildOutputPath());
            }
        }*/
        [MenuItem("GameFrameX/Build/Windows X64", false, 100)]
        public static void BuildPlayerToWindows64BuildTarget()
        {
            // PlayerSettings.SplashScreen.show = false;
            Debug.Log(EditorUserBuildSettings.activeBuildTarget);
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneWindows64)
            {
                Debug.LogError("当前构建目标不是 Windows");
                return;
            }

            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
                PlayerSettings.defaultScreenHeight = 720;
                PlayerSettings.defaultScreenWidth = 1280;
                EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows64;
                UpdateBuildTime();
                AssetDatabase.SaveAssets();
                _buildPath = BuildOutputPath();
                var resultDirectory = _buildPath + Path.DirectorySeparatorChar;
                RunPreHookBuild();
                var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, resultDirectory + PlayerSettings.productName + ".exe", EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
                if (buildReport.summary.result != BuildResult.Succeeded)
                {
                    return;
                }


                var buildDirectory = new DirectoryInfo(resultDirectory);
                foreach (var directoryInfo in buildDirectory.GetDirectories())
                {
                    if (directoryInfo.Name.Contains("BackUpThisFolder_ButDontShipItWithYourGame"))
                    {
                        directoryInfo.Delete(true);
                        break;
                    }
                }

                CopySteamWorksConfig(buildDirectory);

                var pathName = Path.GetDirectoryName(resultDirectory);
                Debug.Log("Build Output Path:" + resultDirectory);
                // ZipHelper.CompressDirectory(resultDirectory, pathName + ".zip");
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }

        [MenuItem("GameFrameX/Build/Windows X32", false, 100)]
        public static void BuildPlayerToWindows32BuildTarget()
        {
            // PlayerSettings.SplashScreen.show = false;
            Debug.Log(EditorUserBuildSettings.activeBuildTarget);
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneWindows)
            {
                Debug.LogError("当前构建目标不是 Windows");
                return;
            }

            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
                PlayerSettings.defaultScreenHeight = 720;
                PlayerSettings.defaultScreenWidth = 1280;
                EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows;
                UpdateBuildTime();
                AssetDatabase.SaveAssets();
                _buildPath = BuildOutputPath();
                var resultDirectory = _buildPath + Path.DirectorySeparatorChar;
                RunPreHookBuild();
                var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, resultDirectory + PlayerSettings.productName + ".exe", EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
                if (buildReport.summary.result != BuildResult.Succeeded)
                {
                    return;
                }

                var buildDirectory = new DirectoryInfo(resultDirectory);
                foreach (var directoryInfo in buildDirectory.GetDirectories())
                {
                    if (directoryInfo.Name.Contains("BackUpThisFolder_ButDontShipItWithYourGame"))
                    {
                        directoryInfo.Delete(true);
                        break;
                    }
                }


                CopySteamWorksConfig(buildDirectory);


                var pathName = Path.GetDirectoryName(resultDirectory);
                Debug.Log("Build Output Path:" + resultDirectory);
                // ZipHelper.CompressDirectory(resultDirectory, pathName + ".zip");
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }

        [MenuItem("GameFrameX/Build/Mac Os", false, 200)]
        public static void BuildPlayerToMacBuildTarget()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneOSX)
            {
                Debug.LogError("当前构建目标不是 Mac Os");
                return;
            }

            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                // PlayerSettings.SplashScreen.show = false;
                PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
                PlayerSettings.defaultScreenHeight = 720;
                PlayerSettings.defaultScreenWidth = 1280;
                UpdateBuildTime();

                AssetDatabase.SaveAssets();
                _buildPath = BuildOutputPath();
                var resultDirectory = _buildPath + Path.DirectorySeparatorChar;
                RunPreHookBuild();
                var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, resultDirectory, EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
                if (buildReport.summary.result != BuildResult.Succeeded)
                {
                    return;
                }

                var buildDirectory = new DirectoryInfo(resultDirectory);
                foreach (var directoryInfo in buildDirectory.GetDirectories())
                {
                    if (directoryInfo.Name.Contains("BackUpThisFolder_ButDontShipItWithYourGame"))
                    {
                        directoryInfo.Delete(true);
                        break;
                    }
                }

                CopySteamWorksConfig(buildDirectory);

                var pathName = Path.GetDirectoryName(resultDirectory);
                Debug.Log("Build Output Path:" + resultDirectory);
                // ZipHelper.CompressDirectory(resultDirectory, pathName + ".zip");
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }


        /// <summary>
        /// 复制 SteamWorks 配置
        /// </summary>
        /// <param name="buildDirectory">目标目录</param>
        private static void CopySteamWorksConfig(DirectoryInfo buildDirectory)
        {
#if STEAMWORKS_NET
            DirectoryInfo projectDirectoryInfo = new DirectoryInfo(Application.dataPath);
            var steamAppidPath = PathHelper.Combine(projectDirectoryInfo.Parent.FullName, "steam_appid.txt");
            if (File.Exists(steamAppidPath))
            {
                File.Copy(steamAppidPath, PathHelper.Combine(buildDirectory.FullName, "steam_appid.txt"), true);
            }
#endif
        }

        /// <summary>
        /// 发布 WebGL
        /// 该接口主要用于外部调用
        /// </summary>
        /// <returns>构建结果地址</returns>
        public static string BuildPlayerWebGL()
        {
            BuildPlayerToWebGL();
            return BuildOutputPath();
        }

        /// <summary>
        /// 发布 WebGL
        /// </summary>
        [MenuItem("GameFrameX/Build/WebGL", false, 300)]
        private static void BuildPlayerToWebGL()
        {
            // PlayerSettings.SplashScreen.show = false;

            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
            {
                Debug.LogError("当前构建目标不是 WebGL");
                return;
            }

            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                UpdateBuildTime();
                AssetDatabase.SaveAssets();
                _buildPath = BuildOutputPath();
                RunPreHookBuild();
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.WebGL, BuildOptions.None);
                Debug.Log("Build Output Path:" + BuildOutputPath());
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }
#if ENABLE_WX_MINI_GAME
        /// <summary>
        /// 发布 微信小游戏 WebGL
        /// </summary>
        [MenuItem("GameFrameX/Build/WeChat MiniGame WebGL", false, 300)]
        private static void BuildPlayerToWeChatMiniGameWebGL()
        {
            // PlayerSettings.SplashScreen.show = false;

            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
            {
                Debug.LogError("当前构建目标不是 WebGL");
                return;
            }

            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                UpdateBuildTime();
                WeChatWASM.WXConvertCore.config.ProjectConf.DST = BuildOutputPath();
                AssetDatabase.SaveAssets();
                WeChatWASM.WXConvertCore.DoExport();
                Debug.Log("Build Output Path:" + BuildOutputPath());
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
            }
        }
#endif
        /// <summary>
        /// 发布 APK
        /// 该接口主要用于外部调用
        /// </summary>
        /// <returns>构建结果地址</returns>
        public static string BuildPlayerAndroid()
        {
            BuildPlayerToAndroid();
            string apkPath = $"{_buildPath}.apk";
            return apkPath;
        }

        /// <summary>
        /// 发布 Xcode
        /// 该接口主要用于外部调用
        /// </summary>
        /// <returns>构建结果地址</returns>
        public static string BuildPlayerXcode()
        {
            ExportToXcodeToRelease();
            return _buildPath;
        }

        /// <summary>
        /// 发布 APK
        /// </summary>
        [MenuItem("GameFrameX/Build/Apk", false, 400)]
        private static void BuildPlayerToAndroid()
        {
            // PlayerSettings.SplashScreen.show = false;
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("当前构建目标不是 Android");
                return;
            }

            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                UpdateBuildTime();
                EditorUserBuildSettings.buildAppBundle = false;
                EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
                _buildPath = BuildOutputPath();
                string apkPath = $"{_buildPath}.apk";
                AssetDatabase.SaveAssets();
                RunPreHookBuild();
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, apkPath, BuildTarget.Android, BuildOptions.None);
                Debug.Log("Build Output Path:" + apkPath);
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }

        /// <summary>
        /// 发布 AAB
        /// </summary>
        [MenuItem("GameFrameX/Build/AAB", false, 400)]
        private static void BuildAppBundleForAndroid()
        {
            // PlayerSettings.SplashScreen.show = false;

            string aabSavePath = Application.dataPath.Replace("Assets", "AAB");
            if (!Directory.Exists(aabSavePath))
            {
                Directory.CreateDirectory(aabSavePath);
            }

            string aabFileName = Application.version + "-" + PlayerSettings.Android.bundleVersionCode + ".aab";
            var aabFilePath = aabSavePath + "/" + aabFileName;
            if (string.IsNullOrEmpty(aabFilePath))
            {
                Debug.LogError("输出路径异常,取消打包AAB");
                return;
            }


            if (string.IsNullOrEmpty(PlayerSettings.Android.keystoreName)
                || string.IsNullOrEmpty(PlayerSettings.Android.keyaliasName)
                || string.IsNullOrEmpty(PlayerSettings.Android.keyaliasPass)
                || string.IsNullOrEmpty(PlayerSettings.Android.keystorePass))
            {
                Debug.LogError("没有设置签名密钥,取消打包AAB");
                return;
            }

            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

                EditorUserBuildSettings.buildAppBundle = true;
                // 开启符号表的输出
                EditorUserBuildSettings.androidCreateSymbolsZip = true;
                AssetDatabase.SaveAssets();
                _buildPath = aabFilePath;
                RunPreHookBuild();
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, aabFilePath, BuildTarget.Android, BuildOptions.None);

                Debug.Log("AAB存储路径=>" + aabFilePath);
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }

        /// <summary>
        /// 复制build.Gradle 到目标目录
        /// </summary>
        /// <param name="targetPath"></param>
        private static void CopyFileByBuildGradle(string targetPath)
        {
#if UNITY_ANDROID
            var resourcesPath = Application.dataPath + "buildConfig/build.gradle";
            if (File.Exists(resourcesPath))
            {
                var path = targetPath + "/build.gradle";
                File.Copy(resourcesPath, path, true);
            }
#endif
        }

        static string GeneratorGradleByWrapper(string path, string extensionSuffix = ".bat")
        {
            File.WriteAllText(path + "/build_init" + extensionSuffix, "gradle wrapper");

            return path + "/build_init" + extensionSuffix;
        }

        static string GeneratorGradlewByAssembleDebug(string path)
        {
            File.WriteAllText(path + "/build_debug.bat", "gradlew assembleDebug");
            return path + "/build_debug.bat";
        }

        static string GeneratorOutPutPathByDebug(string path, string extensionSuffix = ".bat")
        {
            string outputPath = GetOutPutPath(path, "debug");

            File.WriteAllText(path + "/build_output_debug" + extensionSuffix, $"start {outputPath}");

            return path + "/build_output_debug" + extensionSuffix;
        }

        static void GeneratorGradle(string outputPath)
        {
#if UNITY_EDITOR_WIN
            string extensionSuffix = ".bat";
#else
            string extensionSuffix = ".sh";
#endif
            GeneratorGradleByWrapper(outputPath, extensionSuffix);
            GeneratorGradlewByAssembleRelease(outputPath, extensionSuffix);
            GeneratorOutPutPathByRelease(outputPath, extensionSuffix);
        }

        static string GeneratorOutPutPathByRelease(string path, string extensionSuffix = ".bat")
        {
            string outputPath = GetOutPutPath(path, "release");

            File.WriteAllText(path + "/build_output_release" + extensionSuffix, $"start {outputPath}");

            return path + "/build_output_release.bat";
        }

        static string GeneratorGradlewByAssembleRelease(string path, string extensionSuffix = ".bat")
        {
            string outputPath = GetOutPutPath(path, "release");

            File.WriteAllText(path + "/build_release" + extensionSuffix, $"gradlew assembleRelease");

            return path + "/build_release" + extensionSuffix;
        }

        /// <summary>
        /// 获取程序的输出目录
        /// </summary>
        /// <param name="path"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        static string GetOutPutPath(string path, string name)
        {
            return $"{path}/launcher/build/outputs/apk/{name}/";
        }

        /// <summary>
        /// 发布 AS Debug 版本
        /// </summary>
        [MenuItem("GameFrameX/Build/AndroidStudio Project Debug", false, 500)]
        private static void ExportToAndroidStudioToDevelop()
        {
            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                // PlayerSettings.SplashScreen.show = false;
                UpdateBuildTime();
                _buildPath = BuildOutputPath();

                EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
                EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
                EditorUserBuildSettings.development = true;
                EditorUserBuildSettings.allowDebugging = true;
                EditorUserBuildSettings.connectProfiler = true;
                EditorUserBuildSettings.buildWithDeepProfilingSupport = true;
                AssetDatabase.SaveAssets();
                RunPreHookBuild();
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.Android, BuildOptions.AllowDebugging | BuildOptions.Development | BuildOptions.ConnectWithProfiler);
                Debug.Log(_buildPath);

                GeneratorGradle(_buildPath);
                CopyFileByBuildGradle(_buildPath);
                Process.Start(_buildPath);
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }

        /// <summary>
        /// 发布 AS Release 版本
        /// </summary>
        [MenuItem("GameFrameX/Build/AndroidStudio Project Release", false, 500)]
        private static void ExportToAndroidStudioToRelease()
        {
            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                // PlayerSettings.SplashScreen.show = false;
                UpdateBuildTime();
                _buildPath = BuildOutputPath();

                EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
                EditorUserBuildSettings.development = false;
                EditorUserBuildSettings.allowDebugging = false;
                EditorUserBuildSettings.connectProfiler = false;
                EditorUserBuildSettings.buildWithDeepProfilingSupport = false;
                EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
                AssetDatabase.SaveAssets();
                RunPreHookBuild();
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.Android, BuildOptions.None);
                Debug.Log(_buildPath);
                GeneratorGradle(_buildPath);
                CopyFileByBuildGradle(_buildPath);
                Debug.Log("Build Output Path:" + _buildPath);
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }


        /// <summary>
        /// 发布 Xcode Debug 版本
        /// </summary>
        [MenuItem("GameFrameX/Build/Xcode Project Debug", false, 250)]
        private static void ExportToXcodeToDevelop()
        {
            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                // PlayerSettings.SplashScreen.show = false;
                UpdateBuildTime();
                _buildPath = BuildOutputPath();

                EditorUserBuildSettings.development = true;
                EditorUserBuildSettings.allowDebugging = true;
                EditorUserBuildSettings.connectProfiler = true;
                EditorUserBuildSettings.buildWithDeepProfilingSupport = true;
                AssetDatabase.SaveAssets();
                RunPreHookBuild();
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.iOS, BuildOptions.None);
                Process.Start(_buildPath);
                Debug.Log("Build Output Path:" + _buildPath);
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }


        /// <summary>
        /// 发布 Xcode Release 版本
        /// </summary>
        [MenuItem("GameFrameX/Build/Xcode Project Release", false, 250)]
        private static void ExportToXcodeToRelease()
        {
            try
            {
                HotFixEditorCompilerHelper.AddEditor();
                // PlayerSettings.SplashScreen.show = false;
                UpdateBuildTime();
                _buildPath = BuildOutputPath();

                EditorUserBuildSettings.development = false;
                AssetDatabase.SaveAssets();
                RunPreHookBuild();
                BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.iOS, BuildOptions.None);
                Process.Start(_buildPath);
                Debug.Log("Build Output Path:" + _buildPath);
            }
            finally
            {
                HotFixEditorCompilerHelper.RemoveEditor();
                RunPostHookBuild();
            }
        }

        /// <summary>
        /// 设置 发布版本号更新
        /// </summary>
        /// <param name="target"></param>
        /// <param name="path"></param>
        [PostProcessBuild(999)]
        public static void OnPostProcessBuild(BuildTarget target, string path)
        {
            if (target == BuildTarget.Android)
            {
                // Update Build Version Code
                PlayerSettings.Android.bundleVersionCode = Convert.ToInt32(PlayerSettings.Android.bundleVersionCode) + 1;
            }

            if (target == BuildTarget.iOS)
            {
                // Update Build Version Code
                PlayerSettings.iOS.buildNumber = (Convert.ToInt32(PlayerSettings.iOS.buildNumber) + 1).ToString();
            }
        }

        /// <summary>
        /// 获取工程路径
        /// </summary>
        /// <returns></returns>
        private static string GetProjectPath()
        {
            return Application.dataPath.Replace("Assets", string.Empty);
        }

        /// <summary>
        /// 构建导出根目录
        /// </summary>
        private static string GetBuildRootPath
        {
            get { return $"{GetProjectPath()}Builds"; }
        }

        private static string _buildTime;

        static BuildProductHelper()
        {
            _buildPath = string.Empty;
            UpdateBuildTime();
        }

        /// <summary>
        /// 更新时间命名
        /// </summary>
        private static void UpdateBuildTime()
        {
            _buildTime = DateTime.Now.ToString("yyyyMMddHHmmss");
        }

        /// <summary>
        /// 获取发布导出路径
        /// </summary>
        /// <returns></returns>
        private static string BuildOutputPath()
        {
            var pathName = $"{Application.identifier}_{_buildTime}_v_{PlayerSettings.bundleVersion}";
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                pathName = $"{_buildTime}_code_{PlayerSettings.Android.bundleVersionCode}";
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS || EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSX)
            {
                pathName = $"{_buildTime}_code_{PlayerSettings.iOS.buildNumber}";
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows || EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64)
            {
                pathName = $"{_buildTime}";
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL)
            {
                pathName = $"{_buildTime}";
            }

            var path = Path.Combine(GetBuildRootPath, EditorUserBuildSettings.activeBuildTarget.ToString(), Application.identifier, Application.version, pathName);

            switch (EditorUserBuildSettings.activeBuildTarget)
            {
                case BuildTarget.StandaloneOSX:
                case BuildTarget.StandaloneOSXIntel:
                case BuildTarget.StandaloneWindows:
                case BuildTarget.StandaloneLinux:
                case BuildTarget.StandaloneWindows64:
                case BuildTarget.WebGL:
                case BuildTarget.StandaloneLinux64:
                    break;
                default:
                {
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                }
                    break;
            }


            return path.Replace('\\', '/');
        }
    }
}