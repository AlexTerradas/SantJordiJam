using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscoBallRotation : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;
    void Update()
    {
        Vector3 rotation =  gameObject.transform.eulerAngles;
        // rotation.x = 90;
        rotation.z += rotationSpeed * Time.deltaTime;
        gameObject.transform.eulerAngles = rotation;
    }
}
