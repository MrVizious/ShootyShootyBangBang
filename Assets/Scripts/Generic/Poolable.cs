using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Poolable : MonoBehaviour
{
    public UnityEvent onSpawn;
    public UnityEvent onDespawn;
    public abstract Poolable Spawn();
    public abstract Poolable Despawn();

}