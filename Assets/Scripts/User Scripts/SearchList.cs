using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
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

    public class SearchList : MonoBehaviour
    {
        public List<GameObject> Users = new List<GameObject>();
        public GameObject UserModel;
        public TMP_InputField inputField;
       
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

        public void FillList()
        {
            //https://api.github.com/search/users?q=Master&page=1

            HttpWebRequest webRequest = System.Net.WebRequest.Create("https://api.github.com/search/users?q=" + inputField.text + "&page=1") as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {

                        string reader = responseReader.ReadToEnd();
                        Root jsonobj = JsonConvert.DeserializeObject<Root>(reader);
                        spawnPoint = this.transform.position;
                        Debug.Log(jsonobj.items.Count);
                        foreach (UserSearched user in jsonobj.items)
                        {
                            GameObject User = Instantiate(this.UserModel, this.spawnPoint, this.transform.rotation);

                            User.GetComponent<UserLogic>().Spawn(user.login);
                            Gap += User.GetComponent<Renderer>().bounds.size.x+1;

                            User.transform.position = new Vector3(spawnPoint.x + Gap, spawnPoint.y, spawnPoint.z);
                            User.transform.SetParent(this.transform);
                            Users.Add(User);
                        }


                    }

                }
                catch
                {
                    return;
                }
            }
        }
    }
}

