using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projeto_transporte_pilha
{
    class SistemaTransporte
    {
        //Propriedades
        private bool _jornadaIniciada;
        private List<Aeroporto> _aeroportos;
        private List<Van> _vans;
        private List<Viagem> _viagens;

        //Construtor
        public SistemaTransporte()
        {
            JornadaIniciada = false;
            _aeroportos = new List<Aeroporto>();
            _vans = new List<Van>();
            _viagens = new List<Viagem>();
        }

        //Getters e Setters
        public bool JornadaIniciada
        {
            get { return _jornadaIniciada; }
            set { _jornadaIniciada = value; }
        }

        public List<Aeroporto> Aeroportos
        {
            get { return _aeroportos; }
        }

        public List<Van> Vans
        {
            get { return _vans; }
        }

        public List<Viagem> Viagens
        {
            get { return _viagens; }
        }

        //Metodos Publicos
        public void CadastrarVeiculo(int id, int capacidade)
        {
            if (JornadaIniciada)
            {
                throw new Exception("Não é possível cadastrar vans após iniciar a jornada");
            }

            if(capacidade <= 0)
            {
                throw new Exception("Capacidade deve ser maior que zero");
            }

            foreach(Van v in Vans)
            {
                if(v.Id == id)
                {
                    throw new Exception("Já existe uma van com esse ID");
                }
            }

            Van cadastro = new Van(id, capacidade);

            Vans.Add(cadastro);
        }

        public void CadastrarGaragem(Aeroporto aeroporto)
        {
            if (JornadaIniciada)
            {
                throw new Exception("Não é possível cadastrar garagens após iniciar a jornada");
            }

            if(aeroporto == null)
            {
                throw new Exception("Aeroporto inválido");
            }

            foreach(Aeroporto a in Aeroportos)
            {
                if(a.Nome == aeroporto.Nome)
                {
                    throw new Exception("Este aeroporto já está cadastrado");
                }
            }

            Garagem g = new Garagem(aeroporto);
            aeroporto.GaragemLocal = g;

            Aeroportos.Add(aeroporto);
        }

        public void IniciarJornada()
        {
            if (JornadaIniciada)
            {
                throw new Exception("A jornada já foi iniciada");
            }

            if (Aeroportos.Count == 0)
            {
                throw new Exception("Cadastre ao menos um aeroporto");
            }

            if (Vans.Count == 0)
            {
                throw new Exception("Cadastre ao menos uma Van");
            }

            foreach(Aeroporto a in Aeroportos)
            {
                if(a.GaragemLocal == null)
                {
                    throw new Exception($"O aeroporto {a.Nome} não possui garagem cadastrada.");
                }
            }

            int m = Aeroportos.Count;

            for (int i = 0; i < Vans.Count; i++)
            {
                int aeroportIndex = i % m;  // alterna entre 0..m-1
                Aeroportos[aeroportIndex].GaragemLocal.Estacionar(Vans[i]);
            }

            JornadaIniciada = true;
        }

        public void EncerrarJornada()
        {
            if (!JornadaIniciada)
            {
                throw new Exception("A jornada não está iniciada.");
            }

            Console.WriteLine("----- RELATÓRIO DE ENCERRAMENTO -----");

            foreach (Van v in Vans)
            {
                Console.WriteLine($"Van {v.Id} -> Passageiros transportados: {v.PassageirosTransportados} | Viagens: {v.TotalViagens}");
            }

            Console.WriteLine("------------------------------------------------");

            Viagens.Clear();

            foreach (Van v in Vans)
            {
                v.LimparEstatisticas();
            }

            JornadaIniciada = false;
        }

        public void LiberarViagem(Aeroporto origem, Aeroporto destino, int passageiros)
        {
            if (!JornadaIniciada)
            {
                throw new Exception("A jornada ainda não foi iniciada.");
            }

            if (origem == null || destino == null)
            {
                throw new Exception("Origem ou destino inválidos.");
            }

            if (origem == destino)
            {
                throw new Exception("Origem e destino devem ser diferentes.");
            }

            if (passageiros <= 0)
            {
                throw new Exception("Número de passageiros inválido.");
            }

            if (!Aeroportos.Contains(origem))
            {
                throw new Exception("Aeroporto de origem não cadastrado no sistema.");
            }

            if (!Aeroportos.Contains(destino))
            {
                throw new Exception("Aeroporto de destino não cadastrado no sistema.");
            }

            Garagem garOrigem = origem.GaragemLocal;

            if (garOrigem == null)
            {
                throw new Exception("Garagem da origem não existe.");
            }

            if (garOrigem.QuantidadeVans() == 0)
            {
                throw new Exception("Não há vans na garagem da origem.");
            }

            Van vanTopo = garOrigem.Estacionamento.Peek();

            if (passageiros != vanTopo.Capacidade)
            {
                throw new Exception($"A van só pode sair com lotação completa ({vanTopo.Capacidade} passageiros).");
            }

            Van vanLiberada = garOrigem.Retirar();

            vanLiberada.RegistrarViagem(passageiros);

            Viagem viagem = new Viagem(origem, destino, vanLiberada, passageiros);
            Viagens.Add(viagem);

            if (destino.GaragemLocal == null)
            {
                destino.GaragemLocal = new Garagem(destino);
            }

            destino.GaragemLocal.Estacionar(vanLiberada);
        }

        public void ListarVans(Aeroporto aeroporto)
        {
            if (aeroporto == null)
            {
                Console.WriteLine("Aeroporto inválido.");
                return;
            }

            if (!Aeroportos.Contains(aeroporto))
            {
                Console.WriteLine("Aeroporto não cadastrado no sistema.");
                return;
            }

            Garagem g = aeroporto.GaragemLocal;

            if (g == null)
            {
                Console.WriteLine("Garagem não cadastrada para este aeroporto.");
                return;
            }

            int qtd = g.QuantidadeVans();
            int capacidadeTotal = g.CapacidadeTotal();

            Console.WriteLine($"Aeroporto: {aeroporto.Nome}");
            Console.WriteLine($"Quantidade de veículos: {qtd}");
            Console.WriteLine($"Potencial de transporte (soma de capacidades): {capacidadeTotal}");
            Console.WriteLine("Lista de veículos (do topo para o fundo):");

            foreach (Van v in g.Estacionamento)
            {
                Console.WriteLine($" - Van {v.Id} | Capacidade: {v.Capacidade}");
            }
        }

        public int ContarViagens(Aeroporto origem, Aeroporto destino)
        {
            if (origem == null || destino == null)
            {
                throw new Exception("Origem ou destino inválidos.");
            }

            int contador = 0;

            foreach (Viagem v in Viagens)
            {
                if (v.Origem == origem && v.Destino == destino)
                {
                    contador++;
                }
            }

            return contador;
        }

        public void ListarViagens(Aeroporto origem, Aeroporto destino)
        {
            if (origem == null || destino == null)
            {
                Console.WriteLine("Origem ou destino inválidos.");
                return;
            }

            Console.WriteLine($"Viagens de {origem.Nome} para {destino.Nome}:");

            bool achou = false;

            foreach (Viagem v in Viagens)
            {
                if (v.Origem == origem && v.Destino == destino)
                {
                    Console.WriteLine($" - Van {v.Veiculo.Id} | Passageiros: {v.PassageirosTransportados}");
                    achou = true;
                }
            }

            if (!achou)
            {
                Console.WriteLine("Nenhuma viagem registrada para essa rota.");
            }
        }

        public int SomarPassageiros(Aeroporto origem, Aeroporto destino)
        {
            if (origem == null || destino == null)
            {
                throw new Exception("Origem ou destino inválidos.");
            }

            int soma = 0;

            foreach (Viagem v in Viagens)
            {
                if (v.Origem == origem && v.Destino == destino)
                {
                    soma += v.PassageirosTransportados;
                }
            }

            return soma;
        }
    }
}