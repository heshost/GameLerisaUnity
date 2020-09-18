using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class AnswerButton : MonoBehaviour
    {
        public Text teksJawaban;

        private JsonData jsondata;

        
        public JsonData.listJawaban listjawaban; // ambil array jawaban
        void Start()
        {
           jsondata = FindObjectOfType<JsonData>();
          // jsondata = GetComponent<JsonData>();
         
        }

        public void Jawaban(JsonData.listJawaban data)  // parameter yang dikirim "data"
        {
            listjawaban = data;
            teksJawaban.text = listjawaban.id_jawab;
            string id_jawab = listjawaban.id_objektif;
        }

        public void handleClick()
        {
            Debug.Log(listjawaban.id_objektif);

            jsondata.ClickAnswerButton(listjawaban.id_objektif);

            Debug.Log(listjawaban.id_objektif);
        } 
    }
}
