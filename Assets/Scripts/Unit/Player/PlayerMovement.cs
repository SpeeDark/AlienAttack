using UnityEngine;

namespace Unit
{
    public class PlayerMovement : UnitMovement
    {
        [SerializeField] private Player player;

        private Rigidbody2D playerRigidbody2D;

        private float _sensitivity;

        private float BorderX;

        private void Awake()
        {
            Initialize();

            playerRigidbody2D = player.GetComponent<Rigidbody2D>();

            isPaused = false;

            float HalfPlayerSpriteWidth = player.GetComponent<SpriteRenderer>().sprite.rect.width / 2;
            float ScreenResolutionDiffrence = Screen.width / 1920f;

            float PositonNearBorder = HalfPlayerSpriteWidth * ScreenResolutionDiffrence;

            BorderX = Camera.main.ScreenToWorldPoint(new Vector3(PositonNearBorder, 0f, 0f)).x;
        }

        private void Initialize()
        {
            _sensitivity = player.Config.MouseSensitivity;
        }

        private void Update()
        {
            if (isPaused)
                return;

            Move();
        }

        public override void Move()
        {
            var mouseX = Input.GetAxis("Mouse X");

            if (mouseX != 0)
                playerRigidbody2D.position += new Vector2(mouseX * _sensitivity, 0f);

            if (playerRigidbody2D.position.x <= BorderX)
                playerRigidbody2D.position = new Vector2(BorderX, playerRigidbody2D.position.y);
            if (playerRigidbody2D.position.x >= -BorderX)
                playerRigidbody2D.position = new Vector2(-BorderX, playerRigidbody2D.position.y);
        }
    }
}