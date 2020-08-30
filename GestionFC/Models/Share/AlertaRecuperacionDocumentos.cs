using System;
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

        public int expedienteTipoId;
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

        public string expedienteDesc;
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

        public int documentoTipoId;
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

        public string documentoDesc;
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

        public string claveDocumento;
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

        public int consecutivo;
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

        public string mascara;
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

        public StreamImageSource streamImageSrc;
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

        public bool isPdf;
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

        public bool isImage;
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

        public bool isVideo;
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

        public AlertaRecuperacionDocumentos()
        {
        }
    }
}
