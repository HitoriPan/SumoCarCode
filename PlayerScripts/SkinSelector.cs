using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class SkinSelector : MonoBehaviour
{
    [SerializeField] private bool isMainMenu = false;
    [SerializeField] private List<GameObject> _playerSkins;
    [SerializeField] private List<GameObject> _playerButtons;
    [SerializeField] private HashSet<GameObject> _avalibleSkins = new HashSet<GameObject>();
    [SerializeField] private HashSet<GameObject> _avalibleButtons = new HashSet<GameObject>();
    private GameObject playerCurrentSkin;
    private char[] arrayOfIndex;
    private string listOfUnlockSkins;
    void Awake()
    {
        _avalibleSkins.Add(Resources.Load<GameObject>($"Prefab/PlayerModels/{0}"));
        if (isMainMenu == true)
            _avalibleButtons.Add(_playerButtons[0]);
        for (int i = 0; i < PlayerPrefs.GetInt("SetCountSkins"); i++)
        {
            GameObject data = Resources.Load<GameObject>($"Prefab/PlayerModels/{i}");
            _playerSkins.Add(data);
        }
        listOfUnlockSkins = PlayerPrefs.GetString("Unlock Skins");
        arrayOfIndex = listOfUnlockSkins.ToCharArray();
        CheckBouthSkins();  
        playerCurrentSkin = Instantiate(_avalibleSkins.ToArray()[PlayerPrefs.GetInt("Player Skin")], transform.position, transform.rotation);
        playerCurrentSkin.transform.SetParent(transform);
    }
    public void FixedUpdate()
    {
        CheckBouthSkins();
        if(isMainMenu == true)
        {
            for(int i = 0; i <arrayOfIndex.Length; i++)
            {
                _playerButtons[0].SetActive(true);
                _playerButtons[arrayOfIndex[i]].SetActive(true);
            }
        }
    }
    public void ChangeSkin(int buttonIndex)
    {
        PlayerPrefs.SetInt("Player Skin", buttonIndex);
        
        CheckBouthSkins();
        ChangeSkin();
    }
    private void ChangeSkin()
    {
        Destroy(playerCurrentSkin);
        playerCurrentSkin = Instantiate(_avalibleSkins.ToArray()[PlayerPrefs.GetInt("Player Skin")], transform.position, transform.rotation);
        playerCurrentSkin.transform.SetParent(transform);
    }
    public void CheckBouthSkins()
    {
        listOfUnlockSkins = PlayerPrefs.GetString("Unlock Skins");
        arrayOfIndex = listOfUnlockSkins.ToCharArray();
        if(arrayOfIndex.Length != 0)
        {
            for (int i = 0; i < arrayOfIndex.Length; i++)
            {
                _avalibleSkins.Add(_playerSkins[Convert.ToUInt16(arrayOfIndex[i])]);
                if (isMainMenu == true)
                {
                    _avalibleButtons.Add(_playerButtons[Convert.ToUInt16(arrayOfIndex[i])]);
                }
            }
        }
        if(arrayOfIndex.Length == 0)
        {
            PlayerPrefs.SetInt("Player Skin", 0);
        }
        
    }
    [ContextMenu("Show list of Open skins")]
    public void ShowListOfOpenSkins()
    {
        listOfUnlockSkins = PlayerPrefs.GetString("Unlock Skins");
        Debug.Log(arrayOfIndex.Length);
        char[] listOfNums;
        Debug.Log("Начало");
        for (int i = 0; i < _playerSkins.Count-1; i++)
        {
            listOfNums = listOfUnlockSkins.ToCharArray();
            Debug.Log(Convert.ToInt32(listOfNums[i]));
        }
        Debug.Log("Конец");
        Debug.Log(listOfUnlockSkins);
    }
}