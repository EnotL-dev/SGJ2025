namespace EnemySystem
{
    public interface IStateSwitcher
    {
        public void SwitchState<T>() where T : State;
        public bool FierstStateIsLaunched { get; set; }
    }
}