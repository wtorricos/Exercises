namespace SeriouslyGoodSoftware;

public sealed class C31SharedGroup
{
    /// <summary>
    /// Use a shared group object to track connected containers.
    /// This will allow to reduce Amount and AddWater to O(1).
    /// While ConnectTo becomes O(n).
    /// Note that this implementation does not handle connecting the same container multiple times.
    /// </summary>
    public sealed class Container
    {
        private Group group;

        public Container(decimal totalAmount = 0)
        {
            group = new Group();
            group.Containers.AddFirst(this);
            group.TotalAmount = totalAmount;
        }

        public decimal Amount => group.TotalAmount / group.Containers.Count;

        public void ConnectTo(Container other)
        {
            foreach (Container container in other.group.Containers)
            {
                group.Containers.AddLast(container);
            }
            group.TotalAmount += other.group.TotalAmount;
            other.group = group;
        }

        public void AddWater(decimal amount)
        {
            group.TotalAmount += amount;
        }

        private class Group
        {
            public LinkedList<Container> Containers { get; set; } = new();

            public decimal TotalAmount { get; set; }
        }
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

        a.AddWater(12);
        d.AddWater(8);
        Assert.Equal(12, a.Amount);
        Assert.Equal(0, b.Amount);
        Assert.Equal(0, c.Amount);
        Assert.Equal(8, d.Amount);

        a.ConnectTo(b);
        // a.ConnectTo(b); // case not covered
        Assert.Equal(6, a.Amount);
        Assert.Equal(6, b.Amount);
        Assert.Equal(0, c.Amount);
        Assert.Equal(8, d.Amount);

        b.ConnectTo(c);
        // a.ConnectTo(c); // case not covered
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
