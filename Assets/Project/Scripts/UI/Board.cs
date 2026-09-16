using UnityEngine;

namespace Project.Scripts.UI
{
    public class Board : MonoBehaviour
    {
        [SerializeField] private EBoardType _type;

        public EBoardType Type => _type;

        public void Show()
        { 
            gameObject.SetActive(true);
        }

        public void Hide()
        { 
            gameObject.SetActive(false);
        }
    }
}