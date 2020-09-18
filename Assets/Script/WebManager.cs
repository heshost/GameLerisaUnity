using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using Photon.Pun.UtilityScripts;
using TMPro;

namespace lerisa
{
    public class WebManager : MonoBehaviourPunCallbacks
    {

        public InputField masukanUsername;
        public InputField masukanPassword;
        public Button tombollogin;
        public Text pesanEror;
        private Color green = new Color(0, 64, 0, 1);
        private Color red = new Color(64, 0, 0, 1);
        private Color gold = new Color(1, 0.92f, 0.016f, 1);
        public GameObject panelawal;
        public GameObject paneltujuan;
        public Text playername;
        public Text playerlevel;
        public Text playerskor;

      
        //initial pindal panel
        public PindahPanel pindahPanel;



        // memanggil fungsi web
        public IEnumerator WebLogin(string username, string password)
        {
            WWWForm form = new WWWForm();
            form.AddField("loginUsername", username);
            form.AddField("loginPassword", password);
            string url = "http://heszhost.com/dashboard/pages/login.php";
            string url1 = "https://alghanku.000webhostapp.com/koneksi_mysql.php";

            using (UnityWebRequest www = UnityWebRequest.Post("http://heszhost.com/dashboard/pages/login.php", form))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    pesanEror.text = "Check Ur Internet Connection";
                    pesanEror.color = red;
                }
                else
                {
                    Debug.Log(www.downloadHandler.text);
                    string result = www.downloadHandler.text;

                    // show_error(result.ToString());
                    // Debug.Log(www.downloadHandler.text);
                    if (result == "Wrong Credential")
                    {
                        pesanEror.text = "Username atau Password Salah !";
                        pesanEror.color = red;
                    }
                    else
                    {
                        // aktifkan panel
                        panelawal.SetActive(false);
                        paneltujuan.SetActive(true);                   

                        //split info reply data user login dari web
                        string[] info = null;
                        info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);
                        playername.text = info[1];
                        playerlevel.text = info[2];
                        playername.color = playerlevel.color = green;
                        playerskor.text = info[0];
                        playerskor.color = gold;

                        //set photon nickname
                        if (PhotonNetwork.IsConnected)
                        {
                            base.OnConnectedToMaster();
                            PhotonNetwork.NickName = info[1];

                            int skor = Int32.Parse(info[0]);
                            PhotonNetwork.LocalPlayer.SetScore(skor);
                            


                            PhotonNetwork.AutomaticallySyncScene = true;
                            Debug.Log("Nickname Pemain adalah: " + PhotonNetwork.NickName);
                        }
                        


                        //reset form login
                        resetForm();

                    }

                }
            }
        }


       public IEnumerator postNilai(string playName, int score)
        {

            string highscore_url = "http://heszhost.com/dashboard/pages/updateData.php";
        // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playName);

            // The score
            form.AddField("score", score);

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
                // show the highscores
                Debug.Log(download.downloadHandler.text);
            }
        }


        // Start is called before the first frame update
        void Start()
        {
            pindahPanel = GetComponent<PindahPanel>();
            tombollogin.onClick.AddListener(() =>
            {
                StartCoroutine(WebLogin(masukanUsername.text, masukanPassword.text));

            });

            

        }

        private void resetForm()
        {
            masukanUsername.text = "";
            masukanPassword.text = "";
            pesanEror.text = "";

        }

     

        // Update is called once per frame
        void Update()
        {

        }
    }
}
