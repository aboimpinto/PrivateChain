namespace PrivateChain.Model;

public class Block
{
    public string BlockId { get; } = string.Empty;

    public string TimeStamp { get; } = string.Empty;

    public IEnumerable<TransactionBase> Transactions { get; }

    public double Index { get; }

    public string Hash { get; private set; }

    public string PreviousBlockId { get; } = string.Empty;

    public string NextBlockId { get; } = string.Empty;


    public Block(string blockId, string previousBlockId, string nextBlockId, double index)
    {
        this.Transactions = new List<TransactionBase>();
        this.TimeStamp = DateTime.UtcNow.ToString();

        this.PreviousBlockId = previousBlockId;
        this.NextBlockId = nextBlockId;

        this.BlockId = blockId;
        this.Index = index;
    }

    public void FinalizeBlock()
    {
        this.Hash = this.GetHashCode().ToString();
    }
}