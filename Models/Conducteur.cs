using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestCirculation.Models
{
    public class Conducteur
    {
        public Guid Id { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }
        public string Sexe { get; set; }
        public string Nif { get; set; }
    }
}