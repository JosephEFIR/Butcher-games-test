using Project.Scripts.Config;
using UniRx;

namespace Project.Scripts.Player
{
    public class PlayerModel
    {
        public ReactiveProperty<float> Coins = new();
        public bool IsAlive { get; set; } = true;
        public bool IsFinished { get; set; }

        public float MaxRichness { get; }

        public PlayerModel(PlayerConfig config)
        {
            MaxRichness = config.MaxRichness;
        }
    }
}