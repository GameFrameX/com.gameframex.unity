// ==========================================================================================
//  GameFrameX 组织及其衍生项目的版权、商标、专利及其他相关权利
//  GameFrameX organization and its derivative projects' copyrights, trademarks, patents, and related rights
//  均受中华人民共和国及相关国际法律法规保护。
//  are protected by the laws of the People's Republic of China and relevant international regulations.
// 
//  使用本项目须严格遵守相应法律法规及开源许可证之规定。
//  Usage of this project must strictly comply with applicable laws, regulations, and open-source licenses.
// 
//  本项目采用 MIT 许可证与 Apache License 2.0 双许可证分发，
//  This project is dual-licensed under the MIT License and Apache License 2.0,
//  完整许可证文本请参见源代码根目录下的 LICENSE 文件。
//  please refer to the LICENSE file in the root directory of the source code for the full license text.
// 
//  禁止利用本项目实施任何危害国家安全、破坏社会秩序、
//  It is prohibited to use this project to engage in any activities that endanger national security, disrupt social order,
//  侵犯他人合法权益等法律法规所禁止的行为！
//  or infringe upon the legitimate rights and interests of others, as prohibited by laws and regulations!
//  因基于本项目二次开发所产生的一切法律纠纷与责任，
//  Any legal disputes and liabilities arising from secondary development based on this project
//  本项目组织与贡献者概不承担。
//  shall be borne solely by the developer; the project organization and contributors assume no responsibility.
// 
//  GitHub 仓库：https://github.com/GameFrameX
//  GitHub Repository: https://github.com/GameFrameX
//  Gitee  仓库：https://gitee.com/GameFrameX
//  Gitee Repository:  https://gitee.com/GameFrameX
//  官方文档：https://gameframex.doc.alianblank.com/
//  Official Documentation: https://gameframex.doc.alianblank.com/
// ==========================================================================================

using System;

namespace GameFrameX.Runtime
{
    public partial class TimerHelper
    {
        /// <summary>
        /// 获取当前UTC时间，格式为HHmmss的字符串
        /// </summary>
        /// <returns>返回一个6位字符串，表示当前UTC时间。例如：143045表示14:30:45</returns>
        /// <remarks>
        /// 此方法将当前UTC时间转换为6位时间字符串:
        /// - 前2位表示小时(24小时制)
        /// - 中间2位表示分钟
        /// - 最后2位表示秒
        /// 使用DateTime.UtcNow获取UTC时间
        /// </remarks>
        public static string CurrentTimeWithUtcFullString()
        {
            return DateTime.UtcNow.ToString("HHmmss");
        }

        /// <summary>
        /// 获取当前本地时间，格式为HHmmss的字符串
        /// </summary>
        /// <returns>返回一个6位字符串，表示当前本地时间。例如：143045表示14:30:45</returns>
        /// <remarks>
        /// 此方法将当前本地时间转换为6位时间字符串:
        /// - 前2位表示小时(24小时制)
        /// - 中间2位表示分钟
        /// - 最后2位表示秒
        /// 使用DateTime.Now获取本地时间
        /// </remarks>
        public static string CurrentTimeWithLocalFullString()
        {
            return DateTime.Now.ToString("HHmmss");
        }

        /// <summary>
        /// 获取当前UTC时间，格式为HHmmss的整数
        /// </summary>
        /// <returns>返回一个6位整数，表示当前UTC时间。例如：143045表示14:30:45</returns>
        /// <remarks>
        /// 此方法将当前UTC时间转换为6位整数:
        /// - 前2位表示小时(24小时制)
        /// - 中间2位表示分钟
        /// - 最后2位表示秒
        /// 内部调用CurrentTimeWithUtcFullString()获取字符串后转换为整数
        /// </remarks>
        public static int CurrentTimeWithUtcTime()
        {
            return Convert.ToInt32(CurrentTimeWithUtcFullString());
        }

        /// <summary>
        /// 获取当前本地时间，格式为HHmmss的整数
        /// </summary>
        /// <returns>返回一个6位整数，表示当前本地时间。例如：143045表示14:30:45</returns>
        /// <remarks>
        /// 此方法将当前本地时间转换为6位整数:
        /// - 前2位表示小时(24小时制)
        /// - 中间2位表示分钟
        /// - 最后2位表示秒
        /// 内部调用CurrentTimeWithLocalFullString()获取字符串后转换为整数
        /// </remarks>
        public static int CurrentTimeWithLocalTime()
        {
            return Convert.ToInt32(CurrentTimeWithLocalFullString());
        }

        /// <summary>
        /// 获取当前本地时区时间的自定义格式字符串
        /// </summary>
        /// <param name="format">时间格式字符串，默认为"yyyy-MM-dd HH:mm:ss.fff K"</param>
        /// <returns>返回指定格式的本地时间字符串。例如默认格式返回："2023-12-25 14:30:45.123 +08:00"</returns>
        /// <remarks>
        /// 此方法允许自定义时间格式字符串:
        /// - 默认格式包含年月日时分秒毫秒和时区信息
        /// - 可以通过format参数指定其他格式
        /// - 使用DateTime.Now获取本地时间
        /// 支持标准的.NET日期时间格式说明符
        /// </remarks>
        public static string CurrentDateTimeWithFormat(string format = "yyyy-MM-dd HH:mm:ss.fff K")
        {
            return DateTime.Now.ToString(format);
        }

        /// <summary>
        /// 获取当前UTC时区时间的自定义格式字符串
        /// </summary>
        /// <param name="format">时间格式字符串，默认为"yyyy-MM-dd HH:mm:ss.fff K"</param>
        /// <returns>返回指定格式的UTC时间字符串。例如默认格式返回："2023-12-25 06:30:45.123 +00:00"</returns>
        /// <remarks>
        /// 此方法允许自定义UTC时间格式字符串:
        /// - 默认格式包含年月日时分秒毫秒和时区信息
        /// - 可以通过format参数指定其他格式
        /// - 使用DateTime.UtcNow获取UTC时间
        /// 支持标准的.NET日期时间格式说明符
        /// </remarks>
        public static string CurrentDateTimeWithUtcFormat(string format = "yyyy-MM-dd HH:mm:ss.fff K")
        {
            return DateTime.UtcNow.ToString(format);
        }

        /// <summary>
        /// 获取当前UTC时间
        /// </summary>
        /// <returns>当前UTC时间</returns>
        /// <remarks>
        /// 此方法返回当前的UTC时间(协调世界时)
        /// 与本地时间相比会有时区偏移
        /// 主要用于需要统一时间标准的场景
        /// </remarks>
        public static DateTime GetUtcNow()
        {
            return DateTime.UtcNow;
        }

        /// <summary>
        /// 获取当前时间
        /// </summary>
        /// <returns>当前时间</returns>
        /// <remarks>
        /// 此方法返回当前的本地时间
        /// 会根据系统设置的时区自动调整
        /// 主要用于需要显示本地时间的场景
        /// </remarks>
        public static DateTime GetNow()
        {
            return DateTime.Now;
        }
    }
}