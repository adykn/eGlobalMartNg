using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eGlobalMartNg.api.Data;
using Microsoft.AspNetCore.Mvc;

namespace eGlobalMartNg.api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly DataContext _db;
        public ValuesController(DataContext db)
        {
            _db = db;

        }
        // GET api/values
        [HttpGet]
        public async Task<ActionResult<IEnumerable<string>>> Get()
        {
            var v=await _db.Values.ToAsyncEnumerable().ToList();
            if (v.Count==0) {
                _db.Values.Add(new Models.Value{Id=1,Name="Value1"});
                _db.Values.Add(new Models.Value{Id=2,Name="Value2"});
                _db.Values.Add(new Models.Value{Id=3,Name="Value3"});
                _db.SaveChanges();
                v=await _db.Values.ToAsyncEnumerable().ToList();
            }
            return Ok(v);
            //return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
