using BackEnd.IDA;
using Newtonsoft.Json.Linq;
using System;

namespace BackEnd.MSSQL
{
    public class CommonDA : ICommon
    {
        /// <summary>
        /// 저장
        /// </summary>
        /// <param name="view">리소스명</param>
        /// <param name="data">정보</param>
        /// <returns></returns>
        public object Save(string view, string type, string data)
        {
            switch (view)
            {
                case "user":
                    return Utils.getQuery("setUser", data);
                case "MB1001":
                    if (type == "S")
                    {
                        return Utils.getQuery("setMB1001_S", data);
                    }
                    else if (type == "S2")
                    {
                        return Utils.getQuery("setMB1001_S2", data);
                    }
                    else if (type == "D")
                    {
                        return Utils.getQuery("setMB1001_D", data);
                    }
                    else
                    {
                        break;
                    }
                case "MB1003":
                    if (type == "S")
                    {
                        return Utils.getQuery("setMB1003_S", data);
                    }
                    else if (type == "S2")
                    {
                        return Utils.getQuery("setMB1003_S2", data);
                    }
                    else if (type == "D")
                    {
                        return Utils.getQuery("setMB1003_D", data);
                    }
                    else
                    {
                        break;
                    }
                case "MB1004":
                    if(type == "C")
                    {
                        return Utils.getQuery("setMB1004_C", data);
                    }
                    else if (type == "D")
                    {
                        return Utils.getQuery("setMB1004_D", data);
                    }
                    else if((type == "I"))
                    {
                        return Utils.getQuery("setMB1004_I", data);
                    }
                    break;
            }
            throw new NotImplementedException();
        }

        /// <summary>
        /// 조회
        /// </summary>
        /// <param name="view">리소스명</param>
        /// <param name="data">정보</param>
        /// <returns></returns>
        public JArray Search(string view, string data)
        {
            switch (view)
            {
                case "user":
                    return Utils.getQuerys("getUser", data);
                case "MB1002":
                case "MB1005":
                    return Utils.getQuerys("", data);
                case "MB1001":
                case "MB1003":
                case "MB1004":
                case "MB1011":
                case "MB1013":
                case "MB1014":
                    return Utils.getQuerys(reName(true, view), data);

            }
            throw new NotImplementedException();
        }

        public JObject MIBSearch(string view, string data)
        {
            switch (view)
            {
                case "WB1001_1":
                case "WB1001_2":
                case "WB1001_3":
                case "WB1001_4":
                case "WB1001_5":
                case "WB1001_6":
                case "WB1001_7":
                case "WB1001_8":
                case "WB1001_9":
                case "WB1001_10":
                case "WB1001_11":
                case "WB1001_12":
                case "WB1001_13":
                case "WB1002_1":
                case "WB1002_2":
                case "WB1002_3":
                case "WB1003":
                    return Utils.newgetQuerys(reName(true, view), data);
            }
            throw new NotImplementedException();
        }

        private string reName(bool isGet, string name)
        {
            return isGet ? "get" + name : "set" + name;
        }
    }
}
