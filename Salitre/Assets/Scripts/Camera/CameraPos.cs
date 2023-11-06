using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPos : MonoBehaviour
{
    [SerializeField] Transform _cameraTrack;
    [SerializeField] Transform playerOrientation;

    [SerializeField] float lerpSpeed;
    [SerializeField] float rotationSpeed;
    private void Update()
    {
        _cameraTrack.position = Vector3.Lerp(_cameraTrack.position, transform.position, 1 - Mathf.Exp(-lerpSpeed * Time.unscaledDeltaTime)); ;

        Vector3 cameraPosition = Camera.main.transform.position;
        Vector3 lookDirection = cameraPosition - playerOrientation.position;
        lookDirection.y = 0;
        Quaternion targetRotation = Quaternion.LookRotation(lookDirection * -1);

        playerOrientation.rotation = Quaternion.Slerp(playerOrientation.rotation, targetRotation, Time.deltaTime * rotationSpeed);
    }
}
