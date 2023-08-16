public class GalaxyShipClass
{
    public IAttackStrategy Strategy { get; set; } = new BalanceStrategy();
}
