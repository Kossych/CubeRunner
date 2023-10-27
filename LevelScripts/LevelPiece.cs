using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LevelPiece : MonoBehaviour
{
    public event Action GenerateLevelPeaceEvent;


    public void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.TryGetComponent(out Player player))
        {
            GenerateLevelPeaceEvent?.Invoke();
        }
    }

}
