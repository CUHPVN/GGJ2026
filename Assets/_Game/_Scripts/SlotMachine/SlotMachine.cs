using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SlotMachine : MonoBehaviour
{
    [SerializeField] private Slot slotPrefabs;
    [SerializeField] private int slotCount;
    [SerializeField,Range(0,10)] private float slotSpeed=1;
    [SerializeField] private float slotDistance=1;
    [SerializeField] private int slotSymbolCount=3;
    [SerializeField] private int currentSlot = 0;
    [SerializeField] private List<Slot> slots;
    [SerializeField] private int[,] res = new int[5, 3];
    [SerializeField] private List<Sprite> sprites;
    [SerializeField] private Sprite nullSprite;
    [SerializeField] private Coroutine rolling=null;
    [SerializeField] private SymbolPool symbolPool;

    public event Action OnRollDone;

    public Action ShakeCamera;

    public bool IsInit = false;
    private void OnEnable()
    {
        InteractManager.Instance.OnRollAction += Rolling;
        StateController.Instance.OnEnterStateRoll += ResetRoll;

    }
    private void OnDisable()
    {
        if (InteractManager.Instance != null)
        {

            InteractManager.Instance.OnRollAction -= Rolling;
        }
        if(StateController.Instance != null)
        {
            StateController.Instance.OnEnterStateRoll -= ResetRoll;

        }
    }
    void Start()
    {
        Init();
    }
    public void Init()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Slot_Run);
        Application.targetFrameRate = 120;

        for (int i = 0; i < slotCount; i++)
        {
            if (symbolPool != null) symbolPool.RandomPool();
            else
            {
                symbolPool = FindAnyObjectByType<SymbolPool>();
                Debug.LogWarning("Symbol was not ref!");
            }
            Slot slot = Instantiate(slotPrefabs, new Vector2(transform.position.x + i * slotDistance, transform.position.y), Quaternion.identity);
            slot.SetSymbolPool(symbolPool);
            slot.SetSlotMachine(this);
            slot.SetSpeed(slotSpeed);
            slot.SetSymbolCount(slotSymbolCount);
            slot.SetStop(false);
            slot.transform.SetParent(transform);
            slots.Add(slot);
        }
        IsInit=true; 
    }
    public void ResetRoll()
    {
        if (!IsInit) return;
        symbolPool.RandomPool();
        for (int i = 0; i < slotCount; i++)
        {
            slots[i].StartRoll();
        }
        currentSlot = 0;
        rolling = null;
    }

    void Update()
    {
    }
    public void Rolling()
    {
        if(rolling==null)
        rolling = StartCoroutine(Roll());

    }
    WaitForSeconds waitForEndOfRoll = new WaitForSeconds(0.3f);
    public IEnumerator Roll()
    {
        AudioManager.Instance.Stop(AudioManager.SoundType.Slot_Run);
        while (currentSlot < slotCount)
        {
            if (currentSlot == slotCount - 1)
            {
                AudioManager.Instance.Stop(AudioManager.SoundType.Slot_Stop);
                AudioManager.Instance.Play(AudioManager.SoundType.Last_Stop);
            }
            else
                AudioManager.Instance.Play(AudioManager.SoundType.Slot_Stop);
            slots[currentSlot].SetStop(true);
            int[] symbol = slots[currentSlot].GetResultSymbol();
            res[currentSlot, 0] = symbol[0];
            res[currentSlot, 1] = symbol[1];
            res[currentSlot, 2] = symbol[2];
            currentSlot++;
            yield return waitForEndOfRoll;
            
            
        }
        OnRollDone?.Invoke();
    }
    public int[,] GetResult() => res; 
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
