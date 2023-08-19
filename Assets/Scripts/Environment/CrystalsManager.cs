using UnityEngine;

namespace Environment
{
    public class CrystalsManager : MonoBehaviour
    {
        public HealthCrystal[] crystals;

        private void Awake()
        {
            crystals = GetComponentsInChildren<HealthCrystal>();
        }
    }
}