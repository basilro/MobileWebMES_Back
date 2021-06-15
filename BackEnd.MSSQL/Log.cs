using Logger;
using Logger.Info;
using System;

namespace BackEnd.MSSQL
{
    internal class Log
    {
        internal static ILogger DLog;
        private static LogSettingInfo _logSet;
        private static void Init()
        {
            //초기화
            _logSet = new LogSettingInfo(Logger.Enums.LoggerType.NOMAL, @"C:\", "SMLSLog", "DB");

#if DEBUG
            _logSet.WriteLevel = Logger.Enums.LogLevel.ALL;
#endif

            DLog = Logger.LogManager.GetLogger(_logSet);
        }

        private static void Init_chk()
        {
            if (_logSet == null)
            {
                Init();
            }
        }

        /// <summary>
        /// 에러로그
        /// </summary>
        /// <param name="msg">정보</param>
        internal static void ERROR(string msg)
        {
            Init_chk();
            if (DLog != null)
                DLog.ERROR(msg);
        }
        /// <summary>
        /// 에러로그
        /// </summary>
        /// <param name="fmt">StringFormat</param>
        /// <param name="args">Params</param>
        internal static void ERROR(string fmt, params object[] args)
        {
            Init_chk();
            if (DLog != null)
                DLog.ERROR(fmt, args);
        }
        /// <summary>
        /// 경고로그
        /// </summary>
        /// <param name="msg"></param>
        internal static void WARN(string msg)
        {
            Init_chk();
            if (DLog != null)
                DLog.WARN(msg);
        }
        /// <summary>
        /// 경고로그
        /// </summary>
        /// <param name="fmt">StringFormat</param>
        /// <param name="args">Params</param>
        internal static void WARN(string fmt, params object[] args)
        {
            Init_chk();
            if (DLog != null)
                DLog.WARN(fmt, args);
        }
        /// <summary>
        /// 디버그로그
        /// </summary>
        /// <param name="msg">문자</param>
        internal static void DEBUG(string msg)
        {
            Init_chk();
            if (DLog != null)
                DLog.DEBUG(msg);
        }
        /// <summary>
        /// 디버그시에만 나오는 로그
        /// </summary>
        /// <param name="fmt">StringFormat</param>
        /// <param name="args">Params</param>
        internal static void DEBUG(string fmt, params object[] args)
        {
            Init_chk();
            if (DLog != null)
                DLog.DEBUG(fmt, args);
        }
        /// <summary>
        /// 일반 로그
        /// </summary>
        /// <param name="msg"></param>
        internal static void INFO(string msg)
        {
            Init_chk();
            if (DLog != null)
                DLog.INFO(msg);
        }
        /// <summary>
        /// 일반적인 로그
        /// </summary>
        /// <param name="fmt">StringFormat</param>
        /// <param name="args">Params</param>
        internal static void INFO(string fmt, params object[] args)
        {
            Init_chk();
            if (DLog != null)
                DLog.INFO(fmt, args);
        }
    }
}
