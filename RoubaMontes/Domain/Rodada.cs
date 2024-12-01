using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Domain
{
    internal class Rodada
    {
        public int NumeroDaRodada { get; private set; }

        public int JogadorDaVez { get; private set; }

        public Carta CartaDaVez { get; private set; }

        public string? Jogada { get; private set; }

        public Dictionary<Carta, Monte> Montes { get; private set; }
        public Jogador[] Jogadores { get; private set; }

        public Baralho BaralhoDaPartida { get; private set; }

        public Rodada(Jogador[] jogadores)
        {
            Jogadores = jogadores;
            BaralhoDaPartida = new Baralho(jogadores.Length);
            Montes = new Dictionary<Carta, Monte>();

            Jogada = "Jogo começou!";
            NumeroDaRodada = 1;

            SortearPrimeiroAJogar();
        }

        public void DistribuirCartas()
        {
            // Selecionar o jogador da vez
            // Ele saca uma carta
            // Verifica se há algum monte que tem o mesmo número da carta (incluindo o dele)
            // Se houver ele come o monte
            // Se não houver ele descarta a carta criando um novo monte
            // passa a vez

            Jogador jogador = Jogadores[JogadorDaVez];

            try
            {
                BaralhoDaPartida.RetirarCarta(jogador);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
                return;
            }

            CartaDaVez = jogador.UltimaCarta();

            if (Montes.Count == 0) return;

            if (Montes.Count > 0 && jogador.SelecionarMonte(Montes, CartaDaVez))
            {
                Montes.Add(CartaDaVez, jogador.MonteDeCartas);
                return;
            }
            else
            {
                // Talvez seja melhor trocar o dicionário ----- Tuplas???

                Montes.Add(CartaDaVez, new Monte(CartaDaVez));
                try
                {
                    jogador.DescartarUltimaCarta();
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                }
            }

            JogadorDaVez = (JogadorDaVez + 1) % Jogadores.Length;
        }

        private void SortearPrimeiroAJogar()
        {
            Random r = new Random();
            JogadorDaVez = r.Next(0, Jogadores.Length);
        }
    }
}
