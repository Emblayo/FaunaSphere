using PlayFab;
using PlayFab.ClientModels;
using PlayFab.ServerModels;
using UnityEngine;
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

    public void Start()
    {
        //Note: Setting title Id here can be skipped if you have set the value in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "144"; // Please change this value to your own titleId from PlayFab Game Manager
        }
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
        print(username);
        loginPanel.SetActive(false);
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
        loginPanel.SetActive(false);
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