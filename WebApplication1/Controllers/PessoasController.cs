using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using POC.NetCore.Model;
using POC.NetCore.Services;



namespace POC.NetCore.API.Controllers
{
    [Route("api/[controller]")]
    public class PessoasController : Controller
    {
        private IPessoaService _personService;

        public PessoasController(IPessoaService PessoaService)
        {
            _personService = PessoaService;
        }
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var models = _personService.GetAll();

            return Ok(models);
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var model = _personService.Get(id);

            return Ok(model);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post(Pessoa model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var Pessoa = _personService.Add(model);

            return CreatedAtAction("Get", new { id = Pessoa.Id }, Pessoa);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Pessoa model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _personService.Update(id, model);

            return NoContent();
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            _personService.Delete(id);
            return NoContent();
        }
    }
}
