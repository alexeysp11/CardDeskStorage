using CardDeskStorage.Library.Models;

namespace CardDeskStorage.Library.ShuffleAlgorithms;

public static class ShuffleAlgorithmFactory
{
    public static IShuffleAlgorithm? CreateShuffleAlgorithm(ShuffleAlgorithmType shuffleAlgorithmType)
    {
        IShuffleAlgorithm? shuffleAlgorithm = null;
        switch (shuffleAlgorithmType)
        {
            case ShuffleAlgorithmType.Simple:
                shuffleAlgorithm = new SimpleShuffleAlgorithm();
                break;
        }
        return shuffleAlgorithm;
    }
}
