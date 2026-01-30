using UnityEngine;

[CreateAssetMenu(fileName = "NewModifierData", menuName = "Game/Modifier Data")]
public class ModifierDefinition : ScriptableObject
{
    public ModifierID ID; 
    public string DisplayName;

    public Sprite Sprite;

    public Sprite GetSprite()
    {
        return Sprite;
    }
}