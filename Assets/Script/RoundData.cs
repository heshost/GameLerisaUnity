using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

namespace lerisa
{
    [System.Serializable]
    public class RoundData
    {
        public string name = "PreTest";
        public int timeLimitInSeconds = 30;
        public int pointsAddedForCorrectAnswer = 10;
        //   public QuestionData[] questions;
        public string[] DataQuestion;

        string url = "http://heszhost.com/conven/configurasi/GetUser.php";

        public IEnumerator Get(string url)
        {
            using(UnityWebRequest www = UnityWebRequest.Get(url))
            {
                yield return www.SendWebRequest();

                if(www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);

                }
                else
                {
                    if (www.isDone)
                    {
                      string Pertanyaan = www.downloadHandler.text;
                      DataQuestion = Pertanyaan.Split(new string[] { "-" }, StringSplitOptions.RemoveEmptyEntries);

                    }
                }
            }
            
            
            
        }
    }

}
    