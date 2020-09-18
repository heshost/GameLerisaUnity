using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inflasiVid : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject bukabingkai;
    [SerializeField] private GameObject bukaObjek;
    [SerializeField] private GameObject testMaterial;
    Text statusInGame;
   // public GameObject floatingTextPrefabs;

    public string TextStatusAktifitas = "TextStatusBawah";

    void Start()
    {
       // bukabingkai.SetActive(false);
        statusInGame = GameObject.Find(TextStatusAktifitas).GetComponent<Text>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetPhotonView().IsMine)
        {

             bukabingkai.SetActive(true);
          //    testMaterial.SetActive(true);

            statusInGame.text = PhotonNetwork.NickName + " on Video Session";
         
              StartCoroutine(streamVideo());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetPhotonView().IsMine)
        {

            statusInGame.text = PhotonNetwork.NickName + " Exit Video Session";
        }
    }

    IEnumerator streamVideo()
    {
        yield return new WaitForSecondsRealtime(3);

        bukaObjek.SetActive(true);
    }

   
    // Update is called once per frame
    void Update()
    {
        
    }
}
