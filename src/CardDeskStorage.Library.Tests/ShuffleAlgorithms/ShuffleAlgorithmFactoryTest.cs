using CardDeskStorage.Library.Models;
using CardDeskStorage.Library.ShuffleAlgorithms;

namespace CardDeskStorage.Library.Tests.ShuffleAlgorithms
{
    public class ShuffleAlgorithmFactoryTest
    {
        [Theory]
        [InlineData(ShuffleAlgorithmType.Simple, typeof(SimpleShuffleAlgorithm))]
        public static void CreateShuffleAlgorithm_ValidAlgorithm_NotNull(ShuffleAlgorithmType shuffleAlgorithmType, Type expectedType)
        {
            IShuffleAlgorithm? shuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(shuffleAlgorithmType);
            Assert.NotNull(shuffleAlgorithm);
            Assert.Equal(expectedType, shuffleAlgorithm?.GetType());
        }

        [Theory]
        [InlineData(ShuffleAlgorithmType.None)]
        public static void CreateShuffleAlgorithm_NotValidAlgorithm_Null(ShuffleAlgorithmType shuffleAlgorithmType)
        {
            IShuffleAlgorithm? shuffleAlgorithm = ShuffleAlgorithmFactory.CreateShuffleAlgorithm(shuffleAlgorithmType);
            Assert.Null(shuffleAlgorithm);
        }
    }
}
