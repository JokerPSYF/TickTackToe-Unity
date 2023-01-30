using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        Debug.Log("Set game control reference");
        gameController = controller;
    }

    public void SetSpace()
    {
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }

    public bool IsActive()
    {
        return button.interactable;
    }
}