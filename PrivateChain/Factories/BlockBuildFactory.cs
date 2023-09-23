using PrivateChain.Builders;
using PrivateChain.Model;

namespace PrivateChain.Factories
{
    public class BlockBuildFactory : IBlockBuilderFactory
    {
        private readonly TransactionBaseConverter _transactionBaseConverter;
    
        public BlockBuildFactory(TransactionBaseConverter transactionBaseConverter)
        {
                this._transactionBaseConverter = transactionBaseConverter;
        }

        public BlockBuilder GetInstance()
        {
            return new BlockBuilder(this._transactionBaseConverter);
        }   
    }
}