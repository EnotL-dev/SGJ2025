using BattleSystem;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{

    public class SaveData
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

                private int _maxHp = 25;
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
                    _maxHp += 18;
                    _maxMp += 37;
                    _bonusDamage += 3;
                }
            }

            private int money = 0;
            public NodeGraph nodeGraph = new NodeGraph();
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

            public DataParams()
            {

            }

            public DataParams(DataParams data, int lv)
            {
                money += data.money;

                NodeData nodeData = Resources.Load<NodeData>("Nodes/NodeData");
                foreach (Node node in data.nodeGraph.GetNodesInList())
                {
                    if(node is Summon)
                    {
                        if (node is Single)
                            nodeGraph.summon = nodeData.summons[0];
                        else if (node is Triple)
                            nodeGraph.summon = nodeData.summons[1];
                    }
                    else if (node is Shape)
                    {
                        if (node is Sphere)
                            nodeGraph.shape = nodeData.shapes[0];
                        else if (node is Wawe)
                            nodeGraph.shape = nodeData.shapes[1];
                    }
                    else if (node is Impact)
                    {
                        if (node is Explosion)
                            nodeGraph.impact = nodeData.impacts[0];
                        else if (node is Shrapnel)
                            nodeGraph.impact = nodeData.impacts[1];
                        else if (node is Machinegun)
                            nodeGraph.impact = nodeData.impacts[2];
                    }
                    else if (node is Feature)
                    {
                        if (node is PaybackMana)
                            nodeGraph.feature = nodeData.features[0];
                        else if (node is Vampirism)
                            nodeGraph.feature = nodeData.features[1];
                        else if (node is GoldRush)
                            nodeGraph.feature = nodeData.features[2];
                    }
                }

                for(int i = 1; i < lv; i++)
                {
                    playerParams.LvlUp();
                }
            }
        }

        public static Dictionary<string, float> Volumes = new Dictionary<string, float>();
        public static float sensivity = 400f;

        public static int currentSouls = 0;
        public static int maxSouls = 0;

        public static int indexScene = 1;
        public static DataParams MainData = new DataParams();

        public static DataParams TempData = new DataParams();

        public static void Load()
        {
            Debug.Log("Загрузка сейва");
            TempData = new DataParams(MainData, indexScene);
        }

        public static void Save()
        {
            Debug.Log("Сохранение");
            indexScene++;
            TempData = new DataParams(TempData, indexScene);
            MainData = new DataParams(TempData, indexScene);
        }
    }
}