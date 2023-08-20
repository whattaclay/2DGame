using Character;
using EnemyScripts;
using Environment;
using UnityEngine;

namespace SaveAndLoad
{
    public class SaveAndLoadManager : MonoBehaviour
    {
        [SerializeField] private CharacterController2D player;
        [SerializeField] private TotemManager totemManager;
        [SerializeField] private CamManager triggers;
        [SerializeField] private EnemyManager enemies;
        [SerializeField] private CrystalsManager crystals;
        
        private void Start()
        {
            LoadPlayer();
            LoadTotems();
            LoadTriggers();
            LoadEnemies();
            LoadCrystals();
        }
        public void SaveProgress()
        {
            SavePlayer();
            SaveTotems();
            SaveTriggers();
            SaveEnemies();
            SaveCrystals();
        }
        private void SavePlayer()
        {
            SaveSystem.SavePlayer(player);
        }
        private void SaveTotems()
        {
            SaveSystem.SaveTotemsValues(totemManager);
        }
        private void SaveTriggers()
        {
            SaveSystem.SaveTriggersValues(triggers);
        }
        private void SaveEnemies()
        {
            SaveSystem.SaveEnemiesValues(enemies);
        }

        private void SaveCrystals()
        {
            SaveSystem.SaveCrystalsValues(crystals);
        }
        private void LoadPlayer()
        {
            PlayerData data = SaveSystem.LoadPlayer();
            if (data == null) return;
            player.health.CurrentHealth = data.Health;
            Vector3 position;
            position.x = data.Position[0];
            position.y = data.Position[1];
            position.z = data.Position[2];
            player.transform.position = position;
        }
        private void LoadTotems()
        {
            TotemData data = SaveSystem.LoadTotemsValues();
            for (int i = 0; i < data.totemsValue.Length; i++)
            {
                if (data.totemsValue[i] == 1)
                {
                    totemManager.totems[i].isActivated = true;
                    totemManager.TotemsCounter();
                }
            }
        }
        private void LoadTriggers()
        {
            TriggerData data = SaveSystem.LoadTriggersValues();
            for (int i = 0; i < data.triggersValue.Length; i++)
            {
                if (data.triggersValue[i] == 0)
                {
                    triggers.triggers[i].gameObject.SetActive(false);
                }
            }
        }
        private void LoadEnemies()
        {
            EnemyData data = SaveSystem.LoadEnemiesValues();
            for (int i = 0; i < data.aliveEnemies.Length; i++)
            {
                if (data.aliveEnemies[i] == 1)
                {
                    enemies.enemies[i].CurrentHealth = data.enemyHealth[i];
                }
                else if (data.aliveEnemies[i] == 0)
                {
                    enemies.enemies[i].gameObject.SetActive(false);
                }
            }
        }
        private void LoadCrystals()
        {
            CrystalsData data = SaveSystem.LoadCrystalsValues();
            for (int i = 0; i < data.crystals.Length; i++)
            {
                if (data.crystals[i] == 0)
                {
                    crystals.crystals[i].gameObject.SetActive(false);
                }
            }
        }
    }
}