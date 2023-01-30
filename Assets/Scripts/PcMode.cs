using UnityEngine;

namespace Assets.Scripts
{
    public class PcMode : Mode
    {
        public PcMode(GridSpace[] gridSpaces)
            : base(gridSpaces)
        {
        }

        public void pcPlays()
        {
            int rnd = Random.Range(0, 9);

            if (gridSpaces[rnd].IsActive())
            {
                gridSpaces[rnd].SetSpace();
            }
            else
            {
                Debug.Log("AGAIN!!!");
                pcPlays();
            }
            Debug.Log("STOP");
        }
    }
}
