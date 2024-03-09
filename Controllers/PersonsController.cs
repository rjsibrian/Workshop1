using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AppApi.Models;

namespace Controller_based_API
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PersonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET ALL PERSONS: api/Persons
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        {
            try
            {
                return await _context.Persons.ToListAsync();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET PERSONS BY NAME: api/Persons/ByName/name
        [HttpGet("ByName/{name}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonByName(string name)
        {
            try
            {
                var query = "SELECT * " +
                            "FROM [AdventureWorks2022].[HumanResources].[vEmployee] " +
                            $"WHERE (CONCAT(FirstName, ' ', MiddleName, ' ', LastName) LIKE '%{name}%' OR " +
                            $"CONCAT(FirstName, ' ', LastName) LIKE '%{name}%')";

                var persons = await _context.Persons
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (persons.Count == 0)
                {
                    return NotFound();
                }

                return persons;
            } 
            
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET PERSONS BY PERSON TYPE: api/Persons/ByPersonType/personType
        [HttpGet("ByPersonType/{personType}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonByPersonType(string personType)
        {
            try
            {
                var query = "SELECT A.* " +
                            "FROM [AdventureWorks2022].[HumanResources].[vEmployee] A " +
                            "INNER JOIN Person.Person B ON a.BusinessEntityID = b.BusinessEntityID " +
                            $"WHERE B.PersonType = '{personType}'";

                var persons = await _context.Persons
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (persons.Count == 0)
                {
                    return NotFound();
                }

                return persons;
            } 
            
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }

        // GET PERSONS BY NAME AND PERSON TYPE: api/Persons/ByNameAndPersonType/name/personType
        [HttpGet("ByNameAndPersonType/{name}/{personType}")]
        public async Task<ActionResult<IEnumerable<Person>>> GetPersonByNameAndPersonType(string name, string personType)
        {
            try
            {
                var query = "SELECT A.BusinessEntityID, A.Title, A.FirstName, A.MiddleName, A.LastName, A.Suffix, A.JobTitle, " +
                            "A.PhoneNumber, A.PhoneNumberType, A.EmailAddress, A.EmailPromotion, A.AddressLine1, " +
                            "A.AddressLine2, A.City, A.StateProvinceName, A.PostalCode, A.CountryRegionName " +
                            $"FROM [AdventureWorks2022].[HumanResources].[vEmployee] A " +
                            $"INNER JOIN Person.Person B ON A.BusinessEntityID = B.BusinessEntityID " +
                            $"WHERE " +
                            $"    ('{name}' ='' OR '{name}' IS NULL OR CONCAT(A.FirstName, ' ', A.MiddleName, ' ', A.LastName) LIKE '%{name}%' " +
                            $"    OR CONCAT(A. FirstName, ' ', A.LastName) LIKE '%{name}%') " +
                            $"    AND " +
                            $"    ('{personType}' = '' OR '{personType}' IS NULL OR B.PersonType = '{personType}')";

                var persons = await _context.Persons
                                            .FromSqlRaw(query)
                                            .ToListAsync();

                if (persons.Count == 0)
                {
                    return NotFound();
                }

                return persons;
            } 
            
            catch (Exception ex)
            {
                return StatusCode(500, $"Error: {ex.Message}");
            }
        }
    }
}
