using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace GestionFC.ViewModels.Share
{
    public class ViewModelBase: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handle = PropertyChanged;
            if (handle != null)
                handle(this, new PropertyChangedEventArgs(propertyName));
        }

        
    }
}
