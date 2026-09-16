namespace Project.Scripts.Player
{
    public class PlayerVFX
    {
        private readonly PlayerView _view;

        public PlayerVFX(PlayerView view)
        {
            _view = view;
        }

        public void PlayPositive()
        {
            if (_view.PositiveVFX != null)
                _view.PositiveVFX.Play();
        }

        public void PlayNegative()
        {
            if (_view.NegativeVFX != null)
                _view.NegativeVFX.Play();
        }
    }
}