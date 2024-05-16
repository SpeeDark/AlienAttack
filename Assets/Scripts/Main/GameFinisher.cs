using UnityEngine;
using UnityEngine.Events;

using Unit;

public class GameFinisher : MonoBehaviour
{
    public static GameFinisher Instance;

    [SerializeField] private Player player;
    [SerializeField] private RecordSaver recordSaver;
    
    private bool _gameIsOver = false;

    public UnityEvent OnGameOver;
    public bool GameIsOver => _gameIsOver;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

            player.OnPlayerShipsOver.AddListener(GameOver);

            OnGameOver.AddListener(recordSaver.SaveRecord);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void GameOver()
    {
        _gameIsOver = true;
        OnGameOver.Invoke();
    }
}