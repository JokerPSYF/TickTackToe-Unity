using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class GridSpace : MonoBehaviour
{
    public Button button;
    public TMP_Text buttonText;

    private GameController gameController;

    public void SetGameControllerReference(GameController controller)
    {
        Debug.Log("ctor");
        gameController = controller;
    }

    public void SetSpace()
    {
        Debug.Log("Set the button off");
        buttonText.text = gameController.GetPlayerSide();
        button.interactable = false;
        gameController.EndTurn();
    }
}