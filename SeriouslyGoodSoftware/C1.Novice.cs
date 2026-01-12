namespace SeriouslyGoodSoftware;

/// <summary>
///     Software qualities:
///     - time and space efficiency (chapter 3 and 4)
///     - Robust (correct and reliably chapter 5 and 6)
///     - Maintainability (testability and readability chapters 6 and 7)
///     - Thread safe (chapter 8)
///     - Reusable (chapter 9)
///     Functional qualities. What software does.
///     Non-functional qualities. How software is.
///     Multi criteria optimization. finding optimal solutions with respect to multiple competing quality measures.
///     Recurring example: A system of water containers
///     - A container holds and amount of liquid
///     - Any two containers can be connected via a pipe
///     - Liquid flows from one container to another via the pipe until both containers have the same amount of liquid
///     - Containers can be filled or drained
///     - Containers can be connected and disconnected at any time
/// </summary>
public sealed class C1
{
    /// <summary>
    ///     Novice implementation of a container.
    ///     Focuses on correctness only, doesn't consider future cases like for example remove direct connections, and it stores indirect
    ///     connections. Pipe representation is avoided for simplicity.
    ///     This implementation has a lot of issues like time and space efficiency, maintainability, readability and reusability.
    ///     On top of that it doesn't have error handling or considers edge cases like graphs of containers indirectly connected.
    /// </summary>
    private sealed class Container
    {
        public Container(decimal x = 0)
        {
            X = x;
            N = 1;
            G = new Container[100];
            G[0] = this;
        }

        // Array is not appropriate as it puts a fixed limit on the group of containers
        public Container[] G;

        // Number of containers in the group
        public int N { get; set; }

        // Amount
        public decimal X { get; set; }

        public void ConnectTo(Container c)
        {
            decimal z = (X * N + c.X * c.N) / (N + c.N);

            for (int i = 0; i < N; i++) // for each container g[i] in first group
            {
                for (int j = 0; j < c.N; j++) // for each container c.g[j] in second group
                {
                    G[i].G[N + j] = c.G[j]; // add second to group of first
                    c.G[j].G[c.N + i] = G[i]; // add first to group of second
                }
            }

            N += c.N;

            for (int i = 0; i < N; i++)
            {
                G[i].N = N;
                G[i].X = z;
            }
        }

        public void Fill(decimal x)
        {
            decimal y = x / N;
            for (int i = 0; i < N; i++)
            {
                G[i].X += y;
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
        Assert.Equal(0, a.X);
        Assert.Equal(0, b.X);
        Assert.Equal(0, c.X);
        Assert.Equal(0, d.X);

        a.Fill(12);
        d.Fill(8);
        Assert.Equal(12, a.X);
        Assert.Equal(0, b.X);
        Assert.Equal(0, c.X);
        Assert.Equal(8, d.X);

        a.ConnectTo(b);
        // a.ConnectTo(b); // case not covered
        Assert.Equal(6, a.X);
        Assert.Equal(6, b.X);
        Assert.Equal(0, c.X);
        Assert.Equal(8, d.X);

        b.ConnectTo(c);
        // a.ConnectTo(c); // case not covered
        Assert.Equal(4, a.X);
        Assert.Equal(4, b.X);
        Assert.Equal(4, c.X);
        Assert.Equal(8, d.X);

        c.ConnectTo(d);
        Assert.Equal(5, a.X);
        Assert.Equal(5, b.X);
        Assert.Equal(5, c.X);
        Assert.Equal(5, d.X);
    }
}
