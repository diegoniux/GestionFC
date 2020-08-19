using System;
using System.Collections.Generic;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;

namespace GestionFC.ViewModels.Modals
{
    public class AlertasRecuperacionModalViewModel : ViewModelBase
    {
        private int _nominaAP;
        public int NominaAP
        {
            get
            {
                return _nominaAP;
            }
            set
            {
                _nominaAP = value;
                RaisePropertyChanged(nameof(NominaAP));
            }
        }

        private List<FolioSolicitud> _folios;
        public List<FolioSolicitud> Folios
        {
            get
            {
                return _folios;
            }
            set
            {
                _folios = value;
                RaisePropertyChanged(nameof(Folios));
            }
        }

        public AlertasRecuperacionModalViewModel()
        {
        }
    }
}
