using PlayerSystem;
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

        protected bool PlayerAboveMe(Transform body)
        {
            Vector3 checkPosition = body.transform.position;
            checkPosition.y += 2f;
            return Physics.CheckSphere(checkPosition, 2f, PlayerRefs.Instance.PlayerLayer);
        }
    }
}