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

        private int _manaCost = 1;

        public virtual int manaCost
        {
            get => _manaCost;
            set => _manaCost = value;
        }

        private int _moneyCost = 10;

        public virtual int moneyCost
        {
            get => _moneyCost;
            set => _moneyCost = value;
        }

        private int _weight = 1;

        public virtual int weight
        {
            get => _weight;
            set => _weight = value;
        }
    }
}
