using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public class MainList : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform Xmax;
        public Transform Ymax;
        public Transform Startpoint;
        public float Xlength;
        public float Ylength;
        public float  maxfollowers;
        public float maxCommits;
        public float maxrepos;
        public List<GameObject> Users = new List<GameObject>();
        public List<TMP_Text> Xvalues = new List<TMP_Text>();
        public List<TMP_Text> Yvalues = new List<TMP_Text>();
        public SearchList searchList;
        public TMP_Dropdown m_Dropdown;
        public int type = 0;
        public TMP_Dropdown m_Dropdown2;
        void Start()
        {
            Xlength = Xmax.position.x - Startpoint.position.x;
            Ylength = Ymax.position.z - Startpoint.position.z;
            m_Dropdown.onValueChanged.AddListener(delegate {
                DropdownValueChanged();
            });
            m_Dropdown2.onValueChanged.AddListener(delegate {
                DropdownValueChanged2();
            });

        }

        // Update is called once per frame
        void Update()
        {

        }

        void DropdownValueChanged()
        {
            ProjectLogic[] ALLProjects = GameObject.FindObjectsOfType<ProjectLogic>();
            foreach(ProjectLogic project in ALLProjects)
            {

                project.Expose(m_Dropdown.options[m_Dropdown.value].text);
             
            }



        }
        void DropdownValueChanged2()
        {

           type = m_Dropdown2.value;
           
           if(Users.Count>0)
            updatePositions();

        }
        public void updatePositions()
        {
            if (Users.Count > 0)
            {
                maxfollowers = 1.3f * Users.Max(t => t.gameObject.GetComponent<UserLogic>().followers);
                maxCommits = 1.1f * Users.Max(t => t.gameObject.GetComponent<UserLogic>().numberOfAllCommits);
                maxrepos = 1.2f * Users.Max(t => t.gameObject.GetComponent<UserLogic>().public_repos);
            }
              
            switch(type)
            {
                case 0:
                    foreach (GameObject User in Users)
                    {

                        //User.gameObject.transform.position = new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength);
                        User.gameObject.GetComponent<Movement>().Move(new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength));
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        Xvalues[i].text = Mathf.Round(((i + 1) / 8.0f * maxfollowers)).ToString();
                        Yvalues[i].text = Mathf.Round(((i + 1) / 8.0f * maxrepos)).ToString();
                        Yvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                        Xvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                    }
                    break;
                case 1:
                    foreach (GameObject User in Users)
                    {

                       // User.gameObject.transform.position = new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Ylength);
                        User.gameObject.GetComponent<Movement>().Move(new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Ylength));
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        Xvalues[i].text = Mathf.Round(((i + 1) / 8.0f * maxfollowers)).ToString();
                        Yvalues[i].text = Mathf.Round(((i + 1) / 8.0f * maxCommits)).ToString();
                        Yvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                        Xvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                    }
                    break;
                case 2:
                    foreach (GameObject User in Users)
                    {

                        //User.gameObject.transform.position = new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength);
                        User.gameObject.GetComponent<Movement>().Move(new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength));
                    }
                    for (int i = 0; i < 8; i++)
                    {
                        Xvalues[i].text = Mathf.Round(((i + 1) / 8.0f * maxCommits)).ToString();
                        Yvalues[i].text = Mathf.Round(((i + 1) / 8.0f * maxrepos)).ToString();
                        Yvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                        Xvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                    }
                    break;
            }
           
        }
        private void OnCollisionEnter(Collision collision)
        {
           
           
            if (collision.gameObject.tag == "drag")
            {
                    Users.Add(collision.gameObject);
                this.GetComponent<DragandDrop>().Drop();
                    // searchList.RemoveFromList(collision.gameObject.GetHashCode());
                    updatePositions();
                }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.tag == "drag")
            {
                Users.Remove(collision.gameObject);
                // searchList.RemoveFromList(collision.gameObject.GetHashCode());
                updatePositions();
            }
          
        }
    }
}

