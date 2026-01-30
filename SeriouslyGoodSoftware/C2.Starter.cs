namespace SeriouslyGoodSoftware;

public sealed class C2Starter
{
    public sealed class Container : IContainer
    {

        private HashSet<Container> group;

        /* Creates an empty container. */
        public Container()
        {
            group =
            [
                this
            ];
        }

        public decimal Amount { get; set; }

        /* Connects this container with other. */
        public void ConnectTo(Container other)
        {

            // If they are already connected, do nothing
            if (group == other.group) return;

            int size1 = group.Count,
                size2 = other.group.Count;
            decimal tot1 = Amount * size1,
                tot2 = other.Amount * size2,
                newAmount = (tot1 + tot2) / (size1 + size2);

            // Merge the two groups
            group.UnionWith(other.group);
            // Update group of containers connected with other
            foreach (Container c in other.group)
            {
                c.group = group;
            }
            // Update amount of all newly connected containers
            foreach (Container c in group)
            {
                c.Amount = newAmount;
            }
        }

        public void ConnectTo(IContainer other)
        {
            ConnectTo((other as Container)!);
        }

        /* Adds water to this container. */
        public void AddWater(decimal amount)
        {
            decimal amountPerContainer = amount / group.Count;
            foreach (Container c in group)
            {
                c.Amount += amountPerContainer;
            }
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
