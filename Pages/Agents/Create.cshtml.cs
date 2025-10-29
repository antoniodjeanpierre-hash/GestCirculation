using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using GestCirculation.Models;
#nullable disable

namespace GestCirculation.Pages_Agents
{
    public class CreateModel : PageModel
    {
        private readonly GestCirculationContext _context;

        public CreateModel(GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Agent Agent { get; set; } = default!;

        // TempData pour le pop-up
        [TempData]
        public string PopupMessage { get; set; }

        [TempData]
        public string PopupType { get; set; }  // "success", "error", "warning"

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


                _context.Agent.Add(Agent);
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
