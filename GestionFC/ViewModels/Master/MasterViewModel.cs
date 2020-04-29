using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestionFC.ViewModels.Master
{
    public class MasterViewModel : INotifyPropertyChanged
    {
        private string _nombre { get; set; }
        private string _puesto { get; set; }
        private string _foto { get; set; } 
        public event PropertyChangedEventHandler PropertyChanged;

        public string NombreGerenteMaster
        {
            get { return _nombre; }
            set {
                if (_nombre != value)
                {
                    _nombre = value;
                    OnPropertyChanged("NombreGerenteMaster");
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
                    OnPropertyChanged("Puesto");
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
                    OnPropertyChanged("Foto");
                }
            }
        }

        public MasterViewModel()
        {
        }

        public virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
