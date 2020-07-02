using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestionFC.ViewModels.Master
{
    public class MasterViewModel : Share.ViewModelBase
    {
        private string _nombre { get; set; }
        private string _puesto { get; set; }
        private string _foto { get; set; } 

        public string NombreGerenteMaster
        {
            get { return _nombre; }
            set {
                    _nombre = value;
                    RaisePropertyChanged(NombreGerenteMaster);
            }
        }

        public string Puesto
        {
            get { return _puesto; }
            set {
                    _puesto = value;
                    RaisePropertyChanged(Puesto);
            }
        }

        public string Foto
        {
            get { return _foto; }
            set {
                    _foto = value;
                    RaisePropertyChanged(Foto);
            }
        }

        public MasterViewModel()
        {
        }
    }
}
