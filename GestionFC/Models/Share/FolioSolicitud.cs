using GestionFC.ViewModels.Share;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace GestionFC.Models.Share
{
    public class FolioSolicitud: ViewModelBase
    {
        public string folio;
        public string Folio
        {
            get
            {
                return folio;
            }
            set
            {
                Folio = value;
                RaisePropertyChanged(nameof(Folio));
            }
        }

        public string registroTraspasoId;
        public string RegistroTraspasoId
        {
            get
            {
                return registroTraspasoId;
            }
            set
            {
                RegistroTraspasoId = value;
                RaisePropertyChanged(nameof(RegistroTraspasoId));
            }
        }

        private List<MotivoRechazoModel> motivos;
        public List<MotivoRechazoModel> Motivos
        {
            get
            {
                return motivos;
            }
            set
            {
                motivos = value;
                RaisePropertyChanged(nameof(Motivos));
            }
        }

        private string backGroundColor;
        public string BackGroundColor
        {
            get
            {
                return backGroundColor;
            }
            set
            {
                backGroundColor = value;
                RaisePropertyChanged(nameof(BackGroundColor));
            }
        }

    }
}
