using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;


public class LoadInventory : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
        public void GetInventory()
        {
            PlayFabClientAPI.GetUserInventory(new GetUserInventoryRequest(), OnGetInventory, error => Debug.LogError(error.GenerateErrorReport()));
            print("get inventory");
        }


     public int lux;

        public void OnGetInventory(GetUserInventoryResult result)
        {
            Debug.Log("Received the following items:");
            foreach (var eachItem in result.Inventory)
                Debug.Log("Items (" + eachItem.DisplayName + "): " + eachItem.ItemInstanceId);
        
    }




    //}

    // Update is called once per frame
    /*void Update()
    {
        void OnGetInventory(GetUserInventoryResult result)
        {
            Debug.Log("Received the following items:");
            foreach (var eachItem in result.Inventory)
                Debug.Log("Items (" + eachItem.DisplayName + "): " + eachItem.ItemInstanceId);
        }
    }*/
    
}
