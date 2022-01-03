using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Chess
{
    public class MainList : MonoBehaviour
    {
        // Start is called before the first frame update
        public Transform Xmax;
        public Transform Ymax;
        public Transform Startpoint;
        public List<GameObject> Users = new List<GameObject>();
        public SearchList searchList;
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnCollisionEnter(Collision collision)
        {
            Users.Add(collision.gameObject);
        }
    }
}

