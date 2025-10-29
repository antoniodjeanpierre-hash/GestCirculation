// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;
using GestCirculation.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;


namespace GestCirculation.Areas.Identity.Pages.Account
{
    public class RegisterAgentModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GestCirculationContext _context;

        public RegisterAgentModel(UserManager<IdentityUser> userManager, GestCirculationContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "Les mots de passe ne correspondent pas.")]
            public string ConfirmPassword { get; set; }

            [Required]
            public string CodeAgent { get; set; }

            [Required]
            public string Affectation { get; set; }

            [Required]
            public string Nom { get; set; }

            [Required]
            public string Prenom { get; set; }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = Input.Email, Email = Input.Email };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    // Enregistrement des détails de l'agent
                    var agent = new Agent
                    {
                        Code_agent = Input.CodeAgent,
                        Affectation = Input.Affectation,
                        Nom = Input.Nom,
                        Prenom = Input.Prenom
                    };

                    // Ajouter l'agent à la base de données
                    _context.Agent.Add(agent);
                    await _context.SaveChangesAsync();

                    return RedirectToPage("/Index"); // Redirige vers la page d'accueil
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}