using System.Collections;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class AttackExplosionOnPlayer : State
    {
        private Transform _body;
        private CharacterController _player;
        private AnimatorController _animator;
        private RotateToPlayer _rotor;
        private BossConfig _config;
        private Transform _explosionZone;

        public AttackExplosionOnPlayer(
            IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            BossConfig config,
            AnimatorController animator,
            RotateToPlayer rotor,
            Transform explosionZone
            ) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _rotor = rotor;
            _explosionZone = explosionZone;
        }

        public override void Start()
        {
            _animator.SetAttack(true);
            _rotor.enabled = true;
            _animator.StartCoroutine(DoAttack());
        }

        public override void Stop()
        {
            _animator.SetAttack(false);
            _rotor.enabled = false;
        }

        public override void Update()
        {
            
        }

        private IEnumerator DoAttack()
        {
            Debug.Log("ffff");
            Vector3 playerPosition = _player.transform.position;
            _explosionZone.transform.position = playerPosition;
            _explosionZone.parent = null;
            _explosionZone.gameObject.SetActive(true);
            yield return new WaitForSeconds(_config.AttackExplosionOnPlayer.TimeToExplosion);
            GameObject.Instantiate(_config.AttackExplosionOnPlayer.Explosion, playerPosition, Quaternion.identity);
            _explosionZone.gameObject.SetActive(false);
            _explosionZone.parent = _body;
            _stateSwitcher.SwitchState<AttackSelect>();
        }


    }
}