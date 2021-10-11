using System;
using System.Collections;
using System.Collections.Generic;
using PlayFab;

using PlayFab.ClientModels;
using UnityEngine;

public class LoadFauna : MonoBehaviour
{
    public GameObject Sniffer;
    public GameObject Hoofer;
    public GameObject Fauna;
    public Color color;
    public string firstFaunaColor;
    public GameObject Avatar;

    // Start is called before the first frame update
    void Start()
    {
        /*PlayFabLogin loginCheck = FindObjectOfType<PlayFabLogin>();
        string usernameTest = loginCheck.OnClickLogin();


        if (usernameTest!=null)
        {
            CheckFauna();
        }
        */
      

    }
    public string[] faunaData = new string[5];
    //If the player has logged in (one way to check is if PlayerPrefs Username exists?) and the user has at least 1 character:
    //pull the character info for the 1st character created and use that to generate the fauna displayed
    public void CheckFauna(){ 
        if (PlayerPrefs.HasKey("USERNAME"))
        {
            ListUsersCharactersRequest listUsersCharactersRequest = new ListUsersCharactersRequest
            {
                PlayFabId = PlayerPrefs.GetString("PlayFabID")
            };

            //All characters contained in single result
            PlayFabClientAPI.GetAllUsersCharacters(listUsersCharactersRequest,
            result =>
            {
                int characterCount = result.Characters.Count;
               

                // how many came back?
                print(characterCount);
                




                if (characterCount > 0)
                {
                    var charId = result.Characters[0].CharacterId;
                    PlayerPrefs.SetString("CharacterID", charId); //saves to playerpref. might rework this later. It could get complicated when there are several fauna all with diff characterIds
                                                                  //needs to return string array with fauna data


                    string species = PlayerPrefs.GetString("ActiveFaunaSpecies");
                    string color = PlayerPrefs.GetString("ActiveFaunaColor");

                    GetFaunaInfo();
                    //string species = faunaData[0];
                    //string color = faunaData[1];

                    Debug.Log("Character info: " + species+", "+color);

                    //Displays the first fauna in user's character list:

                    ChangeSpecies(species);

                    
                    ChangeColor2(color);













                    //----------------------------------------------------
                }

        foreach (CharacterResult characterResult in result.Characters)
                {

                    //GetFaunaInfo();
                    
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
    // }

    public void GetFaunaInfo() //
    {
        
        if (PlayerPrefs.HasKey("USERNAME"))
        {
            GetCharacterDataRequest listCharacterDataRequest = new GetCharacterDataRequest
            {
                //PlayFabId = PlayerPrefs.GetString("PlayFabID")
            CharacterId= PlayerPrefs.GetString("CharacterID")
        };

            //All characters contained in single result
            PlayFabClientAPI.GetCharacterData(listCharacterDataRequest,
            result =>
            {
                Dictionary<string, UserDataRecord> characterData = result.Data;
                //Dictionary <string, string> characterData = result.Data.TryGetValue("Species", out UserDataRecord dataRecord);
                result.Data.TryGetValue("Species", out UserDataRecord dataRecord);
                result.Data.TryGetValue("Color", out UserDataRecord colorRecord);
                if (characterData.ContainsKey("Species"))
                {
                    print(characterData["Species"].Value + "  " + dataRecord.Value);
                    string faunaSpecies = characterData["Species"].Value;
                    string faunaColor = characterData["Color"].Value;

                    faunaData[0] = characterData["Species"].Value;
                    
                    
                    faunaData[1] = characterData["Color"].Value;
                    print(faunaData[0] + " " + faunaData[1]);

                    //string faunaColor = colorRecord.Value;
                    PlayerPrefs.SetString("ActiveFaunaSpecies", faunaSpecies);
                    PlayerPrefs.SetString("ActiveFaunaColor", faunaColor);
                    print(faunaColor);

                    
                }

                
            }
             
            ,

            error =>
            {
                Debug.Log("Error: " + error.ErrorMessage);
            });


            
            Debug.Log("The key " + "USERNAME" + " exists");
            print(faunaData[0]);
            print(faunaData[1]);
            
            //return faunaData;
        }
        else
            Debug.Log("The key " + "USERNAME" + " does not exist");
            //return null;
    }

    // Update is called once per frame
    void Update()
    {
        
}

    public void ChangeSpecies(string species)
    {

        if(species == "Hoofer")
        {
            Sniffer.SetActive(false);
            Hoofer.SetActive(true);
            Avatar.tag = "Hoofer";
        }
        else if(species == "Sniffer"){
            Sniffer.SetActive(true);
            Hoofer.SetActive(false);
            Avatar.tag = "Sniffer";
        }
        else if(species == "Scooter")
        {

        }

    }

    
    public string ChangeColor2(string colorTemp)
    {

       
        //string colorTemp=PlayerPrefs.GetString("ActiveFaunaColor");
        

        if (colorTemp=="Red")
        {
            color = Color.red;
        }else if(colorTemp == "Brown"){
            color = new Color(0.4313726F, 0.2156863F, 0.03137255F, 1F);
        }else if (colorTemp == "White")
        {
            color = Color.white;
        }

        GameObject[] colorChangers = GameObject.FindGameObjectsWithTag("Recolor");

        foreach (var sprite in colorChangers)
        {
            if (sprite.GetComponent<SpriteRenderer>())
            {
                var renderer = sprite.GetComponent<SpriteRenderer>();
                renderer.color = color;
                PlayerPrefs.SetString("Color", firstFaunaColor);
                
                

}
        }
        string faunaColor = firstFaunaColor;
        return faunaColor;
    }









}

