using Assets.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
    public GameObject chooseModeInfo;
    public GameObject playerVsPlayerBtn;
    public GameObject playerVsPcBtn;
    public GridSpace[] gridSpaces;

    private Mode mode;
    private string currSide;
    private string playerSide;
    private byte moveCount;

    public void ChooseMode(string choosenMode)
    {
        Debug.Log($"chossenMode is {choosenMode}");
        if (choosenMode == "PC")
        {
            Debug.Log(playerSide);

            this.mode = new PcMode(gridSpaces);
        }
        else if (choosenMode == "Player")
        {
            Debug.Log($"Enter in Player");
            this.mode = new PlayerMode(gridSpaces);
        }
        else
        {
            Debug.Log($"Error");
        }
        playerVsPcBtn.SetActive(false);
        playerVsPlayerBtn.SetActive(false);
        chooseModeInfo.SetActive(false);
        playerO.button.gameObject.SetActive(true);
        playerX.button.gameObject.SetActive(true);
    }

    public void SetStartingSide(string startingSide)
    {
        playerSide = startingSide;
        currSide = "X";
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

        if (CheckWinCondition()) GameOver(currSide);
        else if (moveCount >= 9 && !CheckWinCondition()) GameOver("draw");
        else ChangeSide();
    }

    public string GetPlayerSide()
    {
        return currSide;
    }

    public void RestartGame()
    {
        SetBoardInteractable(false);
        moveCount = 0;
        gameOverPanel.SetActive(false);
        restartButton.SetActive(false);
        startInfo.SetActive(true);
        playerVsPlayerBtn.SetActive(true);
        playerVsPcBtn.SetActive(true);
        playerO.button.gameObject.SetActive(false);
        playerX.button.gameObject.SetActive(false);
        chooseModeInfo.SetActive(true);
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
        playerVsPlayerBtn.SetActive(false);
        playerVsPcBtn.SetActive(false);
        SetBoardInteractable(true);
        SetPlayerButtons(false);

        // WARNING
        if (playerSide == "O")
        {
            cpPlay();
        }
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
        Debug.Log("Awake");
        SetGameControllerReferenceOnButtons();
        moveCount = 0;
        playerO.button.gameObject.SetActive(false);
        playerX.button.gameObject.SetActive(false);
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
                if (buttonList[i + j].text != currSide)
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
                if (buttonList[i + j].text != currSide)
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
            if (buttonList[i].text != currSide)
            {
                win = false;
            }
        }

        if (win) return true;

        win = true;

        for (int i = 2; i <= 6; i = i + 2)
        {
            if (buttonList[i].text != currSide)
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
        currSide = (currSide == "X") ? "O" : "X";
        if (currSide == "X")
        {
            SetPlayerColors(playerX, playerO);
        }
        else
        {
            SetPlayerColors(playerO, playerX);
        }

        if (playerSide != currSide)
        {
            cpPlay();
        }
    }

    private void cpPlay()
    {
        int rnd = Random.Range(0, 9);

        if (gridSpaces[rnd].IsActive())
        {
            gridSpaces[rnd].SetSpace();
        }
        else
        {
            Debug.Log("AGAIN!!!");
            cpPlay();
        }
        Debug.Log("STOP");
    }
}