using UnityEngine;

using AlienInvironment.Fleet;
using UnityEngine.Events;

public class InvironmentChanger : MonoBehaviour
{
    [SerializeField] private ObscureCameraView cameraView;
    [SerializeField] private PlayerRocketsPool playerRocketsPool;
    [SerializeField] private BackgroundChanger backgroundChanger;
    [SerializeField] private AlienFleet fleet;

    public UnityEvent InvironmentChanged;

    private void Awake()
    {
        fleet.FleetTemplateChanged.AddListener(cameraView.Close);
        cameraView.Closed.AddListener(Change);
    }

    private void Change()
    {
        backgroundChanger.SetNext();
        playerRocketsPool.ReturnAll();
        cameraView.Open();

        InvironmentChanged.Invoke();
    }
}