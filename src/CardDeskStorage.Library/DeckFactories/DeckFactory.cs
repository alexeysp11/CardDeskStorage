using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.DeckFactories;

public static class DeckFactory
{
    public static List<Card> CreateStandardDeck()
    {
        List<Card> deck = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                deck.Add(new Card { Suit = suit, Rank = rank });
            }
        }
        return deck;
    }

    public static List<Card> CreateRussian36CardDeck()
    {
        List<Card> deck = new List<Card>();
        foreach (Suit suit in Enum.GetValues(typeof(Suit)))
        {
            foreach (Rank rank in Enum.GetValues(typeof(Rank)))
            {
                if (rank < Rank.Six)
                {
                    continue;
                }
                deck.Add(new Card{ Suit = suit, Rank = rank });
            }
        }
        return deck;
    }
}
