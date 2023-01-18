using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class Player
{
    public Image panel;
    public TMP_Text text;
    public Button button;
}
[System.Serializable]
public class PlayerColor
{
    public Color panelColor;
    public Color textColor;
}

public class GameController : MonoBehaviour
{
    public TMP_Text[] buttonList;
    public GameObject gameOverPanel;
    public TMP_Text gameOverText;
    public GameObject restartButton;
    public Player playerX;
    public Player playerO;
    public PlayerColor activePlayerColor;
    public PlayerColor inactivePlayerColor;
    public GameObject startInfo;


    private string playerSide;
    private byte moveCount;

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
        StartGame();
    }

    public void EndTurn()
    {
        moveCount++;

        if (CheckWinCondition()) GameOver(playerSide);
        else if (moveCount >= 9 && !CheckWinCondition()) GameOver("draw");
        else ChangeSide();
    }

    public string GetPlayerSide()
    {
        return playerSide;
    }

    public void RestartGame()
    {
        SetStartingSide("X");
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        startInfo.SetActive(true);
        SetPlayerButtons(true);
        SetPlayerColorsInactive();
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].text = "";
        }
    }

    private void SetPlayerColorsInactive()
    {
        playerX.panel.color = inactivePlayerColor.panelColor;
        playerX.text.color = inactivePlayerColor.textColor;
        playerO.panel.color = inactivePlayerColor.panelColor;
        playerO.text.color = inactivePlayerColor.textColor;
    }

    private void SetPlayerButtons(bool toggle)
    {
        playerX.button.interactable = toggle;
        playerO.button.interactable = toggle;
    }

    private void StartGame()
    {
        startInfo.SetActive(false);
        SetBoardInteractable(true);
        SetPlayerButtons(false);
    }

    private void SetPlayerColors(Player newPlayer, Player oldPlayer)
    {
        newPlayer.panel.color = activePlayerColor.panelColor;
        newPlayer.text.color = activePlayerColor.textColor;
        oldPlayer.panel.color = inactivePlayerColor.panelColor;
        oldPlayer.text.color = inactivePlayerColor.textColor;
    }

    private bool CheckWinCondition()
    {
        if (CheckRows() || CheckCols() || CheckDiagonals()) return true;
        else return false;
    }

    private void SetBoardInteractable(bool toggle)
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
    }

    private void Awake()
    {
        SetGameControllerReferenceOnButtons();
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
    }

    private void SetGameControllerReferenceOnButtons()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    private void GameOver(string winningPlayer)
    {
        SetBoardInteractable(false);
        if (winningPlayer == "draw")
        {
            SetGameOverText("It's a Draw!");
            SetPlayerColorsInactive();
        }
        else
        {
            SetGameOverText(winningPlayer + " Wins!");
        }

        restartButton.SetActive(true);
    }

    private void SetGameOverText(string value)
    {
        gameOverPanel.SetActive(true);
        gameOverText.text = value;
    }

    private bool CheckRows()
    {
        bool win = true;

        for (int i = 0; i < 8; i = i + 3)
        {
            win = true;
            for (int j = 0; j < 3; j++)
            {
                if (buttonList[i + j].text != playerSide)
                {
                    win = false;
                }
            }
            if (win)
            {
                return true;
            }

        }

        return false;
    }

    private bool CheckCols()
    {
        bool win = true;

        for (int j = 0; j < 3; j++)
        {
            win = true;
            for (int i = 0; i <= j + 6; i = i + 3)
            {
                if (buttonList[i + j].text != playerSide)
                {
                    win = false;
                }
            }
            if (win)
            {
                return true;
            }
        }

        return false;
    }

    private bool CheckDiagonals()
    {
        bool win = true;

        for (int i = 0; i <= 8; i = i + 4)
        {
            if (buttonList[i].text != playerSide)
            {
                win = false;
            }
        }

        if (win) return true;

        win = true;

        for (int i = 2; i <= 6; i = i + 2)
        {
            if (buttonList[i].text != playerSide)
            {
                win = false;
            }
        }

        if (win)
        {
            return true;
        }

        return false;
    }

    private void ChangeSide()
    {
        playerSide = (playerSide == "X") ? "O" : "X";
        if (playerSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }
    }
}