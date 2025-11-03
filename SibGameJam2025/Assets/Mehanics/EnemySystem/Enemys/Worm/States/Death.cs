using System;
using System.Collections;
using UnityEngine;

namespace EnemySystem.Worm
{
    public class Death : State
    {
        private AnimatorController _animator;
        private float _timeToDestroy;
        private float _timeBeforeAnimation;
        private Transform _body;
        private DestroyAnimation _destroyAnimation;

        public Death(IStateSwitcher stateSwitcher, AnimatorController animator, float timeToDestroy, float timeBeforeAnimation, Transform body, DestroyAnimation destroyAnimation) : base(stateSwitcher)
        {
            _animator = animator;
            _timeToDestroy = timeToDestroy;
            _timeBeforeAnimation = timeBeforeAnimation;
            _body = body;
            _destroyAnimation = destroyAnimation;
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
            yield return new WaitForSeconds(_timeBeforeAnimation);
            _destroyAnimation.StartPlayAnimation();
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