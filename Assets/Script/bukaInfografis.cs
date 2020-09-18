using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class bukaInfografis : MonoBehaviour
    {
        [SerializeField] private GameObject bukabingkai;
        [SerializeField] private GameObject bukaObjek;
        Text statusInGame;


        public string TextStatusAktifitas = "TextStatusBawah";
        void Start()
        {
            bukabingkai.SetActive(false);
            statusInGame = GameObject.Find(TextStatusAktifitas).GetComponent<Text>();

        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {

                bukabingkai.SetActive(true);
                Debug.Log("in Infografis");

                statusInGame.text = PhotonNetwork.NickName + " on Infographic Session";
               // StartCoroutine(streamVideo());
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
               // bukabingkai.SetActive(false);
                //  panelBuka.SetActive(false);

                statusInGame.text = PhotonNetwork.NickName + " Exit Infographic Session";
            }
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
