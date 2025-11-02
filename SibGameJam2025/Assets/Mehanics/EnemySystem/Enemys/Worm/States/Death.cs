namespace EnemySystem.Worm
{
    public class Death : State
    {
        private AnimatorController _animator;

        public Death(IStateSwitcher stateSwitcher, AnimatorController animator) : base(stateSwitcher)
        {
            _animator = animator;
        }

        public override void Start()
        {
            _animator.CallDeath();
        }

        public override void Stop()
        {
            
        }

        public override void Update()
        {
           
        }
    }
}