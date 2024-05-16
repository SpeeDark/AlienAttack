using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Unit;

namespace AlienInvironment.Fleet
{
    public class FleetBuilder : MonoBehaviour
    {
        [SerializeField] private float _intervalBeetwenAliens = 0.9f;

        private float _screenWidth;
        private float _halfScreenHeight;

        private Vector2 _leftTopScreenPoint;

        public UnityEvent FleetStartCreating;

        private void Awake()
        {
            var mainCamera = Camera.main;

            var leftTopScreenPoint = mainCamera.ScreenToWorldPoint(new Vector3(0f, Screen.height, 0f));
            var rightCentreScreenPoint = mainCamera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height / 2f, 0f));

            _leftTopScreenPoint = leftTopScreenPoint;

            _screenWidth = rightCentreScreenPoint.x - leftTopScreenPoint.x;
            _halfScreenHeight = leftTopScreenPoint.y - rightCentreScreenPoint.y;
        }

        public List<Alien> BuildFleet(Alien alien, Vector2Int alienMap)
        {
            FleetStartCreating.Invoke();

            var alienSum = alienMap.x * alienMap.y;

            List<Alien> fleet = new List<Alien>(alienSum);

            var distance = new Vector2(_screenWidth / alienMap.x, _halfScreenHeight / alienMap.y);

            for (var y = 0; y < alienMap.y; y++)
                for (var x = 0; x < alienMap.x; x++)
                {
                    var PosX = (_leftTopScreenPoint.x + (distance.x * 0.5f) + (distance.x * x)) * _intervalBeetwenAliens;
                    var PosY = _leftTopScreenPoint.y - (distance.y / (alienMap.y + 1)) - distance.y * y;

                    if (x == 0)
                        print(PosY);

                    PosX += Random.Range(distance.x * -0.16f, distance.x * 0.16f);

                    Alien Alien = Instantiate(alien, new Vector2(PosX, PosY), alien.transform.rotation);

                    fleet.Add(Alien);
                    Alien.transform.SetParent(this.transform);
                }

            return fleet;
        }
    }
}