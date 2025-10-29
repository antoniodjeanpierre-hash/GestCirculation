using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestCirculation.Models;

namespace GestCirculation.Pages_Conducteurs
{
    public class DeleteModel : PageModel
    {
        private readonly GestCirculation.Models.GestCirculationContext _context;

        public DeleteModel(GestCirculation.Models.GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Conducteur Conducteur { get; set; } = default!;
        
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

            var conducteur = await _context.Conducteur.FirstOrDefaultAsync(m => m.Id == id);

            if (conducteur == null)
            {
                return NotFound();
            }
            else
            {
                Conducteur = conducteur;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var conducteur = await _context.Conducteur.FindAsync(id);
            if (conducteur != null)
            {
                Conducteur = conducteur;
                _context.Conducteur.Remove(Conducteur);
                await _context.SaveChangesAsync();
            }
            else
            {
                PopupMessage = "Erreur : conducteur introuvable !";
                PopupType = "error";
            }
            return RedirectToPage("./Index");
        }
    }
}
