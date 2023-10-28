using System;
using UnityEngine;


public class MovementController : MonoBehaviour
{
    [SerializeField] InputController _input;
    private bool _isActive = false;

    [Header("Parameters")]
    [Range(5, 10)]
    [SerializeField] private float _speed;
    [Range(1f, 2.5f)]
    [SerializeField] private float _levelFieldX;

    public void FixedUpdate()
    {
        if(!_isActive) return;
        Move(_speed, _input.Dx);
    }
    
    
    public void Move(float speed, float sidewaysSpeed)
    {
        Vector3 offset = Vector3.forward * (speed * Time.fixedDeltaTime);
        offset += Vector3.left * (sidewaysSpeed * _levelFieldX * speed * Time.fixedDeltaTime);
        offset.x = MathF.Abs(transform.position.x + offset.x) < _levelFieldX ? offset.x : 0;
        transform.position += offset;
    }  
}
