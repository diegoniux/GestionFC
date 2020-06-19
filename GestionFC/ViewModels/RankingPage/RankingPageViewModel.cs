using System;
using System.Collections.Generic;
using GestionFC.Models.Ranking;
using GestionFC.Models.Share;
using GestionFC.ViewModels.Share;

namespace GestionFC.ViewModels.RankingPage
{
    public class RankingViewModel: ViewModelBase
    {
        private string _nombreGerente;
        public string NombreGerente
        {
            get { return _nombreGerente; }
            set { _nombreGerente = value;
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

        private List<RankingGte> _topGerentes;

        public List<RankingGte> TopGerentes {
            get { return _topGerentes; }
            set {
                _topGerentes = value;
                RaisePropertyChanged(nameof(TopGerentes));
            }
        }

        private List<RankingGte> _gerentes;

        public List<RankingGte> Gerentes
        {
            get { return _gerentes; }
            set
            {
                _gerentes = value;
                RaisePropertyChanged(nameof(Gerentes));
            }
        }

        public RankingViewModel()
        {
            this.TopGerentes = new List<RankingGte>();
            this.Gerentes = new List<RankingGte>();
        }
    }
}
