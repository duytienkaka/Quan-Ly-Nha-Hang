using System.ComponentModel.DataAnnotations;

namespace RestaurantMVC.ViewModels
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "Tên đăng nhập bắt buộc")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Mật khẩu bắt buộc")]
        [DataType(DataType.Password)]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Họ tên bắt buộc")]
        public string FullName { get; set; } = "";

        [EmailAddress]
        public string? Email { get; set; }

        [StringLength(15)]
        public string? Phone { get; set; }
    }
}
