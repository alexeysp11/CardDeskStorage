namespace CardDeskStorage.Library.Models;

public class Card
{
    public int Id { get; set; }

    public Suit Suit { get; set; }
    public Rank Rank { get; set; }

    public override string ToString()
    {
        return $"{Rank} of {Suit}";
    }

    public override bool Equals(object? obj)
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
