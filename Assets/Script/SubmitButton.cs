using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Pun.UtilityScripts;

namespace lerisa
{

    public class SubmitButton : MonoBehaviourPunCallbacks
    {
        [HideInInspector]
        public Play localPlayer;

        [SerializeField] private PhotonView pv = null;
        [SerializeField] private GameObject panelSelesai;
        public WebData webdata;
        public JsonData jsondata;
        public ShowInfografis infografis;
        public Book buku;

        public string TextStatusAktifitas = "PanelSoalPreTest";

        // Start is called before the first frame update
        void Start()
        {
        //    pv = this.GetComponent<PhotonView>();
            webdata = FindObjectOfType<WebData>();
        //    infografis = FindObjectOfType<ShowInfografis>();
         //   jsondata = FindObjectOfType<JsonData>();
          //  buku = FindObjectOfType<Book>();


        }

        public void handleSubmit()
        {
            
            jsondata.kelarTest = true;
            
            panelSelesai.SetActive(false);
            Debug.Log(jsondata.kelarTest);
            Debug.Log(jsondata.rekapWaktu.text);
            Debug.Log(jsondata.nilaiTest);
        }


        public void handleSubmitQuiz()
        {

            jsondata.kelarQuiz = true;

            Debug.Log(jsondata.kelarQuiz);

            Debug.Log("kelar quiz");

            panelSelesai.SetActive(false);
           
        }

        public void HandleSubmitInfografis()
        {
            infografis.isFirstTime = true;
         //   panelSelesai.SetActive(false);
        }

        public void HandleSubmitBuku()
        {
            buku.isFirstTime = true;
             panelSelesai.SetActive(false);
        }

        public void handleSubmitVideoIPM()
        {
            jsondata.kelarTest = true;

            panelSelesai.SetActive(false);
            Debug.Log(jsondata.kelarTest);
            Debug.Log(jsondata.rekapWaktu.text);
            Debug.Log(jsondata.nilaiTest);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
