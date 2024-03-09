namespace AppApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

//[Table("vProductAndDescription", Schema = "Production")]
public class Product
{
    [Key]
    public long RowID { get; set; }
    public int ProductID { get; set; }
    public string? Name { get; set; }
    public string? ProductModel { get; set; }
    public string? CultureID { get; set; }
    public string? Description { get; set; }
}
