namespace AppApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Sale
{
    [Key]
    public long RowID { get; set; }
    public int SalesPersonID { get; set; }
    public string? FirstName { get; set; }
    public string? MiddleName { get; set; }
    public string? LastName { get; set; }
    public int OrderYear { get; set; }
    public int SalesOrderID { get; set; }
    public decimal TotalDue { get; set; }
}
