// All energy goes to the weapon systems, nothing to engine.
public class TotalAttackStrategy : IAttackStrategy
{
    public decimal GetDamage() => 100m;

    public decimal GetSpeed() => 0m;
}
