using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace lerisa
{
    public class CompleteMission : MonoBehaviour
    {
        public string namaPlayer;
        public string url;
        public string[] dataMisi;

        public RawImage MissionPretest;
        public RawImage MissionIKM;
        public RawImage MissionIPM;
        public RawImage MissionTFR;
        public RawImage MissionNTP;
        public RawImage MissionITK;
        public RawImage MissionPosttest;


        void Start()
        {
            namaPlayer = PhotonNetwork.LocalPlayer.NickName;

            Debug.Log(namaPlayer);

            StartCoroutine(getAllMision(namaPlayer));
        }


        public IEnumerator getAllMision(string playerName)
        {
            Debug.Log(playerName);

            string highscore_url = "http://heszhost.com/dashboard/pages/getMissionCompleted.php";
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
                    Debug.Log(result);
                }
                else
                {
                    dataMisi = null;

                    //split info reply data user login dari web
                    dataMisi = result.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    //pretest
                    var pretestComp = GetValueDataMisi(dataMisi[0], "jumlahmisi:");

                    Debug.Log(pretestComp);
                    if (Int32.Parse(pretestComp) == 0)
                    {
                        MissionPretest.gameObject.SetActive(false);
                    }
                    else
                    {
                        MissionPretest.gameObject.SetActive(true);
                        Debug.Log("Pretest Completed");
                    }

                    //postest
                    var posttestComp = GetValueDataMisi(dataMisi[1], "jumlahmisi:");

                    Debug.Log(posttestComp);
                    if (Int32.Parse(posttestComp) == 0)
                    {
                        MissionPosttest.gameObject.SetActive(false);
                    }
                    else
                    {
                        MissionPosttest.gameObject.SetActive(true);
                        Debug.Log("Posttest Completed");
                    }

                    //IKM
                    var ikmComp = GetValueDataMisi(dataMisi[2], "jumlahmisi:");

                    Debug.Log(ikmComp);
                    if (Int32.Parse(ikmComp) != 4)
                    {
                        MissionIKM.gameObject.SetActive(false);
                    }
                    else
                    {
                        MissionIKM.gameObject.SetActive(true);
                        Debug.Log("IKM Completed");
                    }

                    //NTP
                    var ntpComp = GetValueDataMisi(dataMisi[3], "jumlahmisi:");

                    Debug.Log(ntpComp);
                    if (Int32.Parse(ntpComp) != 4)
                    {
                        MissionNTP.gameObject.SetActive(false);
                    }
                    else
                    {
                        MissionNTP.gameObject.SetActive(true);
                        Debug.Log("NTP Completed");
                    }

                    //IPM
                    var ipmComp = GetValueDataMisi(dataMisi[4], "jumlahmisi:");

                    Debug.Log(ipmComp);
                    if (Int32.Parse(ipmComp) != 4)
                    {
                        Debug.Log("IPM UN-Completed");
                    }
                    else
                    {
                        MissionIPM.gameObject.SetActive(true);
                        Debug.Log("IPM Completed");
                    }

                    //TFR
                    var tfrComp = GetValueDataMisi(dataMisi[5], "jumlahmisi:");

                    Debug.Log(tfrComp);
                    if (Int32.Parse(tfrComp) != 4)
                    {
                        Debug.Log("TFR UN-Completed");
                    }
                    else
                    {
                        MissionTFR.gameObject.SetActive(true);
                        Debug.Log("TFR Completed");
                    }

                    //ITK
                    var itkComp = GetValueDataMisi(dataMisi[6], "jumlahmisi:");

                    Debug.Log(itkComp);
                    if (Int32.Parse(itkComp) != 4)
                    {
                        Debug.Log("ITK UN-Completed");
                    }
                    else
                    {
                        MissionITK.gameObject.SetActive(true);
                        Debug.Log("ITK Completed");
                    }

                }
            }
        }


        string GetValueDataMisi(string data, string index)
        {
            string value = data.Substring(data.IndexOf(index) + index.Length);
            if (value.Contains("-"))
            {
                value = value.Remove(value.IndexOf("-"));
            }
            return value;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
