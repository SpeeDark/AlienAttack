using UnityEngine;

using Unit;

namespace AlienInvironment.Fleet
{
    public class FleetEventsSetter : MonoBehaviour
    {
        [SerializeField] private AlienFleet fleet;
        [SerializeField] private FleetMovement fleetMovement;
        [SerializeField] private FleetBuilder fleetBuilder;
        [SerializeField] private FleetArrivalAnimator fleetArrivalAnimator;
        [SerializeField] private AlienExplosionsPool alienExplosionsPool;
        [SerializeField] private ScoreCounter scoreCounter;

        [SerializeField] private InvironmentChanger InvironmentChanger;
        [SerializeField] private GameDifficulty gameDifficulty;

        private void Awake()
        {
            SetStartFleetEvents();
        }

        private void SetStartFleetEvents()
        {
            fleet.FleetUpdated.AddListener(SetAlienDeathEvents);
            fleet.FleetTemplateChanged.AddListener(gameDifficulty.Add);

            fleetBuilder.FleetStartCreating.AddListener(fleetMovement.ResetPosition);

            fleetArrivalAnimator.OnAnimationStart.AddListener(OnFleetArrivalAnimationStart);
            fleetArrivalAnimator.OnAnimationOver.AddListener(OnFleetArrivalAnimationOver);

            InvironmentChanger.InvironmentChanged.AddListener(fleet.Create);
        }

        private void SetAlienDeathEvents()
        {
            for (var i = 0; i < fleet.AliveAliens.Count; i++)
            {
                Alien alien = fleet.AliveAliens[i];

                alien.OnAlienDeath.AddListener(() => alienExplosionsPool.TakeExplosion(alien.transform.position));
                alien.OnAlienDeath.AddListener(() => scoreCounter.AddScore(alien.PrizeForKill * GameDifficulty.AlienKillPrizeRatio));
                alien.OnAlienDeath.AddListener(() => fleet.RemoveAlienFromFleet(alien));
            }
        }

        private void OnFleetArrivalAnimationOver()
        {
            for (var i = 0; i < fleet.AliveAliens.Count; i++)
                fleet.AliveAliens[i].GetComponent<AlienIdleAnimation>().enabled = true;

            fleetMovement.enabled = true;
        }

        private void OnFleetArrivalAnimationStart()
        {
            fleetMovement.enabled = false;
        }
    }
}