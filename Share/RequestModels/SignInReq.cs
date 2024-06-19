using System.ComponentModel.DataAnnotations;

namespace Share.RequestModels;

public sealed record SignInReq
{
    [Required]
    public string UserName { get; init; }

    [Required]
    public string Password { get; init; }

    [Required]
    public bool RememberMe { get; init; }

    public string? TwoFactorCode { get; init; }
    public string? TwoFactorRecoveryCode { get; init; }
}