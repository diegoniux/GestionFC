using System;
using System.Collections.Generic;
using System.Text;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;
using GestionFC.Models.Alertas;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using System.Linq;
using System.Collections.ObjectModel;

namespace GestionFC.ViewModels.AlertasPage
{
    public class PlantillaImproductivaViewModel : ViewModelBase, INotifyPropertyChanged
    {
        readonly IList<FoliosPendientesSVModel> source;

        readonly IList<FoliosPendientesSVModel> sourcePicker;
        
        readonly IList<PlantillaImproductivaModel> sourceImproductivos;



        public ICommand orderNombreCommand => new Command<FoliosPendientesSVModel>(orderNombre);
        public ICommand orderFolioCommand => new Command<FoliosPendientesSVModel>(orderFolio);
        public ICommand orderSVCommand => new Command<FoliosPendientesSVModel>(orderSV);
        public ICommand orderTipoSolicitudCommand => new Command<FoliosPendientesSVModel>(orderTipoSolicitud);
        public ICommand orderFechaFirmaCommand => new Command<FoliosPendientesSVModel>(orderFechaFirma);
        public ICommand FilterCommand => new Command<string>(FilterItemsFolio);
        //public ICommand FilterCommandNombre => new Command<string>(FilterItemsNombre);

        public bool ascdesc = true;
        public bool ejec = true;

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

        private FoliosPendientesSVResponseModel foliosPendientesSV;

        public FoliosPendientesSVResponseModel FoliosPendientesSV
        {
            get { return foliosPendientesSV; }
            set
            {
                foliosPendientesSV = value;
                RaisePropertyChanged(nameof(FoliosPendientesSV));
            }
        }

        private FoliosPendientesSVResponseModel nombresPicker;
        public FoliosPendientesSVResponseModel NombresPicker
        {
            get { return nombresPicker; }
            set
            {
                nombresPicker = value;
                RaisePropertyChanged(nameof(NombresPicker));
            }
        }

        public PlantillaImproductivaViewModel()
        {
            PlantillaImproductiva = new PlantillaImproductivaResponseModel();
            source = new List<FoliosPendientesSVModel>();
            sourcePicker = new List<FoliosPendientesSVModel>();
            sourceImproductivos = new List<PlantillaImproductivaModel>();
            CreateCollection();
        }

        void CreateCollection()
        {
            source.Add(new FoliosPendientesSVModel
            {
                Nombre = "Angel Grimaldo Arreguin",
                Folio = "9999999999",
                SaldoVirtual = 300000.ToString("C0"),
                TipoSolicitud = "Traspaso FCT",
                FechaFirma = "22/06/20",
                TieneSV = true
            });
            source.Add(new FoliosPendientesSVModel
            {
                Nombre = "Camilo",
                Folio = "9999999998",
                SaldoVirtual = 400000.ToString("C0"),
                TipoSolicitud = "Traspaso FCT",
                FechaFirma = "25/06/20",
                TieneSV = true
            });
            source.Add(new FoliosPendientesSVModel
            {
                Nombre = "Bamilo Angel Arreguin",
                Folio = "9999999999",
                SaldoVirtual = 150000.ToString("C0"),
                TipoSolicitud = "Registro",
                FechaFirma = "24/06/20",
                TieneSV = true
            });
            source.Add(new FoliosPendientesSVModel
            {
                Nombre = "Damilo Angel",
                Folio = "9999999997",
                SaldoVirtual = 109000.ToString("C0"),
                TipoSolicitud = "Traspasop",
                FechaFirma = "26/06/20",
                TieneSV = true
            });

            FoliosPendientesSV = new FoliosPendientesSVResponseModel();
            FoliosPendientesSV.ResultDatos = new ObservableCollection<FoliosPendientesSVModel>(source);

            sourcePicker.Add(new FoliosPendientesSVModel
            {
                Nombre = "Angel Grimaldo Arreguin",
                Folio = "9999999999",
                SaldoVirtual = 300000.ToString("C0"),
                TipoSolicitud = "Traspaso FCT",
                FechaFirma = "22/06/20",
                TieneSV = true
            });
            sourcePicker.Add(new FoliosPendientesSVModel
            {
                Nombre = "Camilo",
                Folio = "9999999998",
                SaldoVirtual = 400000.ToString("C0"),
                TipoSolicitud = "Traspaso FCT",
                FechaFirma = "25/06/20",
                TieneSV = true
            });
            sourcePicker.Add(new FoliosPendientesSVModel
            {
                Nombre = "Bamilo Angel Arreguin",
                Folio = "9999999999",
                SaldoVirtual = 150000.ToString("C0"),
                TipoSolicitud = "Registro",
                FechaFirma = "24/06/20",
                TieneSV = true
            });
            sourcePicker.Add(new FoliosPendientesSVModel
            {
                Nombre = "Damilo Angel",
                Folio = "9999999997",
                SaldoVirtual = 109000.ToString("C0"),
                TipoSolicitud = "Traspasop",
                FechaFirma = "26/06/20",
                TieneSV = true
            });

            NombresPicker = new FoliosPendientesSVResponseModel();
            nombresPicker.ResultDatos = new ObservableCollection<FoliosPendientesSVModel>(sourcePicker);

        }

