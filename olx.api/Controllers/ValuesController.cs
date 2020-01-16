using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace olx.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _context ;
        private readonly ILogger<ValuesController> _logger;

        public ValuesController(DataContext context, ILogger<ValuesController> logger)
        {
            _context = context ;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Values()
        { 
            var vals = _context.Values.ToList() ;
            return Ok(vals) ;
        }

        [HttpGet("{id}")]
        public IActionResult Get ( int id ) 
        {
            var value = _context.Values.FirstOrDefault(x => x.id
            == id) ;
	        return Ok(value);
        }

        [HttpPost]
        public void save([FromBody] string body ) 
        {

        }

        [HttpPut("{id}")]
        public void update(int id, [FromBody] string body )
        {

        }

        [HttpDelete("{id}")]
        public IActionResult Delete (int id ) 
        {
            return Ok(Boolean.TrueString) ;
         }
    }
}
