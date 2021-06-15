using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using BackEnd.IDA;
using System;

namespace BackEnd.MSSQL
{
    public class BasicDA : IBasic
    {
        private readonly IConfiguration _configuration;

        public BasicDA(IConfiguration configuration)
        {
            _configuration = configuration;
            Loader.DBSet(_configuration);
        }

        /// <summary>
        /// 모바일 메뉴 로드
        /// </summary>
        public JArray getMenuList(string auth)
        {
            return Utils.getQuerys("getMenu", auth);
        }

        /// <summary>
        /// 메뉴 추가, 저장, 삭제 처리
        /// </summary>
        public  object setMenu(string data)
        {
            return Utils.getQuery("setMenu", data);
        }

        /// <summary>
        /// 로그인
        /// </summary>
        public JObject Login(string userId)
        {
            return Utils.getQuery("LOGIN", userId);
        }

        /// <summary>
        /// 현황판 메뉴 로드
        /// </summary>
        public JArray getMenu_Board()
        {
            return Utils.getQuerys("getMenu_Board");
        }
    }
}
