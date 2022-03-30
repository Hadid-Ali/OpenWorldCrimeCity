using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    public GameObject shopPanel,  shopButton;
    public ShopItemData[] itemsList;

    public static ShopManager Instance;

    private List<GameObject> btnsList;

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        btnsList = new List<GameObject>();
        btnsList.Add(shopButton);
        InitItemButton(itemsList[0], shopButton.transform,0);
        for (int i = 1; i < itemsList.Length; i++)
        {
            var btn = Instantiate(shopButton, shopButton.transform.parent) as GameObject;
            btnsList.Add(btn);
            InitItemButton(itemsList[i], btn.transform,i);
        }
    }

    public void OpenShop()
    {
        shopPanel.SetActive(true);
    }

    private void InitItemButton(ShopItemData data, Transform btn, int  index)
    {
        btn.GetChild(0).GetComponent<Image>().sprite = data.icon;
        btn.GetChild(1).GetComponentInChildren<Text>().text = "$"+data.price;
        btn.GetChild(2).GetComponent<Text>().text = data.itemName;

        btn.GetComponent<Button>().onClick.RemoveAllListeners();
        btn.GetComponent<Button>().onClick.AddListener(()=> ShopBtnClickEvent(index));
    }

    private void ShopBtnClickEvent(int index)
    {

    }

    public void BackButtonClickEvent()
    {
        shopPanel.SetActive(false);
    }
}

[System.Serializable]
public class ShopItemData
{
    public string itemName = "";
    public string bundle;
    public float price;
    public Sprite icon;
}