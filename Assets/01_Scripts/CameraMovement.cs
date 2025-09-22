using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    public Transform obj;
    public float speedCamera = 0.025f;
    public Vector3 desplaceCamera;
    private void LateUpdate()
    {
        Vector3 destination = obj.position + desplaceCamera;
        Vector3 positionSoft = Vector3.Lerp(transform.position, destination, speedCamera);
        transform.position = positionSoft;
    }
}
