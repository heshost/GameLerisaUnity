using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class RoomManager : MonoBehaviourPunCallbacks
    {

        [SerializeField]
        private GameObject startbutton;

        [SerializeField]
        private GameObject cancelbutton;

        [SerializeField]
        public int maxPlayerPerRoom;

        [SerializeField]
        private Text statusmultiplayer;

        [SerializeField]
        private int multiplayerSceneIndex;


        //ketika berada di panel lobby set awake
        private void Awake()
        {
            statusmultiplayer.text = "Mulai Awake";
            OnConnectedToMaster();

        }


        //set connect to master
        public override void OnConnectedToMaster()
        {
            if (PhotonNetwork.IsConnected)
            {
                base.OnConnectedToMaster();
                
                PhotonNetwork.AutomaticallySyncScene = true;
                statusmultiplayer.text = "Aplikasi Berhasil Connected ke " + PhotonNetwork.CloudRegion + " Server";
                startbutton.SetActive(true);
                Debug.Log("Aplikasi Connected ke " + PhotonNetwork.CloudRegion + " Server");
            }
            else
            {
                startbutton.SetActive(false);
                Debug.Log("Server Disconnect");
                statusmultiplayer.text = "Server Disconnected, Please Log Out";

            }
        }

        //set negatif connect to master
        public override void OnDisconnected(DisconnectCause cause)
        {
            base.OnDisconnected(cause);
            Debug.Log(cause);
        }


        //start game
        public void quickstargame()
        {
            startbutton.SetActive(false);
            cancelbutton.SetActive(true);
            PhotonNetwork.JoinRandomRoom();
            statusmultiplayer.text = "Room Created (Please Waiting) ";
            Debug.Log("Jumlah Room: " + PhotonNetwork.CountOfRooms + " Join Random Room");
        }

        //Gagal Join Room
        public override void OnJoinRandomFailed(short returnCode, string message)
        {
            base.OnJoinRandomFailed(returnCode, message);
            Debug.Log("Gagal Gabung Room");
            statusmultiplayer.text = "Gagal Gabung Room";
            CreateRoom();
        }

        //panggil fungsi create room
        void CreateRoom()
        {
            Debug.Log("Room Proses Create");
            int NomorRoom = Random.Range(0, 100);       //random nama room

            RoomOptions pilihanRoom = new RoomOptions()  // create pilihan room
            {
                IsVisible = true,
                IsOpen = true,
                MaxPlayers = (byte)maxPlayerPerRoom
            };

            PhotonNetwork.CreateRoom("Room" + NomorRoom, pilihanRoom);  //create a new room
            Debug.Log("Room" + NomorRoom + " Berhasil dibuat");
            statusmultiplayer.text = PhotonNetwork.NickName + " Bergabung Ke Room " + NomorRoom;

            Debug.Log(PhotonNetwork.NickName + " Bergabung || Nama Room: " + NomorRoom + " Jumlah Player on Master: " + PhotonNetwork.CountOfPlayersOnMaster);

        }


        // jika gagal buat room
        public override void OnCreateRoomFailed(short returnCode, string message)
        {
            base.OnCreateRoomFailed(returnCode, message);
            Debug.Log("Gagal Buat Room, Try Again..");
            statusmultiplayer.text = "Gagal Buat Room";
            CreateRoom();                                               // coba create room lagi
        }


        //Cancel Gabung Room
        public void cancelGame()
        {
            cancelbutton.SetActive(false);
            startbutton.SetActive(true);
            PhotonNetwork.LeaveRoom();

            Debug.Log("Player Leave Room");
            statusmultiplayer.text = "Player Leave Room";
        }


        //register callback function
        public override void OnEnable()
        {
            base.OnEnable();
            PhotonNetwork.AddCallbackTarget(this);
        }

        //unregister callback function
        public override void OnDisable()
        {
            base.OnDisable();
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        //join room state
        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            Debug.Log("Bergabung ke " + PhotonNetwork.CurrentRoom.Name + " Jumlah Player: " + PhotonNetwork.CurrentRoom.PlayerCount);
            statusmultiplayer.text = PhotonNetwork.NickName + " Bergabung Ke " + PhotonNetwork.CurrentRoom.Name + "Jumlah Player Aktif: " + PhotonNetwork.CurrentRoom.PlayerCount;
            StartGame();

        }

        //gagal Join Room
        public override void OnJoinRoomFailed(short returnCode, string message)
        {
            base.OnJoinRoomFailed(returnCode, message);
            Debug.Log(message);

           
        }

     


        //left room
        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            Debug.Log("Meninggalkan room");
        }

        // Start is called before the first frame update
        void StartGame()
        {
            if (PhotonNetwork.IsMasterClient)                // masterClient initiate
            {
                Debug.Log("Mulai Game");
                PhotonNetwork.LoadLevel(multiplayerSceneIndex); // loadlevel digunakan karena AutoSynscene all player sesuai setup awal AutomaticallySyncScene = true
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


        public void LogOutGame()
        {

            
            cancelbutton.SetActive(false);
            startbutton.SetActive(true);

            if (!PhotonNetwork.IsConnected)
            {

                Debug.Log("Lagi TIdak Di Room");
                OnConnected();
                Debug.Log("Aplikasi Connected ke " + PhotonNetwork.CloudRegion + " Server");

                OnConnectedToMaster();
                Debug.Log("Aplikasi Connected ke " + PhotonNetwork.CloudRegion + " Server");

            }
            else
            {
                PhotonNetwork.LeaveRoom();
                Debug.Log("Leave Room and Log Out");

                OnConnected();
                Debug.Log("Aplikasi Connected ke " + PhotonNetwork.CloudRegion + " Server");

                OnConnectedToMaster();
                Debug.Log("Aplikasi Connected ke " + PhotonNetwork.CloudRegion + " Server");

            }
        }
    }
}
