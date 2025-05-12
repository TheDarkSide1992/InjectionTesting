using System.ComponentModel.DataAnnotations;

namespace Infratructue.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
}