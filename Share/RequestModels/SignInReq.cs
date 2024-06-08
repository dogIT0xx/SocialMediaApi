using System.ComponentModel.DataAnnotations;

namespace Share.RequestModels;

public sealed record SignInReq
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }

    [Required]
    public bool RememberMe { get; set; }

    public string? TwoFactorCode { get; set; }
    public string? TwoFactorRecoveryCode { get; set; }
}