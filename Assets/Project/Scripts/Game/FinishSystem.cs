using Project.Scripts.Player;

namespace Project.Scripts.Game
{
    public class FinishSystem
    {
        private readonly PlayerModel _model;
        private readonly int _winThreshold;

        public FinishSystem(PlayerModel model, int winThreshold)
        {
            _model = model;
            _winThreshold = winThreshold;
        }

        public bool IsWin => _model.Coins.Value >= _winThreshold;
    }
}