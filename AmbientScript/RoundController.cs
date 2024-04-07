using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoundController : MonoBehaviour
{
    [SerializeField] private int _maxPrize = 15;
    [SerializeField] private int _timeoutPrize = 10;
    [SerializeField] private int _midPrize = 5;
    [SerializeField] private int _lowPrize = 1;
    [SerializeField] private float _maxTime = 60f;
    [SerializeField] private TMP_Text _timer;
    [SerializeField] private TMP_Text _enemyCounter;
    [SerializeField] private TMP_Text _resultText;
    [SerializeField] private TMP_Text _resultMoney;
    [SerializeField] private GameObject _ResultMenu;
    [SerializeField] private GameObject _Pause;
    [SerializeField] private GameObject _empty;
    [SerializeField] List<GameObject> _players;

    public int PlayerTakeMoneyBonus { set => money = value; }
    GameObject _player;
    private bool _flagEndGame = false;
    private bool flagAllGone = false;
    private bool timerOn = true;
    private int money;
    private int randomizer;
    int triningCompleteInt;

    private void Start()
    {
        triningCompleteInt = PlayerPrefs.GetInt("TriningComplet", 0);
        randomizer = Random.Range(0, 5);
        _player = _players[0];
        money = PlayerPrefs.GetInt("PlayerMoney");
    }
    private void FixedUpdate()
    {
        //Debug.Log(money);
        if (_Pause.activeSelf == false)
        {
            Timer(timerOn);
            EnemyConunter();
            if (_flagEndGame)
            {
                PlayerPrefs.SetInt("TriningComplet", 1);
                EndGame();
            }
        }
    }
    private void EnemyConunter()
    {
        if (_player == _players[0])
        {
            _enemyCounter.text = $"Enemy Left: {_players.Count - 1}";
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].IsUnityNull())
                    _players.RemoveRange(i, 1);
            }
            if ((_players.Count - 1) <= 0)
            {
                _enemyCounter.text = $"Enemy Left: 0";
                timerOn = false;
                _flagEndGame = true;
            }
        }
        else if(_player != _players[0])
        {
            _enemyCounter.text = $"Enemy Left: {_players.Count}";
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].IsUnityNull())
                    _players.RemoveRange(i, 1);
            }
            if ((_players.Count) > 0)
            {
                _enemyCounter.text = $"Enemy Left: 1";
                timerOn = false;
                _flagEndGame = true;
            }
            if (_players[0] == _empty)
            {
                _enemyCounter.text = $"Nobody Left";
                timerOn = false;
                _flagEndGame = true;
            }
        }
    }
    private void Timer(bool isOn)
    {
        if (isOn)
        {
            if (_maxTime > 0)
                _maxTime -= Time.deltaTime;
            if (_maxTime < 60)
            {
                _timer.text = $"0:{(int)_maxTime}";
                if (_maxTime / 60 < 10)
                {
                    _timer.text = $"0:{(int)_maxTime}";
                    if (_maxTime / 10 < 1)
                    {
                        _timer.text = $"0:0{(int)_maxTime}";
                    }

                }
                if (_maxTime <= 0)
                {
                    _flagEndGame = true;
                    _timer.text = $"0:00";
                }
            }
            if (_maxTime > 60)
            {
                _timer.text = $"{(int)_maxTime / 60}:{(int)_maxTime % 60}";
                if (_maxTime % 60 < 10)
                    _timer.text = $"{(int)_maxTime / 60}:0{(int)_maxTime % 60}";
            }
        }
    }
    private void EndGame()
    {
        int prize = 0;
        if (_players.Count <= 0)
        {
            Instantiate(_empty, transform.position, transform.rotation);
            _players.Add(_empty);
            prize = 0;
            prize = _lowPrize + randomizer;
            flagAllGone = true;
            PlayerPrefs.SetInt("PlayerMoney", money + prize);
            _resultText.text = "All Lose!";
            _resultMoney.text = $"Your prize is {prize} coins!";
        }
        if (!_players[0].IsUnityNull() && (flagAllGone == false))
        {
            if (_player != _players[0])
            {
                prize = 0;
                prize = _midPrize + randomizer;
                PlayerPrefs.SetInt("PlayerMoney", money + prize);
                _resultText.text = "You Lose!";
                _resultMoney.text = $"Your prize is {prize} coins!";
            }
            else if (_player == _players[0])
            {
                prize = 0;
                prize = _maxPrize + randomizer;
                _resultText.text = "You win!";
                if (_maxTime < 0)
                {
                    prize = 0;
                    prize = _timeoutPrize + randomizer;
                    _resultText.text = "Times up!";
                }
                _resultMoney.text = $"Your prize is {prize} coins!";
                PlayerPrefs.SetInt("PlayerMoney", money + prize);
            }
        }
        _ResultMenu.SetActive(true);
    }
    public void ExitMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
