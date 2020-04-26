using GestionFC.Models.Productividad;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;

namespace GestionFC.ViewModels.ProductividadPage
{
    public class ProductividadPageViewModel: ViewModelBase
    {
        private string nombreGerente;

        public string NombreGerente
        {
            get { return nombreGerente; }
            set { nombreGerente = value;
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

        private ProductividadDiariaResponseModel productividadDiaria;

        public ProductividadDiariaResponseModel ProductividadDiaria
        {
            get { return productividadDiaria; }
            set {
                productividadDiaria = value;
                RaisePropertyChanged(nameof(ProductividadDiaria));
            }
        }

        private ProductividadSemanalResponseModel productividadSemanal;

        public ProductividadSemanalResponseModel ProductividadSemanal
        {
            get { return productividadSemanal; }
            set { productividadSemanal = value;
                RaisePropertyChanged(nameof(ProductividadSemanal));
            }
        }

        private ComisionEstimadaResponseModel comisionEstimada;

        public ComisionEstimadaResponseModel ComisionEstimada
        {
            get { return comisionEstimada; }
            set { comisionEstimada = value;
                RaisePropertyChanged(nameof(ComisionEstimada));
            }
        }
    }
}
