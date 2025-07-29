using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.ShuffleAlgorithms;

public interface IShuffleAlgorithm
{
    ShuffleAlgorithmType Type { get; }

    void Shuffle(List<Card> deck);
}