using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class modulManager : MonoBehaviour
    {
        Text namaIndikator;
        public string TeksNamaModul = "TeksNamaModul";
        public string namaModul;
        Text kodeIndikator;
        public string TeksKodeModul = "TeksKodeModul";
        public string kodeModul;

        public int KodeIndi;
        public MissionStatus missionStatus;

        public GameObject panelMisiSamping;

        void Start()
        {
            panelMisiSamping.SetActive(false);
            namaIndikator = GameObject.Find(TeksNamaModul).GetComponent<Text>();
           
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                panelMisiSamping.SetActive(true);
                namaIndikator.gameObject.SetActive(true);
                namaIndikator.text = namaModul;
                missionStatus.KodeIndi = KodeIndi;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                missionStatus.KodeIndi = 0;
                StartCoroutine(tutupPanelSamping());
               
            }
        }

        public IEnumerator tutupPanelSamping()
        {
            yield return new WaitForSecondsRealtime(1);

            panelMisiSamping.SetActive(false);
            namaIndikator.gameObject.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
