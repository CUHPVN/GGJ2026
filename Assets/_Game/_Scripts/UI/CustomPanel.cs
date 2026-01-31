using UnityEngine;

public class CustomPanel : MonoBehaviour
{
    public void PopUp()
    {
        gameObject.SetActive(true);
    }
    public void PopDown()
    { 
        gameObject.SetActive(false);
    }

}
