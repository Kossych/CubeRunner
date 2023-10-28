using UnityEngine;

public abstract class Moveable: MonoBehaviour
{
    public void Move(float speed, float sidewaysSpeed)
    {
        Vector3 offset = Vector3.forward * (speed * Time.fixedDeltaTime);
        offset += Vector3.left * (sidewaysSpeed * speed);
        transform.position += offset;
    }
}
