using RoubaMontes.Domain;
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

                jogadores = new Jogador[numJogadores];

                for (int i = 0; i < jogadores.Length; i++)
                {
                    jogadores[i] = new Jogador($"jogador {i}");
                }

                baralho = new Baralho(jogadores.Length);
                rodada = new Rodada(jogadores, baralho);

                Console.WriteLine(rodada.BaralhoDaPartida.ToString() + "\n");
                do
                {
                    try
                    {
                        rodada.IniciarRodada();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(rodada.ToString());
                        Console.WriteLine(ex.ToString());
                    }
                }
                while (rodada.JogoEncerrado == false);

                Console.WriteLine(VencedoresString(rodada));

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