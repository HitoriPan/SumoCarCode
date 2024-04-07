using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Shop : MonoBehaviour
{

    [SerializeField] private List<GameObject> _skinsToSell = new List<GameObject>();
    [SerializeField] public int[] _coust = new int[0];
    private List<char> arrayOfIndex = new List<char>();
    private string listOfUnlockSkins;
    private int money;

    void Start()
    {
        PlayerPrefs.SetInt("SetCountSkins", _coust.Length);
        for (int i = 0; i < _coust.Length; i++)
        {
            GameObject data = Resources.Load<GameObject>($"Prefab/PlayerModels/{i}");
            _skinsToSell.Add(data);
        }
        listOfUnlockSkins = PlayerPrefs.GetString("Unlock Skins");
        arrayOfIndex.AddRange(listOfUnlockSkins.ToCharArray());
        money = PlayerPrefs.GetInt("PlayerMoney");
    }
    public void Calculate(int _coustIndex)
    {
        bool isBouth = false;
        if (money - _coust[_coustIndex] >= 0)
        {
            for (int i = 0; i < listOfUnlockSkins.Length; i++)
            {
                if (listOfUnlockSkins.Length != 0)
                {
                    if (listOfUnlockSkins.ToCharArray()[i] == _coustIndex)
                        isBouth = true;
                }
            }
            if (!isBouth)
            {
                money -= _coust[_coustIndex];
                PlayerPrefs.SetInt("PlayerMoney", money);
                arrayOfIndex.Add(Convert.ToChar(_coustIndex));
                listOfUnlockSkins = string.Join("", arrayOfIndex.ToArray());
                PlayerPrefs.SetString("Unlock Skins", listOfUnlockSkins);
                PlayerPrefs.Save();
            }
        }
    }
    [ContextMenu("Show list of Open skins")]
    public void ShowListOfOpenSkins()
    {
        listOfUnlockSkins = PlayerPrefs.GetString("Unlock Skins");
        char[] listOfNums;
        Debug.Log("Начало");
        for (int i = 0; i < arrayOfIndex.Count; i++)
        {
            listOfNums = listOfUnlockSkins.ToCharArray();
            Debug.Log(Convert.ToInt32(listOfNums[i]));
        }
        Debug.Log("Конец");
        Debug.Log(listOfUnlockSkins);
    }
}