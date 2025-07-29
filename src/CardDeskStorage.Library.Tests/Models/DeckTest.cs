using CardDeskStorage.Library.Models;
using CardDeskStorage.Library.ShuffleAlgorithms;

public class DeckTests
{
    public class StubShuffleAlgorithm : IShuffleAlgorithm
    {
        public ShuffleAlgorithmType Type => ShuffleAlgorithmType.None;

        public bool ShuffleCalled { get; set; }
        public List<Card>? CardsShuffled { get; set; }

        public void Shuffle(List<Card> deck)
        {
            ShuffleCalled = true;
            CardsShuffled = deck;
        }
    }

    [Fact]
    public void ShuffleAlgorithm_Get_ReturnsSetValue()
    {
        // Arrange
        var stubShuffleAlgorithm = new StubShuffleAlgorithm();
        var deck = new Deck { ShuffleAlgorithm = stubShuffleAlgorithm };

        // Act
        var algorithm = deck.ShuffleAlgorithm;

        // Assert
        Assert.Equal(stubShuffleAlgorithm, algorithm);
    }

    [Fact]
    public void Shuffle_ShuffleAlgorithmNotCreated_CreatesAlgorithmAndShuffles()
    {
        // Arrange
        var stubShuffleAlgorithm = new StubShuffleAlgorithm();

        var deck = new Deck
        {
            ShuffleAlgorithmType = ShuffleAlgorithmType.None,
            ShuffleAlgorithm = stubShuffleAlgorithm
        };
        var cards = new List<Card>
        {
            new Card { Suit = Suit.Hearts, Rank = Rank.Ace },
            new Card { Suit = Suit.Diamonds, Rank = Rank.King }
        };
        deck.Cards = cards;

        // Act
        deck.Shuffle();

        // Assert
        Assert.True(stubShuffleAlgorithm.ShuffleCalled);
        Assert.Equal(cards, stubShuffleAlgorithm.CardsShuffled);
    }

    [Fact]
    public void Shuffle_ShuffleAlgorithmCreated_ShufflesCards()
    {
        // Arrange
        var stubShuffleAlgorithm = new StubShuffleAlgorithm();
        var deck = new Deck { ShuffleAlgorithm = stubShuffleAlgorithm };
        var cards = new List<Card>
        {
            new Card { Suit = Suit.Hearts, Rank = Rank.Ace },
            new Card { Suit = Suit.Diamonds, Rank = Rank.King }
        };
        deck.Cards = cards;

        // Act
        deck.Shuffle();

        // Assert
        Assert.True(stubShuffleAlgorithm.ShuffleCalled);
        Assert.Equal(cards, stubShuffleAlgorithm.CardsShuffled);
    }

    [Fact]
    public void Shuffle_ShuffleAlgorithmFactoryReturnsNull_ThrowsInvalidOperationException()
    {
        var deck = new Deck
        {
            ShuffleAlgorithmType = null
        };
        var exception = Record.Exception(() => deck.Shuffle());
        Assert.Null(exception);
    }

    [Fact]
    public void Copy_CreatesNewDeckWithSameProperties()
    {
        // Arrange
        var deck = new Deck
        {
            Name = "Test Deck",
            Cards = new List<Card> { new Card { Suit = Suit.Hearts, Rank = Rank.Ace } },
            ShuffleAlgorithmType = ShuffleAlgorithmType.Simple,
            ShuffleAlgorithm = new StubShuffleAlgorithm()
        };

        // Act
        var copiedDeck = deck.Copy();

        // Assert
        Assert.Equal(deck.Name, copiedDeck.Name);
        Assert.Equal(deck.Cards, copiedDeck.Cards);
        Assert.Equal(deck.ShuffleAlgorithmType, copiedDeck.ShuffleAlgorithmType);
        Assert.Equal(deck.ShuffleAlgorithm, copiedDeck.ShuffleAlgorithm);
    }

    [Fact]
    public void IsSameOrder_SameOrder_ReturnsTrue()
    {
        // Arrange
        var cards = new List<Card>
        {
            new Card { Suit = Suit.Hearts, Rank = Rank.Ace },
            new Card { Suit = Suit.Diamonds, Rank = Rank.King }
        };
        var deck1 = new Deck { Cards = cards };
        var deck2 = new Deck { Cards = new List<Card>(cards) };

        // Act
        var result = deck1.IsSameOrder(deck2);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public void IsSameOrder_DifferentOrder_ReturnsFalse()
    {
        // Arrange
        var cards1 = new List<Card>
        {
            new Card { Suit = Suit.Hearts, Rank = Rank.Ace },
            new Card { Suit = Suit.Diamonds, Rank = Rank.King }
        };
        var cards2 = new List<Card>
        {
            new Card { Suit = Suit.Diamonds, Rank = Rank.King },
            new Card { Suit = Suit.Hearts, Rank = Rank.Ace }
        };
        var deck1 = new Deck { Cards = cards1 };
        var deck2 = new Deck { Cards = cards2 };

        // Act
        var result = deck1.IsSameOrder(deck2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSameOrder_DifferentCardCounts_ReturnsFalse()
    {
        // Arrange
        var cards1 = new List<Card> { new Card { Suit = Suit.Hearts, Rank = Rank.Ace } };
        var cards2 = new List<Card>
        {
            new Card { Suit = Suit.Hearts, Rank = Rank.Ace },
            new Card { Suit = Suit.Diamonds, Rank = Rank.King }
        };
        var deck1 = new Deck { Cards = cards1 };
        var deck2 = new Deck { Cards = cards2 };

        // Act
        var result = deck1.IsSameOrder(deck2);

        // Assert
        Assert.False(result);
    }

    [Fact]
    public void IsSameOrder_OtherDeckIsNull_ThrowsArgumentNullException()
    {
        // Arrange
        var deck = new Deck { Cards = new List<Card>() };

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => deck.IsSameOrder(null));
    }
}