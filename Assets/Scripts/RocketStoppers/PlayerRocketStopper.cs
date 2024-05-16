using UnityEngine;

using Rocket;

public class PlayerRocketStopper : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out PlayerRocket playerRocket))
            playerRocket.OnReachBorder.Invoke();
    }
}