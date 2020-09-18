using Photon.Pun;
using TMPro;
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;

namespace lerisa
{
    public class namePlate : MonoBehaviour
    {
        [SerializeField] private TextMeshPro NameDisplay = null;
        [SerializeField] private PhotonView PhotonView = null;
        [SerializeField] private Color Friendly = Color.yellow;
        [SerializeField] private Color Enemy = Color.white;



		private void Start()
		{
			if (PhotonView != null)
			{
				if (PhotonView.IsMine)
				{
					
					
					NameDisplay.enabled = true;
					NameDisplay.text = PhotonNetwork.NickName;
				//	NameDisplay.color = Enemy;
					Debug.Log(NameDisplay.text + " View Is Mine");
					return;
					
				}

				
			}  

			NameDisplay.color = Friendly;
			NameDisplay.text = PhotonView.Owner.NickName;

			Debug.Log(NameDisplay.text + " View Is Friend");



		}


	}
}
