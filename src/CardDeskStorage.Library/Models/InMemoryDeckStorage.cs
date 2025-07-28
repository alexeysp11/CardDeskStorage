using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

public class InMemoryDeckStorage : IDeckStorage
{
    private readonly Dictionary<string, Deck> _decks = new Dictionary<string, Deck>();

    public void CreateDeck(string name, List<Card> cards, IShuffleAlgorithm shuffleAlgorithm)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Deck name cannot be null or empty", nameof(name));
        }

        if (cards == null || cards.Count == 0)
        {
            throw new ArgumentException("Deck must contain cards", nameof(cards));
        }

        if (_decks.ContainsKey(name))
        {
            throw new ArgumentException($"A deck with the name '{name}' already exists");
        }

        _decks[name] = new Deck(name, cards, shuffleAlgorithm);
    }

    public void ShuffleDeck(string name)
    {
        if (!_decks.ContainsKey(name))
        {
            throw new ArgumentException($"Deck with name '{name}' not found");
        }

        _decks[name].Shuffle();
    }

    public void DeleteDeck(string name)
    {
        if (!_decks.ContainsKey(name))
        {
            throw new ArgumentException($"Deck with name '{name}' not found");
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
            return _decks[name].Copy();
        }

        return null;
    }

    public bool AreDecksInSameOrder(string deckName1, string deckName2)
    {
        if (!_decks.ContainsKey(deckName1))
        {
            throw new ArgumentException($"Deck with name '{deckName1}' not found");
        }

        if (!_decks.ContainsKey(deckName2))
        {
            throw new ArgumentException($"Deck with name '{deckName2}' not found");
        }

        return _decks[deckName1].IsSameOrder(_decks[deckName2]);
    }
}
