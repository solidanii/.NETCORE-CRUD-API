using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using crud_asp_api.DAL;
using crud_asp_api.Models;

namespace crud_asp_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class branchesController : ControllerBase
    {
        //to connect with database
        private readonly BranchDbContext _context;

        //allows the controller to have access to a database context
        public branchesController(BranchDbContext context)
        {
            _context = context;
        }

        // GET: api/branches
        [HttpGet]
        public async Task<ActionResult<IEnumerable<branch>>> GetBranches()
        {
          if (_context.Branches == null)
          {
              return NotFound(); // returns a 404 Not Found response.
            }
            return await _context.Branches.ToListAsync();
        }

        // GET: api/branches/5
        [HttpGet("{id}")] // expects a parameter `id` to be provided in the URL. 

        public async Task<ActionResult<branch>> Getbranch(int id)
        {
          if (_context.Branches == null)
          {
              return NotFound();
          }
            var branch = await _context.Branches.FindAsync(id);

            if (branch == null)
            {
                return NotFound();
            }

            return branch;
        }

        // PUT: api/branches/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Putbranch(int id, branch branch)
        {
            if (id != branch.branchid)
            {
                return BadRequest();
            }
            //entitystate ->branch
            _context.Entry(branch).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!branchExists(id))
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

        // POST: api/branches
        [HttpPost]
        public async Task<ActionResult<branch>> Postbranch(branch branch)
        {
          if (_context.Branches == null)
          {
              return Problem("Entity set 'BranchDbContext.Branches'  is null.");
          }
            _context.Branches.Add(branch);
            await _context.SaveChangesAsync();

            return CreatedAtAction("Getbranch", new { id = branch.branchid }, branch);
        }

        // DELETE: api/branches/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletebranch(int id)
        {
            //checks if the `branches` DbSet in the `_context` instance is null.
            if (_context.Branches == null)
            {
                return NotFound();
            }
            var branch = await _context.Branches.FindAsync(id);
            if (branch == null)
            {
                return NotFound();
            }

            _context.Branches.Remove(branch);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool branchExists(int id)
        {
            return (_context.Branches?.Any(e => e.branchid == id)).GetValueOrDefault();
        }
    }
}
