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

    private List<GameObject> buttonsList;

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
        buttonsList = new List<GameObject>();
        buttonsList.Add(storeItemButton.gameObject);
        SetStoreButton(storeItemButton, 0);
        for (int i = 1; i < storeItemsDataList.Length; i++)
        {
            buttonsList.Add(Instantiate(storeItemButton.gameObject, storeItemLists[storeItemsDataList[i].listNo]) as GameObject);
            SetStoreButton(buttonsList[i].transform, i);
        }
    }

    private void SetStoreButton(Transform button, int index)
    {
        StoreItemData data = storeItemsDataList[index];
        button.transform.name = data.itemName + index;
        var btn = button.GetComponent<Button>();
        btn.onClick.RemoveAllListeners();
        btn.onClick.AddListener(() => ButtonClickEvent(index));
        button.GetChild(0).GetComponent<Text>().text = data.itemName;
        button.GetChild(1).GetComponent<Image>().sprite = data.icon;
        if (data.isLocked && PreferenceManager.GetWeaponLockedStatus(data.itemName))
        {
            button.GetChild(2).gameObject.SetActive(true);
            button.GetChild(3).gameObject.SetActive(true);
            switch (data.unlockItemBy)
            {
                case UnlockItemType.RAD:
                {
                    button.GetChild(2).GetChild(0).gameObject.SetActive(true);
                    button.GetChild(2).GetChild(1).gameObject.SetActive(false);
                    button.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    button.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "X " + PreferenceManager.GetWeaponRAdCount(data.itemName);
                    break;
                }
                case UnlockItemType.CASH:
                {
                    button.GetChild(2).GetChild(0).gameObject.SetActive(false);
                    button.GetChild(2).GetChild(1).gameObject.SetActive(false);
                    button.GetChild(2).GetChild(2).gameObject.SetActive(true);
                    button.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text = data.unlockItemAmountRequired.ToString();
                    break;
                }
                case UnlockItemType.INAPPS:
                {
                    button.GetChild(2).GetChild(0).gameObject.SetActive(false);
                    button.GetChild(2).GetChild(1).gameObject.SetActive(true);
                    button.GetChild(2).GetChild(2).gameObject.SetActive(false);
                    button.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = data.itemPrice_forInApp;
                    break;
                }
            }
        }
        else
        {
            button.GetChild(2).gameObject.SetActive(false);
            button.GetChild(3).gameObject.SetActive(false);
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

    public void ShowList(int index)
    {
        if (index < storeItemLists.Length)
        {
            for (int i = 0; i < storeItemLists.Length; i++)
            {
                if(i == index)
                {
                    storeItemLists[i].parent.gameObject.SetActive(true);
                }
                else
                {
                    storeItemLists[i].parent.gameObject.SetActive(false);
                }
            }
        }
        else
        {
            storeItemLists[0].parent.gameObject.SetActive(true);
        }
    }

    private void ButtonClickEvent(int storeItemIndex)
    {
        var storeItem = storeItemsDataList[storeItemIndex];
        if (storeItem.isLocked && PreferenceManager.GetWeaponLockedStatus(storeItem.itemName))
        {
            if (PreferenceManager.GetWeaponLockedStatus(storeItem.itemName))
            {
                // Item is locked
                switch (storeItem.unlockItemBy)
                {
                    case UnlockItemType.RAD:
                        GetItemFromRad(storeItem,storeItemIndex);
                        break;
                    case UnlockItemType.CASH:
                        GetItemFromCash(storeItem,storeItemIndex);
                        break;
                    case UnlockItemType.INAPPS:
                        GetItemFromInApps(storeItem, storeItemIndex);
                        break;
                    default:
                        break;
                }
            }
            else
            {
                // Item is unlocked
            }
        }
    }

    public void GetItemFromRad(StoreItemData storeItem,int index)
    {
#if UNITY_EDITOR
        OnRadRewardUser(storeItem, index);
#else
        // Call rad show
        // On reward user call 
        // OnRadRewardUser()
#endif
    }

    private void OnRadRewardUser(StoreItemData storeItem, int index)
    {
        PreferenceManager.SetWeaponLockedStatus(storeItem.itemName, PreferenceManager.GetWeaponRAdCount(storeItem.itemName)-1);
        buttonsList[index].transform.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = "X " + PreferenceManager.GetWeaponRAdCount(storeItem.itemName);
        if (PreferenceManager.GetWeaponRAdCount(storeItem.itemName) <= 0)
        {
            UnlockItem(storeItem,index);
        }
    }

    public void GetItemFromInApps(StoreItemData storeItem, int index)
    {
#if UNITY_EDITOR
        UnlockItem(storeItem, index);
#else
        // Call IAP manager
        // In-app bundle is stored in
        // storeItem.inAppBundle
#endif
    }

    public void GetItemFromCash(StoreItemData storeItem, int index)
    {
        if(PreferenceManager.CashBalance >= storeItem.unlockItemAmountRequired)
        {
            PreferenceManager.CashBalance -= storeItem.unlockItemAmountRequired;
            UnlockItem(storeItem, index);
        }
        else
        {
            // Show not enough coins
            Debug.LogError("Not Enough Cash");
        }
    }

    private void UnlockItem(StoreItemData storeItem, int index)
    {
        buttonsList[index].transform.GetChild(2).gameObject.SetActive(false);
        buttonsList[index].transform.GetChild(3).gameObject.SetActive(false);
        PreferenceManager.SetWeaponLockedStatus(storeItem.itemName);
    }
}

[System.Serializable]
public class StoreItemData
{
    public string itemName = "";
    public int listNo = 0;
    public Sprite icon;
    public bool isLocked;
    public UnlockItemType unlockItemBy = UnlockItemType.NONE;
    public int unlockItemAmountRequired = 2;
    public string inAppBundle = "";
    public string itemPrice_forInApp = "$";
}

public enum UnlockItemType
{
    NONE,RAD,CASH,INAPPS,
}