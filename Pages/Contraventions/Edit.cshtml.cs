using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestCirculation.Models;


namespace GestCirculation.Pages_Contraventions
{
    public class EditModel : PageModel
    {
        private readonly GestCirculationContext _context;

        public EditModel(GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contravention Contravention { get; set; } = default!;

        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var contravention = await _context.Contravention.FirstOrDefaultAsync(m => m.Id == id);
            if (contravention == null)
                return NotFound();

            Contravention = contravention;

            ViewData["AgentNom"] = new SelectList(_context.Agent.Select(a => new
            {
                a.Id,
                NomComplet = a.Nom + " " + a.Prenom
            }), "Id", "NomComplet");

            ViewData["ConducteurNom"] = new SelectList(_context.Conducteur.Select(c => new
            {
                c.Id,
                NomComplet = c.Nom + " " + c.Prenom
            }), "Id", "NomComplet");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    ErrorMessage = "Certains champs sont invalides ou manquants.";
                    return Page();
                }

                _context.Attach(Contravention).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                SuccessMessage = "";
                return RedirectToPage("./Index");
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContraventionExists(Contravention.Id))
                {
                    ErrorMessage = "Cette contravention n'existe plus dans la base.";
                    return Page();
                }
                else
                {
                    throw;
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Une erreur s'est produite : {ex.Message}";
                return Page();
            }
        }

        private bool ContraventionExists(Guid id)
        {
            return _context.Contravention.Any(e => e.Id == id);
        }
    }
}
