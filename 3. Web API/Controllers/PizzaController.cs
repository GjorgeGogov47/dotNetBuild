using _3._Web_API.Services;
using _3._Web_API.Models;
using Microsoft.AspNetCore.Mvc;

namespace _3._Web_API.Controllers;

[ApiController]
[Route("[controller]")]
public class PizzaController : ControllerBase
{
    public PizzaController()
    {
    }

    [HttpGet]
    public ActionResult<List<Pizza>> GetAll() =>
        PizzaService.GetAll();
    
    [HttpGet("{id}")]
    public ActionResult<Pizza> Get(int id)
    {
        var pizza = PizzaService.Get(id);
        if(pizza is null)
            return NotFound();
        
        return pizza;
    }

    // POST action

    // PUT action

    // DELETE action
}