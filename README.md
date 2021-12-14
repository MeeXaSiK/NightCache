# NightCache
> Fast Update Optimization-Caching Framework for Unity

* Auto-Install optimization fast framework by [**Night Train Code**](https://www.youtube.com/c/NightTrainCode/)

* The framework caches all Update methods in one thread, that gives [≈25% performance](https://youtu.be/7Dvir9Bf8X4?t=178)

* Also framework gives more useful features (more details below in the documentation)

# Navigation

* [Main](#nightcache-12)
* [Installation](#how-to-use)
  * [How to use](#how-to-use)
  * [Interfaces](#all-interfaces)
* [Mono Installable and NightCache Installable](#mono-installable-and-nightcache-installable)
* [Cached Base Components](#cached-base-components)
* [NightCache 1.1 [Update]](#whats-new-in-nightcache-11)
  * [Enable & Disable Component](#enable-and-disable-component)
* [NightCache 1.2 [Update]](#whats-new-in-nightcache-12)
  * [MonoAllocation](#component-allocation)
  * [NightSugar](#nightsugar-static-class-attached)
  * [GetInfo < TClass >](#getinfot)
  
## How to use

> YouTube Video based on NightCache 1.0 (last release is 1.2)

[![NightCache YouTube Video](https://img.youtube.com/vi/7Dvir9Bf8X4/0.jpg)](https://www.youtube.com/watch?v=7Dvir9Bf8X4)

***

> `using NTC.Global.Cache;`

1. Install `NightCache` into your Unity project (it will calls all `void Run`, `FixedRun` and `LateRun` in scene)

2. Add component `NightCacheEntry` on any GameObject in scene

3. Inherit any class, which contains `void Update()` / `FixedUpdate()` / `LateUpdate()` from `NightCache`

4. Implement the required interfaces: `INightInit`, `INightRun`, `INightFixedRun` or `INightLateRun`

5. Move the code from the old method to the new one

6. System will automatically add a new component `NightCacheInstallMachine` on GameObject, which contains a `NightCache` by RequireComponent. If for some reason this did not happen, then add it manually.

7) Everything is ready to use :)

***

## For example:

### Old implementation:

```csharp
public class UnitMovement : MonoBehaviour
{
    private void Start()
    {
        //OnStart
    }

    private void Update()
    {
        MoveUnit();
    }

    private void MoveUnit()
    {
        //Movement
    }
}
```

### New implementation:

```csharp
public class UnitMovement : NightCache, INightInit, INightRun
{
    public void Init()
    {
        //OnStart
    }

    public void Run()
    {
        MoveUnit();
    }

    private void MoveUnit()
    {
        //Movement
    }
}
```

### All interfaces

| Interface | Identical |
| ------ | ------ |
| INightInit : ```void Init()``` | ```void Start()``` |
| INightRun : ```void Run()``` | ```void Update()``` |
| INightFixedRun : ```void FixedRun()``` | ```void FixedUpdate()``` |
| INightLateRun : ```void LateRun()``` | ```void LateUpdate()``` |

***
# Mono Installable and NightCache Installable

Installables contains virtual void `OnFirstEnable()`
This method is called once the first time the object is enabled

`MonoInstallable` for `MonoBehaviour`, 
`NightCacheInstallable` for `NightCache`

### For example:

Old:

```csharp
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

```csharp
public class UnitMovement : MonoInstallable
{
    protected override void OnFirstEnable()
    {
      //DoSomething
    }
    
    private void Update()
    {
        //Movement
    }
}
```

```csharp
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

***
# Cached Base Components 

> [Performance test](https://youtu.be/7Dvir9Bf8X4?t=279)

| Old | New |
| ------ | ------ |
| ```transform``` | ```CachedTransform``` |
| ```gameObject``` | ```CachedGameObject``` |
| ```GetInstanceID()``` | ```GetID()``` |

```csharp
public class Enemy : NightCache
{ 

}

public class Player : NightCache
{
    private Enemy _enemy;

    private void Start()
    {
        var enemyTransform = _enemy.CachedTransform;
        var enemyGameObject = _enemy.CachedGameObject;
    }
}
```

***
# What's new in NightCache 1.1

###### Enable and Disable component

* ```EnableComponent()``` and ```DisableComponent()``` now replaces ```SetNightCacheSystemActive(status)```
* `NightCacheInstallMachine` now uses an `Array` instead of a `List`

***
# What's new in NightCache 1.2

</br>

### **Component Allocation**

> ***You can get components cached (≈30% faster than regular methods)***

| Old | New | With Allocation |
| ------ | ------ | ------ |
| ```GetComponent<T>()``` | ```Get<T>()``` | ```GetCached<T>()``` |
| ```GetComponents<T>()``` | ```Gets<T>()``` | ```GetsCached<T>()``` |
| ```GetComponentInChildren<T>()``` | ```ChildrenGet<T>()``` | ```ChildrenGetCached<T>()``` |
| ```GetComponentsInChildren<T>()``` | ```ChildrenGets<T>()``` | ```ChildrenGetsCached<T>()``` |
| ```GetComponentInParent<T>()``` | ```ParentGet<T>()``` | ```ParentGetCached<T>()``` |
| ```GetComponentsInParent<T>()``` | ```ParentGets<T>()``` | ```ParentGetsCached<T>()``` |
| ```FindObjectOfType<T>()``` | ```Find<T>()``` | ```FindCached<T>()``` |
| ```FindObjectsOfType<T>()``` | ```Finds<T>()``` | ```FindsCached<T>()``` |

### Old implementation:

```csharp
public class Player : MonoBehaviour
{
    private void Start()
    {
        var health = GetComponent<UnitHealth>();
        var viewModel = GetComponentInChildren<UnitViewModel>();
    }
}
```

### New implementation

> Also you can use `EnableAllocation()` or `DisableAllocation()`. Allocation enabled initially

```csharp
public class Player : NightCache
{
    private void Start()
    {
        var health = Get<UnitHealth>();
        var viewModel = ChildrenGet<UnitViewModel>();
    }
}
```
or
```csharp
public class Player : MonoAllocation
{
    private void Start()
    {
        var health = Get<UnitHealth>();
        var viewModel = ChildrenGet<UnitViewModel>();
    }
}
```

<br>

</br>

### `NightSugar` static class attached

> `using NTC.Global.System;` or `using static NTC.Global.System.NightSugar`;

| Method | Info |
| ------ | ------ |
| `IfNotNull()` | `GetComponent<T>().IfNotNull(o => Debug.Log($"{typeof(T).Name} not null"))` |
| `IfNull()` | `GetComponent<T>().IfNull(o => Debug.Log($"{typeof(T).Name} is null"))` |
| `Enable()` | Replaces `SetActive(true)` |
| `Disable()` | Replaces `SetActive(false)` |
| `EnableParent()` | Tries to enable parent gameObject |
| `DisableParent()` | Tries to disable parent gameObject |
| `TryGetParent()` | Tries to get parent transform |
| `TryGetChild()` | Tries to get first child transform |
| `GetNearby<T>()` | Tries to get component in parent and in children |

<br>

</br>

### `GetInfo<T>`

You can get component index or type by `static class GetInfo<T>`

> `using NTC.Global.System;`

```csharp
  var id = GetInfo<Player>.Index;
  var type = GetInfo<Player>.Type;
```

<br>
