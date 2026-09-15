using UnityEngine;
using UnityEngine.InputSystem;

namespace Project.Scripts.Common
{
    public class InputService
    {
        public float Horizontal { get; private set; }

        public void Run()
        {
            Horizontal = 0f;

            var pointer = Pointer.current;
            if (pointer == null) return;
            if (!pointer.press.isPressed) return;

            float x = pointer.position.ReadValue().x / Screen.width;

            if (x < 0.4f) Horizontal = -1f;
            else if (x > 0.6f) Horizontal = 1f;
        }
    }
}