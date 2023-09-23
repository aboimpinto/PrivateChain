using PrivateChain.Builders;

namespace PrivateChain.Factories
{
    public interface IBlockBuilderFactory
    {
        BlockBuilder GetInstance();
    }
}