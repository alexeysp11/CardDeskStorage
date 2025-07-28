using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.DeckFactories;

public static class StandardDeckFactory
{
    public static List<Card> CreateStandardDeck()
    {
        List<Card> deck = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new Card(suit, rank));
            }
        }
        return deck;
    }
}
