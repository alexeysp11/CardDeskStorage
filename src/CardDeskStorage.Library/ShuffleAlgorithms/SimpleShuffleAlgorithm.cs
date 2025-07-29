using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.ShuffleAlgorithms;

public class SimpleShuffleAlgorithm : IShuffleAlgorithm
{
    public ShuffleAlgorithmType Type => ShuffleAlgorithmType.Simple;

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