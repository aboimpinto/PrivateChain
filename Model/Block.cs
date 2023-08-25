using System.Text.Json;

namespace PrivateChain.Model;

public class Block
{
    public string BlockId { get; } = string.Empty;

    public string TimeStamp { get; } = string.Empty;

    public IEnumerable<TransactionBase> Transactions { get; set; }

    public double Index { get; }

    public string Hash { get; private set; } = string.Empty;

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

    // public override string ToString()
    // { 
    //     return JsonSerializer.Serialize(this, new JsonSerializerOptions
    //     {
    //         Converters = { new TransactionBaseConverter() }
    //     });
    // }
}