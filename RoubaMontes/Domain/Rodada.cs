using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace RoubaMontes.Domain
{
    public class Rodada
    {
        public int NumeroDaRodada { get; private set; }

        public int JogadorDaVez { get; private set; }

        public Carta? CartaDaVez { get; private set; }

        public bool JogoEncerrado {  get; private set; }

        public Dictionary<Carta, Monte> Montes { get; private set; }
        public Jogador[] Jogadores { get; private set; }

        public Jogador[] Vencedores { get; private set; }

        public Baralho BaralhoDaPartida { get; private set; }

        public StringBuilder Log {  get; private set; }

        public Rodada(Jogador[] jogadores, Baralho baralho)
        {
            if(!ValidarIntanciaDoBaralho(jogadores, baralho))
                throw new InvalidOperationException("Baralho não tem a quantidade correta de cartas dado o número de jogadores.");

            Jogadores = jogadores;
            BaralhoDaPartida = baralho;
            Montes = new Dictionary<Carta, Monte>();
            Vencedores = new Jogador[Jogadores.Length];
            Log = new StringBuilder();

            JogoEncerrado = false;
            NumeroDaRodada = 0;
            JogadorDaVez = 0;
        }

        private bool ValidarIntanciaDoBaralho(Jogador[] jogadores, Baralho baralho)
        {
            if (baralho.Cartas.Length == baralho._quantidadeDeCartasPorJogador * 4 | jogadores.Length <= 4 && jogadores.Length > 0) return true;
            if (baralho.Cartas.Length != jogadores.Length * baralho._quantidadeDeCartasPorJogador) return false;

            return true;
        }

        public void IniciarRodada()
        {
            NumeroDaRodada++;

            try
            {
                CartaDaVez = RetirarCartaJogador();
            }
            catch (Exception ex)
            {
                JogoEncerrado = true;
                throw new ArgumentNullException("Não há mais cartas no baralho. Encerrando jogo...");
            }

            Jogador jogadorDaVez = Jogadores[JogadorDaVez];

            jogadorDaVez.ComprarCarta(CartaDaVez);

            ProcessarCartaJogador(jogadorDaVez);

            GerarLog();
        }

        private Carta RetirarCartaJogador()
        {
            return BaralhoDaPartida.RetirarCarta();
        }

        private void ProcessarCartaJogador( Jogador jogador)
        {
            if (Montes.Count == 0)
            {
                CriarNovoMonte(jogador);
            }
            else if(Montes.TryGetValue(CartaDaVez, out Monte? monteDaVez))
            {
                if (monteDaVez == null) throw new NullReferenceException("Monte da vez era nulo.");

                jogador.ComprarMonte(monteDaVez);
                DesvincularMonteDeJogador(monteDaVez);                
            }
            else
            {
                CriarMonteDeDescarte(jogador);
                AtualizarEstadoDaRodada();
            }
        }

        private void DesvincularMonteDeJogador(Monte monte)
        {
            if (monte.JogadorDono != null) monte.JogadorDono.PerderMonte(monte); 
        }

        private void CriarNovoMonte(Jogador jogador)
        {
            Monte monte = new Monte(CartaDaVez);
            jogador.ComprarMonte(monte);
            Montes.Add(CartaDaVez, monte);
        }

        private void CriarMonteDeDescarte(Jogador jogador)
        {
            Monte novoMonteDeDescarte = new Monte(CartaDaVez);
            Montes.Add(CartaDaVez, novoMonteDeDescarte);
            jogador.DescartarUltimaCarta();
        }

        private void AtualizarEstadoDaRodada()
        {
            JogadorDaVez = (JogadorDaVez + 1) % Jogadores.Length;
        }


        public override string ToString()
        {
            return Log.ToString();
        }

        private void JogadorComMaiorMonte(Jogador[] jogadores)
        {
            int count = 0;

            for (int i = 0; i < jogadores.Length; i++)
            {
                if (Vencedores[i] != null) Vencedores[i] = null;

                if (jogadores[i].TotalDeCartas() >= count)
                {
                    count = jogadores[i].TotalDeCartas();
                    Vencedores[i] = jogadores[i];
                }
            }
        }

        private string VencedoresToString()
        {
            JogadorComMaiorMonte(Jogadores);

            StringBuilder sb = new StringBuilder();

            foreach (Jogador j in  Vencedores)
            {
                if (j != null) sb.Append($"{j.ToString()} - ");
            }
            return sb.ToString();
        }

        private void GerarLog()
        {
            string carta = CartaDaVez != null ? CartaDaVez.ToString() : "Fim Baralho";
            string jogador = JogadorDaVez.ToString();
            string maioresMontes = VencedoresToString();


            Log.Append($"Rodada: {NumeroDaRodada}, Jogador: {jogador}, carta da vez: {carta}, maiores montes {maioresMontes},  baralho tem {BaralhoDaPartida.posicaoDaUltimaCarta} cartas.\n");
        }

    }
}
