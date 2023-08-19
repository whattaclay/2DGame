using System;
using System.Collections.Generic;
using Environment;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

namespace SaveAndLoad
{
    [Serializable]
    public class TotemData
    {
        public int[] totemsValue;
        public TotemData(TotemManager manager)
        {
            totemsValue = new int[manager.totems.Length];
            for (int i = 0; i < totemsValue.Length; i++)
            {
                if (manager.totems[i].isActivated)
                {
                    totemsValue[i] = 0;
                }
                else
                {
                    totemsValue[i] = 1;
                }
            }
        }
    }
}