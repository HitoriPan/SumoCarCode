using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(AudioSource))]
[RequireComponent(typeof(AudioSource))]
public class BotsScript : MonoBehaviour
{
    [SerializeField] private bool loadSkin = false;
    [SerializeField] private bool useWaypoints = true;
    [SerializeField] private List<GameObject> _playerSkins;
    [SerializeField] Transform[] _waypoints = new Transform[7];
    [SerializeField] private AudioSource _hitSoundSource;

    [SerializeField] private float _speed = 2;
    [SerializeField] private float _impulseForce = 4000f;

    [SerializeField] private Animator animator;
    private GameObject playerCurrentSkin;

    private int waypointIndex;
    private float soundValueSave;
    private float speedCoef = 1;
    private float impulseCoef = 1;
    private float impulseCoefEx;
    private float speedTimer;
    private float impluseTimer;
    private float impulseTimer;
    private float dist;

    public float SetSpeedCoef { set => speedCoef = value; }
    public float SetSpeedTimer { set => speedTimer = value; }
    public float SetImpulseCoef { set => impulseCoefEx = value; }
    public float SetImpulseTimer { set => impluseTimer = value; }
    public bool SpeedDecresee { get; set; }
    public bool SpeedIncresee { get; set; }
    public bool ImpulseDecresee { get; set; }
    public bool ImpulseIncresee { get; set; }

    private string[] playersNames = new string[7];
    private Vector3 moveInput = Vector3.zero;

    private float _soundValueSave;
    void Start()
    {
        if (loadSkin == false)
        {
            for (int i = 0; i < PlayerPrefs.GetInt("SetCountSkins"); i++)
            {
                GameObject data = Resources.Load<GameObject>($"Prefab/PlayerModels/{i}");
                _playerSkins.Add(data);
            }
            int randomizer;
            randomizer = Random.Range(0, PlayerPrefs.GetInt("SetCountSkins"));
            playerCurrentSkin = Instantiate(_playerSkins.ToArray()[randomizer], transform.position, transform.rotation);
            playerCurrentSkin.transform.SetParent(transform);
        }
        else 
            useWaypoints = false;
        if (useWaypoints == true)
        {
            waypointIndex = 0;
            transform.LookAt(_waypoints[waypointIndex]);
        }
        animator = GetComponentInChildren<Animator>();
    }

    void FixedUpdate()
    {
        if (useWaypoints == true)
        {
            dist = Vector3.Distance(transform.position, _waypoints[waypointIndex].position);
            if (dist < 5f)
            {
                IncreeseIndex();
                //transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(_waypoints[waypointIndex].position), _rotSpeed * 10 * Time.deltaTime);
            }
        }
        _soundValueSave = PlayerPrefs.GetFloat("VolumeValue");
        _hitSoundSource.volume = _soundValueSave / 3;
        if (useWaypoints)
        {
            if (playersNames[0] == transform.name)
            {
                moveInput = Vector3.zero;
            }
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
        }
        if (ImpulseDecresee == false && ImpulseIncresee == false)
            impulseCoef = 1;
        if (ImpulseIncresee == true)
        {
            impluseTimer -= Time.deltaTime;
            if (speedTimer > 0)
                impulseCoef = impulseCoefEx;
            else ImpulseIncresee = false;
        }
        else if (ImpulseDecresee == true)
        {
            impluseTimer -= Time.deltaTime;
            if (speedTimer > 0)
                impulseCoef = impulseCoefEx;
            else ImpulseDecresee = false;
        }
    }
    private void Move()
    {
        moveInput = transform.forward * Time.deltaTime;
        transform.Translate(Vector3.forward * _speed * speedCoef * Time.deltaTime);
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

    private void IncreeseIndex()
    {
        int randomizer;
        randomizer = Random.Range(0, _waypoints.Length);
        waypointIndex += randomizer;
        if (waypointIndex >= _waypoints.Length)
            waypointIndex = 0;
        transform.LookAt(_waypoints[waypointIndex].position);
    }
}
