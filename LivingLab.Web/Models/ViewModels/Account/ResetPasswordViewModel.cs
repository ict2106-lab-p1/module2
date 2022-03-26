using System.ComponentModel.DataAnnotations;

namespace LivingLab.Web.Models.ViewModels.Login;

public class ResetPasswordViewModel
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; }
    
    [DataType(DataType.Password)]
    [Display(Name = "Confirm Password")]
    [Compare("Password", ErrorMessage = "Password and Confirm Password must match")]
    public string ConfirmPassword { get; set; }
    
    public string Token { get; set; }
    
    public bool IsSuccess { get; set; }
}
