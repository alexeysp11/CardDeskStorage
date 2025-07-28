using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.ShuffleAlgorithms;

public interface IShuffleAlgorithm
{
    void Shuffle(List<Card> deck);
}