using UnityEngine;

public class BackgroundMoving : MonoBehaviour, IPauseHandler, IDifficultyAddable
{
    [SerializeField] private Transform mainBackground;
    [SerializeField] private Transform secondBackground;

    [SerializeField] private float _speed = 2f;

    private Vector2 bottomPoint;
    private Vector2 topPoint;

    public bool isPaused { get; set; }

    private void Awake()
    {
        BoxCollider2D collider = mainBackground.GetComponent<BoxCollider2D>();
        Vector2 secondBackgroundPosition = new Vector2(0, collider.size.y);

        secondBackground.position = secondBackgroundPosition;

        bottomPoint = new Vector2(0f, -collider.size.y);
        topPoint = new Vector2(0f, collider.size.y);
    }

    private void FixedUpdate()
    {
        if (isPaused)
            return;

        if (mainBackground.position.y >= topPoint.y)
            mainBackground.position = bottomPoint;
        else if (secondBackground.position.y >= topPoint.y)
            secondBackground.position = bottomPoint;

        MoveBackgrounds();
    }
    private void MoveBackgrounds()
    {
        Vector3 movement = Vector3.down * Time.fixedDeltaTime * _speed;

        mainBackground.position += movement;
        secondBackground.position += movement;
    }

    public void SetPauseState(bool isPaused)
    {
        this.isPaused = isPaused;
    }

    public void AddDifficulty(float ratio)
    {
        _speed *= ratio;
    }

    private void OnEnable()
    {
        PauseManager.Instance.Register(this);
    }

    private void OnDisable()
    {
        PauseManager.Instance.Unregister(this);
    }

    private void OnDestroy()
    {
        PauseManager.Instance.Unregister(this);
    }
}