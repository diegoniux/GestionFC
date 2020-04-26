using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;
using System;
using System.Collections.Generic;

namespace GestionFC.ViewModels.PlantillaPage
{
    public class PlantillaPageViewModel: ViewModelBase
    {
        private string nombreGerente;

        public string NombreGerente
        {
            get { return nombreGerente; }
            set 
            { 
                nombreGerente = value;
                RaisePropertyChanged(nameof(NombreGerente));
            }
        }

        private string mensaje;

        public string Mensaje
        {
            get { return mensaje; }
            set 
            {
                mensaje = value;
                RaisePropertyChanged(nameof(Mensaje));
            }
        }

        private int plantilla;

        public int Plantilla
        {
            get { return plantilla; }
            set 
            { 
                plantilla = value;
                RaisePropertyChanged(nameof(Plantilla));
            }
        }

        private int aPsMetaAlcanzada;

        public int APsMetaAlcanzada
        {
            get { return aPsMetaAlcanzada; }
            set
            {
                aPsMetaAlcanzada = value;
                RaisePropertyChanged(nameof(APsMetaAlcanzada));
            }
        }

        private Progreso gerente;

        public Progreso Gerente
        {
            get { return gerente; }
            set 
            {
                gerente = value;
                RaisePropertyChanged(nameof(Gerente));
            }
        }

        private List<Progreso> agentes;

        public List<Progreso> Agentes
        {
            get { return agentes; }
            set { agentes = value;
                RaisePropertyChanged(nameof(Agentes));
            }
        }

        public PlantillaPageViewModel()
        {

        }
    }
}
