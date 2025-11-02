using BattleSystem;
using System.Collections.Generic;
using UnityEngine;

namespace SaveSystem
{

    public static class SaveData
    {
        public class DataParams
        {
            public NodeGraph nodeGraph = new NodeGraph(new Single(), new Sphere(), new NothingImpact(), new NothingFeature());
            public int money = 1000;
        }

        public static Dictionary<string, float> Volumes = new Dictionary<string, float>();

        private static DataParams MainData = new DataParams();
        public static DataParams TempData = new DataParams();

        public static void Load()
        {
            TempData = MainData;
        }

        public static void Save()
        {
            MainData = TempData;
        }

        //public static int indexScene = 0; //индекс сцены игрока, при переходе уровня +1
    }
}
