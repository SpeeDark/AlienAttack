using UnityEngine;

namespace Unit
{
    public class AlienIdleAnimation : UnitMovement
    {
        private Transform _transform;

        [SerializeField] private AnimationCurve AxisX;
        [SerializeField] private AnimationCurve AxisY;

        private Vector2 _localPosition;

        private float _curveTimeX;
        private float _curveTimeY;

        private float _startCurveTimeX;
        private float _startCurveTimeY;

        private float _endCurveTimeX;
        private float _endCurveTimeY;

        private int _directionX = 1;
        private int _directionY = 1;

        private void Awake()
        {
            _transform = GetComponent<Transform>();

            isPaused = false;

            _localPosition = _transform.localPosition;

            _startCurveTimeX = AxisX[0].time;
            _startCurveTimeY = AxisY[0].time;

            _endCurveTimeX = AxisX[AxisX.length - 1].time;
            _endCurveTimeY = AxisY[AxisY.length - 1].time;

            _curveTimeX = Random.Range(_startCurveTimeX, _endCurveTimeX);
            _curveTimeY = Random.Range(_startCurveTimeY, _endCurveTimeY);
        }

        private void FixedUpdate()
        {
            if (isPaused)
                return;

            Move();
        }

        public override void Move()
        {
            _transform.localPosition = GetMotionVector();
        }

        public Vector2 GetNextPosition()
        {
            Vector2 NextPosition = new Vector2(AxisX.Evaluate(_curveTimeX) + _transform.position.x,
                AxisY.Evaluate(_curveTimeY) + _transform.position.y);

            return NextPosition;
        }

        private Vector2 GetMotionVector()
        {
            Vector2 MotionVector = new Vector2(AxisX.Evaluate(_curveTimeX), AxisY.Evaluate(_curveTimeY));

            MotionVector += _localPosition;

            _curveTimeX += Time.fixedDeltaTime * _directionX;
            _curveTimeY += Time.fixedDeltaTime * _directionY;

            CheckDirection();

            return MotionVector;
        }

        private void CheckDirection()
        {
            if (_curveTimeX >= _endCurveTimeX)
                _directionX = -1;
            else if (_curveTimeX <= _startCurveTimeX)
                _directionX = 1;

            if (_curveTimeY >= _endCurveTimeY)
                _directionY = -1;
            else if (_curveTimeY <= _startCurveTimeY)
                _directionY = 1;
        }
    }
}