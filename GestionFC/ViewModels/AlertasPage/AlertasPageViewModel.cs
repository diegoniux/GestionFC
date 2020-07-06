using GestionFC.Models.Alertas;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace GestionFC.ViewModels.AlertasPage
{
    public class PlantillaImproductivaViewModel : ViewModelBase, INotifyPropertyChanged
    {
        
        public readonly IList<FoliosPendientesSVModel> sourceOrder;

        public readonly List<FoliosPendientesSVModel> sourcePicker;

        public readonly IList<FoliosPendientesSVModel> Secundarysource;

        bool isBusy = true;


        public ICommand orderNombreCommand => new Command<FoliosPendientesSVModel>(orderNombre);
        public ICommand orderFolioCommand => new Command<FoliosPendientesSVModel>(orderFolio);
        public ICommand orderSVCommand => new Command<FoliosPendientesSVModel>(orderSV);
        public ICommand orderTipoSolicitudCommand => new Command<FoliosPendientesSVModel>(orderTipoSolicitud);
        public ICommand orderFechaFirmaCommand => new Command<FoliosPendientesSVModel>(orderFechaFirma);
        //public ICommand FilterCommand => new Command<string>(FilterItemsFolio);
        //public ICommand FilterCommandNombre => new Command<string>(FilterItemsNombre);

        public bool ascdesc = true;
        public bool ejec = true;

        private string _dia;

        public string Dia
        {
            get { return _dia; }
            set
            {
                _dia = value;
                RaisePropertyChanged(nameof(Dia));
            }
        }

        private string _fecha;

        public string Fecha
        {
            get { return _fecha; }
            set
            {
                _fecha = value;
                RaisePropertyChanged(nameof(Fecha));
            }
        }

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

        public PlantillaImproductivaViewModel()
        {
            PlantillaImproductiva = new PlantillaImproductivaResponseModel();
            FoliosPendientesSV = new FoliosPendientesSVResponseModel();
            sourceOrder = new List<FoliosPendientesSVModel>();
            sourcePicker = new List<FoliosPendientesSVModel>();
            Secundarysource = new List<FoliosPendientesSVModel>();


        }

        public void CreateCollection()
        {
            try
            {
                Secundarysource.Clear();
                sourcePicker.Clear();
                if (FoliosPendientesSV.ResultDatos != null)
                {
                    
                    foreach (var a in FoliosPendientesSV.ResultDatos)
                    {
                        Secundarysource.Add(a);
                        
                        if (!sourcePicker.Exists(x => x.Nombre == a.Nombre + " " + (a.Apellidos ?? "")))
                        {
                            sourcePicker.Add(new FoliosPendientesSVModel
                            {
                                Nombre = a.Nombre + " " + a.Apellidos
                            });
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }


        }

        private void orderNombre(FoliosPendientesSVModel item)
        {
            try
            {
                if (isBusy)
                {
                    isBusy = false;
                    foreach (var a in Secundarysource)
                    {
                        sourceOrder.Remove(a);
                    }
                    foreach (var a in FoliosPendientesSV.ResultDatos)
                    {
                        sourceOrder.Add(a);
                    }
                    var sortFolio = sourceOrder.OrderBy(c => c.Nombre);
                    if (ascdesc)
                    {
                        ascdesc = false;
                    }
                    else
                    {
                        sortFolio = sourceOrder.OrderByDescending(c => c.Nombre);
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
                    isBusy = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }


        }

        private void orderFolio(FoliosPendientesSVModel item)
        {
            try
            {
                if (isBusy)
                {
                    isBusy = false;
                    foreach (var a in Secundarysource)
                    {
                        sourceOrder.Remove(a);
                    }
                    foreach (var a in FoliosPendientesSV.ResultDatos)
                    {
                        sourceOrder.Add(a);
                    }
                    var sortFolio = sourceOrder.OrderBy(c => c.Folio);
                    if (ascdesc)
                    {
                        ascdesc = false;
                    }
                    else
                    {
                        sortFolio = sourceOrder.OrderByDescending(c => c.Folio);
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
                    isBusy = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private void orderSV(FoliosPendientesSVModel item)
        {
            try
            {
                if (isBusy)
                {
                    isBusy = false;
                    foreach (var a in Secundarysource)
                    {
                        sourceOrder.Remove(a);
                    }
                    foreach (var a in FoliosPendientesSV.ResultDatos)
                    {
                        sourceOrder.Add(a);
                    }
                    var sortFolio = sourceOrder.OrderBy(c => c.SaldoVirtual);
                    if (ascdesc)
                    {
                        ascdesc = false;
                    }
                    else
                    {
                        sortFolio = sourceOrder.OrderByDescending(c => c.SaldoVirtual);
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
                    isBusy = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private void orderTipoSolicitud(FoliosPendientesSVModel item)
        {
            try
            {
                if (isBusy)
                {
                    isBusy = false;
                    foreach (var a in Secundarysource)
                    {
                        sourceOrder.Remove(a);
                    }
                    foreach (var a in FoliosPendientesSV.ResultDatos)
                    {
                        sourceOrder.Add(a);
                    }
                    var sortFolio = sourceOrder.OrderBy(c => c.TipoSolicitud);
                    if (ascdesc)
                    {
                        ascdesc = false;
                    }
                    else
                    {
                        sortFolio = sourceOrder.OrderByDescending(c => c.TipoSolicitud);
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
                    isBusy = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        private void orderFechaFirma(FoliosPendientesSVModel item)
        {
            try
            {
                if (isBusy)
                {
                    isBusy = false;
                    foreach (var a in Secundarysource)
                    {
                        sourceOrder.Remove(a);
                    }
                    foreach (var a in FoliosPendientesSV.ResultDatos)
                    {
                        sourceOrder.Add(a);
                    }
                    var sortFolio = sourceOrder.OrderBy(c => c.FechaFirma);
                    if (ascdesc)
                    {
                        ascdesc = false;
                    }
                    else
                    {
                        sortFolio = sourceOrder.OrderByDescending(c => c.FechaFirma);
                        ascdesc = true;
                    }

                    foreach (var c in sortFolio)
                    {
                        FoliosPendientesSV.ResultDatos.Remove(c);
                    }
                    foreach (var c in sortFolio)
                    {
                        if (!FoliosPendientesSV.ResultDatos.Contains(c))
                        {
                            FoliosPendientesSV.ResultDatos.Add(c);
                        }
                    }
                    isBusy = true;
                }
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public void FilterItemsFolio(string filter)
        {
            try
            {
                var filteredItems = Secundarysource.Where(folios => folios.Folio.ToLower().Contains(filter.ToLower())).ToList();
                if (string.IsNullOrEmpty(filter))
                {
                    foreach (FoliosPendientesSVModel folios in Secundarysource)
                    {
                        FoliosPendientesSV.ResultDatos.Remove(folios);
                        FoliosPendientesSV.ResultDatos.Add(folios);
                    }
                }
                else
                {
                    foreach (FoliosPendientesSVModel folios in Secundarysource)
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
            }
            catch (Exception e)
            {
                Console.Write(e.Message);
            }
        }

        public void FilterItemsNombre(string filter)
        {
            try
            {
                if (ejec)
                {
                    ejec = false;
                    var filteredItems = Secundarysource.Where(nombre => nombre.Nombre.ToLower().Contains(filter.ToLower())).ToList();
                    if (filter == "TODOS")
                    {
                        foreach (var nombre in Secundarysource)
                        {
                            FoliosPendientesSV.ResultDatos.Remove(nombre);
                            FoliosPendientesSV.ResultDatos.Add(nombre);
                        }
                    }
                    else
                    {
                        foreach (var nombre in Secundarysource)
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
            catch (Exception e)
            {
                Console.Write(e.Message);
            }


        }


    }
}
