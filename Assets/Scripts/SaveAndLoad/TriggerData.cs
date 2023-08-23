using System;
using System.Linq;

namespace SaveAndLoad
{
    [Serializable]
    public class TriggerData
    {
        public int[] triggersValue;

        public TriggerData(CamManager manager)
        {
            triggersValue = manager.triggers
                .Select(e => e.gameObject.activeSelf ? 1 : 0)
                .ToArray();
            // triggersValue = new int[manager.triggers.Length];
            // for (int i = 0; i < triggersValue.Length; i++)
            // {
            //     if (manager.triggers[i].gameObject.activeSelf)
            //     {
            //         triggersValue[i] = 1;
            //     }
            //     else
            //     {
            //         triggersValue[i] = 0;
            //     }
            // }
        }
    }
}