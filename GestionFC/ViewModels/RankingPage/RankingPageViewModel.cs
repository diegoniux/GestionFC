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
        private int _posicionDireccion;

        public int PosicionDireccion
        {
            get { return _posicionDireccion; }
            set
            {
                _posicionDireccion = value;
                RaisePropertyChanged(nameof(PosicionDireccion));
            }
        }

        private string _imgPosicionSemAntDireccion;

        public string ImgPosicionSemAntDireccion
        {
            get { return _imgPosicionSemAntDireccion; }
            set
            {
                _imgPosicionSemAntDireccion = value;
                RaisePropertyChanged(nameof(ImgPosicionSemAntDireccion));
            }
        }

        private int _posicionNacional;

        public int PosicionNacional
        {
            get { return _posicionNacional; }
            set
            {
                _posicionNacional = value;
                RaisePropertyChanged(nameof(PosicionNacional));
            }
        }

        private string _imgPosicionSemAntNacional;

        public string ImgPosicionSemAntNacional
        {
            get { return _imgPosicionSemAntNacional; }
            set
            {
                _imgPosicionSemAntNacional = value;
                RaisePropertyChanged(nameof(ImgPosicionSemAntNacional));
            }
        }

        public RankingViewModel()
        {
            this.TopGerentes = new List<RankingGte>();
            this.Gerentes = new List<RankingGte>();
        }

    }
}
