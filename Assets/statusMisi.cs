using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace lerisa
{
    public class statusMisi : MonoBehaviour
    {

        public string namaPlayer;
        public string url;
        public int kodeIndikator;

        public string[] dataIKM;
        public Text TimeMissionBuku;
        public Text TimeMissionVideo;
        public Text TimeMissionInfo;
        public Text TimeMissionQuiz;

        public RawImage MissionBuku;
        public RawImage MissionVideo;
        public RawImage MissionInfo;
        public RawImage MissionQuiz;

        private Color red = new Color(64, 0, 0, 1);

        void Start()
        {
            namaPlayer = PhotonNetwork.LocalPlayer.NickName;

            Debug.Log(namaPlayer);

            StartCoroutine(getStatusIKM(namaPlayer,kodeIndikator));
        }

        public IEnumerator getStatusIKM(string playerName, int kodeIndikator)
        {
            Debug.Log(playerName);
            Debug.Log(kodeIndikator);

            string highscore_url = url;

            Debug.Log(highscore_url);
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // The name of the player submitting the scores
            form.AddField("kodeIndikator", kodeIndikator);




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
                    TimeMissionBuku.text = "N/a";
                    TimeMissionBuku.color = red;
                }
                else
                {

                    dataIKM = null;

                    dataIKM = result.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    var waktubuku = GetValueDataJson(dataIKM[0], "waktu:");
                    
                    Debug.Log(waktubuku);
                    if (waktubuku.Length == 0)
                    {
                        TimeMissionBuku.text = "N/a";
                        TimeMissionBuku.color = red;
                    }
                    else
                    {
                        TimeMissionBuku.text = GetValueTimer(waktubuku);
                        MissionBuku.gameObject.SetActive(true);
                    }

                    var waktuvideo = GetValueDataJson(dataIKM[1], "waktu:");

                    Debug.Log(waktuvideo);
                    if (waktuvideo.Length == 0)
                    {
                        TimeMissionVideo.text = "N/a";
                        TimeMissionVideo.color = red;
                    }
                    else
                    {
                        TimeMissionVideo.text = GetValueTimer(waktuvideo);
                        MissionVideo.gameObject.SetActive(true);
                    }
                    

                    var waktuinfo = GetValueDataJson(dataIKM[2], "waktu:");

                    Debug.Log(waktuinfo);
                    if (waktuinfo.Length == 0)
                    {
                        TimeMissionInfo.text = "N/a";
                        TimeMissionInfo.color = red;
                    }
                    else
                    {
                        TimeMissionInfo.text = GetValueTimer(waktuinfo);
                        MissionInfo.gameObject.SetActive(true);
                    }

                    

                    var waktuquiz = GetValueDataJson(dataIKM[3], "waktu:");

                    Debug.Log(waktuquiz);
                    if (waktuquiz.Length == 0)
                    {
                        TimeMissionQuiz.text = "N/a";
                        TimeMissionQuiz.color = red;
                    }
                    else
                    {
                        TimeMissionQuiz.text = GetValueTimer(waktuquiz);
                        MissionQuiz.gameObject.SetActive(true);
                    }
                    //split info reply data user login dari web
                    //   string[] info = null;
                    //   info = result.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);




                    //TimeMission.text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);


                }
            }
        }

        string GetValueDataJson(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);
            if (value.Contains("-"))
            {
                value = value.Remove(value.IndexOf("-"));
            }
            return value;
        }

        string GetValueTimer(string kirimWaktu)
        {
            int waktu = Int32.Parse(kirimWaktu);
            var hours = waktu / 3600;
            var minutes = Mathf.Floor(waktu / 60) % 60;
            var seconds = waktu % 60;

            string value = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);

            return value;
        }


        // Update is called once per frame
        void Update()
        {

        }
    }
}
