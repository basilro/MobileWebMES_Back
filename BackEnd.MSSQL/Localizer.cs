using System;
using System.Resources;

namespace BackEnd.MSSQL
{
    internal sealed class Localizer
    {
        private static ResourceManager _sql;
        private static ResourceManager _txt;

        /// <summary>
        /// 쿼리 리소스
        /// </summary>
        static ResourceManager sql
        {
            get
            {
                if (_sql == null)
                {
                    _sql = new ResourceManager("BackEnd.MSSQL.Resources.Resource_Sql", typeof(Localizer).Assembly);
                }
                return _sql;
            }
        }
        /// <summary>
        /// 문자 리소스
        /// </summary>
        static ResourceManager txt
        {
            get
            {
                if (_txt == null)
                {
                    _txt = new ResourceManager("BackEnd.MSSQL.Resources.Resource_Txt", typeof(Localizer).Assembly);
                }
                return _txt;
            }
        }

        /// <summary>
        /// 리소스에서 이름이 가리키는 문자열을 가져옵니다
        /// </summary>
        internal static string getString(string name)
        {
            string val = sql.GetString(name);

            if (string.IsNullOrEmpty(val)) val = txt.GetString(name);
            if (string.IsNullOrEmpty(val)) val = string.Empty;

            return val;
        }
    }
}
