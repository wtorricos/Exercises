namespace SeriouslyGoodSoftware;

public sealed class C32CircularList
{
    /// <summary>
    /// This time the implementation uses a circular linked list to track connected containers.
    /// This is a tradeoff that allows ConnectTo to be O(1) while keeping AddWater O(1) as well.
    /// However, retrieving the Amount will force a recalculation making it O(n).
    /// Note that this implementation does not handle connecting the same container multiple times.
    /// </summary>
    public class Container : IContainer
    {
        private decimal amount;

        private Container next;

        public Container()
        {
            next = this;
            amount = 0;
        }

        public decimal Amount
        {
            get
            {
                UpdateGroup();
                return amount;
            }
        }

        public void ConnectTo(Container other)
        {
            (next, other.next) = (other.next, next);
        }

        public void ConnectTo(IContainer other)
        {
            ConnectTo((other as Container)!);
        }

        public void AddWater(decimal amount)
        {
            this.amount += amount;
        }

        private void UpdateGroup() {
            Container current = this;
            decimal totalAmount = 0;
            int groupSize = 0;
            // First pass: collect amounts and count
            do {
                totalAmount += current.amount;
                groupSize++;
                current = current.next;
            } while (current != this);
            decimal newAmount = totalAmount / groupSize;
            current = this;
            // Second pass: update amounts
            do {
                current.amount = newAmount;
                current = current.next;
            } while (current != this);
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
