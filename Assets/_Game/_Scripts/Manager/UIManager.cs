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
    public void Click()
    {
        AudioManager.Instance.Play(AudioManager.SoundType.Mouse_Click);
    }
    public void Roll()
    { AudioManager.Instance.Play(AudioManager.SoundType.Button_Click); }

    public void Play()
    {
        SceneManager.LoadScene("Main");
    }    
 
  }
