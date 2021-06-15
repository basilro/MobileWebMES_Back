using Newtonsoft.Json.Linq;
using BackEnd.IDA;
using System;

namespace BackEnd.MW
{
    public class BasicMW
    {
        private IBasic _basic;

        public BasicMW(IBasic basic)
        {
            _basic = basic;
        }

        public JArray getMenuList(string auth) => _basic.getMenuList(auth);       

        public JObject Login(string userId) => _basic.Login(userId);

        public object setMenu(string data) => _basic.setMenu(data);

        public JArray getMenu_Board() => _basic.getMenu_Board();
    }
}
