using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;
using System.Threading.Tasks;
public class DBManager_Save_Read : MonoBehaviour
{
   private string uniqueid;
  /*
    private string userRegion="";
   private string userCountry="";
  */
    [SerializeField] private InputField nameInputField;
    [SerializeField] private InputField scoreInputField;

    [SerializeField] private GameObject nameInputScreen;

    // [SerializeField] private GameObject rowPreb;
    //  [SerializeField] private Transform rowParent;

    /*  [SerializeField] private InputField email;
      [SerializeField] private InputField getName;
      [SerializeField] private Text resultEmail;
      [SerializeField] private Text data;
      */
 /* private  DatabaseReference globalRef;
  private  DatabaseReference regionRef;
  private  DatabaseReference countryRef;
   */
    private DatabaseReference reference;
    void Start()
    {
        /*
        globalRef = FirebaseDatabase.DefaultInstance.RootReference.Child("LeaderBoard").Child("Global");
        regionRef = FirebaseDatabase.DefaultInstance.RootReference.Child("LeaderBoard").Child("Region");
        countryRef = FirebaseDatabase.DefaultInstance.RootReference.Child("LeaderBoard").Child("Country");
        */
        if (PlayerPrefs.GetString("currentPlayerID","null")=="null")
        {
            nameInputScreen.SetActive(true);
        }
        else
        {
            nameInputScreen.SetActive(true);

        }
      reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void SaveData()
    {
        if (!string.IsNullOrEmpty(nameInputField.text) && !string.IsNullOrEmpty(scoreInputField.text))
        {
            User user = new User();

            user.name = nameInputField.text;
            user.rank = "--";
            user.score = int.Parse( scoreInputField.text);
            user.profile_pic_name = user.GetProfilePicName();
            string json = JsonUtility.ToJson(user);


            uniqueid = Guid.NewGuid().ToString();
            Debug.Log("name input field value " + uniqueid);
            PlayerPrefs.SetString("currentPlayerID", uniqueid);

            nameInputScreen.SetActive(true);
            /*
            globalRef.Child(uniqueid).SetRawJsonValueAsync(json);
            regionRef.Child(userRegion).Child(uniqueid).SetRawJsonValueAsync(json);
            countryRef.Child(userCountry).Child(uniqueid).SetRawJsonValueAsync(json);
            */
            
            reference.Child("LeaderBoard").Child(uniqueid).SetRawJsonValueAsync(json);

            //  if(taskCompleted)
            //    {
            //    Debug.Log("Task Compltered eun");
            //   nameInputScreen.SetActive(false);
            // }
            //GameObject newGam = Instantiate(rowPreb, rowParent);

        }
    }
  
    /*  public void Read_Data()
      {

          reference.Child("User").Child(getName.text).GetValueAsync().ContinueWith(task =>
          {

              if (task.IsCompleted)
              {
                  Debug.Log("Successful");
                  DataSnapshot snapshot = task.Result;

                  data.text = snapshot.Child("email").Value.ToString();
              }
              else
              {
                  Debug.Log("UnSuccessful");
              }
          });
      }
    */
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            PlayerPrefs.DeleteKey("currentPlayerID");
        }
        Debug.Log(PlayerPrefs.GetString("currentPlayerID"));
    }



    public class User
    {
        public string rank;
        public string profile_pic_name;
        public string name;

        public int score;



        public string GetProfilePicName()
        {
            List<string> randomImage = new List<string>();

            randomImage.Add("images/1.jpg");
            randomImage.Add("images/2.jpeg");
            randomImage.Add("images/3.jpeg");
            randomImage.Add("images/4.jpeg");
            randomImage.Add("images/5.jpeg");

            return randomImage[UnityEngine.Random.Range(0, 5)];

        }

    }
}

