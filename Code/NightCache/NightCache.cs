using UnityEngine;

namespace NTC.Global.Cache
{
    [RequireComponent(typeof(NightCacheInstallMachine))]
    public abstract class NightCache : MonoBehaviour, INightCached
    {
        public int GetID() => cachedInstanceId ??= GetInstanceID();
        private int? cachedInstanceId;

        public GameObject CachedGameObject => cachedGameObject ??= gameObject;
        private GameObject cachedGameObject;
        
        public Transform CachedTransform => cachedTransform ??= transform;
        private Transform cachedTransform;

        private bool systemIsActiveInScene;

        public bool IsActive()
        {
            return systemIsActiveInScene;
        }

        public void EnableComponent()
        {
            systemIsActiveInScene = true;
        }

        public void DisableComponent()
        {
            systemIsActiveInScene = false;
        }
    }
}