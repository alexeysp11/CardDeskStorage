using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

public interface IDeckStorage
{
    void CreateDeck(string name, List<Card> cards, IShuffleAlgorithm shuffleAlgorithm);
    void ShuffleDeck(string name);
    void DeleteDeck(string name);
    IReadOnlyCollection<string> GetDeckNames();
    Deck? GetDeck(string name);
    bool AreDecksInSameOrder(string deckName1, string deckName2);
}

public class InMemoryDeckStorage : IDeckStorage
{
    private readonly Dictionary<string, Deck> _decks = new Dictionary<string, Deck>();

    public void CreateDeck(string name, List<Card> cards, IShuffleAlgorithm shuffleAlgorithm)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Deck name cannot be null or empty.", nameof(name));
        }

        if (cards == null || cards.Count == 0)
        {
            throw new ArgumentException("Deck must contain cards.", nameof(cards));
        }

        if (_decks.ContainsKey(name))
        {
            throw new ArgumentException($"A deck with the name '{name}' already exists.");
        }

        _decks[name] = new Deck(name, cards, shuffleAlgorithm);
    }

    public void ShuffleDeck(string name)
    {
        if (!_decks.ContainsKey(name))
        {
            throw new ArgumentException($"Deck with name '{name}' not found.");
        }

        _decks[name].Shuffle();
    }

    public void DeleteDeck(string name)
    {
        if (!_decks.ContainsKey(name))
        {
            throw new ArgumentException($"Deck with name '{name}' not found.");
        }

        _decks.Remove(name);
    }

    public IReadOnlyCollection<string> GetDeckNames()
    {
        return _decks.Keys.ToList().AsReadOnly();
    }

    public Deck? GetDeck(string name)
    {
        if (_decks.ContainsKey(name))
        {
            return _decks[name].Copy(); //Возвращаем копию колоды, чтобы внешние изменения не влияли на оригинал.
        }

        return null;
    }

    public bool AreDecksInSameOrder(string deckName1, string deckName2)
    {
        if (!_decks.ContainsKey(deckName1))
        {
            throw new ArgumentException($"Deck with name '{deckName1}' not found.");
        }

        if (!_decks.ContainsKey(deckName2))
        {
            throw new ArgumentException($"Deck with name '{deckName2}' not found.");
        }

        return _decks[deckName1].IsSameOrder(_decks[deckName2]);
    }
}

public static class StandardDeckFactory
{
    public static List<Card> CreateStandardDeck()
    {
        List<Card> deck = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new Card(suit, rank));
            }
        }
        return deck;
    }
}

public class Deck
{
    public string Name { get; set; }

    private readonly List<Card> _cards;
    private readonly IShuffleAlgorithm _shuffleAlgorithm;

    public Deck(string name, List<Card> cards, IShuffleAlgorithm shuffleAlgorithm)
    {
        Name = name;
        _cards = new List<Card>(cards);
        _shuffleAlgorithm = shuffleAlgorithm ?? throw new ArgumentNullException(nameof(shuffleAlgorithm));
    }

    public IReadOnlyList<Card> Cards => _cards.AsReadOnly();

    public void Shuffle()
    {
        _shuffleAlgorithm.Shuffle(_cards);
    }

    public Deck Copy()
    {
        return new Deck(Name, new List<Card>(_cards), _shuffleAlgorithm);
    }

    public bool IsSameOrder(Deck otherDeck)
    {
        if (otherDeck == null)
        {
            throw new ArgumentNullException(nameof(otherDeck));
        }

        if (_cards.Count != otherDeck._cards.Count)
        {
            return false;
        }

        for (int i = 0; i < _cards.Count; i++)
        {
            if (!_cards[i].Equals(otherDeck._cards[i]))
            {
                return false;
            }
        }

        return true;
    }
}
