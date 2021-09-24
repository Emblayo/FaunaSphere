using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;
using TMPro;



public class InventoryManager : MonoBehaviour
{
    public Transform[] slots;
    public bool[] isFull;
    public int lux;
    public Transform slotHolder;
    public TextMeshProUGUI luxDisplay;
    // Start is called before the first frame update
    private void Start()
    {
        UpdateSlots();
    }

    // Update is called once per frame
    public void UpdateSlots()
    {
        slots = new Transform[slotHolder.childCount];
        isFull = new bool[slotHolder.childCount];

        for (int i = 0; i < slotHolder.childCount; i++)
        {
            slots[i] = slotHolder.GetChild(i);
        }
        for (int i = 0; i < slots.Length; i++){
            if (slots[i].childCount < 4)
            {
                isFull[i] = false;
            }
            else if (slots[i].childCount>3)
            {
                isFull[i] = true;
            }
        }
    }
    
    public void AddItem(GameObject item) 
    {

        for (int i = 0; i < slots.Length; i++)
        {
            
                //print(item.name + "  " + slots[i].GetChild(4).name);



                if (isFull[i] == false)
                {

                    
                    Instantiate(item, slots[i]);
                slots[i].GetChild(3).name = item.name;
                TextMeshProUGUI counterObject = slots[i].GetComponentInChildren<TextMeshProUGUI>(true);
                counterObject.text = "1";
                counterObject.enabled = true;
                UpdateSlots();
                
                break;

                } else if (isFull[i] == true)
                {
                
                    if (slots[i].GetChild(3).name == item.name)
                    {
                        //pull text, convert to int and add 1, convert back to string and set
                        TextMeshProUGUI counterObject = slots[i].GetComponentInChildren<TextMeshProUGUI>(true);
                        string notInt = counterObject.text;
                        int intcount = Int32.Parse(notInt);
                        intcount = intcount + 1;
                        counterObject.text = intcount.ToString();
                    
                        break;
                    }
                }
            
           

        }
    }
    public void GrantItem(string itemType) // adds item to PlayFab account
    {
        //var request = (itemType + "ID", PlayerPrefs.GetString("PlayFabID"));
        //PlayFabServerAPI.GrantItemsToUser();
        //request.ItemGrants = new List<ItemGrant>();
        //request.ItemGrants.Add(new ItemGrant { PlayFabId = PlayerPrefs.GetString("PlayFabID"), ItemId = itemType });
        //request.ItemGrants.Add(new ItemGrant { ItemId = itemType});
        List<string> itemIds = new List<string>();
        itemIds.Add(itemType + "ID");
        /*PlayFab.ServerModels.GrantItemsToUserRequest request = new PlayFab.ServerModels.GrantItemsToUserRequest {
            CatalogVersion = "Items",
            PlayFabId = PlayerPrefs.GetString("PlayFabID"),
            ItemIds = itemIds
            };*/

       
        var request = new PlayFab.ServerModels.GrantItemsToUserRequest();
        
        request.PlayFabId = PlayerPrefs.GetString("PlayFabID");
        request.ItemIds = new List<string>() { itemType + "ID" };

        PlayFabServerAPI.GrantItemsToUser(request, LogSuccess, LogFailure);
        
        

        GetInventory();
        //Experimental:
        
        
    
    }
    private void LogSuccess(GrantItemsToUserResult grantItem)
    {
        print("item added to playfab account");
    }
    private void LogFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetInventory()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), OnGetInventory, error => Debug.LogError(error.GenerateErrorReport()));
        
    }
    public void OnGetInventory(PlayFab.ClientModels.GetUserInventoryResult result)
    {
        Debug.Log("Received the following items:");
        foreach (var eachItem in result.Inventory)
            Debug.Log("Items (" + eachItem.DisplayName + "): " + eachItem.ItemInstanceId + "Quantity of item:" + eachItem.RemainingUses);
        

        //Update currency amounts
        result.VirtualCurrency.TryGetValue("LX", out lux);
        print("Lux amount:" + lux);
        PlayerPrefs.SetString("Lux Amount", lux.ToString());
        luxDisplay.text = lux.ToString();
    }
}
