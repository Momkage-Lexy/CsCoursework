using System.ComponentModel.DataAnnotations;
namespace TeamGenerator2.Models;

public class Names
{
    [Required(ErrorMessage = "Name is required")]
    [RegularExpression(@"^([a-zA-Z\s,.\-_'’]+\r?\n?)*$", ErrorMessage = "Names can only contain letters, spaces, and the characters ,.-_'")]
    public string NameInput { get; set; } = string.Empty;

    [Required(ErrorMessage = "Team size is required")]
    [Range(2, 10, ErrorMessage = "Team size must be between 2 and 10.")]
    public int TeamSize { get; set; }

    // Splits the NameInput string into a list by using newline characters and removes any empty entries
    public List<string> NameList 
    { 
        get 
        {
            return NameInput?.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.RemoveEmptyEntries).ToList() ?? new List<string>();
        }
    }

    public override string ToString()
    {
        return $"{NameList} {TeamSize}";
    }
}
