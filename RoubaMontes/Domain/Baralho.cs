using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Domain
{
    public class Baralho
    {
        public int TotalDeCartas { get; private set; }

        public Queue<Carta> Cartas { get; private set; }

        private int _quantidadeDeCartasPorJogador = 13;
        public Baralho() { }

        /// <summary>
        /// Cria uma instância da classe baralho contendo a quantidade de cartas do baralho relativa a cada jogador
        /// 
        /// </summary>
        /// <param name="totalDeJogadores">A quantidade de jogadores da partida</param>
        public Baralho(int totalDeJogadores)
        {
            InstancirBaralho(totalDeJogadores * _quantidadeDeCartasPorJogador);
        }

        /// <summary>
        /// Preenche a Queue de cartas aleatóriamente
        /// </summary>
        /// <param name="totalDeCartas">Total de cartas do baralho</param>
        private void InstancirBaralho(int totalDeCartas)
        {

        }

    }
}
