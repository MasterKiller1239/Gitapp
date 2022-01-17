using Chess;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.Playables;

public class FavouriteList : MonoBehaviour
{
    public List<UserToSave> UserstoSave; 
    public List<GameObject> savedUsers = new List<GameObject>();
    public Vector3 spawnPoint;
    public GameObject UserModel;
    public float Gap;
    public bool Load = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = this.transform.position;
        if(Load)
        LoadFile();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddToLocalDatabase(UserLogic user)
    {
        UserToSave userdata = new UserToSave();
        userdata.Fill(user);
        UserstoSave.Add(userdata);
    }
    public void SaveFile()
    {
        string destination = Application.dataPath + "/StreamingAssets" + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
else file = File.Create(destination);

        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, UserstoSave);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.dataPath + "/StreamingAssets" + "/save.dat";
        Debug.Log(destination+ Application.dataPath);
        FileStream file;
        
        if (File.Exists(destination)) file = File.OpenRead(destination);
        else
        {
            Debug.LogError("File not found");
return;
}
BinaryFormatter bf = new BinaryFormatter();
        UserstoSave = (List<UserToSave>)bf.Deserialize(file);
        file.Close();

        foreach (UserToSave user in UserstoSave)
        {
            GameObject User = Instantiate(this.UserModel, spawnPoint, this.transform.rotation) as GameObject;

            User.GetComponent<UserLogic>().SpawnfromSaved(user);
           
            Gap -= User.GetComponent<BoxCollider>().bounds.size.z + 1;
            User.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + Gap);
            if(spawnPoint.z + Gap<-8)
            {
                spawnPoint.x += User.GetComponent<BoxCollider>().bounds.size.x + 1; ;
                Gap = 0;
            }
            User.transform.parent = transform;
            User.transform.SetParent(this.transform);
           


        }
        Debug.Log(UserstoSave.Count);
 
    }
}
