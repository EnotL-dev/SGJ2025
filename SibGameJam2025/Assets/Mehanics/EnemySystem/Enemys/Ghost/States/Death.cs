using System;
using System.Collections;
using UnityEngine;

namespace EnemySystem.Ghost
{
    public class Death : State
    {
        private AnimatorController _animator;
        private float _timeToDestroy;
        private Transform _body;

        public Death(IStateSwitcher stateSwitcher, AnimatorController animator, float timeToDestroy, Transform body) : base(stateSwitcher)
        {
            _animator = animator;
            _timeToDestroy = timeToDestroy;
            _body = body;
        }

        public override void Start()
        {
            _animator.CallDeath();
            _animator.StartCoroutine(DestroyTimer());
        }

        public override void Stop()
        {
          
        }

        public override void Update()
        {
            
        }

        private IEnumerator DestroyTimer()
        {
            Debug.Log(_timeToDestroy);
            yield return new WaitForSeconds(_timeToDestroy);
            try
            {
                GameObject.Destroy(_body.gameObject);
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error when destroying a mob after death: {ex.Message}");
            }
        }
    }
}