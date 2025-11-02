using UnityEngine;

namespace BattleSystem
{
    public abstract class Node : ScriptableObject
    {
        private string _nameNode = "Ничего";

        public virtual string nameNode
        {
            get => _nameNode;
            set => _nameNode = value;
        }

        private string _description = "Ничего";

        public virtual string description
        {
            get => _description;
            set => _description = value;
        }

        private int _cost = 1;

        public virtual int cost
        {
            get => _cost;
            set => _cost = value;
        }

        private int _weight = 1;

        public virtual int weight
        {
            get => _weight;
            set => _weight = value;
        }
    }
}
