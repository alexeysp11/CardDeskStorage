using CardDeskStorage.Library.Models;
using CardDeskStorage.Library.ShuffleAlgorithms;

public class InMemoryDeckStorageTest
{
    private readonly InMemoryDeckStorage _deckStorage;
    private readonly List<Card> _testCards;
    private readonly IShuffleAlgorithm _shuffleAlgorithm;

    public InMemoryDeckStorageTest()
    {
        _deckStorage = new InMemoryDeckStorage();
        _testCards = new List<Card>
        {
            new Card { Suit = Suit.Hearts, Rank = Rank.Ace },
            new Card { Suit = Suit.Diamonds, Rank = Rank.King },
            new Card { Suit = Suit.Spades, Rank = Rank.Seven },
            new Card { Suit = Suit.Clubs, Rank = Rank.Queen },
            new Card { Suit = Suit.Clubs, Rank = Rank.Jack },
            new Card { Suit = Suit.Spades, Rank = Rank.Nine }
        };
        _shuffleAlgorithm = new SimpleShuffleAlgorithm();
    }

    [Fact]
    public void CreateDeck_ValidInput_CreatesDeck()
    {
        _deckStorage.CreateDeck("TestDeck", _testCards, _shuffleAlgorithm);
        var deckNames = _deckStorage.GetDeckNames();

        Assert.Contains("TestDeck", deckNames);
    }

    [Fact]
    public void CreateDeck_NullName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _deckStorage.CreateDeck(null!, _testCards, _shuffleAlgorithm));
    }

    [Fact]
    public void CreateDeck_EmptyName_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _deckStorage.CreateDeck("", _testCards, _shuffleAlgorithm));
    }

    [Fact]
    public void CreateDeck_NullCards_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _deckStorage.CreateDeck("TestDeck", null!, _shuffleAlgorithm));
    }

    [Fact]
    public void CreateDeck_EmptyCards_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _deckStorage.CreateDeck("TestDeck", new List<Card>(), _shuffleAlgorithm));
    }

    [Fact]
    public void CreateDeck_DuplicateName_ThrowsArgumentException()
    {
        _deckStorage.CreateDeck("TestDeck", _testCards, _shuffleAlgorithm);
        Assert.Throws<ArgumentException>(() => _deckStorage.CreateDeck("TestDeck", _testCards, _shuffleAlgorithm));
    }

    [Fact]
    public void ShuffleDeck_ExistingDeck_ShufflesDeck()
    {
        _deckStorage.CreateDeck("TestDeck", _testCards, _shuffleAlgorithm);
        var deck = _deckStorage.GetDeck("TestDeck");
        var originalOrder = deck!.Cards.Select(c => c.ToString()).ToList();
        _deckStorage.ShuffleDeck("TestDeck");
        deck = _deckStorage.GetDeck("TestDeck");
        var shuffledOrder = deck!.Cards.Select(c => c.ToString()).ToList();

        Assert.NotEqual(originalOrder, shuffledOrder);
    }

    [Fact]
    public void ShuffleDeck_NonExistingDeck_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _deckStorage.ShuffleDeck("NonExistingDeck"));
    }

    [Fact]
    public void DeleteDeck_ExistingDeck_DeletesDeck()
    {
        _deckStorage.CreateDeck("TestDeck", _testCards, _shuffleAlgorithm);
        _deckStorage.DeleteDeck("TestDeck");
        var deckNames = _deckStorage.GetDeckNames();

        Assert.DoesNotContain("TestDeck", deckNames);
    }

    [Fact]
    public void DeleteDeck_NonExistingDeck_ThrowsArgumentException()
    {
        Assert.Throws<ArgumentException>(() => _deckStorage.DeleteDeck("NonExistingDeck"));
    }

    [Fact]
    public void GetDeckNames_ReturnsCorrectNames()
    {
        _deckStorage.CreateDeck("TestDeck1", _testCards, _shuffleAlgorithm);
        _deckStorage.CreateDeck("TestDeck2", _testCards, _shuffleAlgorithm);
        var deckNames = _deckStorage.GetDeckNames();

        Assert.Contains("TestDeck1", deckNames);
        Assert.Contains("TestDeck2", deckNames);
    }

    [Fact]
    public void GetDeck_ExistingDeck_ReturnsDeckCopy()
    {
        _deckStorage.CreateDeck("TestDeck", _testCards, _shuffleAlgorithm);
        var deck = _deckStorage.GetDeck("TestDeck");

        Assert.NotNull(deck);
        Assert.Equal("TestDeck", deck!.Name);
        Assert.Equal(_testCards.Count, deck.Cards.Count);
    }

    [Fact]
    public void GetDeck_NonExistingDeck_ReturnsNull()
    {
        var deck = _deckStorage.GetDeck("NonExistingDeck");

        Assert.Null(deck);
    }

    [Fact]
    public void AreDecksInSameOrder_SameOrder_ReturnsTrue()
    {
       _deckStorage.CreateDeck("TestDeck1", _testCards, _shuffleAlgorithm);
       _deckStorage.CreateDeck("TestDeck2", _testCards, _shuffleAlgorithm);

       Assert.True(_deckStorage.AreDecksInSameOrder("TestDeck1", "TestDeck2"));
    }

     [Fact]
    public void AreDecksInSameOrder_DifferentOrder_ReturnsFalse()
    {
       _deckStorage.CreateDeck("TestDeck1", _testCards, _shuffleAlgorithm);
       _deckStorage.CreateDeck("TestDeck2", new List<Card>
       {
           new Card { Suit = Suit.Diamonds, Rank = Rank.King },
           new Card { Suit = Suit.Hearts, Rank = Rank.Ace }
       }, _shuffleAlgorithm);

       Assert.False(_deckStorage.AreDecksInSameOrder("TestDeck1", "TestDeck2"));
    }

    [Fact]
    public void AreDecksInSameOrder_NonExistingDeck1_ThrowsArgumentException()
    {
        _deckStorage.CreateDeck("TestDeck2", _testCards, _shuffleAlgorithm);
        Assert.Throws<ArgumentException>(() => _deckStorage.AreDecksInSameOrder("NonExistingDeck", "TestDeck2"));
    }

    [Fact]
    public void AreDecksInSameOrder_NonExistingDeck2_ThrowsArgumentException()
    {
        _deckStorage.CreateDeck("TestDeck1", _testCards, _shuffleAlgorithm);
        Assert.Throws<ArgumentException>(() => _deckStorage.AreDecksInSameOrder("TestDeck1", "NonExistingDeck"));
    }
}
