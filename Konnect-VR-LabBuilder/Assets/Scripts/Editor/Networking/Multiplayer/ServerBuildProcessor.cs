using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace KonnectVR.Networking.Multiplayer
{
    public class ServerBuildProcessor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
#if UNITY_SERVER
            //Disable XR when building server
            UnityEngine.XR.XRSettings.enabled = false;
#endif
        }
    }
}