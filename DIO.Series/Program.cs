using System;

namespace DIO.Series
{
    class Program
    {
        static SerieRepositorio repositorio = new SerieRepositorio();
        static void Main(string[] args)
        {
            string opcaoUsuario = ObterOpcaoUsuario();

            while (opcaoUsuario.ToUpper() != "X")
            {
                switch (opcaoUsuario)
                {
                    case "1":
                        InserirSerie();
                        break;
                    
                    case "2":
                        ListarSeries();
                        break;
                
                    case "3":
                        AtualizarSerie();
                        break;

                    case "4":
                        ExcluirSerie();
                        break;

                    case "5":
                        VisualizarSerie();
                        break;

                    case "C":
                        Console.Clear();
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();                            
                }

                opcaoUsuario = ObterOpcaoUsuario();
            }

            Console.WriteLine("Obrigada por utilizar nossos serviços.");
        }

        private static void ListarSeries()
        {
            Console.WriteLine("Lista de Séries");

            var lista = repositorio.Lista();

            if (lista.Count == 0)
            {
                Console.WriteLine("Nenhuma série cadastrada!");
                return;
            }

            foreach (var serie in lista)
            {
                var excluido = serie.retornaExcluido();

                Console.WriteLine("#ID {0}: - {1} {2}", serie.retornaId(), serie.retornaTitulo(), (excluido ? "*Excluido" : ""));
            }
        }

        public static void InserirSerie()
        {
            Console.WriteLine("Inserir nova série");

            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.WriteLine();
            
            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            // Invalida opção de gênero inexistente 
            while(entradaGenero <= 0 || entradaGenero >= 14)
            {
                Console.WriteLine("Opção Inválida!");
                Console.Write("Digite o gênero entre as opções acima: ");
                entradaGenero = int.Parse(Console.ReadLine());
            }

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie novaSerie = new Serie(id: repositorio.ProximoId(),
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);
            
            repositorio.Insere(novaSerie);
            Console.Clear();
        }

        // Verificar quais itens deseja alterar
        private static void AtualizarSerie()
        {
            Console.Write("Digite o Id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());
            
            foreach(int i in Enum.GetValues(typeof(Genero)))
            {
                Console.WriteLine("{0} - {1}", i, Enum.GetName(typeof(Genero), i));
            }

            Console.Write("Digite o gênero entre as opções acima: ");
            int entradaGenero = int.Parse(Console.ReadLine());

            Console.Write("Digite o Título da Série: ");
            string entradaTitulo = Console.ReadLine();

            Console.Write("Digite o Ano de início da Série: ");
            int entradaAno = int.Parse(Console.ReadLine());

            Console.Write("Digite a Descrição da Série: ");
            string entradaDescricao = Console.ReadLine();

            Serie atualizaSerie = new Serie(id: indiceSerie,
                                        genero: (Genero)entradaGenero,
                                        titulo: entradaTitulo,
                                        ano: entradaAno,
                                        descricao: entradaDescricao);

            repositorio.Atualiza(indiceSerie, atualizaSerie);
        }
        
        private static void ExcluirSerie()
        {
            Console.Write("Digite o Id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            // Mostra os dados da série do Id selecionado.
            var serie = repositorio.RetornaPorId(indiceSerie);
            Console.WriteLine(serie);

            // Verifica se a série já foi excluída.
            var excluido = serie.retornaExcluido();
            if(excluido)
            {
                Console.Write("Essa série já foi excluída!");
            }
            else
            {
                Console.WriteLine("Tem certeza que deseja excluir a série?");
                Console.Write("S - Sim / N - Não: ");
                string excluirId = Console.ReadLine();

                while(excluirId.ToUpper() != "S" || excluirId.ToUpper() != "N")
                {
                    if(excluirId.ToUpper() == "S")
                    {
                        repositorio.Exclui(indiceSerie);
                        Console.WriteLine("Série Id {0} excluída com sucesso!", indiceSerie);
                        return;
                    }
                    else if(excluirId.ToUpper() != "N")
                    {
                        Console.WriteLine("Opação inválida!");
                        Console.Write("Tem certeza que deseja excluir a série? S - Sim / N - Não: ");
                        excluirId = Console.ReadLine();
                    }
                    else
                    {
                        return;
                    }
                }                
            }           
        }

        private static void VisualizarSerie()
        {
            Console.Write("Digite o Id da série: ");
            int indiceSerie = int.Parse(Console.ReadLine());

            var serie = repositorio.RetornaPorId(indiceSerie);

            Console.WriteLine(serie);
        }

        private static string ObterOpcaoUsuario()
        {
            Console.WriteLine("DIO Séries ao seu dispor!!!");
            Console.WriteLine();
            Console.WriteLine("Informe a opção desejada: ");

            Console.WriteLine("1 - Inserir nova série");
            Console.WriteLine("2 - Listar Séries");
            Console.WriteLine("3 - Atualizar série");
            Console.WriteLine("4 - Excluir série");
            Console.WriteLine("5 - Visualizar série");
            Console.WriteLine("C - Limpar tela");
            Console.WriteLine("X - Sair");
            Console.WriteLine();

            string opcaoUsuario = Console.ReadLine().ToUpper();
            Console.WriteLine();
            Console.Clear();
            return opcaoUsuario;
        }
    }
}
