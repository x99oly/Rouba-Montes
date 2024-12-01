using RoubaMontes.Domain;

namespace RoubaMontes
{
    class Program
    {
        public static void Main(string[] args)
        {
            Jogador[] jogadores;
            Rodada rodada;
            Console.Write($"Digite o número de jogadores(Min de 2): ");

            if(!int.TryParse(Console.ReadLine(), out int numJogadores) || numJogadores < 2)
            {
                numJogadores = 2;
            }            

            jogadores = new Jogador[numJogadores];

            for (int i = 0; i < jogadores.Length; i++)
            {
                jogadores[i] = new Jogador($"jogador {i}");
            }

            rodada = new Rodada(jogadores);

            do
            {
                try
                {
                    rodada.IniciarRodada();
                    Console.WriteLine(rodada.ToString());
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }
            }
            while (true);

            Console.ReadLine();
        }
    }
}