namespace RoubaMontes.Domain
{
    public class Monte
    {
        public Stack<Carta> MonteDeCartas { get; private set; }

        public Jogador? JogadorDono { get; private set; }

        public int TotalDeCartas { get; private set; }

        public Monte() {
            TotalDeCartas = 0;
            MonteDeCartas = new Stack<Carta>();
        }

        public Monte(Carta carta) : this()
        {                         
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

        /// <summary>
        /// Garante que a última carta (Carta da vez) seja a que ficará no topo do monte após consumir o outro
        /// </summary>
        /// <param name="monte"></param>
        public void AdicionarMonte(Monte monte)
        {
            Carta cartaDaVez = monte.MonteDeCartas.Pop();

            while(monte.MonteDeCartas.Count > 0)
            {
                MonteDeCartas.Push(monte.MonteDeCartas.Pop());
                monte.JogadorDono = null;
            }
            AdicionarCarta(cartaDaVez);
            CalcularTotalDeCartas();
        }

        public void CalcularTotalDeCartas()
        {
            if (MonteDeCartas == null)
            {
                TotalDeCartas = 0;
                return;
            }

            TotalDeCartas = MonteDeCartas.Count();
        }

        public Carta UltimaCarta()
        {
            if (MonteDeCartas == null || MonteDeCartas.Count == 0)
                throw new InvalidOperationException("Não há mais cartas no monte do jogador.");

            return MonteDeCartas.Peek();
        }

        public Carta DescartarUltimaCarta()
        {
            if (MonteDeCartas == null || MonteDeCartas.Count == 0) throw new ArgumentNullException("Não há cartas para serem descartadas");

            Carta carta = MonteDeCartas.Pop();
            CalcularTotalDeCartas();

            return carta;
        }

        public override string ToString()
        {
            string stringCarta = UltimaCarta().ToString();
            return $"Total de cartas: {TotalDeCartas} - Última carta: {stringCarta}";
        }
    }
}