using PrivateChain.Model;

namespace PrivateChain.Services.MemPool
{
    public interface IMemPoolService
    {
        BlockCandidate GetBlockCandidate();
    }
}