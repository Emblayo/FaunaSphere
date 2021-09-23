using System.Collections;
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
    private void UpdateSlots()
    {
        slots = new Transform[slotHolder.childCount];
        isFull = new bool[slotHolder.childCount];

        for (int i = 0; i < slotHolder.childCount; i++)
        {
            slots[i] = slotHolder.GetChild(i);
        }
        for (int i = 0; i < slots.Length; i++){
            if (slots[i].childCount < 3)
            {
                isFull[i] = false;
            }
            else if (slots[i].childCount>0)
            {
                isFull[i] = true;
            }
        }
    }
    public void AddItem(GameObject item)
    {
        GrantItem(item.name);

        for (int i=0;i<slots.Length; i++)
        {
            if (isFull[i] == false)
            {
                print("item added");
                Instantiate(item, slots[i]);
                UpdateSlots();
                break;
            }
        }
    }
    public void GrantItem(string itemType)
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
        
        print(itemIds[0]);
        print(PlayerPrefs.GetString("PlayFabID"));

        GetInventory();
        //Experimental:
        void GetInventory()
        {
            PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), OnGetInventory, error => Debug.LogError(error.GenerateErrorReport()));
            print("get inventory");
        }
        
    void OnGetInventory(PlayFab.ClientModels.GetUserInventoryResult result)
        {
            Debug.Log("Received the following items:");
            foreach (var eachItem in result.Inventory)
                Debug.Log("Items (" + eachItem.DisplayName + "): " + eachItem.ItemInstanceId + "Quantity of item:"+ eachItem.RemainingUses);
            print(result.Inventory);

            //Update currency amounts
            result.VirtualCurrency.TryGetValue("LX", out lux);
            print("Lux amount:" + lux);
            PlayerPrefs.SetString("Lux Amount", lux.ToString());
            luxDisplay.text = lux.ToString();
        }
    }
    private void LogSuccess(GrantItemsToUserResult grantItem)
    {
        print("item added to playfab account");
    }
    private void LogFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }
}
