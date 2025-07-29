using System.ComponentModel.DataAnnotations.Schema;
using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

public class Deck
{
    public string? Name { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
    public ShuffleAlgorithmType? ShuffleAlgorithmType { get; set; }

    [NotMapped]
    public IShuffleAlgorithm? ShuffleAlgorithm
    {
        get
        {
            if (!ShuffleAlgorithmType.HasValue)
            {
                ShuffleAlgorithmType = Models.ShuffleAlgorithmType.Simple;
            }
            if (_shuffleAlgorithm == null)
            {
                _shuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(ShuffleAlgorithmType.Value);
                if (_shuffleAlgorithm == null)
                {
                    throw new InvalidOperationException($"Could not create shuffle algorithm of type {ShuffleAlgorithmType}");
                }
            }
            return _shuffleAlgorithm;
        }
        set
        {
            _shuffleAlgorithm = value;
        }
    }

    private IShuffleAlgorithm? _shuffleAlgorithm;

    public void Shuffle()
    {
        _shuffleAlgorithm?.Shuffle(Cards);
    }

    public Deck Copy()
    {
        return new Deck
        {
            Name = Name,
            Cards = new List<Card>(Cards),
            ShuffleAlgorithm = _shuffleAlgorithm,
            ShuffleAlgorithmType = ShuffleAlgorithmType
        };
    }

    public bool IsSameOrder(Deck otherDeck)
    {
        if (otherDeck == null)
        {
            throw new ArgumentNullException(nameof(otherDeck));
        }

        if (Cards.Count != otherDeck.Cards.Count)
        {
            return false;
        }

        for (int i = 0; i < Cards.Count; i++)
        {
            if (!Cards[i].Equals(otherDeck.Cards[i]))
            {
                return false;
            }
        }

        return true;
    }
}
