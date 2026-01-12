namespace SeriouslyGoodSoftware;

public sealed class C1Personal
{
    private sealed class Container(decimal amount = 0)
    {
        Guid id = Guid.CreateVersion7();

        Dictionary<Guid, Container> connectedContainers = new();

        public decimal Amount { get; set; }

        public void ConnectTo(Container other)
        {
            connectedContainers.TryAdd(other.id, other);
            other.connectedContainers.TryAdd(id, this);
            Fill(0);
        }

        public void Fill(decimal amount)
        {
            Dictionary<Guid, Container> allContainers = new() { { id, this } };
            foreach (Container direct in GetDirectConnectedContainers())
            {
                foreach (Container indirect in direct.GetDirectConnectedContainers())
                {
                    allContainers.TryAdd(indirect.id, indirect);
                }
                allContainers.TryAdd(direct.id, direct);
            }

            decimal newAmount = (allContainers.Values.Sum(c => c.Amount) + amount) / allContainers.Count;
            foreach (Container container in allContainers.Values)
            {
                container.Amount = newAmount;
            }
        }

        List<Container> GetDirectConnectedContainers() => connectedContainers.Values.ToList();
    }

    [Fact]
    public void UseCase1()
    {
        Container a = new();
        Container b = new();
        Container c = new();
        Container d = new();
        Assert.Equal(0, a.Amount);
        Assert.Equal(0, b.Amount);
        Assert.Equal(0, c.Amount);
        Assert.Equal(0, d.Amount);

        a.Fill(12);
        d.Fill(8);
        Assert.Equal(12, a.Amount);
        Assert.Equal(0, b.Amount);
        Assert.Equal(0, c.Amount);
        Assert.Equal(8, d.Amount);

        a.ConnectTo(b);
        a.ConnectTo(b); // case not cover by novice
        Assert.Equal(6, a.Amount);
        Assert.Equal(6, b.Amount);
        Assert.Equal(0, c.Amount);
        Assert.Equal(8, d.Amount);

        b.ConnectTo(c);
        a.ConnectTo(c); // case not cover by novice
        Assert.Equal(4, a.Amount);
        Assert.Equal(4, b.Amount);
        Assert.Equal(4, c.Amount);
        Assert.Equal(8, d.Amount);

        c.ConnectTo(d);
        Assert.Equal(5, a.Amount);
        Assert.Equal(5, b.Amount);
        Assert.Equal(5, c.Amount);
        Assert.Equal(5, d.Amount);
    }
}
