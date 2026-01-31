using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] CustomPanel shopPanel;
    [SerializeField] CustomPanel menuPanel;
    [SerializeField] CustomPanel settingPanel;
    public void OpenShop()
    {
        menuPanel.PopDown();
        shopPanel.PopUp();
    }
    public void CloseShop()
    {
        shopPanel.PopDown();
        menuPanel.PopUp();
    }
    public void Play()
    {
        Debug.Log("Load Scene game play");
        //SceneManager.LoadScene("GamePlay");
    }
    public void OpenSetting()
    {
        menuPanel.PopDown();
        settingPanel.PopUp();
        
    }
    public void CloseSetting()
    {
        settingPanel.PopDown();
        menuPanel.PopUp();
    }
}
