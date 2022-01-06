
using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;


namespace Chess
{
    using System.IO;
    using System.Net;
    using System.Text;
    using Newtonsoft.Json;
    public class UserLogic : MonoBehaviour
{

        public string nick = null;
        public string bio="";
       // public List<ProjectLogic> projects;
        public int numberOfAllCommits = 1;
        public int followers =1;
        public int public_repos = 1;

        public GameObject ProjectModel;
        protected Vector3 spawnPoint;
        public List<GameObject> Projects = new List<GameObject>();
        public float transitionSpeed=5;
        public bool switcher = false;
        public bool Zswitch = false;
        public float Gap=0;
        List<ProjectJson> jsonobj2;
        User jsonobj;

    // Start is called before the first frame update
        void Start()
    {
          
    }

    // Update is called once per frame 
    void Update()
    {
        
    }
        public void Move(Vector3 newPos)
        {
            
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * transitionSpeed);
        }
        public void Spawn(String user)
        {
            nick = user;
            HttpWebRequest webRequest = System.Net.WebRequest.Create("https://api.github.com/users/" + user) as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                var username = "MasterKiller1239";
                var password = "test";

                var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
                webRequest.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {

                        string reader = responseReader.ReadToEnd();
                         jsonobj = JsonConvert.DeserializeObject<User>(reader);
                      

                    }

                }
                catch
                {
                    return;
                }
            }
            followers += jsonobj.followers;
            bio = jsonobj.bio.ToString();
            public_repos = jsonobj.public_repos;
            HttpWebRequest webRequest1 = System.Net.WebRequest.Create("https://api.github.com/users/"+user+"/repos") as HttpWebRequest;
            if (webRequest1 != null)
            {
                webRequest1.Method = "GET";
                webRequest1.UserAgent = "Anything";
                var username = "MasterKiller1239";
                var password = "test";

                var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
                //webRequest1.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
                webRequest1.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest1.GetResponse().GetResponseStream()))
                    {

                        string reader = responseReader.ReadToEnd();
                        jsonobj2 = JsonConvert.DeserializeObject<List<ProjectJson>>(reader);
                        spawnPoint = this.transform.position;
                        Debug.Log("Projekty: "+ jsonobj2.Count);
                      
                   
                    }

                }
                catch
                {
                    return;
                }
            }
            int i = 0;
            foreach (ProjectJson project in jsonobj2)
            {
                if (i < 10)
                {
                   
                    GameObject proj = (GameObject)Instantiate(this.ProjectModel, this.spawnPoint, this.transform.rotation);

                    proj.GetComponent<ProjectLogic>().setStats(project, user);
                    Gap += proj.GetComponent<ProjectLogic>().projectModel.GetComponent<Renderer>().bounds.size.y / 2;

                    proj.transform.position = new Vector3(spawnPoint.x, spawnPoint.y + Gap, spawnPoint.z);
                    proj.transform.SetParent(this.transform);
                    numberOfAllCommits += proj.GetComponent<ProjectLogic>().committs;
                    Projects.Add(proj);
                    Gap += proj.GetComponent<ProjectLogic>().projectModel.GetComponent<Renderer>().bounds.size.y / 2;
                }
                i++; 
               
                  //  break;
            }
            this.GetComponent<TooltipTrigger>().header = nick;
            this.GetComponent<TooltipTrigger>().content = bio;


        }
    }
}