using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        [HttpGet("{id:long}")]
        public async Task<ActionResult<IEnumerable<Todo>>> GetTodo(long id)
        {
            Todo[] arr = await _context.Todo.Where<Todo>(e => e.Uid==id).ToArrayAsync();
            
            if (arr == null)
            {
                return null;
            }

            return arr;
        }


        // GET: api/Todoes/5
        [HttpGet("{item}")]
        public async Task<ActionResult <IEnumerable<Todo>>> GetTodoByItem(string item)
        {

            
           Todo []arr =  await _context.Todo.Where<Todo>(e => e.Items.Contains(item)).ToArrayAsync(); 
            if (arr == null)
            {
                return null;
            }
            return arr;
           
        }





        // PUT: api/Todoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id:long}")]
        public async Task<IActionResult> PutTodo(long id, Todo todo)
        {
            if (id != todo.Uid)
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
        [HttpDelete("{id:long}")]
        public async Task<IActionResult> DeleteTodo(long id,string item)
        {
            
            var todo = _context.Todo.Where(e => e.Uid == id && (e.Items == item)).FirstOrDefault();
           
            if (todo == null)
            {

                return StatusCode(204);
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool TodoExists(long id)
        {
            return _context.Todo.Any(e => e.Uid == id );
        }
    }
}
