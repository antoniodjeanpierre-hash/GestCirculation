using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestCirculation.Models;

namespace GestCirculation.Pages_Agents
{
    public class DeleteModel : PageModel
    {
        private readonly GestCirculationContext _context;

        public DeleteModel(GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Agent Agent { get; set; } = default!;

        [TempData]
        public string PopupMessage { get; set; }

        [TempData]
        public string PopupType { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var agent = await _context.Agent.FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                return NotFound();
            }
            else
            {
                Agent = agent;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var agent = await _context.Agent.FindAsync(id);
            if (agent != null)
            {
                _context.Agent.Remove(agent);
                await _context.SaveChangesAsync();
            }
            else
            {
                PopupMessage = "Erreur : agent introuvable !";
                PopupType = "error";
            }

            return RedirectToPage("./Index");
        }
    }
}