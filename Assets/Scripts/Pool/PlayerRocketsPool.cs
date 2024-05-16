using System.Collections.Generic;
using UnityEngine;

using Rocket;

public class PlayerRocketsPool : MonoBehaviour, IDifficultyAddable
{
    private ObjectsPool<PlayerRocket> PlayerRockets;
    private ObjectsPool<PlayerRocketExplosion> RocketExplosions;

    [SerializeField] private PlayerRocket playerRocketPrefab;
    [SerializeField] private PlayerRocketExplosion playerRocketExplosionPrefab;

    [Space(15f)]
    [SerializeField] private Transform rocketsContainer;
    [SerializeField] private Transform rocketExplosionsContainer;

    [Space(15f)]
    [Min(3), SerializeField] private int _rocketSum = 15;

    private List<PlayerRocket> rockets = null;

    private void Awake()
    {
        Create();
    }

    private void Create()
    {
        rockets = new List<PlayerRocket>(_rocketSum);

        PlayerRockets = new ObjectsPool<PlayerRocket>(_rocketSum, PreloadRocket, OnGetRocket, OnReturnRocket);
        RocketExplosions = new ObjectsPool<PlayerRocketExplosion>(_rocketSum, PreloadExplosion, OnGetExplosion, OnReturnExplosion);
    }

    public void AddDifficulty(float ratio)
    {
        for (var i = 0; i < rockets.Count; i++)
            rockets[i].GetComponent<PlayerRocketMovement>().AddDifficulty(ratio);
    }

    public void ReturnAll()
    {
        for (var i = 0; i < rockets.Count; i++)
            if (rockets[i].gameObject.activeSelf == true)
                PlayerRockets.Return(rockets[i]);
    }

    public PlayerRocket GetRocket()
    {
        return PlayerRockets.Get();
    }

    private void TakeRocketExplosion(Vector2 ExplosionPosition)
    {
        PlayerRocketExplosion Explosion = RocketExplosions.Get();
        Explosion.transform.position = ExplosionPosition;
    }

    private PlayerRocket PreloadRocket()
    {
        PlayerRocket Rocket = Instantiate(playerRocketPrefab);

        Rocket.transform.SetParent(rocketsContainer);
        Rocket.gameObject.SetActive(false);

        Rocket.OnHitTarget.AddListener(() => TakeRocketExplosion(Rocket.transform.position));
        Rocket.OnHitTarget.AddListener(() => PlayerRockets.Return(Rocket));
        Rocket.OnReachBorder.AddListener(() => PlayerRockets.Return(Rocket));

        rockets.Add(Rocket);

        return Rocket;
    }

    private PlayerRocketExplosion PreloadExplosion()
    {
        PlayerRocketExplosion Explosion = Instantiate(playerRocketExplosionPrefab);

        Explosion.transform.SetParent(rocketExplosionsContainer);
        Explosion.gameObject.SetActive(false);

        Explosion.OnDisable.AddListener(() => RocketExplosions.Return(Explosion));

        return Explosion;
    }

    private void OnGetRocket(PlayerRocket playerRoket) => playerRoket.gameObject.SetActive(true);
    private void OnReturnRocket(PlayerRocket playerRoket) => playerRoket.gameObject.SetActive(false);

    private void OnGetExplosion(PlayerRocketExplosion Explosion) => Explosion.TurnOn();
    private void OnReturnExplosion(PlayerRocketExplosion Explosion) => Explosion.TurnOff();
}