using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.Tests.Models
{
    public class CardTest
    {
        [Theory]
        [InlineData(Suit.Hearts, Rank.Two, "Two of Hearts")]
        [InlineData(Suit.Clubs, Rank.Ace, "Ace of Clubs")]
        [InlineData(Suit.Spades, Rank.Seven, "Seven of Spades")]
        [InlineData(Suit.Diamonds, Rank.King, "King of Diamonds")]
        public void ToString_Initialize_GetDescription(Suit suit, Rank rank, string expected)
        {
            var card = new Card { Suit = suit, Rank = rank };
            string result = card.ToString();
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(Suit.Hearts, Rank.Two)]
        [InlineData(Suit.Clubs, Rank.Ace)]
        [InlineData(Suit.Spades, Rank.Seven)]
        [InlineData(Suit.Diamonds, Rank.King)]
        public void Equals_InitializeTwoEqual_GetTwoEqual(Suit suit, Rank rank)
        {
            var card1 = new Card { Suit = suit, Rank = rank };
            var card2 = new Card { Suit = suit, Rank = rank };
            bool result = card1.Equals(card2);
            Assert.True(result);
        }

        [Theory]
        [InlineData(Suit.Hearts, Rank.Two, Suit.Clubs, Rank.Ace)]
        [InlineData(Suit.Clubs, Rank.Ace, Suit.Diamonds, Rank.King)]
        [InlineData(Suit.Spades, Rank.Seven, Suit.Hearts, Rank.Two)]
        [InlineData(Suit.Diamonds, Rank.King, Suit.Spades, Rank.Jack)]
        public void Equals_InitializeTwoDifferent_GetTwoNotEqual(Suit suit1, Rank rank1, Suit suit2, Rank rank2)
        {
            var card1 = new Card { Suit = suit1, Rank = rank1 };
            var card2 = new Card { Suit = suit2, Rank = rank2 };
            bool result = card1.Equals(card2);
            Assert.False(result);
        }

        [Theory]
        [InlineData(Suit.Hearts, Rank.Two)]
        [InlineData(Suit.Clubs, Rank.Ace)]
        [InlineData(Suit.Spades, Rank.Seven)]
        [InlineData(Suit.Diamonds, Rank.King)]
        public void GetHashCode_InitializeTwoEqual_GetTwoEqual(Suit suit, Rank rank)
        {
            var card1 = new Card { Suit = suit, Rank = rank };
            var card2 = new Card { Suit = suit, Rank = rank };
            int result1 = card1.GetHashCode();
            int result2 = card2.GetHashCode();
            Assert.Equal(result1, result2);
        }
    }
}
