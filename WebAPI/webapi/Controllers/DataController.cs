using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.AccessControl;
using WebAPI;
using static System.Runtime.InteropServices.JavaScript.JSType;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private readonly AppServerContext _db;
        public DataController(AppServerContext db)
        {
            _db = db;
        }
        // GET: api/<ValuesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                List<Data>? data = await _db.Datas.ToListAsync();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET api/<ValuesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ValuesController>
        [HttpPost("/data/write")]
        public async Task<IActionResult> Post([FromBody]string valueTest)
        {
            try
            {
                var newEntity = await _db.Datas.AddAsync(new Data { Value = valueTest });
                await _db.SaveChangesAsync();
                return Ok(newEntity.Entity);
            }
            catch
            {
                return BadRequest();
            }
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpGet("/data/read")]
        public async Task<IActionResult> GetData()
        {
            try
            {
                List<Data>? data = await _db.Datas.ToListAsync();
                return Ok(data);
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
