using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_transporte_pilha
{
    class Van
    {
        //Propriedades
        private int _id;
        private int _capacidade;
        private int _passageirosTransportados;
        private int _totalViagens;

        //Construtor
        public Van(int id, int capacidade)
        {
            Id = id;
            Capacidade = capacidade;
        }

        //Getters e Setters
        public int Id
        {
            get { return _id; }
            set { _id = value; }
        }

        public int Capacidade
        {
            get { return _capacidade; }
            set { _capacidade = value; }
        }

        public int PassageirosTransportados
        {
            get { return _passageirosTransportados; }
            private set { _passageirosTransportados = value; }
        }

        public int TotalViagens
        {
            get { return _totalViagens; }
            private set { _totalViagens = value; }
        }

        //Metodos Publicos
        public void RegistrarViagem(int passageiros)
        {
            TotalViagens++;
            PassageirosTransportados += passageiros;
        }

        public void LimparEstatisticas()
        {
            _passageirosTransportados = 0;
            _totalViagens = 0;
        }

    }
}
