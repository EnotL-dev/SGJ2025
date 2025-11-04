using BattleSystem;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveData
    {
        [System.Serializable]
        public class DataParams
        {
            [System.Serializable]
            public class PlayerParams
            {
                public int _lv = 1;
                public int _maxHp = 25;
                public int _maxMp = 20;
                public int _bonusDamage = 2;

                public int lv => _lv;
                public int maxHp => _maxHp;
                public int maxMp => _maxMp;
                public int bonusDamage => _bonusDamage;

                public void LvlUp()
                {
                    _lv++;
                    _maxHp += 13;
                    _maxMp += 25;
                    _bonusDamage += 3;
                }
            }

            public NodeGraph nodeGraph = new NodeGraph();
            public int money = 0;
            public PlayerParams playerParams = new PlayerParams();

            public int GetMoney() => money;

            public void AddMoney(int add)
            {
                if (add < 0) return;
                money += add;
            }

            public void ReduceMoney(int reduce)
            {
                if (reduce < 0) return;
                money -= reduce;
            }
        }

        // Эти данные не сериализуем — можно сбрасывать каждый запуск
        public static Dictionary<string, float> Volumes = new();
        public static float sensivity = 400f;
        public static int currentSouls = 0;
        public static int maxSouls = 0;

        public static DataParams MainData = new DataParams();
        public static DataParams TempData = new DataParams();

        private const string SaveKey = "GameSaveData";

        public static void Load()
        {
            Debug.Log("Загрузка сейва");

            if (PlayerPrefs.HasKey(SaveKey))
            {
                string json = PlayerPrefs.GetString(SaveKey);
                MainData = JsonUtility.FromJson<DataParams>(json);

                // Создаем независимую копию для временных данных
                string tempJson = PlayerPrefs.GetString(SaveKey);
                TempData = JsonUtility.FromJson<DataParams>(tempJson);

                Debug.Log("Сейв загружен успешно");
            }
            else
            {
                Debug.Log("Сейва нет, создаем новый");
                MainData = new DataParams();
                TempData = new DataParams();
            }
        }

        public static void Save()
        {
            Debug.Log("Сохранение");

            // Копируем текущие данные из TempData в MainData
            string tempJson = JsonUtility.ToJson(TempData);
            MainData = JsonUtility.FromJson<DataParams>(tempJson);

            // Сохраняем MainData в PlayerPrefs
            string json = JsonUtility.ToJson(MainData);
            PlayerPrefs.SetString(SaveKey, json);
            PlayerPrefs.Save();

            Debug.Log("Сейв сохранен успешно");
        }
    }
}
