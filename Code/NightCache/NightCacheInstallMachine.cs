using NTC.Global.System;
using UnityEngine;

namespace NTC.Global.Cache
{
    [DisallowMultipleComponent]
    public sealed class NightCacheInstallMachine : MonoInstallable
    {
        private INightCached[] components;
        
        protected override void OnFirstEnable()
        { 
            InstallComponents();
        }

        private void InstallComponents()
        {
            FindComponents();
            
            InitializeComponents();
            
            AddComponents();
        }

        private void FindComponents()
        {
            components = GetComponents<INightCached>();
        }
        
        private void AddComponents()
        {
            foreach (var component in components)
                NightCacheCore.AddSystem(component);
        }
        
        private void RemoveComponents()
        {
            foreach (var component in components)
                NightCacheCore.RemoveSystem(component);
        }
        
        private void InitializeComponents()
        {
            foreach (var component in components)
                if (component is INightInit initSystem) initSystem.Init();
        }

        protected override void OnLateEnable()
        {
            foreach (var component in components)
                component.EnableComponent();
        }

        private void OnDisable()
        {
            foreach (var component in components)
                component.DisableComponent();
        }

        private void OnDestroy()
        {
            RemoveComponents();
        }
    }
}