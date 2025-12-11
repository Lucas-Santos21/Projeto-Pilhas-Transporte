using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_transporte_pilha
{
    class Viagem
    {
        //Propriedades
        private Aeroporto _origem;
        private Aeroporto _destino;
        private Van _veiculo;
        private int _passageirosTransportados;

        //Construtor
        public Viagem(Aeroporto origem, Aeroporto destino, Van veiculo, int passageiros)
        {
            _origem = origem;
            _destino = destino;
            _veiculo = veiculo;
            PassageirosTransportados = passageiros;
        }

        //Getters e Setters
        public Aeroporto Origem
        {
            get { return _origem; }
        }

        public Aeroporto Destino
        {
            get { return _destino; }
        }

        public Van Veiculo
        {
            get { return _veiculo; }
        }

        public int PassageirosTransportados
        {
            get { return _passageirosTransportados; }
            set { _passageirosTransportados = value; }
        }

    }
}
