using RoubaMontes.Domain;
using RoubaMontes.Aid;
using System.Text;

namespace RoubaMontes
{
    class Program
    {
        public static void Main(string[] args)
        {
            do
            {
                Console.Clear();

                Jogador[] jogadores;
                Baralho baralho;
                Rodada rodada;

                Console.Write($"Digite o número de jogadores(Min de 2): ");
                if (!int.TryParse(Console.ReadLine(), out int numJogadores) || numJogadores < 2)
                {
                    numJogadores = 2;
                }

                Console.Write($"Digite o número de cartas (52/1000) *Pra valores inválidos - Default 52: ");
                if (!int.TryParse(Console.ReadLine(), out int numCartas) || numCartas < numJogadores)
                {
                    numCartas = 52;
                }

                jogadores = new Jogador[numJogadores];
                for (int i = 0; i < jogadores.Length; i++)
                {
                    jogadores[i] = new Jogador($"jogador {i}");
                }

                baralho = new Baralho(numCartas);
                rodada = new Rodada(jogadores, baralho);

                do
                {
                    try
                    {
                        rodada.IniciarRodada();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(rodada.ToString());
                        Console.WriteLine(ex.Message+"\n");
                    }
                }
                while (rodada.JogoEncerrado == false);

                Console.WriteLine(VencedoresString(rodada));

                Arquivos.SalvarLogs(rodada.Log.ToString());

                Console.WriteLine($"\nGostaria de rodar outra partida (S)sim ou (Any key)não?");
                if (Console.ReadLine().ToLower() != "s") break;
            }
            while (true);

        }

        public static string VencedoresString(Rodada rodada)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"#################################### VENCEDORES ####################################\n");
            foreach (Jogador jogador in rodada.Vencedores)
            {
                if(jogador != null) sb.AppendLine(jogador.ToString());
            }
            sb.AppendLine($"#################################### VENCEDORES ####################################\n");
            return sb.ToString();
        }
    }
}