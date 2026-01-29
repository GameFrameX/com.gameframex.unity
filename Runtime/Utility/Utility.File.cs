namespace GameFrameX.Runtime
{
    public static partial class Utility
    {
        /// <summary>
        /// 文件相关的实用函数
        /// </summary>
        [UnityEngine.Scripting.Preserve]
        public static class File
        {
            private static readonly string[] UnitList = new[] { "B", "KB", "MB", "GB", "TB", "PB" };

            /// <summary>
            /// 获取字节大小
            /// </summary>
            /// <param name="size">字节大小</param>
            /// <returns>格式化后的字节大小字符串</returns>
            [UnityEngine.Scripting.Preserve]
            public static string GetBytesSize(long size)
            {
                double dSize = size;
                int unitIndex = 0;

                while (dSize >= 1024 && unitIndex < UnitList.Length - 1)
                {
                    dSize /= 1024;
                    unitIndex++;
                }

                if (unitIndex == 0)
                {
                    return dSize + UnitList[unitIndex];
                }

                // 格式化为保留两位小数，但去除尾部不必要的零
                string formattedSize = dSize.ToString("F2").TrimEnd('0').TrimEnd('.');
                return formattedSize + UnitList[unitIndex];
            }
        }
    }
}