using UnityEngine;
using System;


public class Cube : Moveable
{
    [HideInInspector] public bool IsAttached = false;
    public event Action<Cube> CubeCollisionEvent;
    public event Action<Cube> WallCollisionEvent;
    
    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent(out Cube cube))
        {
            if(cube.IsAttached) return;
            CubeCollisionEvent?.Invoke(cube);
        }
        if(collider.gameObject.TryGetComponent(out Wall wall))
        {
            WallCollisionEvent?.Invoke(this);
        }
    }
}
