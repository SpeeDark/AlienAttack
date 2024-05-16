using UnityEngine;

using Unit;
using AlienInvironment.Fleet;

namespace AlienInvironment
{
    public class ScreenBorder : MonoBehaviour
    {
        [SerializeField] private FleetMovement fleetMovement;

        [Range(-1, 1)]
        [SerializeField] private int _directionAfterCollision;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<Alien>())
            {
                if (fleetMovement.enabled == false)
                {
                    return;
                }

                fleetMovement.ChangeMovementDirection(new Vector2(_directionAfterCollision, 0f));
            }
        }
    }
}