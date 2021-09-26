using System.Collections;
using System.Collections.Generic;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;

public class LoadFauna : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //If the player has logged in (one way to check is if PlayerPrefs Username exists?) and the user has at least 1 character:
        //pull the character info for the 1st character created and use that to generate the fauna displayed
        if (PlayerPrefs.HasKey("USERNAME"))
        {
            ListUsersCharactersRequest listUsersCharactersRequest = new ListUsersCharactersRequest
            {
                //PlayFabId = PlayerPrefs.GetString("PlayFabID")
            };

            //All characters contained in single result
            PlayFabClientAPI.GetAllUsersCharacters(listUsersCharactersRequest,
            result =>
            {
                int characterCount = result.Characters.Count; // how many came back?

                if (characterCount > 0)
                {



                }

        foreach (CharacterResult characterResult in result.Characters)
                {
                    //GetDataForCharacter(characterResult);
                    //can use this later to populate the Fauna screen
                }
            },
            error =>
            {
                Debug.Log("Error: " + error.ErrorMessage);
            });
        


        Debug.Log("The key " + "USERNAME" + " exists");
        }
        else
            Debug.Log("The key " + "USERNAME" + " does not exist");
    }




    // Update is called once per frame
    void Update()
    {
        
    }
}

