using UnityEngine;

public class MenubackgroundScroll : MonoBehaviour
{
    [SerializeField] private Transform mainBackground;
    [SerializeField] private Transform secondBackground;

    [SerializeField] private float _speed = 1f;

    private Vector2 bottomPoint;
    private Vector2 topPoint;

    private void Awake()
    {
        BoxCollider2D collider = mainBackground.GetComponent<BoxCollider2D>();

        bottomPoint = new Vector2(-collider.size.x, 0f);
        topPoint = new Vector2(collider.size.x, 0f);
    }

    private void Update()
    {
        if (_speed > 0)
        {
            if (mainBackground.position.x <= bottomPoint.x)
                mainBackground.position = topPoint;
            else if (secondBackground.position.x <= bottomPoint.x)
                secondBackground.position = topPoint;
        }

        if (_speed < 0)
        {
            if (mainBackground.position.x >= topPoint.x)
                mainBackground.position = bottomPoint;
            else if (secondBackground.position.x >= topPoint.x)
                secondBackground.position = bottomPoint;
        }

        MoveBackgrounds();
    }

    private void MoveBackgrounds()
    {
        Vector3 movement = Vector3.left * Time.fixedDeltaTime * _speed;

        mainBackground.position += movement;
        secondBackground.position += movement;
    }
}