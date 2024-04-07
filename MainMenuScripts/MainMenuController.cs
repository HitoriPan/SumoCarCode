using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private bool _gmArena = true;
    [SerializeField] private bool _gmTube = false;
    [SerializeField] private bool _gmMP = false;
    [SerializeField] private int _money;
    [SerializeField] private GameObject _ShopMenu;
    [SerializeField] private GameObject _triningImage;
    [SerializeField] private Shop _Shop;
    [SerializeField] private TMP_Text playerMoney;

    [SerializeField] int _arenaScenes;
    [SerializeField] int _tubeScenes;
    [SerializeField] int _multyPlayerScenes;

    private bool trainingComplete=false;
    private void Start()
    {
        int triningCompleteInt = PlayerPrefs.GetInt("TriningComplet", 0);
        if (triningCompleteInt > 0)
            trainingComplete = true;
        if (trainingComplete == false)
            _triningImage.SetActive(true);
        _money = PlayerPrefs.GetInt("PlayerMoney");
    }
    private void Update()
    {
        MoneyCounter();        
    }
    public void SelectArenaMode()
    {
        _gmArena = true;
        _gmTube = false;
        _gmMP = false;
    }
    public void SelectTubeMode() 
    {
        _gmArena = false;
        _gmTube = true;
        _gmMP = false;
    }
    public void SelectMultyplayerMode()
    {
        _gmArena = false;
        _gmTube = false;
        _gmMP = true;
    }

    private void MoneyCounter()
    {
        _money = PlayerPrefs.GetInt("PlayerMoney");
        playerMoney.text = $"Money: {_money}";
    }

    public void Play()
    {
        if (trainingComplete == false) 
            SceneManager.LoadScene("TrainingRoom");
        else
        {
            if (_gmArena)
            {
                int randomizer = Random.Range(1, _arenaScenes);
                SceneManager.LoadScene($"ArenaScene{randomizer}");
            }

            if (_gmTube)
            {
                int randomizer = Random.Range(1, _tubeScenes);
                SceneManager.LoadScene($"ArenaScene{randomizer}");
            }

            if (_gmMP)
            {
                int randomizer = Random.Range(1, _multyPlayerScenes);
                SceneManager.LoadScene($"ArenaScene{randomizer}");
            }
        }            
    }
    private void Awake()
    {
        _ShopMenu.SetActive(true);
        PlayerPrefs.SetInt("SetCountSkins", _Shop._coust.Length);
        _ShopMenu.SetActive(false);
    }
    [ContextMenu ("GiveMoney")] 
    private void GimeMoney()
    {
        PlayerPrefs.SetInt("PlayerMoney",_money +1000);
    }

    [ContextMenu("Delete All")]
    private void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
