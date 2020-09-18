using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace lerisa
{
    public class PlayerNetwork : MonoBehaviourPun, IPunObservable
    {

        protected Play Play;
        protected Vector3 remotePlayerPosition;              //sync posisi
        protected Quaternion remotePlayerRotation;
        protected Vector3 remoterPlayerVelocity;
        protected float remoteLookX;                         //sync direction
        protected float remoteLookZ;                         //sync direction
        protected float LookXVel;                            //sync speed direction    
        protected float LookZVel;
        protected Text namaClient;
        protected Rigidbody Rigidbody;


        // Start is called before the first frame update
        void Awake()
        {
            Play = GetComponent<Play>();
            Rigidbody = GetComponent<Rigidbody>();

            if (!photonView.IsMine && GetComponent<Control>() != null)
            {
                Destroy(GetComponent<Control>());
                Play.lariStep.enabled = false;
               
            }
            
        }

        // Update is called once per frame
        void Update()
        {
            // cek photon view isMine
            if (photonView.IsMine)
               return;

         


            //hitung lag distance
            var lagDistance = remotePlayerPosition - transform.position;

                                                    //jika lag jauh (>5f) terlalu jauh dari real posisi
            if(lagDistance.magnitude > 5f)
            {
                transform.position = remotePlayerPosition;
                lagDistance = Vector3.zero;
             
            }

            //jump 
            lagDistance.y = 0;
                                                     //jika low (deket banget) do nothing
            if(lagDistance.magnitude < 0.11f)
            {
                Play.Input.RunX = 0;
                Play.Input.RunZ = 0;
            }
                                                     // selain itu player tranform ke point remote biar ga lag
            else
            {
                Play.Input.RunX = lagDistance.normalized.x;
                Play.Input.RunZ = lagDistance.normalized.z;

            }

            Play.Input.Jump = remotePlayerPosition.y - transform.position.y > 0.2f;


            //smooth direction
            Play.Input.LookX = Mathf.SmoothDamp(Play.Input.LookX, remoteLookX, ref LookXVel, 0.2f);
            Play.Input.LookZ = Mathf.SmoothDamp(Play.Input.LookZ, remoteLookZ, ref LookZVel, 0.2f);

            

        }

        // biar animasi jalan di client lain dan synkron
        public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
        {
            if (stream.IsWriting)
            {
                stream.SendNext(transform.position);
                stream.SendNext(Play.Input.LookX);
                stream.SendNext(Play.Input.LookZ);
                stream.SendNext(Play.State);
                
              

            }
            else
            {
                remotePlayerPosition = (Vector3)stream.ReceiveNext();
                remoteLookX = (float)stream.ReceiveNext();
                remoteLookZ = (float)stream.ReceiveNext();

                float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.timestamp));
                remotePlayerPosition += Rigidbody.velocity * lag;

                var state = (Play.PlayerState)stream.ReceiveNext();
                Play.State = state;
            }
        }

        public void FixedUpdate()
        {
            if (!photonView.IsMine)
            {
                Rigidbody.position = Vector3.Lerp(Rigidbody.position, remotePlayerPosition, Time.fixedDeltaTime * 5);

            }
        }
    }
}
