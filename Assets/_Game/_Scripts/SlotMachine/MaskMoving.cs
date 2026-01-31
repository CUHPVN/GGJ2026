using UnityEngine;
using UnityEngine.Rendering;

public class MaskMoving : MonoBehaviour
{
    [SerializeField] float slotSize;
    [SerializeField] int turnCount = 0;
    [SerializeField] int maxTurn = 6;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
            Move();
    }
    public void Move()
    {
        turnCount++;
        Debug.Log(turnCount);
        if (turnCount > maxTurn) return;
        transform.Translate(new Vector2((turnCount%2)*slotSize,-(1-turnCount%2)*slotSize));
    }
}
