using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : Singleton<LevelManager>
{
    public LevelData[] levelDatas;
    public int level = 1;
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
        if (level < levelDatas.Length+1)
        {
            BetSystem.Instance.LoadLevel(levelDatas[level-1].enemyHealth);
        }
        else
        {
            SceneManager.LoadScene("Win");
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
