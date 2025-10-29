using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestCirculation.Models;


namespace GestCirculation.Pages_Conducteurs
{
    public class EditModel : PageModel
    {
        private readonly GestCirculationContext _context;

        public EditModel(GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Conducteur Conducteur { get; set; } = default!;

        [TempData]
        public string SuccessMessage { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
                return NotFound();

            var conducteur = await _context.Conducteur.FirstOrDefaultAsync(m => m.Id == id);
            if (conducteur == null)
                return NotFound();

            Conducteur = conducteur;
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

                var existingConducteur = await _context.Conducteur
                    .FirstOrDefaultAsync(c => c.Nif == Conducteur.Nif && c.Id != Conducteur.Id);

                if (existingConducteur != null)
                {
                    ErrorMessage = "Ce NIF est déjà utilisé par un autre conducteur.";
                    return Page();
                }

                _context.Attach(Conducteur).State = EntityState.Modified;
                await _context.SaveChangesAsync();

                return RedirectToPage("./Index");
            }
            catch (Exception ex)
            {
                ErrorMessage = $"Une erreur s'est produite : {ex.Message}";
                return Page();
            }
        }

        private bool ConducteurExists(Guid id)
        {
            return _context.Conducteur.Any(e => e.Id == id);
        }
    }
}
