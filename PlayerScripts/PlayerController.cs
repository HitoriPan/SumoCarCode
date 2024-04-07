using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _rotSpeed = 2;
    [SerializeField] private AudioSource _hitSoundSource;

    [SerializeField] private float _speed = 12;
    [SerializeField] private float _impulseForce = 40f;

    private Animator animator;
    private Vector3 moveInput = Vector3.zero;
    private float soundValueSave;

    private float speedCoef = 1;
    private float impulseCoef = 1;
    private float impulseCoefEx;
    private float speedTimer;
    private float impulseTimer;
    public float SetSpeedCoef { set => speedCoef = value; }
    public float SetSpeedTimer { set => speedTimer = value; }
    public float SetImpulseCoef { set => impulseCoefEx = value; }
    public float SetImpulseTimer { set => impulseTimer = value; }
    public bool SpeedDecresee { get; set; }
    public bool SpeedIncresee { get; set; }
    public bool ImpulseDecresee { get; set; }
    public bool ImpulseIncresee { get; set; }

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        soundValueSave = PlayerPrefs.GetFloat("VolumeValue");
        _hitSoundSource.volume = soundValueSave / 3;
    }

    void FixedUpdate()
    {
        animator.Play("Idle");
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        moveInput = new Vector3(horizontalInput, 0, verticalInput);
        if (SpeedDecresee == false && SpeedIncresee == false)
            Move();
        if (SpeedIncresee == true)
        {
            speedTimer -= Time.deltaTime;
            if (speedTimer > 0)
                Move();
            else
            {
                SpeedIncresee = false;
                speedCoef = 1;
            }
        }
        else if (SpeedDecresee == true)
        {
            speedTimer -= Time.deltaTime;
            if (speedTimer > 0)
                Move();
            else
            {
                SpeedDecresee = false;
                speedCoef = 1;
            }
        }
        if(ImpulseDecresee == false && ImpulseIncresee == false) 
            impulseCoef = 1;
        if(ImpulseIncresee == true)
        {
            impulseTimer -= Time.deltaTime;
            if (speedTimer > 0)
                impulseCoef = impulseCoefEx;
            else ImpulseIncresee = false;
        } else if( ImpulseDecresee == true)
        {
            impulseTimer -= Time.deltaTime;
            if (speedTimer > 0)
                impulseCoef = impulseCoefEx;
            else ImpulseDecresee = false;
        }
    }
    private void Move()
    {
        float x = transform.rotation.x;
        float z = transform.rotation.z;
        if (moveInput.magnitude > 0.1f)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveInput), _rotSpeed * Time.deltaTime);
            transform.Rotate(-x, 0, -z);
        }
        GetComponent<Rigidbody>().AddForce(moveInput * _speed * speedCoef);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.attachedRigidbody.AddForce(moveInput * _impulseForce * impulseCoef);
            _hitSoundSource.PlayOneShot(_hitSoundSource.clip);
            animator.Play("Hit");
            animator.Play("Idle");
        }
        if (other.tag != "Player")
            moveInput = Vector3.zero;
    }
}
