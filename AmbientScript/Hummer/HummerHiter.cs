using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HummerHiter : MonoBehaviour
{
    [SerializeField] private float _impulseForce = 100f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
            other.attachedRigidbody.AddForce(new Vector3(other.attachedRigidbody.transform.position.x * _impulseForce, 0, other.attachedRigidbody.transform.position.z * _impulseForce));
    }
}
