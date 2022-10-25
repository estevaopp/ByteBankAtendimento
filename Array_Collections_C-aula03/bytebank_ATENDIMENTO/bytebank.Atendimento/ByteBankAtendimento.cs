using bytebank.Modelos.Conta;
using bytebank_ATENDIMENTO.bytebank.Exceptions;

namespace bytebank_ATENDIMENTO.bytebank.Atendimento
{
#nullable disable
    internal class ByteBankAtendimento
    {

        private List<ContaCorrente> _listaDeContas = new List<ContaCorrente>
        {
            new ContaCorrente(95, "123456-X"){Saldo=100,Titular = new Cliente{Cpf="11111",Nome ="Henrique"}},
            new ContaCorrente(95, "951258-X"){Saldo=200,Titular = new Cliente{Cpf="22222",Nome ="Pedro"}},
            new ContaCorrente(94, "987321-W"){Saldo=60,Titular = new Cliente{Cpf="33333",Nome ="Marisa"}}
        };

        public void AtendimentoCliente()
        {
            try
            {
                char opcao = '0';
                while (opcao != '6')
                {
                    Console.Clear();
                    Console.WriteLine("===============================");
                    Console.WriteLine("===       Atendimento       ===");
                    Console.WriteLine("===1 - Cadastrar Conta      ===");
                    Console.WriteLine("===2 - Listar Contas        ===");
                    Console.WriteLine("===3 - Remover Conta        ===");
                    Console.WriteLine("===4 - Ordenar Contas       ===");
                    Console.WriteLine("===5 - Pesquisar Conta      ===");
                    Console.WriteLine("===6 - Sair do Sistema      ===");
                    Console.WriteLine("===============================");
                    Console.WriteLine("\n\n");
                    Console.Write("Digite a opção desejada: ");
                    try
                    {
                        opcao = Console.ReadLine()[0];
                    }
                    catch (Exception excecao)
                    {
                        throw new ByteBankException(excecao.Message);
                    }

                    switch (opcao)
                    {
                        case '1':
                            CadastrarConta();
                            break;
                        case '2':
                            ListarContas();
                            break;
                        case '3':
                            RemoverContas();
                            break;
                        case '4':
                            OrdenarContas();
                            break;
                        case '5':
                            PesquisarContas();
                            break;
                        case '6':
                            EncerrarAplicacao();
                            return;
                        default:
                            Console.WriteLine("Opcao não implementada.");
                            break;
                    }
                }
            }
            catch (ByteBankException excecao)
            {
                Console.WriteLine($"{excecao.Message}");
            }

        }

        private void EncerrarAplicacao()
        {
            Console.WriteLine("... Encerrando aplicação ...");
            Console.ReadKey();
        }

        private void PesquisarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     PESQUISAR CONTA     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.Write("Deseja pesquisar pelo CPF do titular (1) ou numero da conta (2) ou numero agencia (3): ");
            int escolha = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            switch (escolha)
            {
                case 1:
                    PesquisarPorCpf();
                    break;
                case 2:
                    PesquisarPorConta();
                    break;
                case 3:
                    PesquisarPorAgencia();
                    break;
                default:
                    Console.WriteLine("Opcao não implementada.");
                    break;
            }
            Console.ReadKey();
        }

        private void PesquisarPorAgencia()
        {
            Console.Write("Digite a agencia: ");
            int numeroAgencia = int.Parse(Console.ReadLine());
            Console.WriteLine("\n");

            List<ContaCorrente> consulta = (from conta in _listaDeContas where conta.Numero_agencia == numeroAgencia select conta).ToList();

            ExibirListaDeContas(consulta);
        }

        private void ExibirListaDeContas(List<ContaCorrente> consulta)
        {
            if (consulta.Count() == 0)
            {
                Console.WriteLine("... A consulta não retornou dados ...");
            }
            else
            {
                foreach (var item in consulta)
                {
                    Console.WriteLine(item.ToString());
                }
            }
        }

        private void PesquisarPorConta()
        {
            Console.Write("Digite a conta: ");
            string numeroConta = Console.ReadLine();
            Console.WriteLine("\n");

            ContaCorrente conta = (from _conta in _listaDeContas where _conta.Conta.Equals(numeroConta) select _conta).FirstOrDefault();
            if (conta == null)
            {
                Console.WriteLine("Não encontrado");
                return;
            }
            Console.WriteLine(conta.ToString());
        }

        private void PesquisarPorCpf()
        {
            Console.Write("Digite o CPF: ");
            string cpf = Console.ReadLine();
            Console.WriteLine("\n");

            ContaCorrente conta = _listaDeContas.Where(conta => conta.Titular.Cpf == cpf).FirstOrDefault();
            if (conta == null)
            {
                Console.WriteLine("Não encontrado");
                return;
            }
            Console.WriteLine(conta.ToString());
        }

        private void OrdenarContas()
        {
            _listaDeContas.Sort();
            Console.WriteLine("... Lista de Contas ordenadas ...");
            Console.ReadKey();
        }

        private void RemoverContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===      REMOVER CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.Write("Informe o número da Conta: ");
            string numeroConta = Console.ReadLine();
            ContaCorrente conta = null;
            foreach (var item in _listaDeContas)
            {
                if (item.Conta.Equals(numeroConta))
                {
                    conta = item;
                }
            }
            if (conta != null)
            {
                _listaDeContas.Remove(conta);
                Console.WriteLine("... Conta removida da lista! ...");
            }
            else
            {
                Console.WriteLine(" ... Conta para remoção não encontrada ...");
            }
            Console.ReadKey();
        }

        private void ListarContas()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===     LISTA DE CONTAS     ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            if (_listaDeContas.Count <= 0)
            {
                Console.WriteLine("... Não há contas cadastradas! ...");
                Console.ReadKey();
                return;
            }
            foreach (ContaCorrente item in _listaDeContas)
            {
                Console.WriteLine(item.ToString());
                Console.WriteLine(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>");
                Console.ReadKey();
            }

        }

        private void CadastrarConta()
        {
            Console.Clear();
            Console.WriteLine("===============================");
            Console.WriteLine("===   CADASTRO DE CONTAS    ===");
            Console.WriteLine("===============================");
            Console.WriteLine("\n");
            Console.WriteLine("=== Informe dados da conta ===");

            Console.Write("Número da Agência: ");
            int numeroAgencia = int.Parse(Console.ReadLine());
            ContaCorrente conta = new ContaCorrente(numeroAgencia);
            Console.WriteLine($"Numero da conta [NOVA] {conta.Conta}");

            Console.Write("Informe o saldo inicial: ");
            conta.Saldo = double.Parse(Console.ReadLine());

            Console.Write("Infome nome do Titular: ");
            conta.Titular.Nome = Console.ReadLine();

            Console.Write("Infome CPF do Titular: ");
            conta.Titular.Cpf = Console.ReadLine();

            Console.Write("Infome Profissão do Titular: ");
            conta.Titular.Profissao = Console.ReadLine();

            _listaDeContas.Add(conta);

            Console.WriteLine("... Conta cadastrada com sucesso! ...");
            Console.ReadKey();
        }


    }
}
