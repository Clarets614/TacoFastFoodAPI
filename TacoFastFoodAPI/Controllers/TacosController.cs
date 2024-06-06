using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TacoFastFoodAPI.Models;

namespace TacoFastFoodAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TacosController : ControllerBase
    {
        FastFoodTacoDbContext dbContext = new FastFoodTacoDbContext();

        [HttpGet()]
        public IActionResult GetTacos(bool? shell = null)
        {
            List<Taco> result = dbContext.Tacos.ToList();
            if (shell != null) 
            { 
                result = result.Where(t => t.SoftShell == shell).ToList();
            
            }

            return Ok(result);
        }

        [HttpGet("id")]
        public IActionResult GetById(int id)
        {
            Taco result = dbContext.Tacos.Find(id);
            if(result == null)
            {
                return NotFound("Taco Not Found");
            }
            return Ok(result);
        }

        [HttpPost()]
        public IActionResult AddTaco([FromBody]Taco newTaco)
        {
            newTaco.Id = 0;
            dbContext.Tacos.Add(newTaco);
            dbContext.SaveChanges();
            return Created($"/api/Tacos/{newTaco.Id}", newTaco);

        }

        [HttpDelete("id")]
        public IActionResult DeleteTaco(int id)
        {
            Taco t = dbContext.Tacos.Find(id);
            if (t == null) 
            { 
            return NotFound();
            }
            dbContext.Tacos.Remove(t);
            dbContext.SaveChanges();
            return NoContent();

        }


    }
}
