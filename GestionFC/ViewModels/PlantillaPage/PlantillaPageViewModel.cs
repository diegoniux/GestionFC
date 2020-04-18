using GestionFC.Models.Plantilla;
using System;
using System.Collections.Generic;

namespace GestionFC.ViewModels.PlantillaPage
{
    public class PlantillaPageViewModel
    {
        public string NombreGerente { get; set; }
        public string Mensaje { get; set; }
        public int Plantilla { get; set; }
        public int APsMetaAlcanzada { get; set; }
        public Progreso Gerente { get; set; }
        public List<Progreso> Agentes { get; set; }
        public PlantillaPageViewModel()
        {

        }
    }
}
