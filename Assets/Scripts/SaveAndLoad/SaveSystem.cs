using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Character;
using EnemyScripts;
using Environment;
using UnityEngine;

namespace SaveAndLoad
{
    public static class SaveSystem
    {
        private static string _playerPath = "/player.txt";
        private static string _totemsPath = "/totem.txt";
        private static string _triggersPath = "/triggers.txt";
        private static string _enemiesPath = "/enemies.txt";
        private static string _crystalsPath = "/crystals.txt";

        #region Player
        public static void SavePlayer(CharacterController2D player)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + _playerPath;
            FileStream stream = new FileStream(path, FileMode.Create);

            PlayerData data = new PlayerData(player);
            
            formatter.Serialize(stream, data);
            stream.Close();
            
        }
        public static PlayerData LoadPlayer()
        {
            string path = Application.persistentDataPath + _playerPath;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                PlayerData data = formatter.Deserialize(stream) as PlayerData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("Save file not found in" + path);
                return null;
            }
        }

        #endregion

        #region Totems

        public static void SaveTotemsValues(TotemManager totemManager)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + _totemsPath;
            FileStream stream = new FileStream(path, FileMode.Create);
            TotemData data = new TotemData(totemManager);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static TotemData LoadTotemsValues()
        {
            string path = Application.persistentDataPath + _totemsPath;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                TotemData data = formatter.Deserialize(stream) as TotemData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("Save file not found in" + path);
                return null;
            }
        }

        #endregion

        #region Triggers

        public static void SaveTriggersValues(CamManager camManager)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + _triggersPath;
            FileStream stream = new FileStream(path, FileMode.Create);
            TriggerData data = new TriggerData(camManager);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static TriggerData LoadTriggersValues()
        {
            string path = Application.persistentDataPath + _triggersPath;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                TriggerData data = formatter.Deserialize(stream) as TriggerData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("Save file not found in" + path);
                return null;
            }
        }

        #endregion

        #region Enemies

        public static void SaveEnemiesValues(EnemyManager enemyManager)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + _enemiesPath;
            FileStream stream = new FileStream(path, FileMode.Create);
            EnemyData data = new EnemyData(enemyManager);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static EnemyData LoadEnemiesValues()
        {
            string path = Application.persistentDataPath + _enemiesPath;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                EnemyData data = formatter.Deserialize(stream) as EnemyData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("Save file not found in" + path);
                return null;
            }
        }

        #endregion

        #region Crystals

        public static void SaveCrystalsValues(CrystalsManager crystalsManager)
        {
            BinaryFormatter formatter = new BinaryFormatter();
            string path = Application.persistentDataPath + _crystalsPath;
            FileStream stream = new FileStream(path, FileMode.Create);
            CrystalsData data = new CrystalsData(crystalsManager);
            formatter.Serialize(stream, data);
            stream.Close();
        }
        public static CrystalsData LoadCrystalsValues()
        {
            string path = Application.persistentDataPath + _crystalsPath;
            if (File.Exists(path))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                FileStream stream = new FileStream(path, FileMode.Open);
                
                CrystalsData data = formatter.Deserialize(stream) as CrystalsData;
                stream.Close();
                return data;
            }
            else
            {
                Debug.LogError("Save file not found in" + path);
                return null;
            }
        }

        #endregion
    }
}