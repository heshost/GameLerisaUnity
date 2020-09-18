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
    public class WebData : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {

        }



        public IEnumerator postNilai(string playName, int kodeSoal, int score)
        {

            string highscore_url1 = "http://heszhost.com/dashboard/pages/updateData.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playName);

            // The kode Soal
            form.AddField("kodeSoal", kodeSoal);

            Debug.Log(kodeSoal);

            // The score
            form.AddField("score", kodeSoal);

            // Create a download object
            var download = UnityWebRequest.Post(highscore_url1, form);

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

        public IEnumerator updatePoin(string playerName, int poin)
        {

            string highscore_url = "http://heszhost.com/dashboard/pages/updatePoin.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // The score
            form.AddField("poin", poin);

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

        public IEnumerator updateWaktu(string playerName, int kodeIndikator, int jenisMisi, int waktu)
        {

            string highscore_url = "http://heszhost.com/dashboard/pages/updateWaktu.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();

            // Assuming the perl script manages high scores for different games
            form.AddField("game", "MyGameName");

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // The name of indikator
            form.AddField("kodeIndikator", kodeIndikator);

            // The name of misi
            form.AddField("jenisMisi", jenisMisi);

            // The waktu
            form.AddField("waktu", waktu);

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

        public IEnumerator updateNilai(string playerName, int kodeSoal, int kodeIndikator, int nilaiTest)
        {

            string highscore_url = "http://heszhost.com/dashboard/pages/updateNilai.php";
            // Create a form object for sending high score data to the server
            WWWForm form = new WWWForm();
             
          

            // The name of the player submitting the scores
            form.AddField("playerName", playerName);

            // The name of indikator
            form.AddField("kodeSoal", kodeSoal);

            // The name of indikator
            form.AddField("kodeIndikator", kodeIndikator);

            // The name of misi
            form.AddField("nilaiTest", nilaiTest);

          

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



        // Update is called once per frame
        void Update()
        {

        }
    }
}