using UnityEngine;
using TMPro;
using UnityEngine.UIElements;
public class BackButton : MonoBehaviour
{
    [SerializeField] GameObject currentPanel;
    [SerializeField] GameObject nextPanel;
    public void Back()
    {
        currentPanel.SetActive(false);
        nextPanel.SetActive(true);
    }
}
