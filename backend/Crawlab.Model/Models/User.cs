using Crawlab.Model.Base;

namespace Crawlab.Model.Models;

public class User : BaseModel
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
}