using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using TMPro;
using UnityEngine;

namespace Chess
{

    public class UserSearched
    {
        public string login { get; set; }
        public int id { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string followers_url { get; set; }
        public string following_url { get; set; }
        public string gists_url { get; set; }
        public string starred_url { get; set; }
        public string subscriptions_url { get; set; }
        public string organizations_url { get; set; }
        public string repos_url { get; set; }
        public string events_url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
        public bool site_admin { get; set; }
        public double score { get; set; }
    }

    public class Root
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<UserSearched> items { get; set; }
    }
    [Serializable]
    public class SearchList : MonoBehaviour
    {
        public List<GameObject> Users = new List<GameObject>();
        public FavouriteList savelist;
        public GameObject UserModel;
        public TMP_InputField inputField;
        Root jsonobj;
        HttpWebRequest webRequestMain;
        public Vector3 spawnPoint;
        public float Gap;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
       public void Save()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Create, FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, this.gameObject);
            stream.Close();
        }
        public void Load()
        {
            IFormatter formatter = new BinaryFormatter();
            Stream stream = new FileStream("MyFile.bin", FileMode.Open, FileAccess.Read, FileShare.Read);
          //  gameObject.GetComponent< SearchList >() = (SearchList)formatter.Deserialize(stream);
            stream.Close();
        }
    
        public void RemoveFromList(int hashcode)
        {
            var itemToRemove = Users.Single(r => r.GetHashCode() == hashcode);
            Users.Remove(itemToRemove);
        }
        public void ClearList()
        {

            foreach (GameObject user in Users)
            {
                GameObject nearest = null;
                nearest = user;

                Users.Remove(user);
                Destroy(nearest.gameObject);
            }



        }
        public async void TestGet()
        {
            //var url = "https://api.github.com/rate_limit";

            //var httpClient = new HappyHttpClient(new JsonSerializationOption());

            //var result = await httpClient.Get<string>(url);
            //Debug.Log(result);
             HttpWebRequest webRequestMain1 = System.Net.WebRequest.Create("https://api.github.com/rate_limit") as HttpWebRequest;
            if (webRequestMain1 != null)
            {
                webRequestMain1.Method = "GET";
                webRequestMain1.UserAgent = "Anything";
                var username = "MasterKiller1239";
                var password = "test";

                var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
                webRequestMain1.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
                webRequestMain1.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequestMain1.GetResponse().GetResponseStream()))
                    {

                        string reader = responseReader.ReadToEnd();

                        var jsonobja = JsonConvert.DeserializeObject(reader);

                      
    


                    }

                }
                catch
                {
                    return;
                }
            }

        }
        public void FillList()
        {
           TestGet();
            //https://api.github.com/search/users?q=Master&page=1
            webRequestMain = null;
            webRequestMain = System.Net.WebRequest.Create("https://api.github.com/search/users?q=" + inputField.text + "&per_page=1&page=1") as HttpWebRequest;
            if (webRequestMain != null)
            {
                webRequestMain.Method = "GET";
                webRequestMain.UserAgent = "Anything";
                var username = "MasterKiller1239";
                var password = "test";

                var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
                webRequestMain.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
                webRequestMain.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequestMain.GetResponse().GetResponseStream()))
                    {

                        string reader = responseReader.ReadToEnd();
                        jsonobj = null;
                        jsonobj = JsonConvert.DeserializeObject<Root>(reader);
                        spawnPoint = this.transform.position;
                        Debug.Log("Users: "+jsonobj.items.Count);
                       


                    }

                }
                catch
                {
                    return;
                }
            }
            foreach (UserSearched user in jsonobj.items)
            {
                GameObject User = Instantiate(this.UserModel, this.spawnPoint, this.transform.rotation) as GameObject;

                User.GetComponent<UserLogic>().Spawn(user.login);
                savelist.AddToLocalDatabase(User.GetComponent<UserLogic>());
                Gap -= User.GetComponent<BoxCollider>().bounds.size.z + 1;
                User.transform.position = new Vector3(spawnPoint.x, spawnPoint.y, spawnPoint.z + Gap);
                User.transform.parent = transform;
                User.transform.SetParent(this.transform);
                Users.Add(User);
                if (spawnPoint.z + Gap < -5)
                {
                    spawnPoint.x -= User.GetComponent<BoxCollider>().bounds.size.x + 1; ;
                    Gap = 0;
                }

            }
        }
    }
}

