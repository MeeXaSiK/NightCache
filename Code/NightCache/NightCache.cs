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
            if (this == null)
                return;
            
            enabled = true;
            systemIsActiveInScene = true;
        }

        public void DisableComponent()
        {
            if (this == null)
                return;
            
            enabled = false;
            systemIsActiveInScene = false;
        }
    }
}