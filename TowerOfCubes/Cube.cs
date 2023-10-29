using UnityEngine;
using System;
using System.Threading.Tasks;
using Unity.VisualScripting;

public class Cube : MonoBehaviour
{
    [HideInInspector] public bool IsAttached = false;
    public event Action<Cube> CubeCollisionEvent;
    public event Action<Cube> WallCollisionEvent;

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Cube cube))
        {
            if (cube.IsAttached) return;
            CubeCollisionEvent?.Invoke(cube);
        }
    }

    public void OnTriggerEnter(Collider collider)
    {

        if (collider.gameObject.TryGetComponent(out Wall wall))
        {
            WallCollisionEvent?.Invoke(this);

        }
    }

    public async void Delete(float timeToDelete = 3f)
    {
        await Task.Delay(TimeSpan.FromSeconds(timeToDelete));
        if(GameStateController.Instance.CurrentState != GameStates.Play) return;
        Destroy(gameObject);
    }
}
