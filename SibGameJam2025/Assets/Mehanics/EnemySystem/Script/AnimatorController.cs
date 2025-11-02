using UnityEngine;

namespace EnemySystem
{
    public class AnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;

        public void SetIdle(bool value)
        {
            _animator.SetBool("Idle", value);
        }

        public void SetWalk(bool value)
        {
            _animator.SetBool("Walk", value);
        }

        public void SetAttack(bool value)
        {
            _animator.SetBool("Attack", value);
        }

        public void CallDeath()
        {
            _animator.SetTrigger("Death");
        }

        public void ShooseAttack(float index)
        {
            _animator.SetFloat("Attack Index", index);
        }
    }
}