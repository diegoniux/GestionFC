using System;
using GestionFC.ViewModels.Share;

namespace GestionFC.Models.Share
{
    public class AlertaRecuperacionPantallas: ViewModelBase
    {
        private int pantallaId;
        public int PantallaId
        {
            get
            {
                return pantallaId;
            }
            set
            {
                pantallaId = value;
                RaisePropertyChanged(nameof(PantallaId));
            }
        }

        private string pantallaDesc;
        public string PantallaDesc
        {
            get
            {
                return pantallaDesc;
            }
            set
            {
            pantallaDesc = value;
                RaisePropertyChanged(nameof(PantallaDesc));
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


        public AlertaRecuperacionPantallas()
        {
        }
    }
}
