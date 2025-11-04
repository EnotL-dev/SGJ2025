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
        private ExplosionTarget _explosionZone;
        private Coroutine _coroutine;
        private AudioSource _audioSource;

        public AttackExplosionOnPlayer(
            IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            BossConfig config,
            AnimatorController animator,
            RotateToPlayer rotor,
            ExplosionTarget explosionZone,
            AudioSource audioSource
            ) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _rotor = rotor;
            _explosionZone = explosionZone;
            _audioSource = audioSource;
        }

        public override void Start()
        {
            _animator.ChooseAttack(0);
            _animator.SetAttack(true);
            _rotor.enabled = true;
            _coroutine = _animator.StartCoroutine(DoAttack());
            _audioSource.Play();
        }

        public override void Stop()
        {
            _animator.SetAttack(false);
            _rotor.enabled = false;
            _explosionZone.gameObject.SetActive(false);
            _explosionZone.DeactivateExplosion();
            _animator.StopCoroutine(_coroutine);
        }

        public override void Update()
        {
            
        }

        private IEnumerator DoAttack()
        {
            if (_explosionZone != null)
            {
                Vector3 playerPosition = _player.transform.position;
                playerPosition.y = _body.transform.position.y;
                _explosionZone.transform.position = playerPosition;
                _explosionZone.DeactivateExplosion();
                _explosionZone.transform.parent = null;
                _explosionZone.gameObject.SetActive(true);
                yield return new WaitForSeconds(_config.AttackExplosionOnPlayer.EndOfAttackAnimationTime);
                _animator.SetAttack(false);
                _animator.SetIdle(true);
                yield return new WaitForSeconds(_config.AttackExplosionOnPlayer.TimeToExplosion);
                GameObject.Instantiate(_config.AttackExplosionOnPlayer.Explosion, playerPosition, Quaternion.identity);
                _explosionZone.gameObject.SetActive(false);
                _explosionZone.ActivateExplosion();
                _explosionZone.transform.parent = _body;
                _stateSwitcher.SwitchState<AttackSelect>();
            }
        }


    }
}