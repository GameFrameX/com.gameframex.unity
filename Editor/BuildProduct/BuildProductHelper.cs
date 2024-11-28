using System;
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

        /// <summary>
        /// 发布 当前激活的平台
        /// </summary>
        [MenuItem("GameFrameX/Build/Active Build Target", false, 20)]
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
        }

        [MenuItem("GameFrameX/Build/Windows X64", false, 10)]
        public static void BuildPlayerToWindows64BuildTarget()
        {
            PlayerSettings.SplashScreen.show = false;
            Debug.Log(EditorUserBuildSettings.activeBuildTarget);
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneWindows64)
            {
                Debug.LogError("当前构建目标不是 Windows");
                return;
            }

            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            PlayerSettings.defaultScreenHeight = 720;
            PlayerSettings.defaultScreenWidth = 1280;
            EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows64;
            UpdateBuildTime();
            AssetDatabase.SaveAssets();
            var resultDirectory = BuildOutputPath() + Path.DirectorySeparatorChar;
            var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, resultDirectory + PlayerSettings.productName + ".exe", EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
            if (buildReport.summary.result != BuildResult.Succeeded)
            {
                return;
            }

            var pathName = Path.GetDirectoryName(resultDirectory);
            Debug.LogError("Build Output Path:" + resultDirectory);
            ZipHelper.CompressDirectory(resultDirectory, pathName + ".zip");
        }

        [MenuItem("GameFrameX/Build/Mac Os", false, 20)]
        public static void BuildPlayerToMacBuildTarget()
        {
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneOSX)
            {
                Debug.LogError("当前构建目标不是 Mac Os");
                return;
            }

            PlayerSettings.SplashScreen.show = false;
            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            PlayerSettings.defaultScreenHeight = 720;
            PlayerSettings.defaultScreenWidth = 1280;
            UpdateBuildTime();

            AssetDatabase.SaveAssets();
            var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, BuildOutputPath(), EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
            if (buildReport.summary.result != BuildResult.Succeeded)
            {
                return;
            }

            var pathName = Path.GetDirectoryName(BuildOutputPath());
            Debug.LogError("Build Output Path:" + BuildOutputPath());
            ZipHelper.CompressDirectory(BuildOutputPath(), pathName + ".zip");
        }

        [MenuItem("GameFrameX/Build/Windows X32", false, 10)]
        public static void BuildPlayerToWindows32BuildTarget()
        {
            PlayerSettings.SplashScreen.show = false;
            Debug.Log(EditorUserBuildSettings.activeBuildTarget);
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.StandaloneWindows)
            {
                Debug.LogError("当前构建目标不是 Windows");
                return;
            }

            PlayerSettings.fullScreenMode = FullScreenMode.Windowed;
            PlayerSettings.defaultScreenHeight = 720;
            PlayerSettings.defaultScreenWidth = 1280;
            EditorUserBuildSettings.selectedStandaloneTarget = BuildTarget.StandaloneWindows;
            UpdateBuildTime();
            AssetDatabase.SaveAssets();
            var resultDirectory = BuildOutputPath() + Path.DirectorySeparatorChar;
            var buildReport = BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, resultDirectory + PlayerSettings.productName + ".exe", EditorUserBuildSettings.activeBuildTarget, BuildOptions.None);
            if (buildReport.summary.result != BuildResult.Succeeded)
            {
                return;
            }

            var pathName = Path.GetDirectoryName(resultDirectory);
            Debug.LogError("Build Output Path:" + resultDirectory);
            ZipHelper.CompressDirectory(resultDirectory, pathName + ".zip");
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
        [MenuItem("GameFrameX/Build/WebGL", false, 20)]
        private static void BuildPlayerToWebGL()
        {
            PlayerSettings.SplashScreen.show = false;

            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.WebGL)
            {
                Debug.LogError("当前构建目标不是 WebGL");
                return;
            }

            UpdateBuildTime();
            AssetDatabase.SaveAssets();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, BuildOutputPath(), BuildTarget.WebGL, BuildOptions.None);
            Debug.LogError("Build Output Path:" + BuildOutputPath());
        }


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
        [MenuItem("GameFrameX/Build/Apk", false, 20)]
        private static void BuildPlayerToAndroid()
        {
            PlayerSettings.SplashScreen.show = false;
            if (EditorUserBuildSettings.activeBuildTarget != BuildTarget.Android)
            {
                Debug.LogError("当前构建目标不是 Android");
                return;
            }

            UpdateBuildTime();
            EditorUserBuildSettings.buildAppBundle = false;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = false;
            _buildPath = BuildOutputPath();
            string apkPath = $"{_buildPath}.apk";
            AssetDatabase.SaveAssets();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, apkPath, BuildTarget.Android, BuildOptions.None);
            Debug.LogError("Build Output Path:" + apkPath);
        }

        /// <summary>
        /// 发布 AAB
        /// </summary>
        [MenuItem("GameFrameX/Build/AAB", false, 20)]
        private static void BuildAppBundleForAndroid()
        {
            PlayerSettings.SplashScreen.show = false;

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

            EditorUserBuildSettings.exportAsGoogleAndroidProject = true;

            EditorUserBuildSettings.buildAppBundle = true;
            // 开启符号表的输出
            EditorUserBuildSettings.androidCreateSymbolsZip = true;
            AssetDatabase.SaveAssets();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, aabFilePath, BuildTarget.Android, BuildOptions.None);

            Debug.Log("AAB存储路径=>" + aabFilePath);
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
        [MenuItem("GameFrameX/Build/AS Project Debug", false, 20)]
        private static void ExportToAndroidStudioToDevelop()
        {
            PlayerSettings.SplashScreen.show = false;
            UpdateBuildTime();
            _buildPath = BuildOutputPath();

            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
            EditorUserBuildSettings.development = true;
            AssetDatabase.SaveAssets();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.Android, BuildOptions.None);
            Debug.Log(_buildPath);

            GeneratorGradle(_buildPath);
            CopyFileByBuildGradle(_buildPath);
            Process.Start(_buildPath);
        }

        /// <summary>
        /// 发布 AS Release 版本
        /// </summary>
        [MenuItem("GameFrameX/Build/AS Project Release", false, 20)]
        private static void ExportToAndroidStudioToRelease()
        {
            PlayerSettings.SplashScreen.show = false;
            UpdateBuildTime();
            _buildPath = BuildOutputPath();

            EditorUserBuildSettings.androidBuildSystem = AndroidBuildSystem.Gradle;
            EditorUserBuildSettings.development = false;
            EditorUserBuildSettings.exportAsGoogleAndroidProject = true;
            AssetDatabase.SaveAssets();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.Android, BuildOptions.None);
            Debug.Log(_buildPath);
            GeneratorGradle(_buildPath);
            CopyFileByBuildGradle(_buildPath);
            Debug.LogError("Build Output Path:" + _buildPath);
        }


        /// <summary>
        /// 发布 Xcode Debug 版本
        /// </summary>
        [MenuItem("GameFrameX/Build/Xcode Project Debug", false, 30)]
        private static void ExportToXcodeToDevelop()
        {
            PlayerSettings.SplashScreen.show = false;
            UpdateBuildTime();
            _buildPath = BuildOutputPath();

            EditorUserBuildSettings.development = true;
            AssetDatabase.SaveAssets();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.iOS, BuildOptions.None);
            Process.Start(_buildPath);
            Debug.LogError("Build Output Path:" + _buildPath);
        }


        /// <summary>
        /// 发布 Xcode Release 版本
        /// </summary>
        [MenuItem("GameFrameX/Build/Xcode Project Release", false, 30)]
        private static void ExportToXcodeToRelease()
        {
            PlayerSettings.SplashScreen.show = false;
            UpdateBuildTime();
            _buildPath = BuildOutputPath();

            EditorUserBuildSettings.development = false;
            AssetDatabase.SaveAssets();
            BuildPipeline.BuildPlayer(EditorBuildSettings.scenes, _buildPath, BuildTarget.iOS, BuildOptions.None);
            Process.Start(_buildPath);
            Debug.LogError("Build Output Path:" + _buildPath);
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
            get { return $"{GetProjectPath()}/Builds"; }
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
            _buildTime = DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss");
        }

        /// <summary>
        /// 获取发布导出路径
        /// </summary>
        /// <returns></returns>
        private static string BuildOutputPath()
        {
            string pathName = $"{Application.identifier}_{_buildTime}_v_{PlayerSettings.bundleVersion}";
            if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.Android)
            {
                pathName = $"{_buildTime}_v_{PlayerSettings.bundleVersion}_code_{PlayerSettings.Android.bundleVersionCode}";
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.iOS || EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneOSX)
            {
                pathName = $"{_buildTime}_v_{PlayerSettings.bundleVersion}_code_{PlayerSettings.iOS.buildNumber}";
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows || EditorUserBuildSettings.activeBuildTarget == BuildTarget.StandaloneWindows64)
            {
                pathName = $"{_buildTime}";
            }
            else if (EditorUserBuildSettings.activeBuildTarget == BuildTarget.WebGL)
            {
                pathName = $"{_buildTime}_v_{PlayerSettings.bundleVersion}";
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


            return path;
        }
    }
}