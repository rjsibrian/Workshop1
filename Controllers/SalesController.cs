using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppApi.Models;

namespace Controller_based_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SalesController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL SALES OVERVIEW: api/Sales
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Sale>>> GetAll()
        {
             try
            {
                var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, sp.BusinessEntityID AS SalesPersonID, " +
                            "p.FirstName, p.MiddleName, p.LastName, " +
                            "YEAR(soh.OrderDate) AS OrderYear, soh.SalesOrderID, soh.TotalDue " +
                            "FROM " +
                            "Sales.SalesPerson sp " +
                            "INNER JOIN Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID " +
                            "INNER JOIN Sales.SalesOrderHeader soh ON sp.BusinessEntityID = soh.SalesPersonID";
    
                var result = await _context.Sales
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (result.Count == 0)
                {
                    return NotFound();
                }

                return result;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET ALL SALES DETAILS BY NAME: api/SalesByName/name
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSaleByName(string name)
        {
             try
            {
                var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, sp.BusinessEntityID AS SalesPersonID, " +
                            "p.FirstName, p.MiddleName, p.LastName, " +
                            "YEAR(soh.OrderDate) AS OrderYear, soh.SalesOrderID, soh.TotalDue " +
                            "FROM " +
                            "Sales.SalesPerson sp " +
                            "INNER JOIN Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID " +
                            "INNER JOIN Sales.SalesOrderHeader soh ON sp.BusinessEntityID = soh.SalesPersonID " +
                            "WHERE " +
                            $"(CONCAT(p.FirstName, p.MiddleName, p.LastName) LIKE '%{name}%' OR " +
                            $"CONCAT(p. FirstName, ' ', p.LastName) LIKE '%{name}%')";
                            
                var result = await _context.Sales
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (result.Count == 0)
                {
                    return NotFound();
                }

                return result;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET ALL SALES DETAILS BY YEAR: api/SalesByYear/year
        [HttpGet("ByYear/{year}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSaleByYear(int year)
        {
             try
            {
                var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, sp.BusinessEntityID AS SalesPersonID, " +
                            "p.FirstName, p.MiddleName, p.LastName, " +
                            "YEAR(soh.OrderDate) AS OrderYear, soh.SalesOrderID, soh.TotalDue " +
                            "FROM " +
                            "Sales.SalesPerson sp " +
                            "INNER JOIN Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID " +
                            "INNER JOIN Sales.SalesOrderHeader soh ON sp.BusinessEntityID = soh.SalesPersonID " +
                            $"WHERE YEAR(soh.OrderDate) = '{year}'";
    
                var result = await _context.Sales
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (result.Count == 0)
                {
                    return NotFound();
                }

                return result;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET ALL SALES DETAILS BY NAME AND YEAR: api/SalesByNamenAndYear/name/year
        [HttpGet("ByNameAndYear/{name}/{year}")]
        public async Task<ActionResult<IEnumerable<Sale>>> GetSaleByNameAndYear(string name, int year)
        {
             try
            {
                var query = "SELECT ROW_NUMBER() OVER (ORDER BY (SELECT 1)) AS RowID, sp.BusinessEntityID AS SalesPersonID, " +
                            "p.FirstName, p.MiddleName, p.LastName, " +
                            "YEAR(soh.OrderDate) AS OrderYear, soh.SalesOrderID, soh.TotalDue " +
                            "FROM " +
                            "Sales.SalesPerson sp " +
                            "INNER JOIN Person.Person p ON sp.BusinessEntityID = p.BusinessEntityID " +
                            "INNER JOIN Sales.SalesOrderHeader soh ON sp.BusinessEntityID = soh.SalesPersonID " +
                            "WHERE " +
                            $"(CONCAT(p.FirstName, p.MiddleName, p.LastName) LIKE '%{name}%' OR " +
                            $"CONCAT(p. FirstName, ' ', p.LastName) LIKE '%{name}%') " +
                            $"AND (YEAR(soh.OrderDate) = '{year}')";
    
                var result = await _context.Sales
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (result.Count == 0)
                {
                    return NotFound();
                }

                return result;
            } 
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
