using UnityEngine;

namespace BattleSystem
{
    public abstract class Node : ScriptableObject
    {
        private string _nameNode = "Nothing";

        public virtual string nameNode
        {
            get => _nameNode;
            set => _nameNode = value;
        }

        private string _description = "Nothing";

        public virtual string description
        {
            get => _description;
            set => _description = value;
        }
    }
}
