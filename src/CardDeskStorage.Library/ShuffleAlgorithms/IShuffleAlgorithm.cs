using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.ShuffleAlgorithms;

public interface IShuffleAlgorithm
{
    void Shuffle(List<Card> deck);
}

public class SimpleShuffleAlgorithm : IShuffleAlgorithm
{
    private readonly Random _random = new Random();

    public void Shuffle(List<Card> deck)
    {
        int n = deck.Count;
        for (int i = n - 1; i > 0; i--)
        {
            int j = _random.Next(i + 1);
            
            // Swap.
            (deck[i], deck[j]) = (deck[j], deck[i]);
        }
    }
}

