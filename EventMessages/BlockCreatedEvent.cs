using PrivateChain.Model;

namespace PrivateChain.EventMessages
{
    public class BlockCreatedEvent
    {
        public Block Block { get; }

        public BlockCreatedEvent(Block block)
        {
            this.Block = block;
        }
    }
}