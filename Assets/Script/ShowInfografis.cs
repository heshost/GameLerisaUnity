using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace lerisa
{
    public class ShowInfografis : MonoBehaviour
    {
        [SerializeField] private GameObject bingkai;
        [SerializeField] private GameObject completePanel;
        //    [SerializeField] private GameObject bukaObjek;
        public RawImage infog1;

        public GameObject floatingTextPrefabs;
        public GameObject buttonDone;
        public string urlinfo;

        public WebData webdata;
        public bool isFirstTime;
        public bool isStoreData;

        public bool keepWaktu;
        public float waktutotal;
        public Text waktuDisplay;

        public bool kirimdata;

        public int jenisMisi;
        public int kodeSoal;
        public int kodeIndikator;

        public MissionStatus missionStatus;


        void Start()
        {
            Debug.Log(isFirstTime);
            Debug.Log(isStoreData);

            webdata = FindObjectOfType<WebData>();
            missionStatus = FindObjectOfType<MissionStatus>();
         

            if (isFirstTime)
            {
                StartCoroutine(completeTest());
            }
            else
            {
                StartCoroutine(playInfografis());
                updateWaktuTest();
            }


            //"http://heszhost.com/dashboard/infografis/Kemiskinan.jpeg"
        }


        IEnumerator playInfografis()
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(urlinfo))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    
                }
                else
                {
                    Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
                    infog1.texture = myTexture;
                   
                }
                
            }

                yield return new WaitForSecondsRealtime(1);
        }


        public IEnumerator completeTest()
        {

            stopUpdateWaktu();

            int waktu = Mathf.RoundToInt(waktutotal);
            //  int waktu = 90;
            string namaPlayer = PhotonNetwork.LocalPlayer.NickName;
           
           

            if (!kirimdata)
            {
                //Kirim data waktu ke Server function
                StartCoroutine(webdata.updateWaktu(namaPlayer, kodeIndikator, jenisMisi, waktu));

                kirimdata = true;

                StartCoroutine(missionStatus.ShowCheklisInfoG());
            }

            yield return new WaitForSeconds(2);
            Debug.Log("infogragis Segera ditutup");
            bingkai.SetActive(false);

         //   completePanel.SetActive(true);


        }

        public IEnumerator showfloatingtext()
        {

            Debug.Log("show floating");
            int nontonInfografis = 50;

            var go = Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity, transform);
            Debug.Log(go);
            go.GetComponent<Text>().text = "+" + nontonInfografis.ToString();
            Debug.Log("poin infografis");

            PhotonNetwork.LocalPlayer.AddScore(nontonInfografis);

            

            string namaPlayer = PhotonNetwork.LocalPlayer.NickName;
            int newPoin = PhotonNetwork.LocalPlayer.GetScore();

            //kirim nilai ke server
            StartCoroutine(webdata.updatePoin(namaPlayer, newPoin));
            Debug.Log("Data Terkirim ke Server");
            Debug.Log("Poin Terbaru: " + newPoin);

            

            isStoreData = true;
            isFirstTime = false;

            yield return new WaitForSecondsRealtime(2);

            bingkai.SetActive(false);

        }

        private void updateWaktuTest()
        {
            keepWaktu = true;
            var minutes = Mathf.Floor(waktutotal / 60);
            var seconds = waktutotal % 60;
            waktuDisplay.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        float stopUpdateWaktu()
        {
            keepWaktu = false;
            return waktutotal;
        }

        // Update is called once per frame
        void Update()
        {
            if (!isFirstTime && isStoreData)
            {
                buttonDone.GetComponent<Button>().interactable = false;
                Debug.Log(isFirstTime + " 2 " + isStoreData);
                StartCoroutine(completeTest());
            }

            if (!isFirstTime && isStoreData && kodeIndikator == 83)
            {
                Debug.Log(isFirstTime + " 2 " + isStoreData);
                StartCoroutine(completeTest());
            }

            if (isFirstTime)
            {
                Debug.Log(isFirstTime + "ini firstime doank");
                StartCoroutine(showfloatingtext());
            }

            waktutotal += Time.deltaTime;

            if (keepWaktu)                  //boolean is true
            {
                updateWaktuTest();
            }

        }
    }
}
