using System;
using System.ComponentModel;
using GestionFC.Models.Alertas;
using GestionFC.Models.DetalleEspecialista;
using GestionFC.ViewModels.Share;
using SkiaSharp;

namespace GestionFC.ViewModels.DetalleEspecialista
{
    public class DetalleEspecialistaPageViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private DetalleEspecialistaResponseModel detalleEspecialista;

        public DetalleEspecialistaResponseModel DetalleEspecialista
        {
            get { return detalleEspecialista; }
            set
            {
                detalleEspecialista = value;
                RaisePropertyChanged(nameof(DetalleEspecialista));
            }
        }

        private PlantillaImproductivaModel alarmaImproductivo;

        public PlantillaImproductivaModel AlarmaImproductivo
        {
            get { return alarmaImproductivo; }
            set
            {
                alarmaImproductivo = value;
                RaisePropertyChanged(nameof(AlarmaImproductivo));
            }
        }

        private DetalleHistoricoResponseModel detalleHistorico;

        public DetalleHistoricoResponseModel DetalleHistorico
        {
            get { return detalleHistorico; }
            set
            {
                detalleHistorico = value;
                RaisePropertyChanged(nameof(DetalleHistorico));
            }
        }

        private bool alarmaVisible;

        public bool AlarmaVisible
        {
            get { return alarmaVisible; }
            set
            {
                alarmaVisible = value;
                RaisePropertyChanged(nameof(AlarmaVisible));
            }
        }

        private bool likeVisible;

        public bool LikeVisible
        {
            get { return likeVisible; }
            set
            {
                likeVisible = value;
                RaisePropertyChanged(nameof(LikeVisible));
            }
        }



    }
}
