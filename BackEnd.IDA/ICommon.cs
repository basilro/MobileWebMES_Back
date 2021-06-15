using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.IDA
{
    public interface ICommon
    {
        JArray Search(string view, string data);

        JObject MIBSearch(string view, string data);

        object Save(string view, string type, string data);
    }
}
