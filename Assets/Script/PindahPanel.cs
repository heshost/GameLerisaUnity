using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class PindahPanel : MonoBehaviour
    {
        public AudioSource buttonSound;
        public GameObject panelAwal;
        public GameObject panelTujuan;
        public GameObject tombolLain;

        public void GantiPanel()
        {
            buttonSound.PlayOneShot(buttonSound.clip);
            panelAwal.SetActive(false);
            panelTujuan.SetActive(true);
        }

        public void nilaiAkhirku(string totalPoin, int score)
        {
            Debug.Log(totalPoin + score + "coba");
        }

        public void showLeaderboard()
        {
            buttonSound.PlayOneShot(buttonSound.clip);
            panelTujuan.SetActive(true);
            tombolLain.GetComponent<Button>().interactable = false;
            
        }

        public void closeLeaderboard()
        {
            buttonSound.PlayOneShot(buttonSound.clip);
            panelAwal.SetActive(false);
            panelTujuan.SetActive(true);
            tombolLain.GetComponent<Button>().interactable = true;
           
        }


        public void showSatu()
        {
            buttonSound.PlayOneShot(buttonSound.clip);
            panelTujuan.SetActive(true);

        }

        public void closeSatu()
        {
            buttonSound.PlayOneShot(buttonSound.clip);
            panelAwal.SetActive(false);

        }
    }
}
