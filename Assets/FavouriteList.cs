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
    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = this.transform.position;
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
        string destination = Application.persistentDataPath + "/save.dat";
        FileStream file;

        if (File.Exists(destination)) file = File.OpenWrite(destination);
else file = File.Create(destination);

        
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, UserstoSave);
        file.Close();
    }

    public void LoadFile()
    {
        string destination = Application.persistentDataPath + "/save.dat";
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
            GameObject User = Instantiate(this.UserModel, this.transform.position, this.transform.rotation) as GameObject;

            User.GetComponent<UserLogic>().SpawnfromSaved(user);
           
            Gap -= User.GetComponent<BoxCollider>().bounds.size.z + 1;
            User.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z + Gap);
            User.transform.parent = transform;
            User.transform.SetParent(this.transform);
           


        }
        Debug.Log(UserstoSave.Count);
 
    }
}
