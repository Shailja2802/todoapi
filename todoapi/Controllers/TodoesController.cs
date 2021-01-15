using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using todoapi;
using todoapi.Data;

namespace todoapi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly TodoapiContext _context;

        public TodoesController(TodoapiContext context)
        {
            _context = context;
        }

        // GET: api/Todoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo()
        {
            return await _context.Todo.ToListAsync();
        }

        // GET: api/Todoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Todo>> GetTodo(long id)
        {

            //var todos[] = _context.Todo.ToArrayAsync((e => e.Uid == id));


            //  Contact[] contact=contact.<Contact>

            // Todo[] todoes = _context.Todo.Where<Todo >(e=>e.Uid==id).ToArray<Todo>();
            //   string item = todoes.Items;
            var todo = await _context.Todo.FindAsync(id);
           // var todo = await _context.Todo.ToDictionaryAsync(e=>e.Items);
            //FirstOrDefaultAsync(e=>e.Items  ==item, e=>e.Uid==id );
            if (todo == null)
            {
                return null;
            }

            return todo;
        }

        // PUT: api/Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodo(int id, Todo todo)
        {
            if (id != todo.Id)
            {
                return BadRequest();
            }

            _context.Entry(todo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Todoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Todo>> PostTodo(Todo todo)
        {
            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.Id }, todo);
        }

        // DELETE: api/Todoes/5
        [HttpDelete("{id},{item}")]
        public async Task<IActionResult> DeleteTodo(long id,string item)
        {

            Todo todo1 = (Todo)_context.Todo.Where(e => e.Uid == id && (e.Items == item));
            int iden = todo1.Id;
            var todo = await _context.Todo.FindAsync(iden);
            if (todo == null)
            {

                return StatusCode(204);
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Any(e => e.Id == id);
        }
    }
}
