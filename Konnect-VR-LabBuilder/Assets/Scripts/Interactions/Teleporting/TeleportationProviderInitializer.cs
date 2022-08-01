using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace KonnectVR.Interactions
{
    [RequireComponent(typeof(TeleportationProvider))]
    public class TeleportationProviderInitializer : MonoBehaviour
    {
        private TeleportationProvider provider;
        private TeleportationArea[] teleportationAreas;

        private const int teleportationLayerIndex = 8;

        private void Start()
        {
            provider = GetComponent<TeleportationProvider>();

            teleportationAreas = FindObjectsOfType<TeleportationArea>();
            foreach (TeleportationArea area in teleportationAreas)
            {
                if (!area.teleportationProvider) //Set teleportation provider, not already set
                    area.teleportationProvider = provider;

                if (area.gameObject.layer != teleportationLayerIndex)
                    area.gameObject.layer = teleportationLayerIndex;
            }
        }
    }
}