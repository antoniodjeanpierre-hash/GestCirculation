using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestCirculation.Models;

namespace GestCirculation.Pages_Contraventions
{
    public class CreateModel : PageModel
    {
        private readonly GestCirculationContext _context;

        public CreateModel(GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contravention Contravention { get; set; } = default!;

        public IActionResult OnGet()
        {
            // Charger les agents avec Nom + Prénom
            ViewData["AgentNom"] = new SelectList(_context.Agent.Select(a => new
            {
                a.Id,
                NomComplet = a.Nom + " " + a.Prenom
            }), "Id", "NomComplet");

            // Charger les conducteurs avec Nom + Prénom
            ViewData["ConducteurNom"] = new SelectList(_context.Conducteur.Select(c => new
            {
                c.Id,
                NomComplet = c.Nom + " " + c.Prenom
            }), "Id", "NomComplet");

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                // Recharge les listes déroulantes en cas d'erreur de validation
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

            _context.Contravention.Add(Contravention);
            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Contravention enregistrée avec succès !";

            return RedirectToPage("./Index");
        }
    }
}
