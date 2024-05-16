using UnityEngine;

using Unit;
using Rocket;

public class AlienRocketStopper : MonoBehaviour
{
    [SerializeField] private Player player;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out AlienRocket alienRocket))
            alienRocket.OnHit.Invoke();

        if (other.GetComponent<Alien>())
            player.PlayerDead();
    }
}