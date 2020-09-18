using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;

namespace lerisa
{
    public class liveScore : MonoBehaviour
    {
        [SerializeField] private Text m_label = null;
        public Play Player => m_player;
        public int Score => PhotonNetwork.LocalPlayer.GetScore();

        private Play m_player;

        private Transform contentLeaderboardPool;   //pool dari content
        private Transform contentLeaderboard;       //content dari leaderboard yang nanti di instantiate tiap array

        private List<Transform> ListScoreTransformList;

        public int playerCount;

        void Start()
        {
            contentLeaderboardPool = transform.Find("contentPool");
            contentLeaderboard = contentLeaderboardPool.Find("listContent");

            //set false dulu contentnya

            contentLeaderboard.gameObject.SetActive(false);

            getLiveScore();
        }


        private void getLiveScore()
        {
            playerCount = PhotonNetwork.PlayerList.Length;
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}