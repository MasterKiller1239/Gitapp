using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Chess
{
    public static class MathExtensions
    {
        public static float Round(this float i, int nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

            return (i + 5 * nearest / 10) / nearest * nearest;
        }
        public static int Round(this int i, int nearest)
        {
            if (nearest <= 0 || nearest % 10 != 0)
                throw new ArgumentOutOfRangeException("nearest", "Must round to a positive multiple of 10");

            return (i + 5 * nearest / 10) / nearest * nearest;
        }
    }
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
        public TMP_Text YAxe;
        public TMP_Text XAxe;
        public SearchList searchList;
        public TMP_Dropdown m_Dropdown;
        public int type = 0;
        public TMP_Dropdown m_Dropdown2;
        public GameObject tooltip;
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
           
            if (m_Dropdown.IsExpanded || m_Dropdown2.IsExpanded)
            {
                this.gameObject.GetComponent<DragandDrop>().Hudworking = true;
                tooltip.SetActive(false);

            }
            else
            {
                this.gameObject.GetComponent<DragandDrop>().Hudworking = false;
                tooltip.SetActive(true);
            }

        }

       public void DropdownValueChanged()
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
            else
            {
                switch(type)
                {
                    case 0:
                        YAxe.text = "No.Repos";
                        XAxe.text = "Follows";
                        break;
                    case 1:
                        YAxe.text = "Commits";
                        XAxe.text = "Follows";
                        break;
                    case 2:
                        YAxe.text = "No.Repos";
                        XAxe.text = "Commits";
                        break;

                }
            }

        }
        public void updatePositions()
        {
            if (Users.Count > 0)
            {
                maxfollowers = (int)(1.3f * Users.Max(t => t.gameObject.GetComponent<UserLogic>().followers)).Round(10);
                maxCommits =(int)( 1.1f * Users.Max(t => t.gameObject.GetComponent<UserLogic>().numberOfAllCommits)).Round(10);
                maxrepos = (int)(1.2f * Users.Max(t => t.gameObject.GetComponent<UserLogic>().public_repos)).Round(10);
            }
       
            switch (type)
            {
                case 0:
                    foreach (GameObject User in Users)
                    {

                        //User.gameObject.transform.position = new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength);
                        User.gameObject.GetComponent<Movement>().Move(new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength));
                    }
                    
                    for (int i = 0; i < 16; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (maxfollowers < 100)
                                Xvalues[i].gameObject.SetActive(false);
                            else
                                Xvalues[i].gameObject.SetActive(true);

                            if (maxrepos < 100)
                                Yvalues[i].gameObject.SetActive(false);
                            else
                                Yvalues[i].gameObject.SetActive(true);
                        }

                        Xvalues[i].text = (Mathf.Round(((i + 1) / 16.0f * maxfollowers)).Round(10)).ToString();
                        Yvalues[i].text = (Mathf.Round(((i + 1) / 16.0f * maxrepos)).Round(10)).ToString();

                        Yvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                        Xvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                    }
                   
                    YAxe.text = "No.Repos";
                    XAxe.text = "Follows";
                    break;
                case 1:
                    foreach (GameObject User in Users)
                    {

                       // User.gameObject.transform.position = new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Ylength);
                        User.gameObject.GetComponent<Movement>().Move(new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().followers / maxfollowers) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Ylength));
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if (maxfollowers < 100)
                                Xvalues[i].gameObject.SetActive(false);
                            else
                                Xvalues[i].gameObject.SetActive(true);

                            if (maxCommits < 100)
                                Yvalues[i].gameObject.SetActive(false);
                            else
                                Yvalues[i].gameObject.SetActive(true);
                        }
                        Xvalues[i].text = Mathf.Round(((i + 1) / 16.0f * maxfollowers)).ToString();
                        Yvalues[i].text = Mathf.Round(((i + 1) / 16.0f * maxCommits)).ToString();
                        Yvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                        Xvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                    }
                    YAxe.text = "Commits";
                    XAxe.text = "Follows";
                    break;
                case 2:
                    foreach (GameObject User in Users)
                    {

                        //User.gameObject.transform.position = new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength);
                        User.gameObject.GetComponent<Movement>().Move(new Vector3(Startpoint.position.x + (User.GetComponent<UserLogic>().numberOfAllCommits / maxCommits) * Xlength, Startpoint.position.y, Startpoint.position.z + (User.GetComponent<UserLogic>().public_repos / maxrepos) * Ylength));
                    }
                    for (int i = 0; i < 16; i++)
                    {
                        if (i % 2 == 0)
                        {
                            if ( maxCommits < 100)
                            Xvalues[i].gameObject.SetActive(false);
                        else
                        Xvalues[i].gameObject.SetActive(true);

                    if ( maxrepos < 100)
                        Yvalues[i].gameObject.SetActive(false);
                    else
                        Yvalues[i].gameObject.SetActive(true);
            }
                      
                        Xvalues[i].text = Mathf.Round(((i + 1) / 16.0f * maxCommits)).ToString();
                        Yvalues[i].text = Mathf.Round(((i + 1) / 16.0f * maxrepos)).ToString();
                        Yvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                        Xvalues[i].gameObject.GetComponent<Movement>().MovefromZero();
                    }
                    YAxe.text = "No.Repos";
                    XAxe.text = "Commits";
                    break;
            }
           
        }

       
        private void OnCollisionEnter(Collision collision)
        {


            if (collision.gameObject.tag == "drag" )
            {
                    Users.Add(collision.gameObject);
               // this.GetComponent<DragandDrop>().Drop();
                    // searchList.RemoveFromList(collision.gameObject.GetHashCode());
                    updatePositions();
                }
        }
        public void Remove(GameObject item)
        {
          if(Users.Remove(item))
           updatePositions();
        }
        //private void OnCollisionExit(Collision collision)
        //{
        //    if (collision.gameObject.tag == "drag")
        //    {
        //        Users.Remove(collision.gameObject);
        //        // searchList.RemoveFromList(collision.gameObject.GetHashCode());
        //        updatePositions();
        //    }
          
        //}
    }
}

