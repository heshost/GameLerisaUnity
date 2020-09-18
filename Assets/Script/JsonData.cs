using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace lerisa
{
    [System.Serializable]
    public class JsonData : MonoBehaviour
    {

        [System.Serializable]
        public class soalCollection
        {
            public soal[] data;
        }

        [System.Serializable]
        public class soal
        {
            public string id_soal;
            public string poin;
            public listJawaban[] jawaban;
        }

        [System.Serializable]
        public class listJawaban
        {
            public string id_objektif;
            public string id_jawab;

        }

        public string url;
        public string urul;


        //initial
        public soalCollection kumpulansoal;


        public int indexSoal;
        public soal[] daftarSoal;
        public Text pertanyaanDisplay;
        public Text scoreDisplay;
        public Text waktuDisplay;
        public Transform tombolJawabanParent;
        public float waktutotal;
        public int testScore;
        public int nilaiTest;
        public int poinJawabBenar;
        public bool keepWaktu;
        public bool kelarTest;
        public TextMeshProUGUI rekapScore;
        public TextMeshProUGUI rekapWaktu;

        public GameObject startPanel;
        public GameObject pretestPanel;
        public GameObject selesaiPanel;
        public GameObject completePanel;
        public GameObject errorPanel;



        public WebData webdata;

        public byte[] data;
        public string TextData = "NoData";
        public string TextDataku;

        public int jenisMisi;
        public int kodeSoal;
        public int kodeIndikator;
        public bool kirimdata;


        public bool kelarQuiz;
        public bool serverError;


        public SimpleObjectPool poolJawaban;
        private List<GameObject> kumpulanTombolJawaban = new List<GameObject>();

        public GameObject floatingTextPrefabs;





        void Start()
        {
           
            StartCoroutine(GetPretest());
            
          
            indexSoal = 0;
            testScore = 0;
            updateWaktuTest();
            webdata = FindObjectOfType<WebData>();

        }

        public IEnumerator GetData(int kodeSoal, int kodeIndikator)
        {
            WWWForm form = new WWWForm();
            form.AddField("kodeSoal", kodeSoal);
            form.AddField("kodeIndikator", kodeIndikator);

            using (UnityWebRequest www = UnityWebRequest.Post("http://heszhost.com/dashboard/pages/preTest.php", form))
            {
                yield return www.SendWebRequest();
                

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);
                    serverError = true;
                }
                else
                {
                    if (www.isDone)
                    {

                        string Pertanyaan = www.downloadHandler.text;

                        Debug.Log(Pertanyaan);

                        Debug.Log("pretest mulai");

                        processJsonData(Pertanyaan);


                        //  soalku = Pertanyaan.Split(new string[] { "iniSoal" }, StringSplitOptions.RemoveEmptyEntries);

                    }
                }
            }
        }
        public IEnumerator GetPretest()
        {
            // buat ngecek database udah pernah test ato belum

            string url_pretest = "http://heszhost.com/dashboard/pages/cekData.php";

            string namaUser = PhotonNetwork.LocalPlayer.NickName;

            Debug.Log(namaUser);
            Debug.Log(kodeSoal);
            Debug.Log(kodeIndikator);

            WWWForm form = new WWWForm();
            form.AddField("namauser", namaUser);
            form.AddField("kodeSoal", kodeSoal);
            form.AddField("kodeIndikator", kodeIndikator);
            //    var getDataPretest = UnityWebRequest.Post(url_pretest, form);

            using (UnityWebRequest getDataPretest = UnityWebRequest.Post(url_pretest, form))
            {
                yield return getDataPretest.SendWebRequest();

                if (getDataPretest.isNetworkError || getDataPretest.isHttpError)
                {
                    Debug.Log(getDataPretest.error);
                    serverError = true;

                }
                else
                {
                    if (getDataPretest.isDone)
                    {

                        Debug.Log(getDataPretest.downloadHandler.text);
                        string result = getDataPretest.downloadHandler.text;
                        TextDataku = getDataPretest.downloadHandler.text;

                        if (result == "Nihill")
                        {
                            kelarTest = false;
                            StartCoroutine(GetData(kodeSoal,kodeIndikator));
                            Debug.Log(kelarTest + "start pretest dan quiz");
                        }
                        else if (result == "6")
                        {
                            kelarTest = false;
                            StartCoroutine(GetData(kodeSoal, kodeIndikator));
                            Debug.Log(kelarTest + "start post Test");
                        }

                        else
                        {
                            kelarTest = true;

                          //  hasilTest();
                            Debug.Log("ga tau apa");
                        }

                        

                    }

                }
            }
        }


        private void processJsonData(string _dataJson)
        {
            kumpulansoal = JsonUtility.FromJson<soalCollection>(_dataJson);


            Debug.Log(kumpulansoal);


            //retrive data Json ke array daftar soal
            daftarSoal = kumpulansoal.data;



            var hasil = TextDataku;   //cek database

            if (indexSoal == 0 && hasil == "Nihill")
            {
                showPertanyaan();
                Debug.Log("mulai muncul soal");
                Debug.Log(indexSoal);
            }
            else
            {
                Debug.Log(indexSoal);
                Debug.Log(hasil);
                Debug.Log("anda sudah selasai pretest");
                completeTest();
            }

        }

        private void showPertanyaan()
        {
            removeAnswerButton();

            soal urutanSoal = daftarSoal[indexSoal];
            pertanyaanDisplay.text = urutanSoal.id_soal;

            for (int i = 0; i < urutanSoal.jawaban.Length; i++)
            {
                GameObject tombolJawaban = poolJawaban.GetObject();
                kumpulanTombolJawaban.Add(tombolJawaban);
                // tombolJawaban.transform.SetParent(tombolJawabanParent);
                tombolJawaban.transform.SetParent(tombolJawabanParent.transform, false);

                //menampilkan tombol jawaban
                AnswerButton pilihanJawaban = tombolJawaban.GetComponent<AnswerButton>();

                pilihanJawaban.Jawaban(urutanSoal.jawaban[i]);

            }
        }


        // remove list jawaban saat looping
        public void removeAnswerButton()
        {
            //cek jumlah jawaban pool ini diset dinamis, jadi jika ada 2 jawaban tetep bisa
            while (kumpulanTombolJawaban.Count > 0)
            {
                Debug.Log(kumpulanTombolJawaban.Count);

                poolJawaban.ReturnObject(kumpulanTombolJawaban[0]);
                kumpulanTombolJawaban.RemoveAt(0);                     // remove list jawaban

            }

        }

        public void ClickAnswerButton(string id_objektif)
        {

            Debug.Log("ini pilihan dia " + id_objektif);

           


            soal urutanSoal = daftarSoal[indexSoal];
            string jawabanBenar = urutanSoal.poin;
            Debug.Log("ini jawaban benar " + jawabanBenar);

            if (id_objektif == jawabanBenar)
            {
                testScore += poinJawabBenar;
                scoreDisplay.text = "Skor: " + testScore.ToString();
                // nilaiAkhir(testScore.ToString());
            }
            // nambahin soal berikut
            if (daftarSoal.Length > indexSoal + 1)
            {

                indexSoal++;
                showPertanyaan();

            }
            else
            {
                kelarTest = false;
                hasilTest();
            }

            if (floatingTextPrefabs && id_objektif == jawabanBenar)
            {
                ShowFloatingText();
            }
        }


        public void ShowFloatingText()
        {

            var go = Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity, transform);
            go.GetComponent<Text>().text = "+" + poinJawabBenar.ToString();
            Debug.Log("tampil point tiap klik");
        }


        public void nilaiAkhir(string totalPoin, int score)
        {
            Debug.Log(totalPoin + score);
        }

        public void hasilTest()
        {
            var hasil = TextDataku;

            Debug.Log(kelarTest + "Boolean di hasil test");
            kelarTest = false;

            Debug.Log(kelarTest + "Boolean di hasil test");

            //if (kelarTest)
            //{
            //    Debug.Log("string kebaca");
            //    completeTest();
            //}


            if (!kelarTest)
            {

                Debug.Log(data);

                nilaiTest = testScore;
                string namaPlayer = PhotonNetwork.LocalPlayer.NickName;
               
                int waktu = Mathf.RoundToInt(waktutotal);
                

                // indexSoal = 0;

                stopUpdateWaktu();
                var minutes = Mathf.Floor(waktutotal / 60);
                var seconds = waktutotal % 60;
                rekapWaktu.text = "Waktu: " + string.Format("{0:00} : {1:00}", minutes, seconds);

                rekapScore.text = "Score: " + testScore.ToString();

                if (!kirimdata)
                {
                    //kirim nilai ke server
                    // StartCoroutine(webdata.postNilai(namaPlayer, kodeSoal, nilaiTest));
                    StartCoroutine(webdata.updateNilai(namaPlayer, kodeSoal, kodeIndikator, nilaiTest));
                    Debug.Log("Data nilai Terkirim ke Server" + namaPlayer + kodeSoal + nilaiTest);

                    //Kirim waktu
                    StartCoroutine(webdata.updateWaktu(namaPlayer, kodeIndikator, jenisMisi, waktu));
                    Debug.Log("Data waktu Terkirim ke Server");

                    kirimdata = true;

                }
                //Kirim poin
                

                PhotonNetwork.LocalPlayer.AddScore(nilaiTest);

                int newPoin = PhotonNetwork.LocalPlayer.GetScore();

                StartCoroutine(webdata.updatePoin(namaPlayer, newPoin));
                Debug.Log("Data poin Terkirim ke Server");

                Debug.Log(rekapScore.text + "ini rekap" + rekapWaktu.text);

                Debug.Log("Congratulation Your Pretest Session is Done");

                pretestPanel.SetActive(false);
                selesaiPanel.SetActive(true);
            }

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


        public void errorTest()
        {
            startPanel.SetActive(false);
            pretestPanel.SetActive(false);
            errorPanel.SetActive(true);
        }
        //complete panel 

        public void completeTest()
        {
            startPanel.SetActive(false);
            pretestPanel.SetActive(false);
            completePanel.SetActive(true);
        }
        //update waktu per frame
        void Update()
        {
            waktutotal += Time.deltaTime;

           

            if (daftarSoal.Length == indexSoal  && !kelarTest)     //soal abis
            {
                stopUpdateWaktu();
                var minutes = Mathf.Floor(waktutotal / 60);
                var seconds = waktutotal % 60;
                waktuDisplay.text = string.Format("{0:00} : {1:00}", minutes, seconds);

                Debug.Log("ini jalan terus");
                hasilTest();

            }

           if(kelarTest && jenisMisi == 15)
            {

                Debug.Log("ini kelar test");
                completeTest();
            }


            if(kelarQuiz && jenisMisi == 14 && kodeIndikator == 81)
            {

                Debug.Log("ini kelar quiz");
                completeTest();
            }

            if (kelarQuiz && jenisMisi == 14 && kodeIndikator == 83)
            {

                Debug.Log("ini kelar quiz");
                completeTest();
            }

            //if (TextDataku != null)
            //{
            //    if (TextDataku != "Nihill")
            //    {
            //        Debug.Log("ini text dataku");
            //        completeTest();
            //    }
            //}

            if (serverError)
            {
                Debug.Log("Server Error, Curl Gagal");
                errorTest();
            }



            if (keepWaktu)                  //boolean is true
            {
                updateWaktuTest();
            }
        }

    }
}
