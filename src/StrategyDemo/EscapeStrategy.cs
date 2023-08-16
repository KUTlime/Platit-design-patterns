// All energy goes to the engines, nothing to weapons.
public class EscapeStrategy : IAttackStrategy
{
    public decimal GetDamage() => 0m;

    public decimal GetSpeed() => 100m;
}
