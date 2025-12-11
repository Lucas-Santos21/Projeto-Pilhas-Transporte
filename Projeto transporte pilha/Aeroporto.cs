using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_transporte_pilha
{
    class Aeroporto
    {
        //Propriedades
        private string _nome;
        private Garagem _garagemLocal;

        //Construtor
        public Aeroporto(string nome)
        {
            Nome = nome;
        }

        //Getters e Setters
        public string Nome
        {
            get { return _nome; }
            set { _nome = value; }
        }

        public Garagem GaragemLocal
        {
            get { return _garagemLocal; }
            set { _garagemLocal = value; }
        }

    }
}
