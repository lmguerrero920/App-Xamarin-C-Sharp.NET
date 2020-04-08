using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KomodoAPI.Data;
using KomodoAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace KomodoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TblUbicationController : ControllerBase
    {
        private readonly TblUbications1Context _context;

        public TblUbicationController(TblUbications1Context contexto)
        {
            _context = contexto;
        }
        //TblUbication controlador 
        //TblUbications1 clase o modelo de la bd


        // Peticion Get: api/TblUbication
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TblUbications1>>> GetUbicationItems()
        {
            return await _context.UbicationItems.ToListAsync();
        }


        //Peticion  Get: un solo registros : api/TblUbication
        [HttpGet("{Id}")]
        public async Task<ActionResult<TblUbications1>> GetUbicationItem(int id)
        {
            //guarda en la variable lo que encuentra 
            var ubicationItem = await _context.UbicationItems.FindAsync(id);

            if (ubicationItem==null)
            {
                return NotFound();
            }

            return ubicationItem;

        }

        // peticion POST api/TblUbication
        [HttpPost]
        public async Task<ActionResult<TblUbications1>> PostUbicationItem(
            TblUbications1 item)
        {
            _context.UbicationItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetUbicationItem),new { id= item.Id}, item);

        }

        //Peticion de tipo PUT api/TblUbication/1
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutUbicationItem(
            int id, TblUbications1 item
            )
        {
            if(id!=item.Id)
            {
                return BadRequest();

            }
            _context.Entry(item).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return NoContent();
        }


        // DELETE api/TblUbication/1
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUbicationItem(int id)
        {
            var ubicationItem = await _context.UbicationItems.FindAsync(id);

            if (ubicationItem == null)
            {
                return NotFound();

            }

            _context.UbicationItems.Remove(ubicationItem);
            return NoContent();
        }


    }
}