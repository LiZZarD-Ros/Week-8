using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt;
    public Vector3 offSet = new Vector3(0,5f,-10f);

    private void Start()
    {
        transform.position = lookAt.position + offSet;
    }

    private void LateUpdate()
    {
        Vector3 desiredPostion = lookAt.position + offSet;
        desiredPostion.x = 0;
        transform.position = Vector3.Lerp(transform.position, desiredPostion, Time.deltaTime);
    }
}
