using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace GestionFC.ViewModels.Master
{
    public class MasterViewModel : INotifyPropertyChanged
    {
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
        private string _nombre { get; set; }
        public string Puesto { get; set; }

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
