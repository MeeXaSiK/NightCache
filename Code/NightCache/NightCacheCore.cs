using System;
using System.Collections.Generic;
using NTC.Global.System;

namespace NTC.Global.Cache
{
    public static class NightCacheCore
    {
        private static readonly List<INightRun> RunSystems = new List<INightRun>(256);
        private static readonly List<INightFixedRun> FixedRunSystems = new List<INightFixedRun>(64);
        private static readonly List<INightLateRun> LateRunSystems = new List<INightLateRun>(64);

        private static int runCount;
        private static int fixedRunCount;
        private static int lateRunCount;
        
        public static event Action OnRun;
        public static event Action OnFixedRun;
        public static event Action OnLateRun;

        public static void AddSystem(INightCached nightCached)
        {
            if (nightCached is INightRun runSystem) RunSystems.Add(runSystem);
            if (nightCached is INightFixedRun fixedRunSystem) FixedRunSystems.Add(fixedRunSystem);
            if (nightCached is INightLateRun lateRunSystem) LateRunSystems.Add(lateRunSystem);
            
            UpdateCounts();
        }

        public static void RemoveSystem(INightCached nightCached)
        {
            if (nightCached is INightRun runSystem) RunSystems.Remove(runSystem);
            if (nightCached is INightFixedRun fixedRunSystem) FixedRunSystems.Remove(fixedRunSystem);
            if (nightCached is INightLateRun lateRunSystem) LateRunSystems.Remove(lateRunSystem);

            UpdateCounts();
        }
        
        public static void Run()
        {
            for (var i = 0; i < runCount; i++)
                if (RunSystems[i].IsActive()) RunSystems[i].Run();
            
            OnRun?.Invoke();
        }

        public static void FixedRun()
        {
            for (var i = 0; i < fixedRunCount; i++)
                if (FixedRunSystems[i].IsActive()) FixedRunSystems[i].FixedRun();
            
            OnFixedRun?.Invoke();
        }

        public static void LateRun()
        {
            for (var i = 0; i < lateRunCount; i++)
                if (LateRunSystems[i].IsActive()) LateRunSystems[i].LateRun();
            
            OnLateRun?.Invoke();
        }

        public static void Reset()
        {
            ResetLists();
            ResetActions(); 
            UpdateCounts();
        }

        private static void ResetLists()
        {
            RunSystems?.Clear();
            FixedRunSystems?.Clear();
            LateRunSystems?.Clear();
        }

        private static void ResetActions()
        {
            OnRun?.SetNull();
            OnFixedRun?.SetNull();
            OnLateRun?.SetNull();
        }
        
        private static void UpdateCounts()
        {
            runCount = RunSystems.Count;
            fixedRunCount = FixedRunSystems.Count;
            lateRunCount = LateRunSystems.Count;
        }
    }
}