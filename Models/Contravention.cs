using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GestCirculation.Models
{
    public class Contravention
    {
        public Guid Id { get; set; }
        public string Plaque_vehicule { get; set; }
        public string Couleur { get; set; }
        public string Marque { get; set; }
        public string Adresse { get; set; }
        public string Article_violation { get; set; }
        public DateTime Date_contravention { get; set; }
        
        public Conducteur Conducteur { get; set; }
        public Guid ConducteurId { get; set; }
        public Agent Agent { get; set; }
        public Guid AgentId { get; set; }


    }
}