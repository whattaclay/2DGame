using System;
using Environment;

namespace SaveAndLoad
{
    [Serializable]
    public class CrystalsData
    {
        public int[] crystals;

        public CrystalsData(CrystalsManager manager)
        {
            crystals = new int[manager.crystals.Length];
            for (int i = 0; i < manager.crystals.Length; i++)
            {
                if (manager.crystals[i].gameObject.activeSelf)
                {
                    crystals[i] = 1;
                }
                else
                {
                    crystals[i] = 0;
                }
            }
        }
    }
}