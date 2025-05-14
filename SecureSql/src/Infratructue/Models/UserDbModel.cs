using System.ComponentModel.DataAnnotations;

namespace Infratructue.Models;

public class UserDbModel
{
    public string Id { get; set; }
    
    [MaxLength(255)]
    public string Name { get; set; }
}