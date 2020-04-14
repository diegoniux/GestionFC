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
        public Models.PlantillaPage.Progreso Gerente { get; set; }
        public List<Models.PlantillaPage.Progreso> Agentes { get; set; }
        public PlantillaPageViewModel()
        {

        }
    }
}
