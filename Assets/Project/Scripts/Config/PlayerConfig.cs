using UnityEngine;

namespace Project.Scripts.Config
{
    [CreateAssetMenu(fileName = "PlayerConfig", menuName = "Game/Player Config")]
    public class PlayerConfig : ScriptableObject
    {
        public float ForwardSpeed = 8f;
        public float LateralSpeed = 4f;
        public float XLimit = 1.5f;
        public float TurnSmooth = 8f;
        public float LateralSmooth = 10f;

        [Header("Walls")]
        public LayerMask WallLayer;
        public float PlayerRadius = 0.4f;
        public float WallCheckDistance = 5f;
        
        [Header("Obstacles")]
        public LayerMask ObstacleLayer;
        public float ObstacleCheckDistance = 0.8f;

        [Header("Richness")]
        public float MaxRichness = 100f;
    }
}