using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;


namespace lerisa
{
    public class PreTest : MonoBehaviourPunCallbacks
    {
        // Start is called before the first frame update
        public GameManage gM;
        [HideInInspector]
        public Play localPlayer;

        [SerializeField] private PhotonView pv = null;
        [SerializeField] private GameObject panelBuka;

        public string TextStatusAktifitas = "TextStatusBawah";

        Text statusInGame;



        private int skor;
        private int point =100;


        void Start()
        {
            gM = GetComponent<GameManage>();
            pv = this.GetComponent<PhotonView>();
            skor = (int)PhotonNetwork.LocalPlayer.GetScore();
            Debug.Log(skor);
            statusInGame = GameObject.Find(TextStatusAktifitas).GetComponent<Text>();


        }

        // Update is called once per frame
        void Update()
        {

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

                    statusInGame.text = PhotonNetwork.LocalPlayer.NickName + " On Pre-Test Session";

                    Debug.Log(statusInGame.text);

                   
                }
            

            //   
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {

                panelBuka.SetActive(false);

                statusInGame.text = PhotonNetwork.NickName + " Exit Pre-Test Session";
            }
        }

        void addscore()
        {
            Debug.Log("add score");
           
                
                    PhotonNetwork.LocalPlayer.AddScore(point);

                 //   skor = (int)PhotonNetwork.LocalPlayer.GetScore();

                                          
        }
    }
}