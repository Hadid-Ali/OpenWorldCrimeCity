using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StoreMenu : MonoBehaviour
{
    public GameObject storeMenuObject;

    public StoreItemData[] storeItemsDataList;
    public Transform[] storeItemLists;
    public Transform storeItemButton;

    #region Instance
    public static StoreMenu Instance;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    #endregion

    void Start()
    {
        InitStore();
    }

    private void InitStore()
    {
        SetStoreButton(storeItemButton, 0);
        for (int i = 1; i < storeItemsDataList.Length; i++)
        {
            var btn = Instantiate(storeItemButton.gameObject, storeItemLists[storeItemsDataList[i].listNo]) as GameObject;
            SetStoreButton(btn.transform, i);
        }
    }

    public void OpenStoreMenu()
    {
        if (!storeMenuObject.activeInHierarchy)
        {
            storeMenuObject.SetActive(true);
        }
    }

    public void CloseStoreMenu()
    {
        if (storeMenuObject.activeInHierarchy)
        {
            storeMenuObject.SetActive(false);
        }
    }

    private void SetStoreButton(Transform button, int index)
    {
        StoreItemData data = storeItemsDataList[index];
        button.transform.name = data.itemName + index;
        var btn = button.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(()=>ButtonClickEvent(index));
        button.GetChild(0).GetComponent<Text>().text = data.itemName;
        button.GetChild(1).GetComponent<Image>().sprite = data.icon;
        if(data.isLocked && PreferenceManager.GetWeaponLockedStatus(data.itemName))
        {
            button.GetChild(2).gameObject.SetActive(true);
            button.GetChild(3).gameObject.SetActive(true);
            if (data.getFromRad)
            {
                button.GetChild(2).GetChild(0).gameObject.SetActive(true);
                button.GetChild(2).GetChild(1).gameObject.SetActive(false);
                button.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "X " + PreferenceManager.GetWeaponRAdCount(data.itemName);
            }
            else
            {
                button.GetChild(2).GetChild(0).gameObject.SetActive(false);
                button.GetChild(2).GetChild(1).gameObject.SetActive(true);
                button.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = data.itemPrice_forInApp;
            }
        }
        else
        {
            button.GetChild(2).gameObject.SetActive(false);
            button.GetChild(3).gameObject.SetActive(false);
        }
    }

    private void ButtonClickEvent(int storeItemIndex)
    {
        var storeItem = storeItemsDataList[storeItemIndex];
        if (storeItem.isLocked)
        {
            if (PreferenceManager.GetWeaponLockedStatus(storeItem.itemName))
            {
                // Item is locked
                if (storeItem.getFromRad)
                {

                }
                else
                {

                }
            }
            else
            {
                // Item is unlocked
            }
        }
    }
}

[System.Serializable]
public class StoreItemData
{
    public string itemName = "";
    public int listNo = 0;
    public Sprite icon;
    public bool isLocked, getFromRad = false;
    public int rAdCount = 2;
    public string inAppBundle = "";
    public string itemPrice_forInApp = "$";
}