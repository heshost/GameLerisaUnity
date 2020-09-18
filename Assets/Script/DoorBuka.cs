using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class DoorBuka : MonoBehaviour
    {

        Animator BukaPintu;
        AudioSource audioPintu;

        

        // Start is called before the first frame update
        void Start()
        {
            BukaPintu = GetComponent<Animator>();
            audioPintu = GetComponent<AudioSource>();

        }

        // Update is called once per frame
        void Update()
        {

        }

        //buka pintu
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {          
                BukaPintu.SetTrigger("BukaPintu");
                audioPintu.PlayOneShot(audioPintu.clip);
                
            }
        }

        //tutup

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.GetPhotonView().IsMine)
            {
                BukaPintu.enabled = true;

            }
        }

        //pas lewat
        void PauseAninamationEvent()
        {
            BukaPintu.enabled = false;
        }


    }
}
