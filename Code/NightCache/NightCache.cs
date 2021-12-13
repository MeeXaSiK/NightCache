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
            systemIsActiveInScene = true;

            if (this == null)
                return;

            enabled = true;
        }

        public void DisableComponent()
        {
            systemIsActiveInScene = false;

            if (this == null)
                return;

            enabled = false;
        }
    }
}