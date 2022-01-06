using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using UnityEngine;

namespace Chess
{
    public enum Language
    {
        Cplusplus,
        Csharp,
        Java,
        Dart,
        JavaScript,
        Other
    }
    public class ProjectLogic : MonoBehaviour
    {
        // Start is called before the first frame update
        public Color color;
        public int committs;
        public String language;
        public int size;
        public GameObject projectModel;
        List<Committ> jsonobj;
        void Start()
        {


            
        }

        // Update is called once per frame
        void Update()
        {
          
        }
        public void Resize(float amount, Vector3 direction)
        {
            if(amount < 100 )
            {
                this.transform.position += direction * 1 / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
                this.transform.localScale += direction * 1; // Scale object in the specified direction 
            }
            else if (amount > 100 && amount < 200)
            {
                this.transform.position += direction * 2 / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
                this.transform.localScale += direction * 2; // Scale object in the specified direction 
            }
            else 
            {
                this.transform.position += direction * 3 / 2; // Move the object in the direction of scaling, so that the corner on ther side stays in place
                this.transform.localScale += direction * 3; // Scale object in the specified direction 
            }

        }
        public void setStats(ProjectJson json, String user)
        {

            size = json.size;
            language = json.language;
            HttpWebRequest webRequest = System.Net.WebRequest.Create("https://api.github.com/repos/"+user+"/"+ json.name + "/commits") as HttpWebRequest;
            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                var username = "MasterKiller1239";
                var password = "test";

                var bytes = Encoding.UTF8.GetBytes($"{username}:{password}");
               // webRequest.Headers.Add("Authorization", $"Basic {Convert.ToBase64String(bytes)}");
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {

                        string reader = responseReader.ReadToEnd();
                        jsonobj = JsonConvert.DeserializeObject<List<Committ>>(reader);

                        Debug.Log(jsonobj.Count);
                        

                    }

                }
                catch
                {
                    Debug.Log("Xd");
                    return;
                }
            }
            Debug.Log(jsonobj.Count);
            committs = jsonobj.Count;
            Resize(size / committs, new Vector3(0f, 1f, 0f));
            switch (language)
            {
                case "C++":
                    projectModel.GetComponent<Renderer>().materials[1].color = Color.red;
                    gameObject.tag = "C++";
                    break;
                case "C#":
                    projectModel.GetComponent<Renderer>().materials[1].color = Color.blue;
                    gameObject.tag = "C#";
                    break;
                case "Java":
                    projectModel.GetComponent<Renderer>().materials[1].color = Color.yellow;
                    gameObject.tag = "Java";
                    break;
                case "Dart":
                    projectModel.GetComponent<Renderer>().materials[1].color = Color.cyan;
                    gameObject.tag = "Dart";
                    break;
                case "JavaScript":
                    projectModel.GetComponent<Renderer>().materials[1].color = Color.black;
                    gameObject.tag = "JavaScript";
                    break;
                case "C":
                    projectModel.GetComponent<Renderer>().materials[1].color = Color.magenta;
                    gameObject.tag = "C";
                    break;
                default:
                    projectModel.GetComponent<Renderer>().materials[1].color = Color.gray;
                    gameObject.tag = "Other";
                    break;
            }
        }
   
        public void Expose(String lan)
        {
            if(lan==language ||lan=="None")
            {
                switch (language)
                {
                    case "C++":
                        projectModel.GetComponent<Renderer>().materials[1].color = Color.red;
                        break;
                    case "C#":
                        projectModel.GetComponent<Renderer>().materials[1].color = Color.blue;
                        break;
                    case "Java":
                        projectModel.GetComponent<Renderer>().materials[1].color = Color.yellow;
                        break;
                    case "Dart":
                        projectModel.GetComponent<Renderer>().materials[1].color = Color.cyan;
                        break;
                    case "JavaScript":
                        projectModel.GetComponent<Renderer>().materials[1].color = Color.black;
                        break;
                    case "C":
                        projectModel.GetComponent<Renderer>().materials[1].color = Color.magenta;
                        break;
                    default:
                        projectModel.GetComponent<Renderer>().materials[1].color = Color.gray;
                        break;
                }
            }
            else
            {
                projectModel.GetComponent<Renderer>().materials[1].color = Color.gray;
            }
        }
    }







    public class Item
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

    public class Users
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<Item> items { get; set; }
    }
    public class User
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
        public string name { get; set; }
        public object company { get; set; }
        public string blog { get; set; }
        public string location { get; set; }
        public object email { get; set; }
        public object hireable { get; set; }
        public object bio { get; set; }
        public object twitter_username { get; set; }
        public int public_repos { get; set; }
        public int public_gists { get; set; }
        public int followers { get; set; }
        public int following { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
    }
    public class Owner
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
    }

    public class License
    {
        public string key { get; set; }
        public string name { get; set; }
        public string spdx_id { get; set; }
        public string url { get; set; }
        public string node_id { get; set; }
    }

    public class ProjectJson
    {
        public int id { get; set; }
        public string node_id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public bool @private { get; set; }
        public Owner owner { get; set; }
        public string html_url { get; set; }
        public string description { get; set; }
        public bool fork { get; set; }
        public string url { get; set; }
        public string forks_url { get; set; }
        public string keys_url { get; set; }
        public string collaborators_url { get; set; }
        public string teams_url { get; set; }
        public string hooks_url { get; set; }
        public string issue_events_url { get; set; }
        public string events_url { get; set; }
        public string assignees_url { get; set; }
        public string branches_url { get; set; }
        public string tags_url { get; set; }
        public string blobs_url { get; set; }
        public string git_tags_url { get; set; }
        public string git_refs_url { get; set; }
        public string trees_url { get; set; }
        public string statuses_url { get; set; }
        public string languages_url { get; set; }
        public string stargazers_url { get; set; }
        public string contributors_url { get; set; }
        public string subscribers_url { get; set; }
        public string subscription_url { get; set; }
        public string commits_url { get; set; }
        public string git_commits_url { get; set; }
        public string comments_url { get; set; }
        public string issue_comment_url { get; set; }
        public string contents_url { get; set; }
        public string compare_url { get; set; }
        public string merges_url { get; set; }
        public string archive_url { get; set; }
        public string downloads_url { get; set; }
        public string issues_url { get; set; }
        public string pulls_url { get; set; }
        public string milestones_url { get; set; }
        public string notifications_url { get; set; }
        public string labels_url { get; set; }
        public string releases_url { get; set; }
        public string deployments_url { get; set; }
        public DateTime created_at { get; set; }
        public DateTime updated_at { get; set; }
        public DateTime pushed_at { get; set; }
        public string git_url { get; set; }
        public string ssh_url { get; set; }
        public string clone_url { get; set; }
        public string svn_url { get; set; }
        public string homepage { get; set; }
        public int size { get; set; }
        public int stargazers_count { get; set; }
        public int watchers_count { get; set; }
        public string language { get; set; }
        public bool has_issues { get; set; }
        public bool has_projects { get; set; }
        public bool has_downloads { get; set; }
        public bool has_wiki { get; set; }
        public bool has_pages { get; set; }
        public int forks_count { get; set; }
        public object mirror_url { get; set; }
        public bool archived { get; set; }
        public bool disabled { get; set; }
        public int open_issues_count { get; set; }
        public License license { get; set; }
        public bool allow_forking { get; set; }
        public bool is_template { get; set; }
        public List<object> topics { get; set; }
        public string visibility { get; set; }
        public int forks { get; set; }
        public int open_issues { get; set; }
        public int watchers { get; set; }
        public string default_branch { get; set; }
    }
    public class Author
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
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
    }

    public class Committer
    {
        public string name { get; set; }
        public string email { get; set; }
        public DateTime date { get; set; }
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
    }

    public class Tree
    {
        public string sha { get; set; }
        public string url { get; set; }
    }

    public class Verification
    {
        public bool verified { get; set; }
        public string reason { get; set; }
        public string signature { get; set; }
        public string payload { get; set; }
    }

    public class Commit
    {
        public Author author { get; set; }
        public Committer committer { get; set; }
        public string message { get; set; }
        public Tree tree { get; set; }
        public string url { get; set; }
        public int comment_count { get; set; }
        public Verification verification { get; set; }
    }

    public class Parent
    {
        public string sha { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
    }

    public class Committ
    {
        public string sha { get; set; }
        public string node_id { get; set; }
        public Commit commit { get; set; }
        public string url { get; set; }
        public string html_url { get; set; }
        public string comments_url { get; set; }
        public Author author { get; set; }
        public Committer committer { get; set; }
        public List<Parent> parents { get; set; }
    }
}

