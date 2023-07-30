namespace PrivateChain.EventMessages
{
    public class BlockchainInitializedFromGenesisEvent
    {
        public string GenesisBlockId { get; }
        public string NextBlockId { get; }

        public BlockchainInitializedFromGenesisEvent(string genesisBlockId, string nextBlockId)
        {
            this.GenesisBlockId = genesisBlockId;
            this.NextBlockId = nextBlockId;
        }
    }
}