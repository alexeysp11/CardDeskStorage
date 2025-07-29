using Microsoft.EntityFrameworkCore;
using CardDeskStorage.Library.DbContexts;
using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

public class DeckStorageFromDb : IDeckStorage
{
    private readonly DeckStorageDbContext _context;

    public DeckStorageFromDb(DeckStorageDbContext context)
    {
        _context = context;
    }

    public void CreateDeck(string name, List<Card> cards, IShuffleAlgorithm shuffleAlgorithm)
    {
        var deck = new Deck
        {
            Name = name,
            ShuffleAlgorithmType = shuffleAlgorithm?.Type == null ? ShuffleAlgorithmType.None : shuffleAlgorithm.Type,
            Cards = cards.Select(c => new Card { Suit = c.Suit, Rank = c.Rank }).ToList()
        };

        _context.Decks.Add(deck);
        _context.SaveChanges();
    }

    public void ShuffleDeck(string name)
    {
        var deck = _context.Decks
            .Include(d => d.Cards)
            .FirstOrDefault(d => d.Name == name);

        if (deck == null)
        {
            throw new Exception($"Deck with name '{name}' not found");
        }
        if (!deck.ShuffleAlgorithmType.HasValue)
        {
            throw new Exception($"Field '{nameof(deck.ShuffleAlgorithmType)}' is not initialized for the deck '{name}'");
        }

        deck.ShuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(deck.ShuffleAlgorithmType.Value);
        deck.ShuffleAlgorithm?.Shuffle(deck.Cards);

        _context.SaveChanges();
    }

    public void DeleteDeck(string name)
    {
        var deck = _context.Decks.FirstOrDefault(d => d.Name == name);

        if (deck == null)
        {
            throw new Exception($"Deck with name '{name}' not found");
        }

        _context.Decks.Remove(deck);
        _context.SaveChanges();
    }

    public IReadOnlyCollection<string> GetDeckNames()
    {
        return _context.Decks.Select(d => d.Name).ToList().AsReadOnly();
    }

    public Deck? GetDeck(string name)
    {
        var deck = _context.Decks
            .Include(d => d.Cards)
            .FirstOrDefault(d => d.Name == name);

        if (deck == null)
        {
            return null;
        }
        if (!deck.ShuffleAlgorithmType.HasValue)
        {
            throw new Exception($"Field '{nameof(deck.ShuffleAlgorithmType)}' is not initialized for the deck '{name}'");
        }

        // Create a new "Deck" object for avoid reference the EF object
        Deck result = new Deck()
        {
            Id = deck.Id,
            Name = deck.Name,
            ShuffleAlgorithmType = deck.ShuffleAlgorithmType,
            ShuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(deck.ShuffleAlgorithmType.Value),
            Cards = deck.Cards.Select(c => new Card { Suit = c.Suit, Rank = c.Rank }).ToList()
        };

        return result;
    }

    public bool AreDecksInSameOrder(string deckName1, string deckName2)
    {
        var deck1 = _context.Decks
            .Include(d => d.Cards)
            .FirstOrDefault(d => d.Name == deckName1);

        var deck2 = _context.Decks
            .Include(d => d.Cards)
            .FirstOrDefault(d => d.Name == deckName2);

        if (deck1 == null)
        {
            throw new Exception($"Deck with name '{deckName1}' not found");
        }
        if (!deck1.ShuffleAlgorithmType.HasValue)
        {
            throw new Exception($"Field '{nameof(deck1.ShuffleAlgorithmType)}' is not initialized for the deck '{deckName1}'");
        }

        if (deck2 == null)
        {
            throw new Exception($"Deck with name '{deckName2}' not found");
        }
        if (!deck2.ShuffleAlgorithmType.HasValue)
        {
            throw new Exception($"Field '{nameof(deck2.ShuffleAlgorithmType)}' is not initialized for the deck '{deckName2}'");
        }

        deck1.ShuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(deck1.ShuffleAlgorithmType.Value);
        deck2.ShuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(deck2.ShuffleAlgorithmType.Value);

        return deck1.IsSameOrder(deck2);
    }
}