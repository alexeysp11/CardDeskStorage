using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Models;

public interface IDeckStorage
{
    void CreateDeck(string name, List<Card> cards, IShuffleAlgorithm shuffleAlgorithm);
    void ShuffleDeck(string name);
    void DeleteDeck(string name);
    IReadOnlyCollection<string> GetDeckNames();
    Deck? GetDeck(string name);
    bool AreDecksInSameOrder(string deckName1, string deckName2);
}
