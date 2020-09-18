using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class MissionStatus : MonoBehaviour
    {
        public GameObject statusQuiz;
        public GameObject misiQuizIKM;
        public GameObject misiQuizNTP;
        public GameObject misiQuizIPM;
        public GameObject misiQuizTFR;
        public GameObject misiQuizITK;

        Text namaModul;
        public string TeksNamaModul = "TeksNamaModul";
        public int KodeIndi;

        public GameObject vCheckPrefabs;
        public GameObject vCheckPrefabs1;
        public GameObject vCheckPrefabs2;
        public GameObject videoFloatingPrefabs;

        public bool buku;
        public bool video;
        public bool infografis;

        public bool buku2;
        public bool video2;
        public bool infografis2;

        public bool buku3;
        public bool video3;
        public bool infografis3;

        public bool buku4;
        public bool video4;
        public bool infografis4;

        public bool buku5;
        public bool video5;
        public bool infografis5;

        private GameObject check3;
        private GameObject check1;
        private GameObject check2;





        void Start()
        {
            statusQuiz.SetActive(false);
            vCheckPrefabs.SetActive(false);

        }

        private void Awake()
        {
            vCheckPrefabs.SetActive(false);
            vCheckPrefabs1.SetActive(false);
            vCheckPrefabs2.SetActive(false);
            statusQuiz.SetActive(false);
        }

        public IEnumerator ShowCheklisBuku(int kodeIndikator)
        {
           
                vCheckPrefabs.SetActive(true);
                
                check1 = Instantiate(vCheckPrefabs, transform.position, Quaternion.identity, transform);
                Debug.Log(check1);

                Debug.Log("ceklis buku");


                yield return new WaitForSecondsRealtime(1);

            if (KodeIndi == 81)
            {
                buku = true;
            }
            if (KodeIndi == 82)
            {
                buku2 = true;
            }
            if (KodeIndi == 83)
            {
                buku3 = true;
            }
            if (KodeIndi == 84)
            {
                buku4 = true;
            }
            if (KodeIndi == 85)
            {
                buku5 = true;
            }

        }


        public IEnumerator ShowCheklisVideo()
        {
            vCheckPrefabs1.SetActive(true);

            check2 = Instantiate(vCheckPrefabs1, transform.position, Quaternion.identity, transform);
            Debug.Log(check2);

            Debug.Log("ceklis video");


            yield return new WaitForSecondsRealtime(1);

            if (KodeIndi == 81)
            {
                video = true;
            }
            if (KodeIndi == 82)
            {
                video2 = true;
            }
            if (KodeIndi == 83)
            {
                video3 = true;
            }
            if (KodeIndi == 84)
            {
                video4 = true;
            }
            if (KodeIndi == 85)
            {
                video5 = true;
            }

        }

    public IEnumerator ShowCheklisInfoG()
    {
        vCheckPrefabs2.SetActive(true);
        check3 = Instantiate(vCheckPrefabs2, transform.position, Quaternion.identity, transform);
        Debug.Log(check3);

        Debug.Log("ceklis infografis");


        yield return new WaitForSecondsRealtime(1);
            if (KodeIndi == 81)
            {
                infografis = true;
            }
            if (KodeIndi == 82)
            {
                infografis2 = true;
            }
            if (KodeIndi == 83)
            {
                infografis3 = true;
            }
            if (KodeIndi == 84)
            {
                infografis4 = true;
            }
            if (KodeIndi == 85)
            {
                infografis5 = true;
            }

        }


        public IEnumerator ShowFloatingVideo()
        {
            int nontonVideo = 200;

            var floatVideo = Instantiate(videoFloatingPrefabs, transform.position, Quaternion.identity, transform);
            Debug.Log(floatVideo);

            floatVideo.GetComponent<Text>().text = "+" + nontonVideo.ToString();


            Debug.Log("poin nonton video");


            yield return new WaitForSecondsRealtime(1);

        }

        // Update is called once per frame
        void Update()
        {
            if(buku && video && infografis && KodeIndi == 81)
            {
                statusQuiz.SetActive(true);
                misiQuizIKM.SetActive(true);
            }

            if (buku2 && video2 && infografis2 && KodeIndi == 82)
            {
                statusQuiz.SetActive(true);
                misiQuizNTP.SetActive(true);
            }

            if (buku3 && video3 && infografis3 && KodeIndi == 83)
            {
                statusQuiz.SetActive(true);
                misiQuizIPM.SetActive(true);
            }

            if (buku4 && video4 && infografis4 && KodeIndi == 84)
            {
                statusQuiz.SetActive(true);
                misiQuizTFR.SetActive(true);
            }

            if (buku5 && video5 && infografis5 && KodeIndi == 85)
            {
                statusQuiz.SetActive(true);
                misiQuizITK.SetActive(true);
            }

            if(KodeIndi == 0)
            {
                Destroy(check1);
                Destroy(check2);
                Destroy(check3);

                statusQuiz.SetActive(false);
                //check1.SetActive(false);
                //check2.SetActive(false);
                //check3.SetActive(false);
            }

            
        
    
    }
    }
}
