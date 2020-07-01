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
                if (_nombre != value)
                {
                    _nombre = value;
                    RaisePropertyChanged(NombreGerenteMaster);
                }
            }
        }

        public string Puesto
        {
            get { return _puesto; }
            set {
                if (_puesto != value)
                {
                    _puesto = value;
                    RaisePropertyChanged(Puesto);
                }
            }
        }

        public string Foto
        {
            get { return _foto; }
            set {
                if (_foto != value)
                {
                    _foto = value;
                    RaisePropertyChanged(Foto);
                }
            }
        }

        public MasterViewModel()
        {
        }
    }
}
