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
                private int _lv = 1;
                public int lv
                {
                    get => _lv;
                }

                private int _maxHp = 30;
                public int maxHp
                {
                    get => _maxHp;
                }

                private int _maxMp = 20;
                public int maxMp
                {
                    get => _maxMp;
                }

                private int _bonusDamage = 2;
                public int bonusDamage
                {
                    get => _bonusDamage;
                }

                public void LvlUp()
                {
                    _lv++;
                    _maxHp += 8;
                    _maxMp += 10;
                    _bonusDamage += 3;
                }
            }

            public NodeGraph nodeGraph = new NodeGraph();
            private int money = 0;
            public PlayerParams playerParams = new PlayerParams();

            public int GetMoney()
            {
                return money;
            }

            public void AddMoney(int add)
            {
                if (add < 0)
                    return;

                money += add;
            }

            public void ReduceMoney(int reduce)
            {
                if (reduce < 0)
                    return;

                money -= reduce;
            }
        }

        public static Dictionary<string, float> Volumes = new Dictionary<string, float>();

        public static int currentSouls = 0;
        public static int maxSouls = 0;

        private static DataParams MainData = new DataParams();
        public static DataParams TempData = new DataParams();

        public static void Load()
        {
            Debug.Log("Загрузка сейва");
            TempData = MainData;
        }

        public static void Save()
        {
            Debug.Log("Сохранение");
            MainData = TempData;
        }

        //public static int indexScene = 0; //индекс сцены игрока, при переходе уровня +1
    }
}
