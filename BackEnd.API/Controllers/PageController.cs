using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BackEnd.MW;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace BackEnd.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class PageController : ControllerBase
    {
        private readonly CommonMW _common;

        public PageController(CommonMW common)
        {
            _common = common;
        }

        [HttpGet("[action]/{page}/{data}")]
        public object Get(string page, string data)
        {
            Log.INFO("[Page.Get] 조회시도 : {0}", page);
            Log.DEBUG(data);
            //JObject jsondata = JObject.Parse(data);
            //string param = string.Empty;
            //foreach (var val in jsondata)
            //{
            //    param = string.IsNullOrEmpty(param) ? "'" + val.Value + "'" : param + ",'" + val.Value + "'";
            //}
            string param = string.Empty;
            string[] sdata = data.Split(',');
            foreach(var value in sdata)
            {
                param = string.IsNullOrEmpty(param) ? "'" + value + "'" : param + ",'" + value + "'";
            }
            var result = _common.Search(page, param);

            return result;
        }

        [HttpPost("[action]")]
        public object PostGet(object data)
        {
            var json = JObject.Parse(data.ToString());
            string page = string.Empty;
            string type = string.Empty;
            string param = string.Empty;
            foreach (var val in json)
            {
                switch (val.Key)
                {
                    case "page":
                        page = val.Value.ToString();
                        break;
                    default:
                        param = string.IsNullOrEmpty(param) ? "'" + val.Value.ToString() + "'"
                            : param + ",'" + val.Value.ToString() + "'";
                        break;
                }
                //Console.Write(val);
            }
            param = string.IsNullOrEmpty(param) ? DateTime.Now.ToString("yyyyMMdd") : 
                param + ",'" + DateTime.Now.ToString("yyyyMMdd");
            Log.INFO("[Page.PostGet] 조회시도 : {0}", page);
            var result = _common.MIBSearch(page, param);
            return result;
        }


        [HttpPost("[action]")]
        public object Save(object data)
        {

            var json = JObject.Parse(data.ToString());
            string page = string.Empty;
            string type = string.Empty;
            string param = string.Empty;
            foreach (var val in json)
            {
                switch (val.Key)
                {
                    case "page":
                        page = val.Value.ToString();
                        break;
                    case "type":
                        type = val.Value.ToString();
                        break;
                    default:
                        param = string.IsNullOrEmpty(param) ? "'" + val.Value.ToString() + "'" 
                            : param + ",'" + val.Value.ToString() + "'";
                        break;
                }
                //Console.Write(val);
            }

            Log.INFO("[Page.Save] 저장시도 : {0} {1}", page, type);
            Log.DEBUG(param);
            var result = _common.Save(page, type, param);
            return result;
        }

        [HttpGet("[action]")]
        public string test()
        {
            return "Page호출";
        }
    }
}