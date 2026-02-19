namespace SeriouslyGoodSoftware;

public sealed class C33UnionFind2
{
    /// <summary>
    /// Based on Union-Find (Disjoint Set Union) data structure with path compression and union by size.
    /// This implementation uses a shared root container to track the amount and size of the group
    /// this way it stores the amount and size only in the root container, which reduces memory usage.
    /// </summary>
    public class Container : IContainer
    {
        private class Root
        {
            public int Size { get; set; }
            public decimal Amount { get; set; }
        }

        private Container parent;
        private Root root;

        public Container()
        {
            parent = this; // initially each container is its own parent (root)
            root = new() { Size = 1, Amount = 0 };
        }

        public decimal Amount => FindRootAndCompressPath().root.Amount;

        public void ConnectTo(IContainer other)
        {
            ConnectTo((Container)other);
        }

        void ConnectTo(Container other)
        {
            Container root1 = FindRootAndCompressPath();
            Container root2 = other.FindRootAndCompressPath();

            if (root1 == root2)
            {
                return; // already connected
            }

            decimal newAmount = (root1.root.Amount * root1.root.Size + root2.root.Amount * root2.root.Size) / (root1.root.Size + root2.root.Size);
            int newSize = root2.root.Size + root1.root.Size;

            // Union by size: attach smaller tree to larger tree
            if (root1.root.Size < root2.root.Size)
            {
                // root1 becomes child of root2
                root1.parent = root2;
                root1.root = root2.root;
                root2.root.Amount = newAmount;
                root2.root.Size = newSize;
            }
            else
            {
                // root2 becomes child of root1
                root2.parent = root1;
                root2.root = root1.root;
                root1.root.Amount = newAmount;
                root1.root.Size = newSize;
            }
        }

        public void AddWater(decimal amount)
        {
            Container root = FindRootAndCompressPath();
            root.root.Amount += amount / root.root.Size;
        }

        // Recursively finds the root of the container and applies path compression
        // So all parents on the path point directly to the root
        Container FindRootAndCompressPath()
        {
            if (parent != this)
            {
                parent = parent.FindRootAndCompressPath();
                root = parent.root;
            }

            return parent;
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
        a.ConnectTo(b); // case covered
        Assert.Equal(6, a.Amount);
        Assert.Equal(6, b.Amount);
        Assert.Equal(0, c.Amount);
        Assert.Equal(8, d.Amount);

        b.ConnectTo(c);
        a.ConnectTo(c); // case covered
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
