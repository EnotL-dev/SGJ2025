using BattleSystem;
using System.Collections;
using UnityEngine;

namespace EnemySystem.Boss
{
    public class AttackLunge : State
    {
        private Transform _body;
        private CharacterController _player;
        private AnimatorController _animator;
        private RotateToPlayer _rotor;
        private BossConfig _config;
        private Vector3 _startPosition;
        private bool _damaged = false;
        private Transform _wave;
        private Coroutine _coroutine;

        public AttackLunge(
            IStateSwitcher stateSwitcher,
            Transform body,
            CharacterController player,
            BossConfig config,
            AnimatorController animator,
            float distanceDetect,
            RotateToPlayer rotor,
            Transform wave
            ) : base(stateSwitcher)
        {
            _body = body;
            _player = player;
            _animator = animator;
            _config = config;
            _rotor = rotor;
            _wave = wave;
        }

        public override void Start()
        {
            _wave.transform.position = _body.position;
            _wave.transform.rotation = Quaternion.identity;
            _wave.transform.parent = null;
            _animator.ChooseAttack(0.5f);
            _animator.SetAttack(true);
            _wave.gameObject.SetActive(true);
        }

        public override void Stop()
        {
            _wave.transform.parent = _body;
            _animator.SetAttack(false);
            _wave.gameObject.SetActive(false);
        }

        public override void Update()
        {
         //   _coroutine = _animator.StartCoroutine(ShootWave());
        }

        //private IEnumerator ShootWave()
        //{
        //    yield return new WaitForSeconds();
        //}
    }
}