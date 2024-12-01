using System;
using Xunit;
using RoubaMontes.Aid.Search.ArvoreAvl;
using RoubaMontes.Domain;

namespace RoubaMontes.Tests
{
    public class NodeTests
    {
        [Fact]
        public void CriarNode_CriaNodeCorretamente()
        {
            // Arrange
            Carta carta = new Carta(1, '*');

            // Act
            Node<Carta> node = new Node<Carta>(carta);

            // Assert
            Assert.NotNull(node);
            Assert.Equal(carta, node.Raiz);
            Assert.Null(node.NoEsq);
            Assert.Null(node.NoDir);
        }

        [Fact]
        public void CompareTo_CompararNodeComOutroNode_RetornaValorEsperado()
        {
            // Arrange
            Carta carta1 = new Carta(1, '*');
            Carta carta2 = new Carta(2, '*');
            Node<Carta> node = new Node<Carta>(carta1);

            // Act
            int resultado = node.CompareTo(carta2);

            // Assert
            Assert.True(resultado > 0); // carta2 deve ser maior que carta1
        }

        [Fact]
        public void Equals_CompararNodeComOutroNode_RetornaTrueSeIgual()
        {
            // Arrange
            Carta carta1 = new Carta(1, '*');
            Node<Carta> node1 = new Node<Carta>(carta1);
            Node<Carta> node2 = new Node<Carta>(carta1);

            // Act & Assert
            Assert.True(node1.Equals(node2));
        }

        [Fact]
        public void Equals_CompararNodeComOutroNode_RetornaFalseSeDiferente()
        {
            // Arrange
            Carta carta1 = new Carta(1, '*');
            Carta carta2 = new Carta(2, '*');
            Node<Carta> node1 = new Node<Carta>(carta1);
            Node<Carta> node2 = new Node<Carta>(carta2);

            // Act & Assert
            Assert.False(node1.Equals(node2));
        }
    }
}
