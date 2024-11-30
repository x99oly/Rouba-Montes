using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Domain
{
    public class Baralho
    {
        public Dictionary<int, char> Naipes = new Dictionary<int, char>
        {
            {1, '♥'}, {2,'♦' },{3,'♠'},{4,'♣'}
        };
        public int TotalDeCartas { get; private set; }

        public Queue<Carta> Cartas { get; private set; }

        private int _quantidadeDeCartasPorJogador = 13;

        /// <summary>
        /// Cria uma instância da classe baralho contendo a quantidade de cartas do baralho relativa a cada jogador
        /// O mínimo de cartas geradas será 52.
        /// Para valores menores que 2 será gerada instância com 2 jogadores.
        /// </summary>
        /// <param name="totalDeJogadores">A quantidade de jogadores da partida - min 2</param>
        /// <return>Retorna uma instância de Baralho com no mínimo 2 jogadores</return>
        public Baralho(int totalDeJogadores)
        {
            Cartas = new Queue<Carta>();

            int totalDeCartas;
            if (totalDeJogadores < 2)
            {
                totalDeJogadores = 2;
            }
            totalDeCartas = totalDeJogadores * 13;

            if (totalDeCartas < 52) totalDeCartas = 52;

            InstancirBaralho(totalDeCartas);
        }

        public void DistribuirCarta(Jogador jogadorDaVez)
        {

        }

        /// <summary>
        /// Preenche a Queue de cartas aleatóriamente
        /// </summary>
        /// <param name="totalDeCartas">Total de cartas do baralho</param>
        private void InstancirBaralho(int totalDeCartas)
        {
            int indexNaipe = 1;
            int numCarta = 1;
            for (int i = 0; i < totalDeCartas; i++)
            {
                if(Naipes.TryGetValue(indexNaipe, out char charNaipe))
                {
                    Cartas.Enqueue(new Carta(indexNaipe, charNaipe));
                }
                indexNaipe++;
                numCarta++;

                if (indexNaipe == 5) indexNaipe = 1;
                if (numCarta == 14) numCarta = 1;
            }
        }

    }
}
