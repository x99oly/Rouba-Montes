using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Domain
{
    public class Rodada
    {
        public int NumeroDaRodada { get; private set; }

        public int JogadorDaVez { get; private set; }

        public Carta? CartaDaVez { get; private set; }

        public string? Jogada { get; private set; }

        public bool JogoEncerrado {  get; private set; }

        public Dictionary<Carta, Monte> Montes { get; private set; }
        public Jogador[] Jogadores { get; private set; }

        public Baralho BaralhoDaPartida { get; private set; }

        public Rodada(Jogador[] jogadores)
        {
            Jogadores = jogadores;
            BaralhoDaPartida = new Baralho(jogadores.Length);
            Montes = new Dictionary<Carta, Monte>();

            JogoEncerrado = false;

            Jogada = "Jogo começou!";
            NumeroDaRodada = 0;

            SortearPrimeiroAJogar();
        }

        public void IniciarRodada()
        {
            if (JogoEncerrado == true) throw new Exception("Jogo encerrado.");

            Jogador jogador = Jogadores[JogadorDaVez];

            try
            {
                BaralhoDaPartida.RetirarCarta(jogador);
            }
            catch (ArgumentNullException e)
            {
                JogoEncerrado = true;
                throw new ArgumentNullException(e.Message);
            }


            CartaDaVez = jogador.UltimaCarta();

            if (Montes.Count == 0)
            {
                Monte monte = new Monte(CartaDaVez);
                monte.VincularJogador(jogador);
                Montes.Add(CartaDaVez, monte);
            }
            else if (!jogador.SelecionarMonte(Montes, CartaDaVez))
            {
                Monte novoMonteDeDescarte = new Monte(CartaDaVez);
                Montes.Add(CartaDaVez, novoMonteDeDescarte);
                jogador.DescartarUltimaCarta();

            }
            {
                JogadorDaVez = (JogadorDaVez + 1) % Jogadores.Length;
            }
            NumeroDaRodada++;
        }

        public override string ToString()
        {
            return Log();
        }

        private string Log()
        {
            string carta = CartaDaVez != null ? CartaDaVez.ToString() : "Fim Baralho";

            return $"Rodada: {NumeroDaRodada}, Jogador: {Jogadores[JogadorDaVez].Nome}, carta: {carta}, Baralho com {BaralhoDaPartida.posicaoDaUltimaCarta} cartas.";
        }

        private void SortearPrimeiroAJogar()
        {
            Random r = new Random();
            JogadorDaVez = r.Next(0, Jogadores.Length);
        }
    }
}
