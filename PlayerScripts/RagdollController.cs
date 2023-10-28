using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RagdollController : MonoBehaviour
{
    [SerializeField] private List<Rigidbody> _ragdollElements = new();
    [SerializeField] private Player _player;

    private void Awake()
    {
        if(_player == null) _player = GetComponentInParent<Player>();
        if (_ragdollElements.Count == 0) _ragdollElements = GetComponentsInChildren<Rigidbody>().ToList();
        DisableRagdoll();
    }

    public void EnableRagdoll()
    {
        _ragdollElements.ForEach(element =>
        {
            element.isKinematic = false;
        });
    }

    public void DisableRagdoll()
    {
        _ragdollElements.ForEach(element =>
        {
            element.isKinematic = true;
        });
    }

    public void OnEnable()
    {
        _player.DieEvent += EnableRagdoll;
    }
}
