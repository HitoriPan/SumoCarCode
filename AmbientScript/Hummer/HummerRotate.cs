using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerRotate : MonoBehaviour
{
    [SerializeField] private float _rotationSpeed = 200f;
    void FixedUpdate()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
    }
}
