using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using RoubaMontes.Domain;
using System.Reflection;

namespace RoubaMontes.Tests
{
    public class MonteTests
    {
        [Fact]
        public void Monte_CriaInstancia_ComCartaInicial()
        {
            // Arrange
            Carta cartaInicial = new Carta(1, '♥');

            // Act
            Monte monte = new Monte(cartaInicial);

            // Assert
            Assert.Single(monte.MonteDeCartas);
            Assert.Equal(1, monte.TotalDeCartas);
        }

        [Fact]
        public void Monte_AdicionarCarta_IncrementaTotalDeCartas()
        {
            // Arrange
            Monte monte = new Monte(new Carta(1, '♥'));
            Carta novaCarta = new Carta(2, '♦');

            // Act
            monte.AdicionarCarta(novaCarta);

            // Assert
            Assert.Equal(2, monte.TotalDeCartas);
            Assert.Contains(novaCarta, monte.MonteDeCartas);
        }

        [Fact]
        public void Monte_AdicionarMonte_IncrementaTotalDeCartas()
        {
            // Arrange
            Monte monte1 = new Monte(new Carta(1, '♥'));
            Monte monte2 = new Monte(new Carta(2, '♦'));
            monte1.AdicionarMonte(monte2);

            // Act
            int totalEsperado = monte1.TotalDeCartas;

            // Assert
            Assert.Equal(2, totalEsperado);
        }

        [Fact]
        public void Monte_UltimaCarta_RetornaUltimaCartaCorretamente()
        {
            // Arrange
            Carta carta1 = new Carta(1, '♥');
            Carta carta2 = new Carta(2, '♦');
            Monte monte = new Monte(carta1);
            
            monte.AdicionarCarta(carta2);

            // Act
            Carta ultimaCarta = monte.UltimaCarta();

            // Assert
            Assert.Equal(carta2, ultimaCarta);
        }

        [Fact]
        public void Monte_ToString_RetornaStringCorretamente()
        {
            // Arrange
            Carta cartaInicial = new Carta(1, '♥');
            Monte monte = new Monte(cartaInicial);

            // Act
            string resultado = monte.ToString();

            // Assert
            Assert.Contains("Total de cartas: 1", resultado);
            Assert.Contains("Última carta: 1♥", resultado);
        }

        [Fact]
        public void Monte_AdicionarCarta_CalculaTotalDeCartasCorretamente()
        {
            // Arrange
            Monte monte = new Monte(new Carta(1, '♥'));
            Carta novaCarta = new Carta(2, '♦');

            // Act
            monte.AdicionarCarta(novaCarta);

            // Assert
            Assert.Equal(2, monte.TotalDeCartas);
            Assert.Contains(novaCarta, monte.MonteDeCartas);
        }

        [Fact]
        public void Monte_CalcularTotalDeCartas_RetornaZero_QuandoMonteDeCartasForNulo()
        {
            // Arrange
            Monte monte = new Monte(new Carta(1, '♥'));
            monte.MonteDeCartas.Clear(); // Simula um MonteDeCartas nulo

            // Act
            monte.CalcularTotalDeCartas();

            // Assert
            Assert.Equal(0, monte.TotalDeCartas);
        }

        [Fact]
        public void Monte_AdicionarMonte_CalculaTotalDeCartasCorretamente()
        {
            // Arrange
            Monte monte1 = new Monte(new Carta(1, '♥'));
            Monte monte2 = new Monte(new Carta(2, '♦'));
            monte1.AdicionarCarta(new Carta(3, '♠'));
            monte2.AdicionarCarta(new Carta(4, '♣'));

            // Act
            monte1.AdicionarMonte(monte2);

            // Assert
            Assert.Equal(4, monte1.TotalDeCartas);
        }


    }
}
