using GestionFC.Models.Share;
using GestionFC.Models.VisionBoard;
using GestionFC.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.ViewModels.VisionBoard
{
    public class VisionBoardViewModel: ViewModelBase
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

        private GetMetaPlantillaResponseModel getMetaPlantilla;

        public GetMetaPlantillaResponseModel GetMetaPlantilla
        {
            get { return getMetaPlantilla; }
            set 
            {
                getMetaPlantilla = value;
                RaisePropertyChanged(nameof(GetMetaPlantilla));
            }
        }

        private GetMetaPlantillaIndividualResponseModel getMetaPlantillaIndividual;

        public GetMetaPlantillaIndividualResponseModel GetMetaPlantillaIndividual
        {
            get { return getMetaPlantillaIndividual; }
            set 
            {
                getMetaPlantillaIndividual = value;
                RaisePropertyChanged(nameof(GetMetaPlantillaIndividual));
            }
        }

        private MetaPlantillaResponseModel metaPlantilla;

        public MetaPlantillaResponseModel MetaPlantilla
        {
            get { return metaPlantilla; }
            set 
            { 
                metaPlantilla = value;
                RaisePropertyChanged(nameof(MetaPlantillaResponseModel));
            }
        }

        private MetaPlantillaIndividualResponseModel metaPlantillaIndividual;

        public MetaPlantillaIndividualResponseModel MetaPlantillaIndividual
        {
            get { return metaPlantillaIndividual; }
            set
            { 
                metaPlantillaIndividual = value;
                RaisePropertyChanged(nameof(MetaPlantillaIndividual));
            }
        }

        private MetaPlantillaFoliosResponseModel metaPlantillaFolios;

        public MetaPlantillaFoliosResponseModel MetaPlantillaFolios
        {
            get { return metaPlantillaFolios; }
            set 
            {
                metaPlantillaFolios = value;
                RaisePropertyChanged(nameof(MetaPlantillaFolios));
            }
        }








    }
}
