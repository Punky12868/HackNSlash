using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField] Transform _camera;
    [SerializeField] float lerpSpeed;
    private void Update()
    {
        if (_camera.position != transform.position)
        {
            _camera.position = Vector3.Lerp(_camera.position, transform.position, lerpSpeed);
        }
    }
}
