using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using Photon.Pun;
using TMPro;

namespace lerisa
{

    public class playerNaa : MonoBehaviourPunCallbacks
    {
    public TextMeshPro nameTag;




        [PunRPC]
        public void updateName(string name)
        {
            nameTag.text = name;
            Debug.Log(nameTag.text);
        }
    }
}
