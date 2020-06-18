using System;
using System.Collections.Generic;
using System.Text;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;
using GestionFC.Models.Alertas;

namespace GestionFC.ViewModels.AlertasPage
{
    public class PlantillaImproductivaViewModel : ViewModelBase
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

        private PlantillaImproductivaResponseModel plantillaImproductiva;

        public PlantillaImproductivaResponseModel PlantillaImproductiva
        {
            get { return plantillaImproductiva; }
            set
            {
                plantillaImproductiva = value;
                RaisePropertyChanged(nameof(PlantillaImproductiva));
            }
        }

        public PlantillaImproductivaViewModel()
        {
            PlantillaImproductiva = new PlantillaImproductivaResponseModel();
            CreateDetalleFolioCollection();
        }

        void CreateDetalleFolioCollection()
        {
            PlantillaImproductiva.ResultDatos = new List<PlantillaImproductivaModel>();
            PlantillaImproductiva.ResultDatos.Add(new PlantillaImproductivaModel
            {
                Foto = "",
                IdAlerta = 1,
                IdEstatusAlerta = 1,
                NominaAp = 11111,
                NombreAP = "Camilo Angel",
                ApellidosAP = "Grimaldo Arreguin",
                DiasSinFolios = 2,
                DiasRestantes = 1,
                Msj1 = "Actividad en riesgo",
                Msj2 = "",
                Msj3 = ""
            });
            PlantillaImproductiva.ResultDatos.Add(new PlantillaImproductivaModel
            {
                Foto = "",
                IdAlerta = 2,
                IdEstatusAlerta = 2,
                NominaAp = 22222,
                NombreAP = "Diex",
                ApellidosAP = "Nieto Nieto",
                DiasSinFolios = 0,
                DiasRestantes = 0,
                Msj1 = "Actividad Completada",
                Msj2 = "",
                Msj3 = ""
            });
            PlantillaImproductiva.ResultDatos.Add(new PlantillaImproductivaModel
            {
                Foto = "",
                IdAlerta = 3,
                IdEstatusAlerta = 3,
                NominaAp = 33333,
                NombreAP = "Ricardiño",
                ApellidosAP = "Sierra",
                DiasSinFolios = 2,
                DiasRestantes = 1,
                Msj1 = "Tienes",
                Msj2 = "2 días",
                Msj3 = "para convertir en exitoso a tu especialista."
            });
        }
    }
}
