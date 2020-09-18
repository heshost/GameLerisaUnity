using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class openQuiz : MonoBehaviour
    {
        public GameObject namaQuiz;

        Text statusInGame;

        public string TextStatusAktifitas = "TextStatusBawah";

        void Start()
        {
            namaQuiz.SetActive(false);
            statusInGame = GameObject.Find(TextStatusAktifitas).GetComponent<Text>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {

                namaQuiz.SetActive(true);

                statusInGame.text = PhotonNetwork.NickName + " on Quiz Session";
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {

                namaQuiz.SetActive(false);


                statusInGame.text = PhotonNetwork.NickName + " Exit Quiz Session";
            }
        }


        void Update()
        {

        }
    }
}