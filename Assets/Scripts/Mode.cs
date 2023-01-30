public abstract class Mode
{
    public string currSide;
    public GridSpace[] gridSpaces;

    public Mode(GridSpace[] gridSpaces)
    {
        this.currSide = "X";
        this.gridSpaces = gridSpaces;
    }

    public string ChangeSide()
    {
        this.currSide = (currSide == "X") ? "O" : "X";

        return currSide;
    }

    public bool CheckRows()
    {
        bool win = true;

        for (int i = 0; i < 8; i = i + 3)
        {
            win = true;
            for (int j = 0; j < 3; j++)
            {
                if (gridSpaces[i + j].buttonText.text != currSide)
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

    public bool CheckCols()
    {
        bool win = true;

        for (int j = 0; j < 3; j++)
        {
            win = true;
            for (int i = 0; i <= j + 6; i = i + 3)
            {
                if (gridSpaces[i + j].buttonText.text != currSide)
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

    public bool CheckDiagonals()
    {
        bool win = true;

        for (int i = 0; i <= 8; i = i + 4)
        {
            if (gridSpaces[i].buttonText.text != currSide)
            {
                win = false;
            }
        }

        if (win) return true;

        win = true;

        for (int i = 2; i <= 6; i = i + 2)
        {
            if (gridSpaces[i].buttonText.text != currSide)
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
}
