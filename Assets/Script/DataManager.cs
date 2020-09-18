using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace lerisa
{
    public class DataManager : MonoBehaviour
    {

        [System.Serializable]
        public class soal
        {
            public string TextSoal;
            public string JawabA, JawabB, JawabC, JawabD;
            public bool A, B, C, D;
        }

        public soal[] kumpulanSoal;
        public List<soal> daftarSoal;
        public string[] soalku;

        string url = "http://heszhost.com/dashboard/pages/postTest.php";
        void Start()
        {
            StartCoroutine(GetData());
        }

        public IEnumerator GetData()
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);

                }
                else
                {
                    if (www.isDone)
                    {

                        string Pertanyaan = www.downloadHandler.text;

                        soalku = Pertanyaan.Split(new string[] { "iniSoal" }, StringSplitOptions.RemoveEmptyEntries) ;

                       // daftarSoal = Pertanyaan.Split(new List<soal> { ";" }, StringSplitOptions.RemoveEmptyEntries);
                      //  daftarSoal = Pertanyaan.Split(new List<soal> { "iniSoal" }, StringSplitOptions.RemoveEmptyEntries);
                        // daftarSoal = Pertanyaan.Split(new string[] { "iniSoal" }, StringSplitOptions.RemoveEmptyEntries);

                        // jawaban MyJawaban = JsonUtility.FromJson<jawaban>(Pertanyaan);

                        // items.Add(MyJawaban);



                    }
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }


    }
}
