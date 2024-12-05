using UnityEngine;

public class NextShape : MonoBehaviour
{
    public void ChangePieceDisplay(TetrominoData data)
    {
        DisableAllShapeDisplay();
        transform.GetChild((int)data.tetromino).gameObject.SetActive(true);
    }

    void DisableAllShapeDisplay()
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }
}