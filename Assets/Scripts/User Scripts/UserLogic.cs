
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Chess
{
    using System.IO;
    using System.Net;
    using Newtonsoft.Json;
    public class UserLogic : MonoBehaviour
{

        public string nick = null;
        public string bio="";
       // public List<ProjectLogic> projects;
        public int numberOfAllCommits = 0;
        public int followers = 0;
        public int public_repos = 0;

        public GameObject ProjectModel;
        protected Vector3 spawnPoint;
        public List<GameObject> Projects = new List<GameObject>();
      
        public bool switcher = false;
        public bool Zswitch = false;
        public float Gap=0;
        

    // Start is called before the first frame update
    void Start()
    {
          
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
        public void Spawn(String user)
        {
            nick = user;
            HttpWebRequest webRequest = System.Net.WebRequest.Create("https://api.github.com/users/" + user) as HttpWebRequest;
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
                        User jsonobj = JsonConvert.DeserializeObject<User>(reader);
                        followers = jsonobj.followers;
                        bio = jsonobj.bio.ToString();
                        public_repos = jsonobj.public_repos;

                    }

                }
                catch
                {
                    return;
                }
            }
             webRequest = System.Net.WebRequest.Create("https://api.github.com/users/"+user+"/repos") as HttpWebRequest;
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
                        List<ProjectJson> jsonobj = JsonConvert.DeserializeObject<List<ProjectJson>>(reader);
                        spawnPoint = this.transform.position;
                        foreach (ProjectJson project in jsonobj)
                        {
                            GameObject proj = Instantiate(this.ProjectModel, this.spawnPoint, this.transform.rotation);

                            proj.GetComponent<ProjectLogic>().setStats(project,user);
                            Gap += proj.GetComponent<Renderer>().bounds.size.y;
                            
                            proj.transform.position = new Vector3(spawnPoint.x,spawnPoint.y+Gap,spawnPoint.z) ;
                            proj.transform.SetParent(this.transform);
                            Projects.Add(proj);

                        }
                   
                    }

                }
                catch
                {
                    return;
                }
            }
            //for (int i = 0; i < number; i++)
            //{
            //    Gap = this.Projects[i].GetComponent<Renderer>().bounds.size.y;
            //    spawnPoint = this.transform.position;
            //    if (Zswitch == false)
            //        spawnPoint.y += Gap * Projects.Count;
            //    else
            //        spawnPoint.z += Gap * Projects.Count;
            //    // Create an instance of the enemy prefab at the randomly selected spawn point's position and rotation.
            //    GameObject project = Instantiate(this.ProjectModel, this.spawnPoint, this.transform.rotation);
            //    project.transform.SetParent(this.transform);

            //    Projects.Add(project);
            //}

        }
    }
}