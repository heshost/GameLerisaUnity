using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using UnityEngine.UI;

namespace lerisa
{
    public class NetworkManager : MonoBehaviourPunCallbacks
    {
        //pengaturan Connect Photon Sebelum Login 
        //Jika Network Connect Maka Tombol Login Aktif

        protected bool TriesToConnectToMaster;           //untuk coba lagi connect
        public GameObject loginButton;                  //panggil tombol login

        
        
        // Cek Connected
        void Start()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.ConnectUsingSettings();
                Debug.Log("Network Konek");
                TriesToConnectToMaster = false;
                loginButton.GetComponent<Button>().interactable = false;
            //    DontDestroyOnLoad(this);
            }

            else
            {
                Debug.Log("Gagal Konek");
                loginButton.GetComponent<Button>().interactable = false;
                TriesToConnectToMaster = true;
            }

        }

        // Update untuk bool tries to connect
        void Update()
        {
           // if (loginButton.GetComponent<Button>().interactable = false)
           //    loginButton.gameObject.SetActive(!PhotonNetwork.IsConnected && !TriesToConnectToMaster);
        }

        public override void OnConnected()
        {
            base.OnConnected();
            Debug.Log("Connected To Photon");
        }

        //connect to master
        public override void OnConnectedToMaster()
        {
            if (PhotonNetwork.IsConnected)
            {
                base.OnConnectedToMaster();
                PhotonNetwork.AutomaticallySyncScene = true;
                Debug.Log("Aplikasi Connected ke " + PhotonNetwork.CloudRegion + " Server");
                loginButton.GetComponent<Button>().interactable = true;
                PhotonNetwork.OfflineMode = false;
                TriesToConnectToMaster = true;

            }
            else
            {
                loginButton.GetComponent<Button>().interactable = false;
                Debug.Log("Server Offline");

            }
        }

        //Jika Gagal Connect
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            loginButton.GetComponent<Button>().interactable = false;
            Debug.Log(cause);
            TriesToConnectToMaster = false;
            OnConnectedToMaster();
        }


    }
}