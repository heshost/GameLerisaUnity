using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lerisa
{
    public class bukafilem : MonoBehaviour
    {
        [SerializeField] private GameObject bukabingkai;
        [SerializeField] private GameObject bukaObjek;
        void Start()
        {

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
               // bukabingkai.SetActive(true);
                bukaObjek.SetActive(true);
                //   StartCoroutine(streamVideo());


            }
        }

        IEnumerator streamVideo()
        {
            yield return new WaitForSecondsRealtime(3);

            bukaObjek.SetActive(true);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
