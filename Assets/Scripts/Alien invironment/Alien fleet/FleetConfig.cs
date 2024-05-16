using UnityEngine;

using Unit;

namespace AlienInvironment.Fleet
{
    [CreateAssetMenu(menuName = "AlienFleet/Settings", fileName = "FleetConfig")]
    public class FleetConfig : ScriptableObject
    {
        [SerializeField] private FleetTemplate[] fleetTemplates;

        public FleetTemplate[] FleetTemplates => fleetTemplates;
    }
}