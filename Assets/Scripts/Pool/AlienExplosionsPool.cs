using System.Collections.Generic;
using UnityEngine;

public class AlienExplosionsPool : MonoBehaviour
{
    private ObjectsPool<AlienExplosion> AlienExplosions = null;

    private List<AlienExplosion> allAlienExplosions = null;

    private AlienExplosion alienExplosionPrefab;

    [Space(15f)]
    [SerializeField] private int _explosionsSum = 10;

    public void Create(AlienExplosion alienExplosion)
    {
        alienExplosionPrefab = alienExplosion;

        if (AlienExplosions != null)
            DestroyAll();

        allAlienExplosions = new List<AlienExplosion>(_explosionsSum);
        AlienExplosions = new ObjectsPool<AlienExplosion>(_explosionsSum, PreloadExplosion, OnGetExplosion, OnReturnExplosion);
    }

    public void TakeExplosion(Vector2 AlienPosition)
    {
        AlienExplosions.Get().transform.position = AlienPosition;
    }

    private void DestroyAll()
    {
        foreach (AlienExplosion alienExplosion in allAlienExplosions)
            Destroy(alienExplosion.gameObject);
    }

    private AlienExplosion PreloadExplosion()
    {
        AlienExplosion Explosion = Instantiate(alienExplosionPrefab);

        Explosion.transform.SetParent(this.transform);
        Explosion.gameObject.SetActive(false);

        Explosion.OnDisable.AddListener(() => AlienExplosions.Return(Explosion));

        allAlienExplosions.Add(Explosion);

        return Explosion;
    }

    private void OnGetExplosion(AlienExplosion Explosion) => Explosion.TurnOn();
    private void OnReturnExplosion(AlienExplosion Explosion) => Explosion.TurnOff();
}