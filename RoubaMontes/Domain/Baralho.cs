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
        public Carta[] Cartas { get; private set; }

        public int _quantidadeDeCartasPorJogador { get; private set; } = 13;

        private Random _cartaEscolhida;

        public int posicaoDaUltimaCarta { get; private set; }

        public Baralho(int total)
        {
            _cartaEscolhida = new Random();
            int totalDeCartas;

            totalDeCartas = total;

            if (total <= 0 || total > 1000) totalDeCartas = 52;

            Cartas = new Carta[totalDeCartas];

            posicaoDaUltimaCarta = Cartas.Length - 1;

            InstancirBaralho(totalDeCartas);
            Embaralhar();
        }

        public Carta RetirarCarta()
        {
            if (posicaoDaUltimaCarta == 0) throw new ArgumentNullException("Não há mais cartas para serem compradas no baralho.");

            Carta ultimaCarta = Cartas[posicaoDaUltimaCarta];
            posicaoDaUltimaCarta--;
            return ultimaCarta;
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
            Cartas[indice2] = temp;
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
                sb.Append($"{carta.ToString()}, ");
                contador++;
            }

            return sb.ToString();
        }


    }
}
