using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _playerModel;
    [SerializeField] private Vector3 _offset;
    [SerializeField] private float _smoothing = 1f;

    private bool playerExist = true;

    void FixedUpdate()
    {
        if (_playerModel == null)
        {
            CheckPlayerModel();
        }
        if(playerExist)
            Move();
    }

    private void Move()
    {
        Vector3 nextPostiton = Vector3.Lerp(transform.position, _playerModel.position + _offset, _smoothing * Time.fixedDeltaTime);
        transform.position = nextPostiton;
    }
    private void CheckPlayerModel()
    {
        transform.position = new Vector3(0, 40, 0);
        transform.rotation = Quaternion.AngleAxis(90, new Vector3(1,0,0));
        _playerModel = transform;
        playerExist = false;
    }
}
