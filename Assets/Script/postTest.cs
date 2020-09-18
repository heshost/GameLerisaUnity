using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;

namespace lerisa
{
    public class postTest : MonoBehaviourPunCallbacks
    {
        public GameManage gM;
        [HideInInspector]
        public Play localPlayer;

        [SerializeField] private PhotonView pv = null;
        [SerializeField] private GameObject panelBuka;

        public string TextStatusAktifitas = "TextStatusBawah";

        Text statusInPost;



        private int skor;
        private int point = 100;


        void Start()
        {
            gM = GetComponent<GameManage>();
            pv = this.GetComponent<PhotonView>();
            skor = (int)PhotonNetwork.LocalPlayer.GetScore();
            Debug.Log(skor);
            statusInPost = GameObject.Find(TextStatusAktifitas).GetComponent<Text>();
        }

        private void OnTriggerEnter(Collider other)
        {
            panelBuka.SetActive(false);

            Debug.Log(PhotonNetwork.NickName);
            Debug.Log(PhotonNetwork.LocalPlayer.NickName);
            Debug.Log(other.gameObject.GetPhotonView().IsMine);

            if (other.gameObject.GetPhotonView().IsMine)

            {

                panelBuka.SetActive(true);

                statusInPost.text = PhotonNetwork.LocalPlayer.NickName + " On Post-Test Session";

                Debug.Log(statusInPost.text);

                // addscore();
            }
        }

        private void OnTriggerExit(Collider other)
        {

            if (other.gameObject.GetPhotonView().IsMine)
            {

                panelBuka.SetActive(false);

                statusInPost.text = PhotonNetwork.NickName + " Exit Post-Test Session";
            }

        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}