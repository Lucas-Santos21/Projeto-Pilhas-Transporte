using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_transporte_pilha
{
    class Garagem
    {
        //Propriedades
        private Aeroporto _localizacao;
        private Stack<Van> _estacionamento;

        //Construtor
        public Garagem(Aeroporto localizacao)
        {
            Localizacao = localizacao;
            _estacionamento = new Stack<Van>();
        }

        //Getters e Setters
        public Aeroporto Localizacao
        {
            get { return _localizacao; }
            set { _localizacao = value; }
        }

        public Stack<Van> Estacionamento
        {
            get { return _estacionamento; }
        }

        //Metodos Publicos
        public void Estacionar(Van v)
        {
            Estacionamento.Push(v);
        }

        public Van Retirar()
        {
            return Estacionamento.Pop();
        }

        public int QuantidadeVans()
        {
            return Estacionamento.Count;
        }

        public int CapacidadeTotal()
        {
            int total = 0;

            foreach(Van camada in Estacionamento)
            {
                total += camada.Capacidade;
            }

            return total;
        }

    }
}
