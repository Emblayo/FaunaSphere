using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;
using PlayFab.AuthenticationModels;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;
public class PlayFabLogin : MonoBehaviour
{
    private string userEmail;
    private string userPassword;
    private string username;
    public GameObject loginPanel;
    public Transform[] slots;
    public bool[] isFull;
    public Transform slotHolder;
    public int lux;
    public TextMeshProUGUI luxDisplay;

    public void Start()
    {
        


        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "144"; // Please change this value to your own titleId from PlayFab Game Manager
        }

        PlayFabClientAPI.ForgetAllCredentials();
        //To delete accounts:
        //PlayerPrefs.DeleteAll(); 
        ;        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
         //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        /*if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            username = PlayerPrefs.GetString("USERNAME");
            //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            var request = new LoginWithPlayFab{ Email = userEmail, Password = userPassword };
            //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        }*/
        if (PlayerPrefs.HasKey("USERNAME"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");
            username = PlayerPrefs.GetString("USERNAME");
            //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            var request = new LoginWithPlayFabRequest {Password = userPassword, TitleId = PlayFabSettings.TitleId, Username = username};
            //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
            PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);
        }
    }
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        PlayerPrefs.SetString("USERNAME", username);
        PlayerPrefs.SetString("PlayFabID", result.PlayFabId);
        loginPanel.SetActive(false);

        //Pull inventory data on login (works)
        //InventoryManager loadInventory = FindObjectOfType<InventoryManager>();
        //loadInventory.GetInventory(); 
        InitialInventoryPull();

    }
    public void InitialInventoryPull()
    {
        PlayFabClientAPI.GetUserInventory(new PlayFab.ClientModels.GetUserInventoryRequest(), OnGetInitialInventory, error => Debug.LogError(error.GenerateErrorReport()));
        //Updates lux:
        
    }

    public void OnGetInitialInventory(PlayFab.ClientModels.GetUserInventoryResult result)
    {
        //Updates Lux:
        result.VirtualCurrency.TryGetValue("LX", out lux);
        print("Lux amount:" + lux);
        PlayerPrefs.SetString("Lux Amount", lux.ToString());
        luxDisplay.text = lux.ToString();


        List<string> itemNames = new List<string>();
        foreach (var eachItem in result.Inventory)
        {
            string itemName = eachItem.DisplayName;
            string itemCount = eachItem.RemainingUses.ToString();
            
            itemNames.Add(itemName);

            //For debugging purposes:
            foreach (var x in itemNames)
            {
                Debug.Log(x.ToString());
            }
            string filepath = "Items/" + itemName;
            GameObject itemInput = Resources.Load<GameObject>(filepath);
            //find gameobject prefab of item using the name:


            print(itemInput.name); // test to see if its finding the gameobject
            InventoryManager addItem = FindObjectOfType<InventoryManager>();
            addItem.AddItem(itemInput);

            slots = new Transform[slotHolder.childCount];
            isFull = new bool[slotHolder.childCount];

            for (int i = 0; i < slotHolder.childCount; i++)
            {
                slots[i] = slotHolder.GetChild(i);
            }
            for (int i = 0; i < slots.Length; i++)
            {
                if (slots[i].childCount == 4)
                {
                    
                    TextMeshProUGUI counterObject = slots[i].GetComponentInChildren<TextMeshProUGUI>(true);
                    counterObject.text = itemCount;
                    counterObject.enabled = true;
                    

                }
                
            }


        }

            //call update slots here: (is this necessary? prob not)
            //InventoryManager updateSlots = FindObjectOfType<InventoryManager>();
        //updateSlots.UpdateSlots();

        /*void AddItem(GameObject item)
        {
            //GrantItem(item.name);
            InventoryManager updateSlots = FindObjectOfType<InventoryManager>();
            updateSlots.UpdateSlots();
            print("AddItem method is starting");
            for (int i = 0; i < slots.Length; i++)
            {
                print("is working");
                if (isFull[i] == false)
                {
                    
                    print("item added");
                    Instantiate(item, slots[i]);
                    
                    updateSlots.UpdateSlots();
                    break;
                }
            }
        }
        */



    }

    public TextMeshProUGUI userNameDisplay;
    private void Update()
    {
        userNameDisplay.text = PlayerPrefs.GetString("USERNAME");
    }

    
    private void OnRegisterSuccess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Congratulations, you made your first successful API call!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        PlayerPrefs.SetString("USERNAME", username);
        //loginPanel.SetActive(false); //Hides login/registration screen
        PlayerPrefs.SetString("PlayFabID", result.PlayFabId);
        //Load Fauna creation screen
        SceneManager.LoadScene("Start");
        


}

    private void OnLoginFailure(PlayFabError error)
    {
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = username };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSuccess, OnRegisterFailure);
           
    }
    private void OnRegisterFailure (PlayFabError error)
    {
        

        Debug.LogError(error.GenerateErrorReport());
    }
    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }
    public void GetUsername(string usernameIn)
    {
        username = usernameIn;
    }

    public void OnClickLogin()
    {
        //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        var request = new LoginWithPlayFabRequest { Password = userPassword, TitleId = PlayFabSettings.TitleId, Username = username };
        
        PlayFabClientAPI.LoginWithPlayFab(request, OnLoginSuccess, OnLoginFailure);

        
        
    }

}