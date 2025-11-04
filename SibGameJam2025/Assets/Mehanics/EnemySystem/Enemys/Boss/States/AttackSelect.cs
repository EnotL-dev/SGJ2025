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
        private int _attackCount = 0;
        private BossStates _states;

        public AttackSelect(
            IStateSwitcher stateSwitcher,
            AnimatorController animator,
            RotateToPlayer rotor,
            int attacsCount,
            BossConfig config,
            BossStates states
            ) : base(stateSwitcher)
        {
            _animator = animator;
            _rotor = rotor;
            _attacksCount = attacsCount;
            _config = config;
            _states = states;
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
           // Debug.Log("Boss start Attack");
            if (_attackCount > 3 && _states.CountUnitsToSpawn <= 0)
                _stateSwitcher.SwitchState<Summon>();
            else
            {
                switch (Random.Range(0, _attacksCount))
                {
                    case 0:
                        _stateSwitcher.SwitchState<AttackExplosionOnPlayer>();
                        break;
                }
            }
            _attackCount++;
        }
    }
}