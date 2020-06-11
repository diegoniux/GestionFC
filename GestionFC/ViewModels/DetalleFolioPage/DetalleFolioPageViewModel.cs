using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Input;
using GestionFC.Models.DetalleFolio;
using GestionFC.ViewModels.Share;
using Xamarin.Forms;

namespace GestionFC.ViewModels.DetalleFolioPage
{
    public class DetalleFolioPageViewModel : INotifyPropertyChanged
    {
        private IList<DetalleFolioModel> source;

        public ObservableCollection<DetalleFolioModel> detalleFolioModel { get; private set; }
        public IList<DetalleFolioModel> emptyDetalleFolioModel { get; private set; }
        
        public DetalleFolioModel PreviousDetalleFolioModel { get; set; }
        public DetalleFolioModel CurrentDetalleFolioModel { get; set; }
        public DetalleFolioModel CurrentItem { get; set; }
        public int PreviousPosition { get; set; }
        public int CurrentPosition { get; set; }
        public int Position { get; set; }

        public ICommand FilterCommand => new Command<string>(FilterItems);
        public ICommand ItemChangedCommand => new Command<DetalleFolioModel>(ItemChanged);
        public ICommand PositionChangedCommand => new Command<int>(PositionChanged);
        public ICommand DeleteCommand => new Command<DetalleFolioModel>(RemoveDetalleFolio);
        //public ICommand FavoriteCommand => new Command<DetalleFolioModel>(FavoriteMonkey);



        public DetalleFolioPageViewModel()
        {
            source = new List<DetalleFolioModel>();
            CreateDetalleFolioCollection();

            CurrentItem = detalleFolioModel.Skip(3).FirstOrDefault();
            OnPropertyChanged("CurrentItem");
            Position = 3;
            OnPropertyChanged("Position");
        }

        void CreateDetalleFolioCollection()
        {
            source.Add(new DetalleFolioModel
            {
                pantallaId = 1,
                NombrePantalla = "Documento1",
                preguntas = "pregunta 1?"+ Environment.NewLine + "pregunta 2?",
                foto = "foto1",
                pdf = "pdf1"
            });
            source.Add(new DetalleFolioModel
            {
                pantallaId = 2,
                NombrePantalla = "Documento2",
                preguntas = "pregunta 1?" + Environment.NewLine + "pregunta 2?",
                foto = "foto2",
                pdf = "pdf2"
            });
            source.Add(new DetalleFolioModel
            {
                pantallaId = 3,
                NombrePantalla = "Documento3",
                preguntas = "pregunta 1?" + Environment.NewLine + "pregunta 2?",
                foto = "foto3",
                pdf = "pdf3"
            });
            source.Add(new DetalleFolioModel
            {
                pantallaId = 4,
                NombrePantalla = "Documento4",
                preguntas = "pregunta 1?" + Environment.NewLine + "pregunta 2?",
                foto = "foto4",
                pdf = "pdf4"
            });
            source.Add(new DetalleFolioModel
            {
                pantallaId = 5,
                NombrePantalla = "Documento5",
                preguntas = "pregunta 1?" + Environment.NewLine + "pregunta 2?",
                foto = "foto5",
                pdf = "pdf5"
            });
            source.Add(new DetalleFolioModel
            {
                pantallaId = 6,
                NombrePantalla = "Documento6",
                preguntas = "pregunta 1?" + Environment.NewLine + "pregunta 2?",
                foto = "foto6",
                pdf = "pdf6"
            });

            detalleFolioModel = new ObservableCollection<DetalleFolioModel>(source);
        }

        void FilterItems(string filter)
        {
            var filteredItems = source.Where(detalleFolio => detalleFolio.NombrePantalla.ToLower().Contains(filter.ToLower())).ToList();
            foreach (var detalleFolio in source)
            {
                if (!filteredItems.Contains(detalleFolio))
                {
                    detalleFolioModel.Remove(detalleFolio);
                }
                else
                {
                    if (!detalleFolioModel.Contains(detalleFolio))
                    {
                        detalleFolioModel.Add(detalleFolio);
                    }
                }
            }
        }

        void ItemChanged(DetalleFolioModel item)
        {
            PreviousDetalleFolioModel = CurrentDetalleFolioModel;
            CurrentDetalleFolioModel = item;
            OnPropertyChanged("PreviousDetalleFolioModel");
            OnPropertyChanged("CurrentDetalleFolioModel");
        }

        void PositionChanged(int position)
        {
            PreviousPosition = CurrentPosition;
            CurrentPosition = position;
            OnPropertyChanged("PreviousPosition");
            OnPropertyChanged("CurrentPosition");
        }

        void RemoveDetalleFolio(DetalleFolioModel detalleFolio)
        {
            if (detalleFolioModel.Contains(detalleFolio))
            {
                detalleFolioModel.Remove(detalleFolio);
            }
        }

        //void FavoriteMonkey(DetalleFolioModel monkey)
        //{
        //    monkey.pantallaId = !monkey.pantallaId;
        //}

        #region INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
