namespace SeriouslyGoodSoftware;

public interface IContainer
{
    decimal Amount { get; }

    public void ConnectTo(IContainer other);

    public void AddWater(decimal amount);
}
