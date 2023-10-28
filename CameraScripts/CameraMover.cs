using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _playerPosition;
    [SerializeField] private Vector3  shift;

    public void FixedUpdate()
    {
        transform.position += (_playerPosition.position - transform.position).z * Vector3.forward * .5f + shift;
    } 
}
