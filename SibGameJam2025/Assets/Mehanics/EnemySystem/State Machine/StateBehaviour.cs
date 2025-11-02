using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace EnemySystem
{
    public abstract class StateBehaviour : MonoBehaviour, IStateSwitcher
    {
        public State CurrentState { get; protected set; }
        public bool FierstStateIsLaunched { get => _fierstStateIsLaunched; set { _fierstStateIsLaunched = value; } }
        protected List<State> _states = new();
        protected bool _fierstStateIsLaunched = false;

        public void SwitchState<T>() where T : State
        {
            var newState = _states.FirstOrDefault(s => s is T);
            if (newState == null)
            {
                Debug.LogError("State not found!");
                return;
            }
            if (CurrentState != null)
                CurrentState.Stop();
            newState.Start();
            CurrentState = newState;
            //Debug.Log($"switch {CurrentState}");
        }

        private void Update()
        {
            if (CurrentState != null)
                CurrentState.Update();
        }

        private void Start()
        {
            InitializeStates();
            OnAwake();
        }

        protected abstract void InitializeStates();

        protected abstract void OnAwake();
    }
}
