namespace RoubaMontes.Domain
{
    public class Jogador
    {
        public string Nome { get; private set; }
        public LinkedList<Monte> MonteDeCartas { get; private set; }
        public int PosicaoNaUltimaPartida { get; private set; }
        public int TamanhoDoMonteNaUltimaPartida { get; private set; }

        public Jogador(string nome)
        {
            Nome = nome;
            MonteDeCartas = new LinkedList<Monte>();
        }

        public void ComprarCarta(Carta carta)
        {
            if (MonteDeCartas.Count < 1) MonteDeCartas.AddLast(new Monte(carta));

            else MonteDeCartas.Last().AdicionarCarta(carta);
        }

        //public Monte? SelecionarMonte(Dictionary<Carta, Monte> montes, Carta cartaDaVez)
        //{
        //    if (montes == null || montes.Count < 1 || cartaDaVez == null) return null;

        //    if (montes.TryGetValue(cartaDaVez, out Monte? novoMonte))
        //    {
        //        return novoMonte;
        //    }
        //    return null;
        //}

        public void ComprarMonte(Monte novoMonte)
        {
            if (novoMonte == null || novoMonte.MonteDeCartas.Count < 1)
                throw new ArgumentNullException("Não há cartas no monte para serem compradas.");

            novoMonte.VincularJogador(this);

            MonteDeCartas.AddLast(novoMonte);
        }

        /// <summary>
        /// Retorna a última carta do monte do jogador.
        /// Não desassocia a carta do monte do jogador - Não há perda de referências
        /// </summary>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        public Carta? UltimaCarta()
        {
            if (MonteDeCartas == null || MonteDeCartas.Last().MonteDeCartas.Count <= 0) return null;

            return MonteDeCartas.Last().UltimaCarta();
        }

        public void PerderMonte(Monte monte)
        {
            MonteDeCartas.Remove(monte);
        }
        public Carta? DescartarUltimaCarta()
        {
            if (MonteDeCartas == null) return null;

            try
            {
                return MonteDeCartas.Last().DescartarUltimaCarta();
            }
            catch (ArgumentNullException e)
            {
                throw new ArgumentNullException(e.Message);
            }
        }

        public int TotalDeCartas()
        {
            int res = 0;
            foreach (Monte monte in MonteDeCartas)
            {
                res += monte.TotalDeCartas;
            }
            return res;
        }

        public override string ToString()
        {
            if (MonteDeCartas == null)
                return $"Jogador: {Nome} com total de 0 cartas";

            int totalDeCartas = TotalDeCartas();

            return $"Jogador: {Nome} - Total de cartas: {totalDeCartas}";
        }

    }
}