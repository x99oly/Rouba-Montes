using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Domain
{
    public class Baralho
    {
        private Dictionary<int, char> Naipes = new Dictionary<int, char>
        {
            {1, '♥'}, {2,'♦' },{3,'♠'},{4,'♣'}
        };
        public int TotalDeCartas { get; private set; }
        public Carta[] Cartas { get; private set; }

        private int _quantidadeDeCartasPorJogador = 13;

        private Random _cartaEscolhida;

        public int posicaoDaUltimaCarta { get; private set; }


        /// <summary>
        /// Cria uma instância da classe baralho contendo a quantidade de cartas do baralho relativa a cada jogador
        /// O mínimo de cartas geradas será 52.
        /// Para valores menores que 2 será gerada instância com 2 jogadores.
        /// </summary>
        /// <param name="totalDeJogadores">A quantidade de jogadores da partida - min 2</param>
        /// <return>Retorna uma instância de Baralho com no mínimo 2 jogadores</return>
        public Baralho(int totalDeJogadores)
        {
            _cartaEscolhida = new Random();
            int totalDeCartas;
            if (totalDeJogadores < 2)
            {
                totalDeJogadores = 2;
            }
            totalDeCartas = totalDeJogadores * _quantidadeDeCartasPorJogador;

            if (totalDeCartas < 52) totalDeCartas = 52;

            Cartas = new Carta[totalDeCartas];
            posicaoDaUltimaCarta = Cartas.Length - 1;

            InstancirBaralho(totalDeCartas);
            Embaralhar();
        }

        /// <summary>
        /// Retira lógicamente o último índice do baralho e entrega ao jogador.
        /// </summary>
        /// <param name="jogadorDaVez">Jogador que tem a vez na rodada</param>
        /// <exception cref="ArgumentOutOfRangeException">Em caso de não haver mais cartas no baralho</exception>
        public void RetirarCarta(Jogador jogadorDaVez)
        {
            if (posicaoDaUltimaCarta == 0) throw new ArgumentOutOfRangeException("Não há mais cartas para serem compradas no baralho.");

            jogadorDaVez.ComprarCarta(Cartas[posicaoDaUltimaCarta]);
            posicaoDaUltimaCarta--;
        }

        private void InstancirBaralho(int totalDeCartas)
        {
            int indexDoNaipe = 1;
            int numeroDaCarta = 1;

            for (int i = 0; i < totalDeCartas; i++)
            {
                if (Naipes.TryGetValue(indexDoNaipe, out char charDoNaipe))
                {
                    Cartas[i] = new Carta(numeroDaCarta, charDoNaipe);
                    indexDoNaipe++;
                    numeroDaCarta++;
                    if (indexDoNaipe > 4) indexDoNaipe = 1;
                    if (numeroDaCarta > 13) numeroDaCarta = 1;
                }
            }
        }

        private void Embaralhar()
        {
            int ultima = Cartas.Length - 1;
            Fisher_YatesShuffle(ultima);
        }

        /// <summary>
        /// Ordena de forma aleatória
        /// Usa o último índice lógico e troca com um aleatório, então decrementa o último.
        /// Como para na metade não garante total aleatóriedade, mas como o índice trocado com último é aleatório também garante que todo jogo seja diferente.
        /// </summary>
        /// <param name="ultima">O índice a ser trocado - Iniciamente Length - 1</param>
        private void Fisher_YatesShuffle(int ultima)
        {
            if (ultima == Cartas.Length / 2) return;

            int carta = _cartaEscolhida.Next(0, ultima);

            TrocarPosicao(carta, ultima);

            Fisher_YatesShuffle(ultima - 1);
        }

        private void TrocarPosicao(int indice1, int indice2)
        {
            Carta temp = Cartas[indice1];
            Cartas[indice1] = Cartas[indice2];
            Cartas[2] = temp;
        }

        public override string ToString()
        {
            return ParseBaralhoParaString();
        }

        private string ParseBaralhoParaString()
        {
            StringBuilder sb = new StringBuilder();

            int contador = 0;
            foreach (var carta in Cartas)
            {
                if (contador % 13 == 0 && contador != 0) sb.Append("\n");
                sb.Append($"{carta}, ");
                contador++;
            }

            return sb.ToString();
        }


    }
}
