# ContosoPizza
# Example on create a Controller in .net core webapi
# Controllers
  > ContosoPizza
  -------------------------------------------------
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
    ------------------------------------------------
# Models
  > Pizza
  -------------------------------------------------
   public class Pizza
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool IsGlutenFree { get; set; }
    }
   -------------------------------------------------
# Services
  > PizzaService
  --------------------------------------------------
    public static class PizzaService
    {
        static List<Pizza> Pizzas { get; }
        static int NextId = 3;
        static PizzaService()
        {
            Pizzas = new List<Pizza>{
                new Pizza { Id = 1, Name = "Classic Italian", IsGlutenFree = false },
                new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true }
            };
        }

        public static List<Pizza> GetAll() => Pizzas;

        public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

        public static void Add(Pizza pizza)
        {
            pizza.Id = NextId++;
            Pizzas.Add(pizza);
        }

        public static void Delete(int id)
        {
            Pizza? pizza = Get(id);
            if (pizza == null)
                return;
            Pizzas.Remove(pizza);
        }

        public static void Update(Pizza pizza)
        {
            var index = Pizzas.FindIndex(p => p.Id == pizza.Id);
            if(index == -1) return;
            Pizzas[index] = pizza;
        }
    }
