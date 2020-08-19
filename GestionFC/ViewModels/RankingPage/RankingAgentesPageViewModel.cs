using System;
using System.Collections.Generic;
using GestionFC.Models.Ranking;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;

namespace GestionFC.ViewModels.RankingPage
{
    public class RankingAgentesPageViewModel : ViewModelBase
    {
        private string _nombreGerente;
        public string NombreGerente
        {
            get { return _nombreGerente; }
            set
            {
                _nombreGerente = value;
                RaisePropertyChanged(nameof(NombreGerente));
            }
        }

        private string _mensaje;

        public string Mensaje
        {
            get { return _mensaje; }
            set
            {
                _mensaje = value;
                RaisePropertyChanged(nameof(Mensaje));
            }
        }

        private int _plantilla;

        public int Plantilla
        {
            get { return _plantilla; }
            set
            {
                _plantilla = value;
                RaisePropertyChanged(nameof(Plantilla));
            }
        }

        private int _aPsMetaAlcanzada;

        public int APsMetaAlcanzada
        {
            get { return _aPsMetaAlcanzada; }
            set
            {
                _aPsMetaAlcanzada = value;
                RaisePropertyChanged(nameof(APsMetaAlcanzada));
            }
        }

        private Progreso _gerente;

        public Progreso Gerente
        {
            get { return _gerente; }
            set
            {
                _gerente = value;
                RaisePropertyChanged(nameof(Gerente));
            }
        }

        private List<RankingEspecialista> _topEspecialistas;

        public List<RankingEspecialista> TopEspecialistas
        {
            get { return _topEspecialistas; }
            set
            {
                _topEspecialistas = value;
                RaisePropertyChanged(nameof(TopEspecialistas));
            }
        }

        private List<RankingEspecialista> _especialistas;

        public List<RankingEspecialista> Especialistas
        {
            get { return _especialistas; }
            set
            {
                _especialistas = value;
                RaisePropertyChanged(nameof(Especialistas));
            }
        }

        private string _dias;

        public string Dias
        {
            get { return _dias; }
            set
            {
                _dias = value;
                RaisePropertyChanged(nameof(Dias));
            }
        }

        private string _horas;

        public string Horas
        {
            get { return _horas; }
            set
            {
                _horas = value;
                RaisePropertyChanged(nameof(Horas));
            }
        }

        public RankingAgentesPageViewModel()
        {
            this.Especialistas = new List<RankingEspecialista>();
            this.TopEspecialistas = new List<RankingEspecialista>();
        }
    }
}
