using Caching.Interface;
using CachingApp.Model;
using Caching.Attributes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Collections.Generic;

namespace CachingApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IMemoryCache _cache;
        private readonly ICachingService<Response> _cachingService;

        public ValuesController(IMemoryCache cache, ICachingService<Response> cachingService)
        {
            _cache = cache;
            _cachingService = cachingService;
        }

        // GET api/values
        [HttpGet]
        [ServiceFilter(typeof(CachingAttribute))]
        public ActionResult<IEnumerable<Response>> Get()
        {
            var items = new List<Response>();
            items.Add(new Response { Id = 1, Name = "HS" });
            items.Add(new Response { Id = 2, Name = "MP" });
            return Ok(items);
        }

       
        [HttpGet("{id}")]
        [ServiceFilter(typeof(CachingAttribute))]
        public ActionResult<IEnumerable<Response>> Get(int id)
        {
            var items = new List<Response>();
            items.Add(new Response { Id = 2, Name = "MP" });
            return Ok(items);
        }

        // POST api/values
        [HttpPost]
        [ServiceFilter(typeof(CachingAttribute))]
        public void Post([FromBody] string value)
        {
           
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        [ServiceFilter(typeof(CachingAttribute))]
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
