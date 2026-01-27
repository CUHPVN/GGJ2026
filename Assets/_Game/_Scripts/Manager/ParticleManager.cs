using UnityEngine;

public class ParticleManager : Singleton<ParticleManager>
{
    public void PlayStopSlotVFX(Vector2 pos)
    {
        SimplePool.Spawn<VFXUnit>(PoolType.SlotStopVFX, pos,Quaternion.identity);
    }
}
