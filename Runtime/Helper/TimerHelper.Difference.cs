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
        /// 计算两个DateTime之间的时间差
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>时间差TimeSpan对象</returns>
        /// <remarks>
        /// 返回endTime - startTime的时间差
        /// 如果endTime早于startTime，返回负的TimeSpan
        /// 此方法可用于计算任意两个DateTime之间的精确时间间隔
        /// 返回的TimeSpan对象包含天数、小时、分钟、秒和毫秒等详细信息
        /// </remarks>
        public static TimeSpan GetTimeDifference(DateTime startTime, DateTime endTime)
        {
            return endTime - startTime;
        }

        /// <summary>
        /// 计算两个时间戳之间的时间差（秒级）
        /// </summary>
        /// <param name="startTimestamp">开始时间戳（秒）</param>
        /// <param name="endTimestamp">结束时间戳（秒）</param>
        /// <param name="utc">是否使用UTC时间，默认为true</param>
        /// <returns>时间差TimeSpan对象</returns>
        /// <remarks>
        /// 此方法会先将时间戳转换为DateTime后再计算差值
        /// 时间戳以1970年1月1日为起点
        /// 当utc=true时使用UTC时间，否则使用本地时间
        /// 返回的TimeSpan对象包含完整的时间差信息
        /// </remarks>
        public static TimeSpan GetTimeDifference(long startTimestamp, long endTimestamp, bool utc = true)
        {
            var startTime = TimestampToDateTime(startTimestamp, utc);
            var endTime = TimestampToDateTime(endTimestamp, utc);
            return endTime - startTime;
        }

        /// <summary>
        /// 计算两个毫秒时间戳之间的时间差
        /// </summary>
        /// <param name="startTimestampMs">开始时间戳（毫秒）</param>
        /// <param name="endTimestampMs">结束时间戳（毫秒）</param>
        /// <param name="utc">是否使用UTC时间，默认为true</param>
        /// <returns>时间差TimeSpan对象</returns>
        /// <remarks>
        /// 此方法提供毫秒级的精确时间差计算
        /// 时间戳以1970年1月1日为起点
        /// 当utc=true时使用UTC时间，否则使用本地时间
        /// 适用于需要高精度时间差计算的场景
        /// </remarks>
        public static TimeSpan GetTimeDifferenceMs(long startTimestampMs, long endTimestampMs, bool utc = true)
        {
            var startTime = MillisecondsTimeStampToDateTime(startTimestampMs, utc);
            var endTime = MillisecondsTimeStampToDateTime(endTimestampMs, utc);
            return endTime - startTime;
        }

        /// <summary>
        /// 计算指定时间到当前时间的时间差
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <param name="useUtc">是否使用UTC时间作为当前时间，默认为false（使用本地时间）</param>
        /// <returns>时间差TimeSpan对象</returns>
        /// <remarks>
        /// 此方法计算指定时间到当前时间的差值
        /// 当useUtc=true时使用UTC时间，否则使用本地时间
        /// 如果指定时间在当前时间之后，将返回负值
        /// 常用于计算时间间隔和判断过期时间
        /// </remarks>
        public static TimeSpan GetTimeDifferenceFromNow(DateTime time, bool useUtc = false)
        {
            var now = useUtc ? DateTime.UtcNow : DateTime.Now;
            return now - time;
        }

        /// <summary>
        /// 计算指定时间戳到当前时间的时间差
        /// </summary>
        /// <param name="timestamp">时间戳（秒）</param>
        /// <param name="useUtc">是否使用UTC时间，默认为true</param>
        /// <returns>时间差TimeSpan对象</returns>
        /// <remarks>
        /// 此方法先将时间戳转换为DateTime，再计算与当前时间的差值
        /// 时间戳以1970年1月1日为起点
        /// 当useUtc=true时使用UTC时间，否则使用本地时间
        /// 适用于处理Unix时间戳格式的时间差计算
        /// </remarks>
        public static TimeSpan GetTimeDifferenceFromNow(long timestamp, bool useUtc = true)
        {
            var time = TimestampToDateTime(timestamp, useUtc);
            return GetTimeDifferenceFromNow(time, useUtc);
        }

        /// <summary>
        /// 计算指定毫秒时间戳到当前时间的时间差
        /// </summary>
        /// <param name="timestampMs">时间戳（毫秒）</param>
        /// <param name="useUtc">是否使用UTC时间，默认为true</param>
        /// <returns>时间差TimeSpan对象</returns>
        /// <remarks>
        /// 此方法提供毫秒级精度的时间差计算
        /// 时间戳以1970年1月1日为起点
        /// 当useUtc=true时使用UTC时间，否则使用本地时间
        /// 适用于需要高精度时间差计算的场景
        /// </remarks>
        public static TimeSpan GetTimeDifferenceFromNowMs(long timestampMs, bool useUtc = true)
        {
            var time = MillisecondsTimeStampToDateTime(timestampMs, useUtc);
            return GetTimeDifferenceFromNow(time, useUtc);
        }

        /// <summary>
        /// 获取两个时间之间的秒数差
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>秒数差（可能为负数）</returns>
        /// <remarks>
        /// 此方法返回两个时间之间的总秒数
        /// 结果会被转换为长整型，可能损失小数部分精度
        /// 如果endTime早于startTime，返回负数
        /// 适用于需要秒级时间差的场景
        /// </remarks>
        public static long GetSecondsDifference(DateTime startTime, DateTime endTime)
        {
            return (long)(endTime - startTime).TotalSeconds;
        }

        /// <summary>
        /// 获取两个时间之间的毫秒数差
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>毫秒数差（可能为负数）</returns>
        /// <remarks>
        /// 此方法返回两个时间之间的总毫秒数
        /// 结果会被转换为长整型，可能损失小数部分精度
        /// 如果endTime早于startTime，返回负数
        /// 适用于需要毫秒级精确时间差的场景
        /// </remarks>
        public static long GetMillisecondsDifference(DateTime startTime, DateTime endTime)
        {
            return (long)(endTime - startTime).TotalMilliseconds;
        }

        /// <summary>
        /// 获取两个时间之间的分钟数差
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>分钟数差（可能为负数）</returns>
        /// <remarks>
        /// 此方法返回两个时间之间的总分钟数，保留小数部分
        /// 如果endTime早于startTime，返回负数
        /// 返回double类型以保持精度
        /// 适用于需要分钟级时间差的场景
        /// </remarks>
        public static double GetMinutesDifference(DateTime startTime, DateTime endTime)
        {
            return (endTime - startTime).TotalMinutes;
        }

        /// <summary>
        /// 获取两个时间之间的小时数差
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>小时数差（可能为负数）</returns>
        /// <remarks>
        /// 此方法返回两个时间之间的总小时数，保留小数部分
        /// 如果endTime早于startTime，返回负数
        /// 返回double类型以保持精度
        /// 适用于需要小时级时间差的场景
        /// </remarks>
        public static double GetHoursDifference(DateTime startTime, DateTime endTime)
        {
            return (endTime - startTime).TotalHours;
        }

        /// <summary>
        /// 获取两个时间戳之间的秒数差
        /// </summary>
        /// <param name="startTimestamp">开始时间戳（秒）</param>
        /// <param name="endTimestamp">结束时间戳（秒）</param>
        /// <returns>秒数差</returns>
        /// <remarks>
        /// 此方法直接计算两个时间戳的差值
        /// 不需要转换为DateTime，计算更快
        /// 如果endTimestamp小于startTimestamp，返回负数
        /// 适用于Unix时间戳的秒级差值计算
        /// </remarks>
        public static long GetSecondsDifference(long startTimestamp, long endTimestamp)
        {
            return endTimestamp - startTimestamp;
        }

        /// <summary>
        /// 获取两个毫秒时间戳之间的毫秒数差
        /// </summary>
        /// <param name="startTimestampMs">开始时间戳（毫秒）</param>
        /// <param name="endTimestampMs">结束时间戳（毫秒）</param>
        /// <returns>毫秒数差</returns>
        /// <remarks>
        /// 此方法直接计算两个毫秒时间戳的差值
        /// 不需要转换为DateTime，计算更快
        /// 如果endTimestampMs小于startTimestampMs，返回负数
        /// 适用于Unix时间戳的毫秒级差值计算
        /// </remarks>
        public static long GetMillisecondsDifference(long startTimestampMs, long endTimestampMs)
        {
            return endTimestampMs - startTimestampMs;
        }

        /// <summary>
        /// 计算指定时间到当前时间经过了多少秒
        /// </summary>
        /// <param name="time">指定时间</param>
        /// <param name="useUtc">是否使用UTC时间作为当前时间，默认为false</param>
        /// <returns>经过的秒数（如果指定时间在未来，返回负数）</returns>
        /// <remarks>
        /// 此方法计算从指定时间到现在经过的总秒数
        /// 当useUtc=true时使用UTC时间，否则使用本地时间
        /// 结果会被转换为长整型，可能损失小数部分精度
        /// 常用于计算时间是否过期或剩余时间
        /// </remarks>
        public static long GetElapsedSeconds(DateTime time, bool useUtc = false)
        {
            var now = useUtc ? DateTime.UtcNow : DateTime.Now;
            return (long)(now - time).TotalSeconds;
        }

        /// <summary>
        /// 计算指定时间戳到当前时间经过了多少秒
        /// </summary>
        /// <param name="timestamp">时间戳（秒）</param>
        /// <param name="useUtc">是否使用UTC时间，默认为true</param>
        /// <returns>经过的秒数</returns>
        /// <remarks>
        /// 此方法直接使用时间戳计算经过的秒数
        /// 当useUtc=true时使用UTC时间戳，否则使用本地时间戳
        /// 计算效率高于DateTime转换方式
        /// 适用于Unix时间戳的剩余时间计算
        /// </remarks>
        public static long GetElapsedSeconds(long timestamp, bool useUtc = true)
        {
            var currentTimestamp = useUtc ? UnixTimeSeconds() : NowTimeSeconds();
            return currentTimestamp - timestamp;
        }

        /// <summary>
        /// 计算指定毫秒时间戳到当前时间经过了多少毫秒
        /// </summary>
        /// <param name="timestampMs">时间戳（毫秒）</param>
        /// <param name="useUtc">是否使用UTC时间，默认为true</param>
        /// <returns>经过的毫秒数</returns>
        /// <remarks>
        /// 此方法直接使用毫秒时间戳计算经过的毫秒数
        /// 当useUtc=true时使用UTC时间戳，否则使用本地时间戳
        /// 计算效率高于DateTime转换方式
        /// 适用于需要毫秒级精度的剩余时间计算
        /// </remarks>
        public static long GetElapsedMilliseconds(long timestampMs, bool useUtc = true)
        {
            var currentTimestamp = useUtc ? UnixTimeMilliseconds() : NowTimeMilliseconds();
            return currentTimestamp - timestampMs;
        }

        /// <summary>
        /// 获取时间差的绝对值（秒）
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>时间差的绝对秒数</returns>
        /// <remarks>
        /// 此方法返回两个时间之间的绝对秒数差
        /// 无论endTime是否早于startTime，都返回正数
        /// 结果会被转换为长整型，可能损失小数部分精度
        /// 适用于只需要时间间隔而不关心先后顺序的场景
        /// </remarks>
        public static long GetAbsoluteSecondsDifference(DateTime startTime, DateTime endTime)
        {
            return System.Math.Abs(GetSecondsDifference(startTime, endTime));
        }

        /// <summary>
        /// 获取时间差的绝对值（毫秒）
        /// </summary>
        /// <param name="startTime">开始时间</param>
        /// <param name="endTime">结束时间</param>
        /// <returns>时间差的绝对毫秒数</returns>
        /// <remarks>
        /// 此方法返回两个时间之间的绝对毫秒数差
        /// 无论endTime是否早于startTime，都返回正数
        /// 结果会被转换为长整型，可能损失小数部分精度
        /// 适用于需要毫秒级精度且不关心先后顺序的场景
        /// </remarks>
        public static long GetAbsoluteMillisecondsDifference(DateTime startTime, DateTime endTime)
        {
            return System.Math.Abs(GetMillisecondsDifference(startTime, endTime));
        }
    }
}