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

        private List<RankingAP> _agentesTop;

        public List<RankingAP> AgentesTop {
            get { return _agentesTop; }
            set { _agentesTop = value;
                RaisePropertyChanged(nameof(AgentesTop));
            }
        }

        private List<RankingAP> _agentes;

        public List<RankingAP> Agentes
        {
            get { return _agentes; }
            set
            {
                _agentes = value;
                RaisePropertyChanged(nameof(Agentes));
            }
        }

        public RankingViewModel()
        {
            this.AgentesTop = new List<RankingAP>();
            this.Agentes = new List<RankingAP>();
        }
    }
}
