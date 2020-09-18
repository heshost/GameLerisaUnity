using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class onTriggerr : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update

    public GameObject floatingTextPrefabs;
    
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetPhotonView().IsMine)
        {
            if (floatingTextPrefabs)
            {
                shownilai();
            }
            
        }
    }

    void shownilai()
    {
        Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity, transform);
        Debug.Log("tampil point tiap klik");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
