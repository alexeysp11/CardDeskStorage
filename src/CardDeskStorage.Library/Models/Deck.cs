using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

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
