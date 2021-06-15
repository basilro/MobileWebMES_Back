using System;
using System.Collections.Generic;
using System.Text;

namespace BackEnd.Model
{
    public class User
    {
        /// <summary>
        /// 사용자 아이디
        /// </summary>
        public string user_cd { get; set; }
        /// <summary>
        /// 사용자 이름
        /// </summary>
        public string user_nm { get; set; }
        /// <summary>
        /// 소속회사
        /// </summary>
        public string company { get; set; }
        /// <summary>
        /// 권한
        /// </summary>
        public string auth { get; set; }
        /// <summary>
        /// 사용여부
        /// </summary>
        public string useflag { get; set; }
        /// <summary>
        /// 아이디 만료기간
        /// </summary>
        public string expiry_DT { get; set; }
        /// <summary>
        /// 사진
        /// </summary>
        public string picture { get; set; }
        /// <summary>
        /// 계정 생성날짜
        /// </summary>
        public string insrt_DT { get; set; }
        /// <summary>
        /// 계정 생성사용자
        /// </summary>
        public string insrt_User { get; set; }
        /// <summary>
        /// 계정 수정날짜
        /// </summary>
        public string updt_DT { get; set; }
        /// <summary>
        /// 계정 수정사용자
        /// </summary>
        public string updt_User { get; set; }
    }
}
