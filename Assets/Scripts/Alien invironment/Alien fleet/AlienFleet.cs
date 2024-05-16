using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

using Unit;

namespace AlienInvironment.Fleet
{
    public class AlienFleet : MonoBehaviour
    {
        [Header("Components"), Space]
        [SerializeField] private FleetBuilder builder;
        [SerializeField] private FleetArrivalAnimator arrivalAnimator;
        [SerializeField] private AlienRocketsPool alienRocketsPool;
        [SerializeField] private AlienExplosionsPool alienExplosionsPool;

        [Header("FleetConfig")]
        [SerializeField] private FleetConfig config;

        private List<Alien> aliveAliens;

        private int _currentFleetIndex = 0;
        private int _currentRowIndex = 0;

        private int currentFleetIndex
        {
            get => _currentFleetIndex;
            set
            {
                _currentFleetIndex = value;

                if (_currentFleetIndex >= config.FleetTemplates.Length)
                    _currentFleetIndex = 0;

                FleetTemplateChanged.Invoke();

                UpdateFleetPools();
            }
        }

        private int currentRowIndex
        {
            get => _currentRowIndex;
            set
            {
                _currentRowIndex = value;

                if (_currentRowIndex >= currentAlienMap.NumberOfRow.Length)
                    _currentRowIndex = 0;
            }
        }

        private Alien currentAlien => config.FleetTemplates[currentFleetIndex].Alien;
        private AlienMap currentAlienMap => config.FleetTemplates[currentFleetIndex].AlienMap;
        public List<Alien> AliveAliens => aliveAliens;

        public UnityEvent FleetUpdated;
        public UnityEvent FleetTemplateChanged;

        private void Awake()
        {
            Initialize();
        }

        private void Start()
        {
            Create();
        }

        private void Initialize()
        {
            UpdateFleetPools();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.K) & !PauseManager.Instance.IsPaused)
                foreach (Alien alien in aliveAliens.ToArray())
                    alien.Kill();
        }

        public void Create()
        {
            aliveAliens = builder.BuildFleet(currentAlien, new Vector2Int(
                currentAlienMap.AliensInRow, currentAlienMap.NumberOfRow[currentRowIndex]));

            FleetUpdated.Invoke();

            arrivalAnimator.Animate(aliveAliens);
        }

        public void RemoveAlienFromFleet(Alien alien)
        {
            aliveAliens.Remove(alien);

            if (aliveAliens.Count <= 0)
            {
                currentRowIndex++;

                if (currentRowIndex == 0)
                {
                    currentFleetIndex++;
                    return;
                }

                Create();
            }
        }

        private void UpdateFleetPools()
        {
            alienRocketsPool.Create(currentAlien.Rocket);
            alienExplosionsPool.Create(currentAlien.Explosion);
        }
    }

    [System.Serializable]
    public class AlienMap
    {
        public int AliensInRow;
        public int[] NumberOfRow;
    }

    [System.Serializable]
    public class FleetTemplate
    {
        public Alien Alien;
        public AlienMap AlienMap;
    }
}