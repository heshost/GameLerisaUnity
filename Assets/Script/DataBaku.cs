using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


namespace lerisa
{
    [System.Serializable]
    public class DataBaku : MonoBehaviour
    {
        public string name = "PreTest";
        public int timeLimitInSeconds = 30;
        public int pointsAddedForCorrectAnswer = 10;
        public string[] DataQuestion;
        public string[] Bener;
        public jawaban[] DataJawaban;
        public List<jawaban> items; 



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

                         DataQuestion = Pertanyaan.Split(new string[] { "iniSoal" }, StringSplitOptions.RemoveEmptyEntries);

                       // jawaban MyJawaban = JsonUtility.FromJson<jawaban>(Pertanyaan);

                       // items.Add(MyJawaban);

                        
                        
                    }
                }
            }
        }
        [field:SerializeField]
        public class jawaban
        {
            public string answerText;
          //  public bool isCorrect;

            jawaban( string answer)
            {
                answerText = answer;
             //   koreksi = isCorrect;
            }
        }


    }
}