        private void orderNombre(FoliosPendientesSVModel item)
        {
            var sortFolio = source.OrderBy(c => c.Nombre); ;
            if (ascdesc)
            {
                ascdesc = false;
            }
            else
            {
                sortFolio = source.OrderByDescending(c => c.Nombre);
                ascdesc = true;
            }

            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Remove(c);
            }
            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Add(c);
            }
        }

        private void orderFolio(FoliosPendientesSVModel item)
        {
            var sortFolio = source.OrderBy(c => c.Folio); ;
            if (ascdesc)
            {
                ascdesc = false;
            }
            else
            {
                sortFolio = source.OrderByDescending(c => c.Folio);
                ascdesc = true;
            }

            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Remove(c);
            }
            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Add(c);
            }
        }

        private void orderSV(FoliosPendientesSVModel item)
        {
            var sortFolio = source.OrderBy(c => c.SaldoVirtual); ;
            if (ascdesc)
            {
                ascdesc = false;
            }
            else
            {
                sortFolio = source.OrderByDescending(c => c.SaldoVirtual);
                ascdesc = true;
            }

            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Remove(c);
            }
            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Add(c);
            }
        }

        private void orderTipoSolicitud(FoliosPendientesSVModel item)
        {
            var sortFolio = source.OrderBy(c => c.TipoSolicitud); ;
            if (ascdesc)
            {
                ascdesc = false;
            }
            else
            {
                sortFolio = source.OrderByDescending(c => c.TipoSolicitud);
                ascdesc = true;
            }

            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Remove(c);
            }
            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Add(c);
            }
        }

        private void orderFechaFirma(FoliosPendientesSVModel item)
        {
            var sortFolio = source.OrderBy(c => c.FechaFirma); ;
            if (ascdesc)
            {
                ascdesc = false;
            }
            else
            {
                sortFolio = source.OrderByDescending(c => c.FechaFirma);
                ascdesc = true;
            }

            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Remove(c);
            }
            foreach (var c in sortFolio)
            {
                FoliosPendientesSV.ResultDatos.Add(c);
            }
        }

        void FilterItemsFolio(string filter)
        {
            var filteredItems = source.Where(folios => folios.Folio.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var folios in source)
            {
                if (!filteredItems.Contains(folios))
                {
                    FoliosPendientesSV.ResultDatos.Remove(folios);
                }
                else
                {
                    if (!FoliosPendientesSV.ResultDatos.Contains(folios))
                    {
                        FoliosPendientesSV.ResultDatos.Add(folios);
                    }
                }
            }
        }

         public void FilterItemsNombre(string filter)
        {
            if (ejec)
            {
                ejec = false;
                var filteredItems = source.Where(nombre => nombre.Nombre.ToLower().Contains(filter.ToLower())).ToList();
                foreach (var nombre in source)
                {
                    if (!filteredItems.Contains(nombre))
                    {
                        FoliosPendientesSV.ResultDatos.Remove(nombre);
                    }
                    else
                    {
                        if (!FoliosPendientesSV.ResultDatos.Contains(nombre))
                        {
                            FoliosPendientesSV.ResultDatos.Add(nombre);
                        }
                    }
                }
            }
            ejec = true;
            
        }

    }
}
