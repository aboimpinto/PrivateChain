using PrivateChain.Model;

namespace PrivateChain.Services.Blockchain
{
    public interface IBlockchainService
    {
        BlockchainInfo BlockchainInfo { get; }
    }
}