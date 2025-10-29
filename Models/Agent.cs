using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestCirculation.Models
{
    public class Agent
    {
        public Guid Id { get; set; }
        public string Code_agent { get; set; }
        public string Affectation { get; set; }
        public string Nom { get; set; }
        public string Prenom { get; set; }


    }
}