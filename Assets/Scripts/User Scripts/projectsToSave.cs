using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chess
{
    [Serializable]
    public class projectsToSave 
    {
        public int committs;
        public string language;
        public int size;
        public int watchers_count;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void Fill(ProjectLogic project)
        {
            committs = project.committs;
            language = project.language;
            size = project.size;
            watchers_count = project.stars;
        }
    }
}

