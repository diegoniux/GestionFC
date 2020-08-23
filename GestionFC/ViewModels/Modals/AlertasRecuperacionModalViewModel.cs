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

        private string _nombreAP;
        public string NombreAP
        {
            get
            {
                return _nombreAP;
            }
            set
            {
                _nombreAP = value;
                RaisePropertyChanged(nameof(NombreAP));
            }
        }

        private string _apellidosAP;
        public string ApellidosAP
        {
            get
            {
                return _apellidosAP;
            }
            set
            {
                _apellidosAP = value;
                RaisePropertyChanged(nameof(ApellidosAP));
            }
        }

        private string _fotoAP;
        public string FotoAP
        {
            get
            {
                return _fotoAP;
            }
            set
            {
                _fotoAP = value;
                RaisePropertyChanged(nameof(FotoAP));
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

        private string _tituloPantallaDoc;
        public string TituloPantallaDoc
        {
            get
            {
                return _tituloPantallaDoc;
            }
            set
            {
                _tituloPantallaDoc = value;
                RaisePropertyChanged(nameof(TituloPantallaDoc));
            }
        }

        private List<Models.Share.AlertaRecuperacionPantallas> _pantallas;
        public List<Models.Share.AlertaRecuperacionPantallas> Pantallas
        {
            get
            {
                return _pantallas;
            }
            set
            {
                _pantallas = value;
                RaisePropertyChanged(nameof(Pantallas));
            }
        }

        private List<Models.Share.AlertaRecuperacionPreguntas> _preguntas;
        public List<Models.Share.AlertaRecuperacionPreguntas> Preguntas
        {
            get
            {
                return _preguntas;
            }
            set
            {
                _preguntas = value;
                RaisePropertyChanged(nameof(Preguntas));
            }

        }

        private List<Models.Share.AlertaRecuperacionDocumentos> _documentos;
        public List<Models.Share.AlertaRecuperacionDocumentos> Documentos
        {
            get
            {
                return _documentos;
            }
            set
            {
                _documentos = value;
                RaisePropertyChanged(nameof(Documentos));
            }
        }

        public AlertasRecuperacionModalViewModel()
        {
        }
    }
}
