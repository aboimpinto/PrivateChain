namespace PrivateChain.Model;

public class BlockCandidate
{
    public string BlockId { get; }

    public ICollection<TransactionBase> Transactions { get; }

    public double Index { get; }

    public string PreviousBlockId { get; } = string.Empty;

    public string NextBlockId { get; } = string.Empty;

    public BlockCandidate(string candidateBlockId, double index, string previousBlockId, string nextBlockId)
    {
        this.BlockId = candidateBlockId;
        this.Index = index;
        this.Transactions = new List<TransactionBase>();
        this.PreviousBlockId = previousBlockId;
        this.NextBlockId = nextBlockId;
    }
}
