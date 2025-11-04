using BattleSystem;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{
    public static class SaveData
    {
        public class DataParams
        {
            public class PlayerParams
            {
                private int _lv;
                public int lv
                {
                    get => _lv;
                }

                private int _maxHp;
                public int maxHp
                {
                    get => _maxHp;
                }

                private int _maxMp;
                public int maxMp
                {
                    get => _maxMp;
                }

                private int _bonusDamage;
                public int bonusDamage
                {
                    get => _bonusDamage;
                }

                public PlayerParams()
                {
                    Load();
                }

                private void Load()
                {
                    _lv = PlayerPrefs.GetInt("Player_Lv", 1);
                    _maxHp = PlayerPrefs.GetInt("Player_MaxHp", 25);
                    _maxMp = PlayerPrefs.GetInt("Player_MaxMp", 20);
                    _bonusDamage = PlayerPrefs.GetInt("Player_BonusDamage", 2);
                }

                public void Save()
                {
                    PlayerPrefs.SetInt("Player_Lv", _lv);
                    PlayerPrefs.SetInt("Player_MaxHp", _maxHp);
                    PlayerPrefs.SetInt("Player_MaxMp", _maxMp);
                    PlayerPrefs.SetInt("Player_BonusDamage", _bonusDamage);
                }

                public void LvlUp()
                {
                    _lv++;
                    _maxHp += 13;
                    _maxMp += 25;
                    _bonusDamage += 3;
                    Save();
                }
            }

            public NodeGraph nodeGraph;
            private int money;
            public PlayerParams playerParams;

            public DataParams()
            {
                playerParams = new PlayerParams();
                nodeGraph = new NodeGraph();
                Load();
            }

            public int GetMoney()
            {
                return money;
            }

            public void AddMoney(int add)
            {
                if (add < 0)
                    return;

                money += add;
                PlayerPrefs.SetInt("Money", money);
            }

            public void ReduceMoney(int reduce)
            {
                if (reduce < 0)
                    return;

                money -= reduce;
                PlayerPrefs.SetInt("Money", money);
            }

            private void Load()
            {
                money = PlayerPrefs.GetInt("Money", 0);
            }

            public void Save()
            {
                PlayerPrefs.SetInt("Money", money);
                playerParams.Save();
                // NodeGraph сохраняется автоматически при изменениях
            }
        }

        // Статические свойства с сохранением в PlayerPrefs
        public static Dictionary<string, float> Volumes
        {
            get
            {
                var volumes = new Dictionary<string, float>();
                // Загрузка громкости для разных каналов
                volumes["Master"] = PlayerPrefs.GetFloat("Volume_Master", 1f);
                volumes["Music"] = PlayerPrefs.GetFloat("Volume_Music", 1f);
                volumes["SFX"] = PlayerPrefs.GetFloat("Volume_SFX", 1f);
                return volumes;
            }
            set
            {
                foreach (var kvp in value)
                {
                    PlayerPrefs.SetFloat($"Volume_{kvp.Key}", kvp.Value);
                }
            }
        }

        public static float sensivity
        {
            get => PlayerPrefs.GetFloat("Sensivity", 400f);
            set => PlayerPrefs.SetFloat("Sensivity", value);
        }

        public static int currentSouls
        {
            get => PlayerPrefs.GetInt("CurrentSouls", 0);
            set => PlayerPrefs.SetInt("CurrentSouls", value);
        }

        public static int maxSouls
        {
            get => PlayerPrefs.GetInt("MaxSouls", 0);
            set => PlayerPrefs.SetInt("MaxSouls", value);
        }

        // Основные данные
        private static DataParams _mainData;
        public static DataParams MainData
        {
            get
            {
                if (_mainData == null)
                {
                    _mainData = new DataParams();
                }
                return _mainData;
            }
            private set => _mainData = value;
        }

        private static DataParams _tempData;
        public static DataParams TempData
        {
            get
            {
                if (_tempData == null)
                {
                    _tempData = new DataParams();
                }
                return _tempData;
            }
            private set => _tempData = value;
        }

        public static void Load()
        {
            Debug.Log("Загрузка сейва");
            // Данные автоматически загружаются при создании объектов
            // Просто пересоздаем TempData как копию MainData
            _tempData = new DataParams();

            // Копируем значения из MainData в TempData
            _tempData.playerParams = MainData.playerParams;
            _tempData.nodeGraph = MainData.nodeGraph;
            // Money копируется автоматически через PlayerPrefs
        }

        public static void Save()
        {
            Debug.Log("Сохранение");
            MainData.Save();
            PlayerPrefs.Save(); // Сохраняем все изменения на диск
        }

        public static void DeleteAllSaveData()
        {
            PlayerPrefs.DeleteAll();
            _mainData = null;
            _tempData = null;
        }
    }
}