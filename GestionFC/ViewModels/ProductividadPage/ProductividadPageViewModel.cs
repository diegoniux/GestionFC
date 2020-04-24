using GestionFC.Models.Productividad;
using GestionFC.Models.Share;

namespace GestionFC.ViewModels.ProductividadPage
{
    public class ProductividadPageViewModel
    {
        public string NombreGerente { get; set; }
        public string Mensaje { get; set; }
        public int Plantilla { get; set; }
        public int APsMetaAlcanzada { get; set; }
        public Progreso Gerente { get; set; }
        public ProductividadDiariaResponseModel ProductividadDiaria { get; set; }
        public ProductividadSemanalResponseModel ProductividadSemanal { get; set; }
        public ComisionEstimadaResponseModel ComisionEstimada { get; set; }
    }
}
