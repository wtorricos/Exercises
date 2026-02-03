namespace SeriouslyGoodSoftware;

public sealed class C4Memory2
{
    /// <summary>
    /// This implementation is similar to chapter 3.1 using a shared group object to track connected containers.
    /// However, it focuses on memory efficiency by using float instead of decimal and an Array instead of a set.
    /// This reduces memory usage at the cost of some precision and performance.
    /// Note that this implementation does not handle connecting the same container multiple times.
    /// Getting the amount of water has O(1) time complexity, while ConnectTo has O(n) and AddWater has O(n) time complexity.
    /// </summary>
    public class Container : IContainer
    {
        private float amount; // saved memory by using float instead of decimal

        private Container[]? group; // using an array instead of a set to save memory as well

        public decimal Amount => (decimal)amount;

        public void ConnectTo(IContainer other)
        {
            ConnectTo((Container)other);
        }

        void ConnectTo(Container other)
        {
            group ??= new Container[1] { this };

            other.group ??= new Container[1] { other };

            if (group == other.group)
            {
                return; // already connected
            }

            // merge groups
            int size1 = group.Length;
            int size2 = other.group.Length;
            float newAmount = (amount * size1 + other.amount * size2) / (size1 + size2);
            Container[] newGroup = new Container[size1 + size2];
            int i = 0;
            foreach (Container c in group)
            {
                c.group = newGroup;
                c.amount = newAmount;
                newGroup[i++] = c;
            }

            foreach (Container c in other.group)
            {
                c.group = group;
                c.amount = newAmount;
                newGroup[i++] = c;
            }
        }

        public void AddWater(decimal amount)
        {
            if (group == null)
            {
                this.amount += (float)amount;
            }
            else
            {
                float amoutPerContainer = (float)amount / group.Length;
                foreach (Container c in group)
                {
                    c.amount += amoutPerContainer;
                }
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
