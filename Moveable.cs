using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(BoxCollider))]
public abstract class Moveable: MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidBody;

    public Rigidbody Rigidbody => _rigidBody;

    protected virtual void Awake()
    {
        if(_rigidBody == null) _rigidBody = GetComponent<Rigidbody>();
    }

    public void Move(float speed, float sidewaysSpeed)
    {
        Vector3 offset = Vector3.forward * (speed * Time.fixedDeltaTime);
        offset += Vector3.left * (sidewaysSpeed * speed);
        transform.position += offset;
    }
}
