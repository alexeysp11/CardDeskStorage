using System.ComponentModel.DataAnnotations.Schema;
using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

public class Deck
{
    public int Id { get; set; }
    public string? Name { get; set; }
    
    public ShuffleAlgorithmType? ShuffleAlgorithmType { get; set; }

    [NotMapped]
    public IShuffleAlgorithm? ShuffleAlgorithm { get; set; }

    public List<Card> Cards { get; set; } = new List<Card>();

    public void Shuffle()
    {
        CheckShuffleAlgorithmCreated();
        ShuffleAlgorithm?.Shuffle(Cards);
    }

    public Deck Copy()
    {
        CheckShuffleAlgorithmCreated();
        return new Deck
        {
            Name = Name,
            Cards = new List<Card>(Cards),
            ShuffleAlgorithm = ShuffleAlgorithm,
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

    public void CheckShuffleAlgorithmCreated()
    {
        if (!ShuffleAlgorithmType.HasValue)
        {
            ShuffleAlgorithmType = Models.ShuffleAlgorithmType.Simple;
        }
        if (ShuffleAlgorithm == null)
        {
            IShuffleAlgorithm? shuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(ShuffleAlgorithmType.Value);
            if (shuffleAlgorithm == null)
            {
                throw new InvalidOperationException($"Could not create shuffle algorithm of type {ShuffleAlgorithmType}");
            }
            ShuffleAlgorithm = shuffleAlgorithm;
        }
    }
}
