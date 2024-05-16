using System.Collections.Generic;
using UnityEngine;

using Rocket;

public class AlienRocketsPool : MonoBehaviour
{
    private ObjectsPool<AlienRocket> AlienRockets = null;

    private List<AlienRocket> allAlienRockets = null;

    private AlienRocket alienRocketPrefab;

    [Space(15f)]
    [SerializeField] private int _rocketsSum = 15;

    public void Create(AlienRocket alienRocket)
    {
        alienRocketPrefab = alienRocket;

        if (allAlienRockets != null)
            DestroyAll();

        allAlienRockets = new List<AlienRocket>(_rocketsSum);
        AlienRockets = new ObjectsPool<AlienRocket>(_rocketsSum, PreloadExplosion, OnGetExplosion, OnReturnExplosion);
    }

    public AlienRocket GetRocket()
    {
        return AlienRockets.Get();
    }

    private void DestroyAll()
    {
        foreach (AlienRocket alienRocket in allAlienRockets)
            Destroy(alienRocket.gameObject);
    }

    private AlienRocket PreloadExplosion()
    {
        AlienRocket Rocket = Instantiate(alienRocketPrefab);

        Rocket.transform.SetParent(this.transform);
        Rocket.gameObject.SetActive(false);

        Rocket.OnHit.AddListener(() => AlienRockets.Return(Rocket));

        allAlienRockets.Add(Rocket);

        return Rocket;
    }

    private void OnGetExplosion(AlienRocket Rocket) => Rocket.gameObject.SetActive(true);
    private void OnReturnExplosion(AlienRocket Rocket) => Rocket.gameObject.SetActive(false);
}