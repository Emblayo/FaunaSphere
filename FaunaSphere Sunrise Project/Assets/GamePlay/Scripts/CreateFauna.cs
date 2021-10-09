using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab.ServerModels;
using PlayFab;
using PlayFab.Internal;
using PlayFab.ClientModels;


public class CreateFauna : MonoBehaviour
{
    public void GrantFauna()
    {
        var request = new PlayFab.ServerModels.GrantCharacterToUserRequest();
        request.CharacterName = PlayerPrefs.GetString("Species");
        request.CharacterType = "Fauna";
        request.PlayFabId = PlayerPrefs.GetString("PlayFabID");


        PlayFabServerAPI.GrantCharacterToUser(request, LogSuccess, LogFailure);


        //FirstFaunaCreate();


    }
    private void LogSuccess(PlayFab.ServerModels.GrantCharacterToUserResult grantFauna)
    {
        PlayerPrefs.SetString("CharacterID", grantFauna.CharacterId);

        FirstFaunaCreate();

        //grantFauna.CustomData = faunaData;
        //PullCharData();

        //

        print("Fauna added to playfab account. CharacterID= " + PlayerPrefs.GetString("CharacterID"));

        
    }
    private void LogFailure(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }



    public void PullCharData() {
        //Get character data using the info stored in PlayerPrefs:
        var dataRequest = new PlayFab.ClientModels.GetCharacterDataRequest();
        dataRequest.CharacterId = PlayerPrefs.GetString("CharacterID");
        PlayFabClientAPI.GetCharacterData(dataRequest, GetCharacterDataSuccess, errorCallbackGetData);
    }

   

    private void GetCharacterDataSuccess(PlayFab.ClientModels.GetCharacterDataResult dataResult)
    {
        print(dataResult.Data);
    }
    private void errorCallbackGetData(PlayFabError error)
    {
        Debug.LogError(error.GenerateErrorReport());
    }


    public void FirstFaunaCreate()
    {



        string species = PlayerPrefs.GetString("Species");
        string color = PlayerPrefs.GetString("Color");
        Dictionary<string, string> faunaData = new Dictionary<string, string>(){
        {"Species", species},
        {"Color",color},
        };

        PlayFab.ServerModels.UpdateCharacterDataRequest updateRequest = new PlayFab.ServerModels.UpdateCharacterDataRequest();

        updateRequest.CharacterId = PlayerPrefs.GetString("CharacterID");
        updateRequest.PlayFabId = PlayerPrefs.GetString("PlayFabID");
        print("CharacterId: " + updateRequest.CharacterId + "  " + PlayerPrefs.GetString("CharacterID"));
        print("PlayFabId:" + updateRequest.PlayFabId);
        updateRequest.Data = faunaData;

        PlayFabServerAPI.UpdateCharacterData(updateRequest, (result) =>
        {
            //result.CustomData = faunaData;




            Debug.Log("Successfully updated user data");
        }, (error) =>
        {
            Debug.Log("Got error setting user data Ancestor to Arthur");
            Debug.Log(error.ErrorMessage);
        });

    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
