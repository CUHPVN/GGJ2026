using System.Collections.Generic;
using UnityEngine;

public class ModifierRegistry : ScriptableObject
{
    public List<ModifierDefinition> AllDefinitions;
    private Dictionary<ModifierID, ModifierDefinition> _cache;

    public void Initialize()
    {
        _cache = new Dictionary<ModifierID, ModifierDefinition>();
        foreach (var def in AllDefinitions) _cache[def.ID] = def;
    }

    public ModifierDefinition GetDefinition(ModifierID id)
    {
        if (_cache == null) Initialize();
        return _cache.TryGetValue(id, out var def) ? def : null;
    }
}