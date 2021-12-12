using UnityEngine;

namespace NTC.Global.Cache
{
    [RequireComponent(typeof(NightCacheInstallMachine))]
    public abstract class NightCache : MonoAllocation, INightCached
    {
        private bool systemIsActiveInScene;

        public bool IsActive()
        {
            return systemIsActiveInScene;
        }

        public void EnableComponent()
        {
            enabled = true;
            systemIsActiveInScene = true;
        }

        public void DisableComponent()
        {
            enabled = false;
            systemIsActiveInScene = false;
        }
    }
}