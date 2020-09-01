using System;
using System.Collections.Generic;
using System.IO;
using GestionFC.ViewModels.Share;
using Xamarin.Forms;

namespace GestionFC.Models.Share
{
    public class AlertaRecuperacionDocumentos: ViewModelBase
    {
        private int pantalla;
        public int Pantalla
        {
            get
            {
                return pantalla;
            }
            set
            {
                pantalla = value;
                RaisePropertyChanged(nameof(Pantalla));
            }
        }

        private int expedienteTipoId;
        public int ExpedienteTipoId
        {
            get
            {
                return expedienteTipoId;
            }
            set
            {
                expedienteTipoId = value;
                RaisePropertyChanged(nameof(ExpedienteTipoId));
            }
        }

        private string expedienteDesc;
        public string ExpedienteDesc
    {
            get
            {
                return expedienteDesc;
            }
            set
            {
                expedienteDesc = value;
                RaisePropertyChanged(nameof(ExpedienteDesc));
            }
        }

        private int documentoTipoId;
        public int DocumentoTipoId
        {
            get
            {
                return documentoTipoId;
            }
            set
            {
                documentoTipoId = value;
                RaisePropertyChanged(nameof(DocumentoTipoId));
            }
        }

        private string documentoDesc;
        public string DocumentoDesc
    {
            get
            {
                return documentoDesc;
            }
            set
            {
                documentoDesc = value;
                RaisePropertyChanged(nameof(DocumentoDesc));
            }
        }

        private string claveDocumento;
        public string ClaveDocumento
        {
            get
            {
                return claveDocumento;
            }
            set
            {
                claveDocumento = value;
                RaisePropertyChanged(nameof(ClaveDocumento));
            }
        }

        private int consecutivo;
        public int Consecutivo
        {
            get
            {
                return consecutivo;
            }
            set
            {
                consecutivo = value;
                RaisePropertyChanged(nameof(Consecutivo));
            }
        }

        private string mascara;
        public string Mascara
        {
            get
            {
                return mascara;
            }
            set
            {
                mascara = value;
                RaisePropertyChanged(nameof(Mascara));
            }
        }

        private StreamImageSource streamImageSrc;
        public StreamImageSource StreamImageSrc
        {
            get
            {
                return streamImageSrc;
            }
            set
            {
                streamImageSrc = value;
                RaisePropertyChanged(nameof(StreamImageSrc));
            }
        }

        private bool isPdf;
        public bool IsPdf
        {
            get
            {
                return isPdf;
            }
            set
            {
                isPdf = value;
                RaisePropertyChanged(nameof(IsPdf));
            }
        }

        private bool isImage;
        public bool IsImage
        {
            get
            {
                return isImage;
            }
            set
            {
                isImage = value;
                RaisePropertyChanged(nameof(IsImage));
            }
        }

        private bool isVideo;
        public bool IsVideo
        {
            get
            {
                return isVideo;
            }
            set
            {
                isVideo = value;
                RaisePropertyChanged(nameof(IsVideo));
            }
        }

        public bool isData;
        public bool IsData
        {
            get
            {
                return isData;
            }
            set
            {
                isData = value;
                RaisePropertyChanged(nameof(IsData));
            }
        }

        private bool esPrincipal;
        public bool EsPrincipal
        {
            get
            {
                return esPrincipal;
            }
            set
            {
                esPrincipal = value;
                RaisePropertyChanged(nameof(EsPrincipal));
            }
        }

        private string scriptVideo;
        public string ScriptVideo
        {
            get
            {
                return scriptVideo;
            }
            set
            {
                scriptVideo = value;
                RaisePropertyChanged(nameof(ScriptVideo));
            }
        }

        private List<DatoCapturado> datosCapturados;
        public List<DatoCapturado> DatosCapturados
        {
            get
            {
                return datosCapturados;
            }
            set
            {
                datosCapturados = value;
                RaisePropertyChanged(nameof(DatosCapturados));
            }
        }


        public AlertaRecuperacionDocumentos()
        {
        }
    }
}
