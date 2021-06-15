using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using BackEnd.Model;
using BackEnd.MW;
using BackEnd.Common;

namespace BackEnd.API.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly BasicMW _basic;

        public LoginController(BasicMW basic)
        {
            _basic = basic;
        }

        [HttpPost("[action]")]
        public LoginData check(object loginInfo)
        {
            Log.INFO("[Login.cehck] 로그인시도");
            //Json 변환
            JObject json = JObject.Parse(loginInfo.ToString());
            //DB조회
            JObject result = _basic.Login(json["user_id"].ToString());

            var user = new LoginData();
            //결과값이 있는지 확인
            if (json.HasValues)
            {
                //if (SecurityHelper.AESEncrypt256(json["user_pw"].ToString(),Global.MasterKey) == result["PASSWORD"].ToString())
                if (SecurityHelper.VerifyHashValue(json["user_pw"].ToString(), result["PASSWORD"].ToString()) == false)
                {
                    user.isLogin = true;
                    user.msg = "로그인 성공";
                    user.userid = result["USER_ID"].ToString();
                    user.username = result["USER_NM"].ToString();
                    user.dept = result["DEPT"].ToString();
                    user.useflag = result["USE_YN"].ToString();
                    user.expiry_DT = result["EXPIRE_DT"].ToString();

                    //string ip = httpcontext.request.headers["http_x_forwarded_for"].tostring();
                    //user.msg = ip;
                }
                else
                {
                    user.isLogin = false;
                    user.msg = "아이디나 비밀번호를 잘못입력하였습니다.";
                }
            }
            else
            {
                user.isLogin = false;
                user.msg = "아이디나 비밀번호를 잘못입력하였습니다.";
            }

            return user;   
        }

        [HttpGet]
        public bool LogOut()
        {
            HttpContext.Session.Remove("User_Login_Key");
            return true;
        }

        [HttpGet("[action]")]
        public string start()
        {
            return "서버시작";
        }
    }
}