using Newtonsoft.Json.Linq;
using BackEnd.IDA;

namespace BackEnd.MW
{
    public class CommonMW 
    {
        private ICommon _common;

        public CommonMW(ICommon common)
        {
            _common = common;
        }
        public object Save(string view, string type, string data) => _common.Save(view, type, data);

        public JArray Search(string view, string data) => _common.Search(view, data);

        public JObject MIBSearch(string view, string data) => _common.MIBSearch(view, data);
    }
}
