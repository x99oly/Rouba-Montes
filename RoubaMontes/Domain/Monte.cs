namespace RoubaMontes.Domain
{
    public class Monte
    {
        public Stack<Carta> MonteDeCartas { get; private set; }

        public Stack<Monte> MonteDeMonteDeCartas { get; private set; }

        public Jogador? JogadorDono { get; private set; }

        public int TotalDeCartas { get; private set; }

        public Monte(Carta carta) {
            TotalDeCartas = 0;

            MonteDeCartas = new Stack<Carta>();
            MonteDeMonteDeCartas = new Stack<Monte>();
            
            AdicionarCarta(carta);
        }

        public void VincularJogador(Jogador jogador)
        {
            JogadorDono = jogador;
        }

        public void AdicionarCarta(Carta carta)
        {
            MonteDeCartas.Push(carta);
            TotalDeCartas++;
        }

        public void AdicionarMonte(Monte monte)
        {
            MonteDeMonteDeCartas.Push(monte);
            TotalDeCartas += monte.TotalDeCartas;
        }

        public Carta UltimaCarta()
        {
            if (MonteDeMonteDeCartas.Count == 0)
                return MonteDeCartas.Peek();

            return MonteDeMonteDeCartas.Peek().MonteDeCartas.Peek();
        }

        public override string ToString()
        {
            string stringCarta = UltimaCarta().ToString();
            return $"Total de cartas: {TotalDeCartas} - Última carta: {stringCarta}";
        }
    }
}