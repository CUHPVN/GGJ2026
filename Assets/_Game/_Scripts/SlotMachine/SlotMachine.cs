using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Slot slotPrefabs;
    [SerializeField] private int slotCount;
    [SerializeField,Range(0,10)] private float slotSpeed=1;
    [SerializeField] private int slotSymbolCount=3;
    [SerializeField] private int currentSlot = 0;
    [SerializeField] private List<Slot> slots;
    [SerializeField] private List<int> result;
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Sprite nullSprite;

    public event Action<string> PullResult;


    void Start()
    {
        Application.targetFrameRate = 120;

        for(int i = 0;i< slotCount; i++)
        {
            Slot slot = Instantiate(slotPrefabs,new Vector2(transform.position.x+i,transform.position.y),Quaternion.identity);
            slot.SetSlotMachine(this);
            slot.SetSpeed(slotSpeed);
            slot.SetSymbolCount(slotSymbolCount);
            slot.SetStop(false);
            slot.transform.SetParent(transform);
            slots.Add(slot);
        }
    }


    void Update()
    {
        CheckClick();
    }
    void CheckClick()
    {
        Mouse mouse = Mouse.current;
        Keyboard keyboard = Keyboard.current;

        if (mouse.leftButton.wasPressedThisFrame)
        {
            if (currentSlot < slotCount)
            {
                slots[currentSlot].SetStop(true);
                int symbol = slots[currentSlot].GetResultSymbol();
                result.Add(symbol);
                PullResult?.Invoke(GetNameSprite(symbol));
                currentSlot++;
            }
        }
        if (keyboard.rKey.wasPressedThisFrame)
        {
            for (int i = 0; i < slotCount; i++)
            {
                slots[i].StartRoll();
            }
            result.Clear();
            currentSlot = 0;
        }
    }
    public Sprite GetSymbolSpriteRule(int symbolData)
    {
        if (symbolData >= sprites.Count) return nullSprite;
        return sprites[symbolData];
    }
    public string GetNameSprite(int symbolData)
    {
        if (symbolData >= sprites.Count) return "No Name";
        return sprites[symbolData].name;
    }
}
