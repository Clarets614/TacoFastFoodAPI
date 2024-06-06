using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DrinksController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetDrinks([FromBody]string? SortByCost = null)
        {
            List<Drink> result = dbContext.Drinks.ToList();
            if(SortByCost != null)
            {
                if(SortByCost.ToLower() == "descending")
                {
                    return Ok(result.OrderByDescending(d => d.Cost));
                }
                else
                {
                     if (SortByCost.ToLower() == "ascending")
                    {
                        return Ok(result.OrderBy((d => d.Cost));
                    }
                }
            }
            return Ok(result);

        }

        [HttpGet("/Drinks/{id}")]
        public IActionResult GetDrinks(int id)
        {
            Drink result = dbContext.Drinks.Find(id);
            if (id == null)
            {
                return NotFound("No stored Drink Found");
            }

            return Ok(result);
        }

        [HttpPost("/Drinks")]
        public IActionResult AddDrink([FromBody] Drink newDrink)
        {
            newDrink.Id = 0;
            dbContext.Drinks.Add(newDrink);
            dbContext.SaveChanges();
            return Created($"/api/Drinks/{newDrink.Id}", newDrink);

        }

        [HttpPut("{id}")]
        public IActionResult UpdateDrinks([FromBody]Drink targetDrink, int id)
        {
            if(id != targetDrink.Id)
            {
                return BadRequest();
            }
            if(!dbContext.Drinks.Any(d =>d.Id == id))
            {
                return NoContent();
            }
            dbContext.Drinks.Update(targetDrink);
            dbContext.SaveChanges();
            return NoContent();

        }

    }
}
