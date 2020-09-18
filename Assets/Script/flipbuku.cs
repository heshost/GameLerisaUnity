using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class flipbuku : MonoBehaviour
    {
        public GameObject namabuku;
       
        Text statusInGame;

        public string TextStatusAktifitas = "TextStatusBawah";

        void Start()
        {
            namabuku.SetActive(false);
            statusInGame = GameObject.Find(TextStatusAktifitas).GetComponent<Text>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {

               namabuku.SetActive(true);

               statusInGame.text = PhotonNetwork.NickName + " on Book Session";
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {

                namabuku.SetActive(false);


                statusInGame.text = PhotonNetwork.NickName + " Exit Book Session";
            }
        }


        void Update()
        {

        }
    }
}
