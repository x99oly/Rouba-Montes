using System;
using System.Collections.Generic;
using Xunit;
using RoubaMontes.Domain;

namespace RoubaMontes.Tests
{
    public class JogadorTests
    {
        [Fact]
        public void Jogador_CriaInstancia_ComNomeCorreto()
        {
            // Arrange
            string nomeJogador = "Jogador1";

            // Act
            Jogador jogador = new Jogador(nomeJogador);

            // Assert
            Assert.Equal(nomeJogador, jogador.Nome);
            Assert.Empty(jogador.MonteDeCartas);
        }

        [Fact]
        public void Jogador_ComprarCarta_AdicionaCartaAoMonte()
        {
            // Arrange
            Jogador jogador = new Jogador("Jogador1");
            Carta carta = new Carta(1, '♥');

            // Act
            jogador.ComprarCarta(carta);

            // Assert
            Assert.Single(jogador.MonteDeCartas);
            Assert.Equal(carta, jogador.MonteDeCartas.Peek());
        }

        [Fact]
        public void Jogador_SelecionarMonte_AdicionaCartasDeOutroMonte()
        {
            // Arrange
            Jogador jogador = new Jogador("Jogador1");
            Carta carta1 = new Carta(1, '♥');
            Carta carta2 = new Carta(2, '♦');
            Monte monte = new Monte(carta1);
            monte.AdicionarCarta(carta2);

            Dictionary<Carta, Monte> montes = new Dictionary<Carta, Monte>
            {
                { carta1, monte }
            };

            // Act
            jogador.SelecionarMonte(montes, carta1);

            // Assert
            Assert.Equal(2, jogador.MonteDeCartas.Count);
            Assert.Contains(carta1, jogador.MonteDeCartas);
            Assert.Contains(carta2, jogador.MonteDeCartas);
            Assert.Empty(monte.MonteDeCartas);
        }

        [Fact]
        public void Jogador_SelecionarMonte_NaoAdicionaCartas_SeCartaNaoExiste()
        {
            // Arrange
            Jogador jogador = new Jogador("Jogador1");
            Carta carta = new Carta(1, '♥');
            Dictionary<Carta, Monte> montes = new Dictionary<Carta, Monte>();

            // Act
            jogador.SelecionarMonte(montes, carta);

            // Assert
            Assert.Empty(jogador.MonteDeCartas);
        }

        [Fact]
        public void Jogador_ToString_RetornaDescricaoCorreta()
        {
            // Arrange
            Jogador jogador = new Jogador("Jogador1");
            jogador.ComprarCarta(new Carta(1, '♥'));
            jogador.ComprarCarta(new Carta(2, '♦'));

            // Act
            string resultado = jogador.ToString();

            // Assert
            Assert.Contains("Jogador: Jogador1", resultado);
            Assert.Contains("2 cartas", resultado);
        }
    }
}
