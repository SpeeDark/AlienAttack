using UnityEngine;

using AlienInvironment.Fleet;
using Unit;

public class GameDifficulty : MonoBehaviour
{
    [Header("Start components")]
    [SerializeField] private BackgroundMoving backgroundMoving;
    [SerializeField] private FleetMovement fleetMovement;
    [SerializeField] private PlayerShooting playerShooting;
    [SerializeField] private PlayerRocketsPool playerRocketsPool;
    [SerializeField] private Player player;

    [Header("Difficulty ratios")]
    [SerializeField, Range(0.1f, 1f)] private float _playerTreatment;
    [SerializeField, Min(1.1f)] private float _backgroundSpeedRatio;
    [SerializeField, Min(1.1f)] private float _fleetSpeedRatio;
    [SerializeField, Min(1.1f)] private float _playerAttackRateRatio;
    [SerializeField, Min(1.1f)] private float _playerRocketSpeedRatio;
    [SerializeField, Min(0.025f)] private float _alienHealthRatioAdding;
    [SerializeField, Min(0.05f)] private float _alienKillPrizeRatioAdding;

    private float _alienHealthRatio = 1f;
    private float _alienKillPrizeRatio = 1f;

    [SerializeField, Min(1)] private int _difficultyAddingInterval = 2;
    private int _currentAddingDifficultySum = 0;

    private static GameDifficulty instance;

    public static float AlienHealthRatio => instance._alienHealthRatio;
    public static float AlienKillPrizeRatio => instance._alienKillPrizeRatio;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    public void Add()
    {
        if (CheckAddingPossibility())
        {
            print("yes");
            player.Treat(player.MaxHealth * _playerTreatment);
            backgroundMoving.AddDifficulty(_backgroundSpeedRatio);
            fleetMovement.AddDifficulty(_fleetSpeedRatio);
            playerShooting.AddDifficulty(_playerAttackRateRatio);
            playerRocketsPool.AddDifficulty(_playerRocketSpeedRatio);

            _alienHealthRatio += _alienHealthRatioAdding;
            _alienKillPrizeRatio += _alienKillPrizeRatioAdding;
        }
    }

    private bool CheckAddingPossibility()
    {
        if (_currentAddingDifficultySum == _difficultyAddingInterval)
        {
            _currentAddingDifficultySum = 0;
            print(_currentAddingDifficultySum);
            return true;
        }
        else
        {
            _currentAddingDifficultySum++;
            print(_currentAddingDifficultySum);
            return false;
        }
    }
}