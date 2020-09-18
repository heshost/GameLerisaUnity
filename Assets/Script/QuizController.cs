using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


namespace lerisa
{
    public class QuizController : MonoBehaviour
    {
        // initial variabel
        public SimpleObjectPool poolJawaban;
        public Text displayTeksPertanyaan;
        public Transform jawabanParent;
        public JsonData jsonData;

        public JsonData.soalCollection koleksiSoal;
        public JsonData.soal[] daftarSoal;
        public float waktuberjalan;
        public int indexSoal;

        //variabel exten
        private List<GameObject> answerButtonGameObject = new List<GameObject>();


        void Start()
        {
            jsonData = GetComponent<JsonData>();
            
            daftarSoal = koleksiSoal.data;
            Debug.Log(daftarSoal);
            indexSoal = 0;                      // array elemen 0

            showPertanyaan();
        }


        // tampil pertanyaan
        private void showPertanyaan()
        {
            //remove list jawaban jika ada
            removeAnswerButton();

            JsonData.soal urutanSoal = daftarSoal[indexSoal];
            Debug.Log(urutanSoal);

            displayTeksPertanyaan.text = urutanSoal.id_soal;

            //tampilan seluruh soal

            for(int i = 0; i < urutanSoal.jawaban.Length; i++)
            {
                GameObject answerButtonGameObject = poolJawaban.GetObject();        //menampilkan masing2 jawaban dari pool jawaban
                                                                               //  answerButtonGameObject.Add(answerButtonGameObject);
                answerButtonGameObject.transform.SetParent(jawabanParent);

                AnswerButton tombolJawaban = answerButtonGameObject.GetComponent<AnswerButton>();
                tombolJawaban.Jawaban(urutanSoal.jawaban[i]);                                       // tampilan list jawaban
            }
        }

        // remove list jawaban saat looping
        private void removeAnswerButton()
        {
            //cek jumlah jawaban pool ini diset dinamis, jadi jika ada 2 jawaban tetep bisa
            while(answerButtonGameObject.Count > 0)  {
                    
                poolJawaban.ReturnObject(answerButtonGameObject[0]);
                answerButtonGameObject.RemoveAt(0);                     // remove list jawaban

            }

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}