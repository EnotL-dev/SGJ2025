using UnityEngine;

namespace EnemySystem
{
    public abstract class State
    {
        protected IStateSwitcher _stateSwitcher;

        public State(IStateSwitcher stateSwitcher)
        {
            _stateSwitcher = stateSwitcher;
        }

        public abstract void Update();

        public abstract void Start();

        public abstract void Stop();
    }
}