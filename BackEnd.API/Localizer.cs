using System;
using System.Resources;

namespace SMLS.Main
{
    internal sealed class Localizer
    {
        private static ResourceManager _txt;
        
        /// <summary>
        /// 문자 리소스
        /// </summary>
        static ResourceManager txt
        {
            get
            {
                if (_txt == null)
                {
                    _txt = new ResourceManager("SMLS.Main.Resources.Resource_Txt", typeof(Localizer).Assembly);
                }
                return _txt;
            }
        }

        /// <summary>
        /// 리소스에서 이름이 가리키는 문자열을 가져옵니다
        /// </summary>
        internal static string getString(string name)
        {
            string val = txt.GetString(name);
            
            if (string.IsNullOrEmpty(val)) val = string.Empty;

            return val;
        }
    }
}
