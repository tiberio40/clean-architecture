using Application.Interfaces;
using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ScaffoldingApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _personRepository;

        public PersonsController(IPersonRepository personRepository)
        {
            _personRepository = personRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Person>>> GetAll()
        {
            var persons = await _personRepository.GetAllAsync();
            return Ok(persons);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> GetById(string id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }
            return Ok(person);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Person person)
        {
            await _personRepository.AddAsync(person);
            return CreatedAtAction(nameof(GetById), new { id = person.Id }, person);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(string id, Person person)
        {
            var existingPerson = await _personRepository.GetByIdAsync(id);
            if (existingPerson == null)
            {
                return NotFound();
            }

            await _personRepository.UpdateAsync(id, person);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var person = await _personRepository.GetByIdAsync(id);
            if (person == null)
            {
                return NotFound();
            }

            await _personRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}
