using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace lerisa
{
    public class statistik : MonoBehaviour
    {

        public string namaPlayer;
        //public Text playername;
        //public Text playerlevel;
        //public Text playerskor;

        public Text pretestSkor;
        public Text pretestTimer;

        public Text posttestSkor;
        public Text posttestTimer;

        public Text ikmSkor;
        public Text ikmTimer;

        public Text ipmSkor;
        public Text ipmTimer;

        public Text tfrSkor;
        public Text tfrTimer;

        public Text ntpSkor;
        public Text ntpTimer;

        public Text itkSkor;
        public Text itkTimer;

        public Text allSkor;
        public Text allTimer;
        public Text allfullname;
        

        private Color green = new Color(0, 64, 0, 1);
        private Color red = new Color(64, 0, 0, 1);
        private Color gold = new Color(1, 0.92f, 0.016f, 1);
        void Start()
        {
            namaPlayer = PhotonNetwork.LocalPlayer.NickName;

            Debug.Log(namaPlayer);

            StartCoroutine (getStatistikPretest(namaPlayer));
            StartCoroutine(getStatistikPosttest(namaPlayer));
            StartCoroutine(getStatistikIKM(namaPlayer));
            StartCoroutine(getStatistikIPM(namaPlayer));
            StartCoroutine(getStatistikTFR(namaPlayer));
            StartCoroutine(getStatistikNTP(namaPlayer));
            StartCoroutine(getStatistikITK(namaPlayer));
            StartCoroutine(getStatistikAll(namaPlayer));

        }


        public IEnumerator getStatistikPretest(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStats.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);



            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;

               
                if (result == "Wrong Credential")
                {
                    pretestSkor.text = "N/a";
                    pretestTimer.text = "N/a";
                    pretestSkor.color = pretestTimer.color= red;
                }
                else
                {


                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int waktu = Int32.Parse(info[2]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    pretestSkor.text = info[1];
                    pretestTimer.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);


                }
            }
        }


        public IEnumerator getStatistikPosttest(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStatsPost.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);



            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;


                if (result == "Wrong Credential")
                {
                    posttestSkor.text = "N/a";
                    posttestTimer.text = "N/a";
                    posttestSkor.color = posttestTimer.color = red;
                }
                else
                {


                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    int waktu = Int32.Parse(info[2]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    posttestSkor.text = info[1];
                    posttestTimer.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
                    


                }
            }
        }


        public IEnumerator getStatistikIKM(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStatsIKM.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);



            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;


                if (result == "Wrong Credential")
                {
                    ikmSkor.text = "N/a";
                    ikmTimer.text = "N/a";
                    ikmSkor.color = ikmTimer.color = red;
                }
                else
                {


                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int waktu = Int32.Parse(info[2]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    ikmSkor.text = info[1];
                    ikmTimer.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);


                }
            }
        }


        public IEnumerator getStatistikIPM(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStatsIPM.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);



            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;


                if (result == "Wrong Credential")
                {
                    ipmSkor.text = "N/a";
                    ipmTimer.text = "N/a";
                    ipmSkor.color = ipmTimer.color = red;
                }
                else
                {


                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int waktu = Int32.Parse(info[2]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    ipmSkor.text = info[1];
                    ipmTimer.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);


                }
            }
        }

        public IEnumerator getStatistikTFR(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStatsTFR.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;


                if (result == "Wrong Credential")
                {
                    tfrSkor.text = "N/a";
                    tfrTimer.text = "N/a";
                    tfrSkor.color = tfrTimer.color = red;
                }
                else
                {

                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int waktu = Int32.Parse(info[2]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    tfrSkor.text = info[1];
                    tfrTimer.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);


                }
            }
        }

        public IEnumerator getStatistikNTP(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStatsNTP.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;


                if (result == "Wrong Credential")
                {
                    ntpSkor.text = "N/a";
                    ntpTimer.text = "N/a";
                    ntpSkor.color = ntpTimer.color = red;
                }
                else
                {

                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                    
                    int waktu = Int32.Parse(info[2]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    ntpSkor.text = info[1];
                    ntpTimer.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds); 


                }
            }
        }

        public IEnumerator getStatistikITK(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStatsITK.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;


                if (result == "Wrong Credential")
                {
                    itkSkor.text = "N/a";
                    itkTimer.text = "N/a";
                    itkSkor.color = itkTimer.color = red;
                }
                else
                {

                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int waktu = Int32.Parse(info[2]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    itkSkor.text = info[1];
                    itkTimer.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);


                }
            }
        }

        public IEnumerator getStatistikAll(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getStatsAll.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // Create a download object
            var download = UnityWebRequest.Post(highscore_url, form);

            // Wait until the download is done
            yield return download.SendWebRequest();

            if (download.isNetworkError || download.isHttpError)
            {
                print("Error downloading: " + download.error);
            }
            else
            {
                Debug.Log(download.downloadHandler.text);
                string result = download.downloadHandler.text;


                if (result == "Wrong Credential")
                {
                    allSkor.text = "N/a";
                    allTimer.text = "N/a";
                    allSkor.color = allTimer.color = red;
                }
                else
                {

                    //split info reply data user login dari web
                    string[] info = null;
                    info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    int waktu = Int32.Parse(info[4]);
                    var hours = waktu / 3600;
                    var minutes = Mathf.Floor(waktu / 60) % 60;
                    var seconds = waktu % 60;

                    allSkor.text ="Total Poin: "+ info[3];
                    allTimer.text ="Total Waktu: "+ string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);
                    allfullname.text = info[2];
                    


                }
            }
        }


        void Update()
        {

        }
    }
}
