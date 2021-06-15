using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BackEnd.API
{
    public class utils
    {
        /// <summary>
        /// 공통파라미터 생성
        /// </summary>
        public static string lastParam(JObject data, string type)
        {
            return ",'" + data["user_id"] + "',"+ type + ",'',''";
        }

        /// <summary>
        /// 공통파라미터 생성
        /// </summary>
        public static string lastParam(JObject data)
        {
            return ",'" + data["user_id"] + "','',''";
        }

        public static string getParam(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                data = "'','',''";
            }
            return data;
        }

        /// <summary>
        /// 경로생성
        /// </summary>
        public static string CreateDirectory(string root)
        {
            string path = root;
            string today = DateTime.Now.ToString("yyyyMMdd");
            //폴더는 년>월>일 순으로 생성
            for(int i=4; i< 9; i+=2)
            {
                path = Path.Combine(path, today.Substring(0, i));
            }
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            return path;
        }
    }
}
