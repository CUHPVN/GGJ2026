public class PlayerEntity : EntityView
{

    public void DecideAction(out bool isMagical)
    {
   
        isMagical = Stats.TotalInt > Stats.TotalStr;
    }
}