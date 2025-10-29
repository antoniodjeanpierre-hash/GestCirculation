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
    public class IndexModel : PageModel
    {
        private readonly GestCirculation.Models.GestCirculationContext _context;

        public IndexModel(GestCirculation.Models.GestCirculationContext context)
        {
            _context = context;
        }

        public IList<Contravention> Contravention { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Contravention = await _context.Contravention
                .Include(c => c.Agent)
                .Include(c => c.Conducteur).ToListAsync();
        }
        
    }
}
