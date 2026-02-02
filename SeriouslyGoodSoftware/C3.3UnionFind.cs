namespace SeriouslyGoodSoftware;

public sealed class C33UnionFind
{
    /// <summary>
    /// This implementation keeps a representative from its group using the Union-Find data structure.
    /// This allows both ConnectTo and AddWater to be very efficient.
    /// The Amount property retrieves the amount from the representative.
    ///
    /// It suggests to represent a group as a tree of containers, where each container only needs to know its parent in the tree.
    /// The root of each tree is the representative for the group. Roots also should store the size of their tree.
    ///
    /// To have good performance two techniques need to be implemented:
    /// 1. Path compression: when looking for the representative of a container, we make all containers on the path point directly to the root.
    /// 2. Union by size: when connecting two containers, we always attach the smaller tree to the root of the larger tree.
    /// </summary>
    public class Container : IContainer
    {
        private Container parent;
        private int size;
        private decimal amount;

        public Container()
        {
            parent = this; // initially each container is its own parent (root)
            size = 1;
        }

        public decimal Amount
        {
            get => FindRootAndCompressPath().amount;

            private set => amount = value;
        }

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

            // Union by size: attach smaller tree to larger tree
            if (root1.size < root2.size)
            {
                // root1 becomes child of root2
                root1.parent = root2;
                root2.amount = (root1.amount * root1.size + root2.amount * root2.size) / (root1.size + root2.size);
                root2.size += root1.size;
            }
            else
            {
                // root2 becomes child of root1
                root2.parent = root1;
                root1.amount = (root1.amount * root1.size + root2.amount * root2.size) / (root1.size + root2.size);
                root1.size += root2.size;
            }
        }

        public void AddWater(decimal amount)
        {
            Container root = FindRootAndCompressPath();
            root.Amount += amount / root.size;
        }

        // Recursively finds the root of the container and applies path compression
        // So all parents on the path point directly to the root
        Container FindRootAndCompressPath()
        {
            if (parent != this)
            {
                parent = parent.FindRootAndCompressPath();
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
