using System.ComponentModel.DataAnnotations.Schema;
using Company.ClassLibrary1;

namespace API;
[Table("Photos")]
public class Photo
{
    public int Id { get; set; }
    public string? Url { get; set; }
    public string? PublicId { get; set; }
    public bool IsMain { get; set; }
    public int AppUserID { get; set; }
    public AppUser? AppUser { get; set; }
}
