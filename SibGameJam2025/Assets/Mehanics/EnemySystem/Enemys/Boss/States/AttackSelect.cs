using System.Collections;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class AttackSelect : State
    {
        private AnimatorController _animator;
        private RotateToPlayer _rotor;
        private int _attacksCount;
        private BossConfig _config;

        public AttackSelect(
            IStateSwitcher stateSwitcher,
            AnimatorController animator,
            RotateToPlayer rotor,
            int attacsCount,
            BossConfig config
            ) : base(stateSwitcher)
        {
            _animator = animator;
            _rotor = rotor;
            _attacksCount = attacsCount;
            _config = config;
        }

        public override void Start()
        {
            _animator.SetIdle(true);
            _rotor.enabled = true;
            _animator.StartCoroutine(SelectAttack());
        }

        public override void Stop()
        {
            _animator.SetIdle(false);
            _rotor.enabled = false;
        }

        public override void Update()
        {
        }

        private IEnumerator SelectAttack()
        {
            yield return new WaitForSeconds(_config.TimeBeforeAttack);
            Debug.Log("Boss start Attack");
            switch (Random.Range(0, _attacksCount))
            {
                case 0:
                    _stateSwitcher.SwitchState<AttackExplosionOnPlayer>();
                    break;
            }
        }
    }
}