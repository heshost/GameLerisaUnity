using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;
using UnityEngine.UI;


namespace lerisa
{
    public class GameManage : MonoBehaviourPunCallbacks
    {
       

        public Play playerPrefabs;

        public WebManager data;
        
        [HideInInspector]
        public Play localPlayer;

        public TextMeshPro namaviewOwner;
        public Text namaPlayer;
        public Text namaRoom;
        public Text skorPlayer;
        public Text statusInGame;

        public Button tombolKeluarLevel;
        Button TombolKeluarLevel;
        public string namaSceneKeluar;

        [SerializeField] private PhotonView pv = null;

        private int skor;


        private void Awake()
        {
           

            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
                Debug.Log("Gagal Konek Balik Ke Screen Login");
                SceneManager.LoadScene("UI");
                return;
            }

         
        }

        // create player
        void Start()
        {
            pv = this.GetComponent<PhotonView>();

            Play.RefreshInstance(ref localPlayer, playerPrefabs);


            Debug.Log("Create Pemain");

            if (pv != null)
            {

                if (pv.IsMine)
                {
                    namaPlayer.enabled = true;
                    namaPlayer.text = PhotonNetwork.NickName;
                    Debug.Log(namaPlayer.text + " View Is Mine");
                    
                   

                    skor = (int)PhotonNetwork.LocalPlayer.GetScore();
                    skorPlayer.text = skor.ToString();
                    Debug.Log(skorPlayer.text + "IsMine");

                   

                }
            }

            

            pv.RPC("broadcastPesan", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName + " Bergabung");
            Debug.Log("Spawn Pemain Baru Bergabung");

            namaRoom.text = PhotonNetwork.CurrentRoom.Name;
            statusInGame.text = PhotonNetwork.NickName + " Mulai Bergabung";

            namaPlayer.text = PhotonNetwork.NickName;
            Debug.Log(namaPlayer.text + " View Is Other");
            skor = (int)PhotonNetwork.LocalPlayer.GetScore();
            skorPlayer.text = skor.ToString();
            Debug.Log(skorPlayer.text + " Is other");

            TombolKeluarLevel = tombolKeluarLevel.GetComponent<Button>();
            TombolKeluarLevel.onClick.AddListener(() => LeaveRoom());


        }

        
        public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            Play.RefreshInstance(ref localPlayer, playerPrefabs);

                         
            
        }

        //update jika ada player lain gabung, dipanggil per frame

        void Update()
        {
            if (!PhotonNetwork.IsConnected)
            {
                PhotonNetwork.OfflineMode = true;
            }

           
            if (pv != null)
            {
                if (pv.IsMine)
                {
                    skor = (int)PhotonNetwork.LocalPlayer.GetScore();
                    skorPlayer.text = skor.ToString();
                 //   Debug.Log(skorPlayer.text + " Is Me");
                    namaPlayer.text = PhotonNetwork.NickName;
                    namaRoom.text = PhotonNetwork.CurrentRoom.Name;
                }
            }

            skor = (int)PhotonNetwork.LocalPlayer.GetScore();
            skorPlayer.text = skor.ToString();
        //    Debug.Log(skorPlayer.text + " Is Friend");
            namaPlayer.text = PhotonNetwork.NickName;




        }






        public override void OnLeftRoom()
        {
           
            Debug.Log(PhotonNetwork.NickName + "Leave Room yeah 1"); 

            Scene sceneIni = SceneManager.GetActiveScene();
            Debug.Log(sceneIni);
            if (sceneIni.name != namaSceneKeluar)
            {
                PhotonNetwork.LoadLevel(namaSceneKeluar);
            }
        }


        // [PunRPC]
        //     void synNamaPlayer(string namaDiatasKepala)
        //    {
        //      localPlayer.namapemain.text = namaDiatasKepala;
        //    }

        void LeaveRoom()
        {
            if (PhotonNetwork.IsConnected)
            {
                pv.RPC("broadcastPesan", RpcTarget.All, PhotonNetwork.LocalPlayer.NickName + " Leave Room");
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.LeaveLobby();
                //otonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer.NickName);
            }
            Debug.Log(PhotonNetwork.NickName + "Leave Room yeah");
        }


        [PunRPC]
        void broadcastPesan(string pesan)
        {
            statusInGame.text = pesan;
        }



    }
}
