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
    public class IndexModel : PageModel
    {
        private readonly GestCirculation.Models.GestCirculationContext _context;

        public IndexModel(GestCirculation.Models.GestCirculationContext context)
        {
            _context = context;
        }

        public IList<Conducteur> Conducteur { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Conducteur = await _context.Conducteur.ToListAsync();
        }
    }
}
