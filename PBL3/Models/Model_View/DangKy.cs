using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PBL3.Models.Model_View
{
    public class DangKy
    {
        [Required(ErrorMessage = "Không được bỏ trống tên đăng nhập")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống mật khẩu")]
        [MaxLength(30,ErrorMessage ="Mật khẩu có độ dài từ 8 đến 30 ký tự")]
        [MinLength(8,ErrorMessage = "Mật khẩu có độ dài từ 8 đến 30 ký tự")]
        public string Password { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống ")]
        [Compare("Password", ErrorMessage = "Không trùng với mật khẩu")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Không được bỏ trống tên người dùng ")]
        public string Ten { get; set; }
        public DateTime? NgaySinh { get; set; }  
        public string DiaChi { get; set; }
        public bool GioiTinh { get; set; }
        [MaxLength(10, ErrorMessage = "Số điện thoại không đúng")]
        [MinLength(10, ErrorMessage = "Số điện thoại không đúng")]
        [Phone(ErrorMessage = "Số điện thoại không đúng")]
        public string SDT { get; set; }
    }
}