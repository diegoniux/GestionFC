using System;
using System.Collections.Generic;
using System.ComponentModel;
using GestionFC.Models.Alertas;
using GestionFC.Models.DetalleEspecialista;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;
using SkiaSharp;

namespace GestionFC.ViewModels.DetalleEspecialista
{
    public class DetalleFolioPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private DetalleFoliosModel detalleFolios;

        public DetalleFoliosModel DetalleFolios
        {
            get { return detalleFolios; }
            set
            {
                detalleFolios = value;
                RaisePropertyChanged(nameof(DetalleFolios));
            }
        }

        private List<DetalleEtapaModel> detalleEtapas;

        public List<DetalleEtapaModel> DetalleEtapas
        {
            get { return detalleEtapas; }
            set
            {
                detalleEtapas = value;
                RaisePropertyChanged(nameof(DetalleEtapas));
            }
        }



    }
}
