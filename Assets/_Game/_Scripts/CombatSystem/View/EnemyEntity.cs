public class EnemyEntity : EntityView
{

    public void DecideAction(out bool isMagical)
    {
   
        isMagical = Stats.TotalInt > Stats.TotalStr;
    }
}