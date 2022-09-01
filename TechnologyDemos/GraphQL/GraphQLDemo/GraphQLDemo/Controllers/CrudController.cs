using GraphQLDemo.Domain;
using GraphQLDemo.DomainService;
using Microsoft.AspNetCore.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace GraphQLDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class CrudController<T> : Controller
        where T : DomainObject
    {
        public CrudController(IRepository<T> repository)
        {
            Repository = repository;
        }

        protected IRepository<T> Repository { get; }

        [HttpPost]
        public IActionResult Post(T item)
        {
            return Ok(Repository.Add(item).Id);
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(Repository.FindAll());
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            T res = Repository.Retrieve(id);
            if (res != null)
                return Ok(res);
            else
                return NotFound(id);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] T item)
        {
            if (Repository.Update(item))
                return Ok(id);
            else
                return BadRequest(item);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            return Ok(Repository.Delete(id));
        }
    }
}
