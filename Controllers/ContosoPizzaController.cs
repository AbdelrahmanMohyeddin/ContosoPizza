using ContosoPizza.Models;
using ContosoPizza.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContosoPizza.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContosoPizzaController : ControllerBase
    {

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(PizzaService.GetAll());
        }


        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var pizza = PizzaService.Get(id);
            if (pizza == null)
                return NotFound();
            return Ok(pizza);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Pizza pizza)
        {
            PizzaService.Add(pizza);
            return CreatedAtAction(nameof(Create), new { Id = pizza.Id }, pizza);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Pizza pizza)
        {
            if (id != pizza.Id)
                return BadRequest();
            var existPizza = Get(id);
            if (existPizza == null)
                return NotFound();
            PizzaService.Update(pizza);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            PizzaService.Delete(id);
            return NoContent();
        }
    }
}
