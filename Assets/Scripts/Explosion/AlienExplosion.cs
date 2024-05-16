using UnityEngine;
using UnityEngine.Events;

public class AlienExplosion : MonoBehaviour
{
    private ParticleSystem explosionParticle;
    private AudioSource explosionSound;

    public UnityEvent OnDisable;

    private void Awake()
    {
        explosionParticle = GetComponent<ParticleSystem>();
        explosionSound = GetComponent<AudioSource>();
    }

    private void OnParticleSystemStopped()
    {
        OnDisable.Invoke();
    }

    public void TurnOn()
    {
        gameObject.SetActive(true);

        explosionParticle.Play();

        explosionSound.Play();
    }

    public void TurnOff()
    {
        gameObject.SetActive(false);
    }
}