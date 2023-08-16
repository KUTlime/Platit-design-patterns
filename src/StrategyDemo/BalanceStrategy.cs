// Energy is distributed between engines and weapons.
public class BalanceStrategy : IAttackStrategy
{
    public decimal GetDamage() => 50m;

    public decimal GetSpeed() => 50m;
}
