namespace CardDeskStorage.Library.Models;

public class Card
{
    public Suit Suit { get; }
    public Rank Rank { get; }

    public Card(Suit suit, Rank rank)
    {
        Suit = suit;
        Rank = rank;
    }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }

    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }

        Card other = (Card)obj;
        return Suit == other.Suit && Rank == other.Rank;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Suit, Rank);
    }
}

