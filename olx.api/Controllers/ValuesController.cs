using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using olx.api.Data ;
using Microsoft.EntityFrameworkCore ;

namespace olx.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        public async Task<IActionResult> Values()
        { 
            var vals = await _context.Values.ToListAsync() ;
            // 1. Is it avaiable in Sqlite
            // 2. yes --> Fetch
            // 3. No --> Fetch & update sqlite
            return Ok(vals);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get ( int id )
        {
            var value = await _context.Values.FirstOrDefaultAsync(x => x.id== id) ;
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
