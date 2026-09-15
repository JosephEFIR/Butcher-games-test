using UnityEngine;

namespace Project.Scripts.Common
{
    public class CameraFollow : MonoBehaviour
    {
        [Header("Target")]
        public Transform Target;

        [Header("Position")]
        [Tooltip("Высота камеры над игроком")]
        public float Height = 5f;

        [Tooltip("Расстояние камеры сзади игрока")]
        public float Distance = 7f;

        [Tooltip("Боковое смещение камеры (0 = ровно за спиной)")]
        public float SideOffset = 0f;

        [Header("Angle")]
        [Tooltip("Угол наклона камеры вниз. 0 = смотрит горизонтально, 30 = смотрит вниз под 30°")]
        [Range(0f, 80f)]
        public float Pitch = 20f;

        [Header("Smoothing")]
        public float PositionSmooth = 8f;
        public float RotationSmooth = 8f;

        [Header("Look At")]
        [Tooltip("Смещение точки взгляда относительно игрока (Y вверх)")]
        public Vector3 LookAtOffset = new (0f, 1f, 0f);

        private void LateUpdate()
        {
            if (Target == null) return;
            
            float pitchRad = Pitch * Mathf.Deg2Rad;
            
            Vector3 localOffset = new Vector3(SideOffset, Height, -Distance * Mathf.Cos(pitchRad));
            
            localOffset.y = Height + Distance * Mathf.Sin(pitchRad);

            Vector3 desiredPos = Target.position + Target.rotation * localOffset;

            transform.position = Vector3.Lerp(
                transform.position, desiredPos, PositionSmooth * Time.deltaTime);
            
            Vector3 lookTarget = Target.position + LookAtOffset;
            Vector3 lookDir = lookTarget - transform.position;
            if (lookDir.sqrMagnitude > 0.0001f)
            {
                Quaternion desiredRot = Quaternion.LookRotation(lookDir, Vector3.up);
                transform.rotation = Quaternion.Slerp(transform.rotation, desiredRot, RotationSmooth * Time.deltaTime);
            }
        }
    }
}