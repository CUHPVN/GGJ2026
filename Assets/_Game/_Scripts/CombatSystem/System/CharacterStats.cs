public class CharacterStats
{
    public float BaseStr, BaseInt, BaseDex;
    public float BonusStr, BonusInt, BonusDex;
    private float str;
    private float intel;
    private float dex;

    public CharacterStats(float str, float intel, float dex)
    {
        this.str = str;
        this.intel = intel;
        this.dex = dex;
    }

    public float TotalStr => BaseStr + BonusStr;
    public float TotalInt => BaseInt + BonusInt;
    public float TotalDex => BaseDex + BonusDex;

    public float MaxHP => TotalStr * 10f;
    public float PAtk => TotalStr * 1.5f;
    public float PDef => TotalStr * 0.5f;
    public float HPRegen => TotalStr * 0.2f;

    public float MAtk => TotalInt * 3f;
    public float MDef => TotalInt * 1f;
    public float MaxMP => TotalInt * 5f;

    public float HitRate => TotalDex * 1f;
    public float CritChance => TotalDex * 0.2f;
    public int HitCounts => 1 + (int)(TotalDex / 50f);
}