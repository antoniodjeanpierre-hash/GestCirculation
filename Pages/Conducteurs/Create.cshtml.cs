using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestCirculation.Models;
using Microsoft.EntityFrameworkCore;

namespace GestCirculation.Pages_Conducteurs
{
    public class CreateModel : PageModel
    {
        private readonly GestCirculation.Models.GestCirculationContext _context;

        public CreateModel(GestCirculation.Models.GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Conducteur Conducteur { get; set; } = default!;

        // TempData pour messages pop-up
        [TempData]
        public string PopupMessage { get; set; }

        [TempData]
        public string PopupType { get; set; } // success / error / warning

        public IActionResult OnGet()
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    PopupMessage = "Certains champs sont invalides ou manquants.";
                    PopupType = "warning";
                    return Page();
                }

                // Vérification NIF unique
                if (await _context.Conducteur.AnyAsync(c => c.Nif == Conducteur.Nif))
                {
                    PopupMessage = "Ce conducteur existe déjà, impossible d'en créer un autre.";
                    PopupType = "error";
                    return Page();
                }

                _context.Conducteur.Add(Conducteur);
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                PopupMessage = $"Une erreur s'est produite : {ex.Message}";
                PopupType = "error";
                return Page();
            }
        }
    }
}
