using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    [SerializeField] private TowerOfCubes _towerOfCubes;
    [Range(0.1f, 1f)]
    [SerializeField] private float _duration;
    [Range(0.05f, 1f)]
    [SerializeField] private float _magnitude;

    private bool _isShaked;

    private void Awale()
    {
        if(_towerOfCubes == null) enabled = false;
    }

    public void EnableShake(Cube _)
    {
        if(_isShaked) return;
        Handheld.Vibrate();
        StartCoroutine(Shake(_duration, _magnitude));
    }
    IEnumerator Shake(float duration, float magnitude)
    {
        _isShaked = true;
        Quaternion originalRotation = transform.rotation;
        float elapsed = 0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);
            Vector3 shakedRotation = originalRotation.eulerAngles + new Vector3(x, y, 0);

            transform.rotation = Quaternion.Euler(shakedRotation.x, shakedRotation.y, shakedRotation.z);

            elapsed += Time.deltaTime;

            yield return null;
        }
        transform.rotation = originalRotation;
        _isShaked = false;
    }

    public void OnEnable()
    {
        _towerOfCubes.CubeRemovalEvent += EnableShake;
    }

    public void OnDisable()
    {
        _towerOfCubes.CubeRemovalEvent -= EnableShake;
    }
}
