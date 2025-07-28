using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

public class Deck
{
    public string Name { get; set; }
    private List<Card> Cards { get; set; }

    private readonly IShuffleAlgorithm _shuffleAlgorithm;

    public Deck(string name, List<Card> cards, IShuffleAlgorithm shuffleAlgorithm)
    {
        Name = name;
        Cards = new List<Card>(cards);
        _shuffleAlgorithm = shuffleAlgorithm ?? throw new ArgumentNullException(nameof(shuffleAlgorithm));
    }

    public void Shuffle()
    {
        _shuffleAlgorithm.Shuffle(Cards);
    }

    public Deck Copy()
    {
        return new Deck(Name, new List<Card>(Cards), _shuffleAlgorithm);
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
