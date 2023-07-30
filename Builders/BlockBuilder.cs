using PrivateChain.Model;

namespace PrivateChain.Builders
{
    public class BlockBuilder
    {
        private string _previousBlockId = string.Empty;
        private string _nextBlockId = string.Empty;
        private string _blockId = string.Empty;
        private double _index;

        public BlockBuilder WithBlockId(string blockId)
        {
            this._blockId = blockId;
            return this;
        }

        public BlockBuilder WithPreviousBlockId(string previousBlockId)
        {
            this._previousBlockId = previousBlockId;
            return this;
        }

        public BlockBuilder WithNextBlockId(string nextBlockId)
        {
            this._nextBlockId = nextBlockId;
            return this;
        }

        public BlockBuilder WithIndex(double index)
        {
            this._index = index;
            return this;
        }

        public Block Build()
        {
            return new Block(
                this._blockId, 
                this._previousBlockId, 
                this._nextBlockId, 
                this._index);
        }
    }
}