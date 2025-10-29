using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestCirculation.Models;

namespace GestCirculation.Pages_Agents
{
    public class EditModel : PageModel
    {
        private readonly GestCirculation.Models.GestCirculationContext _context;

        public EditModel(GestCirculation.Models.GestCirculationContext context)
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
                PopupMessage = "Agent non trouvé.";
                PopupType = "error";
                return NotFound();
            }

            var agent = await _context.Agent.FirstOrDefaultAsync(m => m.Id == id);
            if (agent == null)
            {
                PopupMessage = "Agent non trouvé.";
                PopupType = "error";
                return NotFound();
            }

            Agent = agent;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PopupMessage = "Certains champs sont invalides ou manquants.";
                PopupType = "warning";
                return Page();
            }

            _context.Attach(Agent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgentExists(Agent.Id))
                {
                    PopupMessage = "Agent introuvable ou supprimé.";
                    PopupType = "error";
                    return NotFound();
                }
                else
                {
                    PopupMessage = "Erreur de mise à jour, veuillez réessayer.";
                    PopupType = "error";
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }


        private bool AgentExists(Guid id)
        {
            return _context.Agent.Any(e => e.Id == id);
        }
    }
}
