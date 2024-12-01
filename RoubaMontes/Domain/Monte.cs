namespace RoubaMontes.Domain
{
    public class Monte
    {
        public Stack<Carta> MonteDeCartas { get; private set; }

        public Jogador? JogadorDono { get; private set; }

        public int TotalDeCartas { get; private set; }

        public Monte(Carta carta) {
            TotalDeCartas = 0;

            MonteDeCartas = new Stack<Carta>();
            
            AdicionarCarta(carta);
        }

        public void VincularJogador(Jogador jogador)
        {
            JogadorDono = jogador;
        }

        public void AdicionarCarta(Carta carta)
        {
            MonteDeCartas.Push(carta);
            CalcularTotalDeCartas();
        }

        public void CalcularTotalDeCartas()
        {
            if (MonteDeCartas == null) TotalDeCartas = 0;

            TotalDeCartas = MonteDeCartas.Count();
        }

        public void AdicionarMonte(Monte monte)
        {
            while(monte.MonteDeCartas.Count > 0)
            {
                MonteDeCartas.Push(monte.MonteDeCartas.Pop());
            }
            TotalDeCartas += monte.TotalDeCartas;
        }

        public Carta UltimaCarta()
        {
            if (MonteDeCartas == null || MonteDeCartas.Count == 0)
                throw new ArgumentNullException("Não há mais cartas no monte do jogador.");

            return MonteDeCartas.Peek();
        }

        public override string ToString()
        {
            string stringCarta = UltimaCarta().ToString();
            return $"Total de cartas: {TotalDeCartas} - Última carta: {stringCarta}";
        }
    }
}