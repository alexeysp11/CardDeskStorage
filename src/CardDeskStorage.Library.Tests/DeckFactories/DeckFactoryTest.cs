using CardDeskStorage.Library.DeckFactories;
using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.Tests.DeckFactories
{
    public class DeckFactoryTest
    {
        [Fact]
        public void CreateStandardDeck()
        {
            List<Card> cards = DeckFactory.CreateStandardDeck();
            Assert.Equal(52, cards.Count);
        }

        [Fact]
        public void CreateRussian36CardDeck()
        {
            List<Card> cards = DeckFactory.CreateRussian36CardDeck();
            Assert.Equal(36, cards.Count);
        }
    }
}