using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using GestCirculation.Models;

namespace GestCirculation.Pages_Contraventions
{
    public class DeleteModel : PageModel
    {
        private readonly GestCirculation.Models.GestCirculationContext _context;

        public DeleteModel(GestCirculation.Models.GestCirculationContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Contravention Contravention { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contravention = await _context.Contravention.FirstOrDefaultAsync(m => m.Id == id);

            if (contravention == null)
            {
                return NotFound();
            }
            else
            {
                Contravention = contravention;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var contravention = await _context.Contravention.FindAsync(id);
            if (contravention != null)
            {
                Contravention = contravention;
                _context.Contravention.Remove(Contravention);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
