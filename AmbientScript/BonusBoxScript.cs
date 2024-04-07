using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBoxScript : MonoBehaviour
{
    [SerializeField] private List<GameObject> _bonusMeshs;
    [SerializeField] private Collider _selfCollision;
     private bool _tempBuffImpulse = false;
     private bool _tempDebuffImpulse = false;
    [SerializeField, Space] private bool _tempBuffSpeed = false;
    [SerializeField] private bool _tempDebuffSpeed = false;
    [SerializeField] private bool _moneyBonus = false;
    [SerializeField, Space] private int _moneyMin = 1;
    [SerializeField] private int _moneyMax = 5;
     private float _timerImpulseBuff = 5;
    [SerializeField, Space] private float _timerSpeedBuff = 5;
    [SerializeField] private float _timerToRespawn = 5;
    [SerializeField, Space] private float _speedBuff = 1.5f;
    [SerializeField] private float _speedDebuff = 0.75f;
     private float _impulseBuff = 1.25f;
     private float _impulseDebuff = 0f;
    [SerializeField, Space] private float _rotationSpeed = 200f;
    private float tempTimer;
    private GameObject _bonusMesh;
    private int money;

    private void Start()
    {
        tempTimer = _timerToRespawn;
        money = PlayerPrefs.GetInt("PlayerMoney");
        for (int i = 0; i < 5; i++)
        {
            GameObject data = Resources.Load<GameObject>($"Prefab/Ambient/Interactive/{i}");
            _bonusMeshs.Add(data);
        }
        if (_moneyBonus)
        {
            _bonusMesh = Instantiate(_bonusMeshs.ToArray()[0], transform.position, transform.rotation);
            _bonusMesh.transform.SetParent(transform);
        }
        if (_tempBuffSpeed)
        {
            _bonusMesh = Instantiate(_bonusMeshs.ToArray()[1], transform.position, transform.rotation);
            _bonusMesh.transform.SetParent(transform);
        }
        if (_tempDebuffSpeed)
        {
            _bonusMesh = Instantiate(_bonusMeshs.ToArray()[2], transform.position, transform.rotation);
            _bonusMesh.transform.SetParent(transform);
        }
        if (_tempBuffImpulse)
        {
            _bonusMesh = Instantiate(_bonusMeshs.ToArray()[3], transform.position, transform.rotation);
            _bonusMesh.transform.SetParent(transform);
        }
        if (_tempDebuffImpulse)
        {
            _bonusMesh = Instantiate(_bonusMeshs.ToArray()[4], transform.position, transform.rotation);
            _bonusMesh.transform.SetParent(transform);
        }
    }
    private void FixedUpdate()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0);
        if (_bonusMesh == null)
        {
            _timerToRespawn -= Time.deltaTime;
            if (_moneyBonus && _timerToRespawn < 0)
            {
                _bonusMesh = Instantiate(_bonusMeshs.ToArray()[0], transform.position, transform.rotation);
                _bonusMesh.transform.SetParent(transform);
                _selfCollision.enabled = true;
                _timerToRespawn = tempTimer;
            }
            if (_tempBuffSpeed && _timerToRespawn < 0)
            {
                _bonusMesh = Instantiate(_bonusMeshs.ToArray()[1], transform.position, transform.rotation);
                _bonusMesh.transform.SetParent(transform);
                _selfCollision.enabled = true;
                _timerToRespawn = tempTimer;
            }
            if (_tempDebuffSpeed && _timerToRespawn < 0)
            {
                _bonusMesh = Instantiate(_bonusMeshs.ToArray()[2], transform.position, transform.rotation);
                _bonusMesh.transform.SetParent(transform);
                _selfCollision.enabled = true;
                _timerToRespawn = tempTimer;
            }
            if (_tempBuffImpulse && _timerToRespawn < 0)
            {
                _bonusMesh = Instantiate(_bonusMeshs.ToArray()[3], transform.position, transform.rotation);
                _bonusMesh.transform.SetParent(transform);
                _selfCollision.enabled = true;
                _timerToRespawn = tempTimer;
            }
            if (_tempDebuffImpulse && _timerToRespawn < 0)
            {
                _bonusMesh = Instantiate(_bonusMeshs.ToArray()[4], transform.position, transform.rotation);
                _bonusMesh.transform.SetParent(transform);
                _selfCollision.enabled = true;
                _timerToRespawn = tempTimer;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (_moneyBonus)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                if (other.tag == "Player")
                {
                    int randomizer;
                    randomizer = Random.Range(_moneyMin, _moneyMax);
                    money += randomizer;
                    RoundController roundController = GameObject.Find("Game Controller").GetComponent<RoundController>();
                    roundController.PlayerTakeMoneyBonus = money;
                    PlayerPrefs.SetInt("PlayerMoney", money);
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
        }
        else if (_tempBuffSpeed)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            BotsScript bot = other.GetComponent<BotsScript>();
            if (player != null)
            {
                if (other.tag == "Player")
                {
                    player.SpeedIncresee = true;
                    player.SetSpeedCoef = _speedBuff;
                    player.SetSpeedTimer = _timerSpeedBuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
            else if (bot != null)
            {
                if (other.tag == "Player")
                {
                    bot.SpeedIncresee = true;
                    bot.SetSpeedCoef = _speedBuff;
                    bot.SetSpeedTimer = _timerSpeedBuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
        }
        else if (_tempDebuffSpeed)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            BotsScript bot = other.GetComponent<BotsScript>();
            if (player != null)
            {
                if (other.tag == "Player")
                {
                    player.SpeedDecresee = true;
                    player.SetSpeedCoef = _speedDebuff;
                    player.SetSpeedTimer = _timerSpeedBuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
            else if (bot != null)
            {
                if (other.tag == "Player")
                {
                    bot.SpeedDecresee = true;
                    bot.SetSpeedCoef = _speedDebuff;
                    bot.SetSpeedTimer = _timerSpeedBuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
        }
        else if (_tempBuffImpulse)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            BotsScript bot = other.GetComponent<BotsScript>();
            if (player != null)
            {
                if (other.tag == "Player")
                {
                    player.ImpulseIncresee = true;
                    player.SetImpulseCoef = _impulseBuff;
                    player.SetImpulseTimer = _timerImpulseBuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
            else if (bot != null)
            {
                if (other.tag == "Player")
                {
                    bot.ImpulseIncresee = true;
                    bot.SetImpulseCoef = _impulseBuff;
                    bot.SetImpulseTimer = _timerImpulseBuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
        }
        else if (_tempDebuffImpulse)
        {
            PlayerController player = other.GetComponent<PlayerController>();
            BotsScript bot = other.GetComponent<BotsScript>();
            if (player != null)
            {
                if (other.tag == "Player")
                {
                    player.ImpulseDecresee = true;
                    player.SetImpulseCoef = _impulseDebuff;
                    player.SetImpulseTimer = _timerImpulseBuff;
                    //player._impulseForce = player._speed / _impulseDebuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
            else if (bot != null)
            {
                if (other.tag == "Player")
                {
                    bot.ImpulseDecresee = true;
                    bot.SetImpulseCoef = _impulseDebuff;
                    bot.SetImpulseTimer = _timerImpulseBuff;
                    Destroy(_bonusMesh);
                    _selfCollision.enabled = false;
                }
            }
        }
    }
}
