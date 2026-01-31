using UnityEngine;

public class LevelManager : Singleton<LevelManager>
{
    public LevelData[] levelDatas;
    int level = 1;
    void Start()
    {
        LoadLevel();
    }
    void LoadLevel()
    {
        if(level == 1)
        {
            BetSystem.Instance.LoadLevelFistTime(100,levelDatas[level - 1].enemyHealth);
        }
        else
        if (level < levelDatas.Length)
        {
            BetSystem.Instance.LoadLevel(levelDatas[level-1].enemyHealth);
        }
        else
        {
            Debug.Log("Not Enought Level");
        }
    }
    public Sprite GetMaskSprite()
    {
        return levelDatas[level-1].maskSprite;
    }
    public void LevelUp()
    {
        level++;
        LoadLevel();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    [System.Serializable]
    public struct LevelData
    {
        public int level;
        public int enemyHealth;
        public int icon;
        public Sprite maskSprite;
    }
}
