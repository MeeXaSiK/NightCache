## NightCache - Fast Update Optimization-Caching System for Unity

Auto-Install void Update() optimization fast system by Night Train Code

The system caches all components, that need an void Update() into one list, then a single void Update() calls them all from the list

# How to use

https://www.youtube.com/watch?v=7Dvir9Bf8X4

1) Install NightCache components into your Unity project (it will calls all void Run, FixedRun and LateRun in scene)

2) Add component «NightCacheEntry» on any GameObject in scene

3) Inherit any class, which contains void Update / FixedUpdate / LateUpdate from «NightCache»

4) Implement the required interfaces: «INightInit», «INightRun», «INightFixedRun» or «INightLateRun»

5) Move the code from the old method to the new one

6) System will automatically add a new component «NightCacheInstallMachine» on GameObject, which contains a «NightCache» by RequireComponent. 
If for some reason this did not happen, then add it manually.

7) Everything is ready to use :)

UPD: See below documentation for MonoInstallable & NightCacheInstallable

# For example:

### Old implementation:

```sh
public class UnitMovement : MonoBehaviour
{
  private void Start()
  {
    //OnStart
  }

  private void MoveUnit()
  {
    //Movement
  }
  
  private void Update()
  {
    MoveUnit();
  }
}
```

### New implementation:

```sh
public class UnitMovement : NightCache, INightInit, INightRun
{
  public void Init()
  {
    //OnStart
  }

  private void MoveUnit()
  {
    //Movement
  }
  
  public void Run()
  {
    MoveUnit();
  }
}
```

# Interfaces

| Interface | Identical |
| ------ | ------ |
| INightInit : void Init() | void Start() |
| INightRun : void Run() | void Update() |
| INightFixedRun : void FixedRun() | void FixedUpdate() |
| INightLateRun : void LateRun() | void LateUpdate() |

# MonoInstallable & NightCacheInstallable

Installables contains virtual void OnFirstEnable()
This method is called once the first time the object is enabled

MonoInstallable for MonoBehaviour, 
NightCacheInstallable for NightCache

## For example:

Old:

```sh
public class UnitMovement : MonoBehaviour
{
  private bool installed;

  private void OnEnable()
  {
    if (installed) return;
    //DoSomething
    installed = true;
  }
}
```

New:

```sh
public class UnitMovement : MonoInstallable
{
  protected override void OnFirstEnable()
  {
    //DoSomething
  }
}
```

```sh
public class UnitMovement : NightCacheInstallable, INightRun
{
  protected override void OnFirstEnable()
  {
    //DoSomething
  }
  
  public void Run()
  {
    //Movement
  }
}
```
