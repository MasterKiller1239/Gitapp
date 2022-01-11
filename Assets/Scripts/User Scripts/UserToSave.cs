using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Chess
{
    [Serializable]
    public class UserToSave
    {
        public string nick = null;
        public string bio = "";
        public List<projectsToSave> projects = new List<projectsToSave>();
        public int numberOfAllCommits = 1;
        public int followers = 1;
        public int public_repos = 1;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Fill(UserLogic user)
        {
            nick = user.nick;
            bio = user.bio;
            numberOfAllCommits = user.numberOfAllCommits;
            followers = user.followers;
            public_repos = user.public_repos;
            foreach(GameObject project in user.Projects)
            {
                projectsToSave pro = new projectsToSave();
                pro.Fill(project.GetComponent<ProjectLogic>());
                projects.Add(pro);
            }
        }
    }
}

