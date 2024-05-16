using UnityEngine;

namespace AlienInvironment
{
    public class ScreenBorderSetter : MonoBehaviour
    {
        [SerializeField] private Transform leftBorder;
        [SerializeField] private Transform rightBorder;

        private void Awake()
        {
            var MainCamera = Camera.main;

            leftBorder.position = MainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height / 2f, 0f));
            rightBorder.position = MainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2f, 0f));

            var LeftBoardCollider = leftBorder.GetComponent<BoxCollider2D>();
            var RightBoardCollider = rightBorder.GetComponent<BoxCollider2D>();

            leftBorder.position -= new Vector3(LeftBoardCollider.size.x / 2, 0f, 0f);
            rightBorder.position += new Vector3(RightBoardCollider.size.x / 2, 0f, 0f);
        }
    }
}