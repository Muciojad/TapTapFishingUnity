using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple controller for pointer object.
/// Follows mouse position.
/// </summary>
public class PointerController : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    void Update()
    {
        transform.position = _camera.ScreenToWorldPoint(Input.mousePosition);
    }
}
