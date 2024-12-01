namespace RoubaMontes.Domain
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public Stack<Carta> MonteDeCartas { get; private set; }
        public int PosicaoNaUltimaPartida { get; private set; }
        public int TamanhoDoMonteNaUltimaPartida { get; private set; }

        public Jogador(string nome)
        {
            Nome = nome;
            MonteDeCartas = new Stack<Carta>();
        }

        public void ComprarCarta(Carta carta)
        {
            MonteDeCartas.Push(carta);
        }

        public void SelecionarMonte(Dictionary<Carta,Monte> montes, Carta carta)
        {
            if (montes.TryGetValue(carta, out Monte? novoMonte))
            {
                novoMonte.VincularJogador(this);
                while(novoMonte.MonteDeCartas.Count > 0)
                {
                    MonteDeCartas.Push(novoMonte.MonteDeCartas.Pop());
                }
            }
        }

        public void PassarVez()
        {
            throw new NotImplementedException();
        }

        
    }
}