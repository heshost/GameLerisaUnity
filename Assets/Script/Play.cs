using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace lerisa
{
    public class Play : MonoBehaviourPunCallbacks
    {

        // untuk input control

        [HideInInspector]
        public InputStr Input;                  //input controller, mobile, enemy, network dll
        public struct InputStr
        {
            public float LookX;                   //look direction
            public float LookZ;
            public float RunX;                    //run direction
            public float RunZ;
            public bool Jump;                     //jump
        }

        public const float Speed = 3f;           //makin besar makin cepat speednya
        public const float JumpForce = 3f;

        [HideInInspector]
        public PlayerState State = PlayerState.NORMAL;

        protected Rigidbody Rigidbody;              // untuk rigidbody
        protected Quaternion LookRotation;          // untuk rotasi
        protected Collider MainCollider;
        protected Animator CharacterAnimator;       //untuk animator
        protected GameObject CharacterRagdoll;
        public AudioSource lariStep;
        public TextMeshPro namapemain;
        public AudioClip jalanStep;
        public AudioClip jumpStep;
        public AudioClip readBook;
       

       
        
    



        protected bool Grounded = true;

        private void Awake()
        {
            Rigidbody = GetComponent<Rigidbody>();
            CharacterAnimator = GetComponentInChildren<Animator>();
            //   CharacterRagdoll = transform.Find("CharacterRagdoll").gameObject;
            MainCollider = GetComponent<Collider>();
            lariStep = GetComponent<AudioSource>();
            


        }

        // Update is called once per frame
        private void Update()
        {
            CharacterAnimator.SetBool("Grounded", Grounded);

            var localVelocity = Quaternion.Inverse(transform.rotation) * (Rigidbody.velocity / Speed);  // set velocity dari local velocity
            CharacterAnimator.SetFloat("RunX", localVelocity.x);
            CharacterAnimator.SetFloat("RunZ", localVelocity.z);
        }


        void FixedUpdate()
        {
            if (Rigidbody == null)
                return;

            switch (State)
            {
                case PlayerState.NORMAL:
                    //set nickname 

                    //  namapemain.text = PhotonNetwork.NickName;

                    // posisi
                    var inputRun = Vector3.ClampMagnitude(new Vector3(Input.RunX, 0, Input.RunZ), 1);
                    var inputLook = Vector3.ClampMagnitude(new Vector3(Input.LookX, 0, Input.LookZ), 1);

                    Rigidbody.velocity = new Vector3(inputRun.x * Speed, Rigidbody.velocity.y, inputRun.z * Speed);

                    if (inputRun.magnitude > 0.01f)
                    {
                        lariStep.enabled = true;
                        lariStep.loop = true;
                    }
                    if (inputRun.magnitude < 0.01f)
                    {
                        lariStep.enabled = false;
                        lariStep.loop = false;
                    }

                    //rotation to go target
                    if (inputLook.magnitude > 0.01f)
                        LookRotation = Quaternion.AngleAxis(Vector3.SignedAngle(Vector3.forward, inputLook, Vector3.up), Vector3.up);



                    transform.rotation = LookRotation;
                    

                    //untuk jump
                    Grounded = Physics.OverlapSphere(transform.position, 0.3f, 1).Length > 1;

                    if (Input.Jump)
                    {
                        if (Grounded)
                        {
                            Rigidbody.velocity = new Vector3(Rigidbody.velocity.x, JumpForce, Rigidbody.velocity.z);
                            lariStep.enabled = true;
                            lariStep.PlayOneShot(jumpStep);

                           
                        }

                    }
                    if (!photonView.IsMine)
                    {
                        lariStep.volume = 0;
                    }

                    
                  

                    break;

            }

            Rigidbody.useGravity = State == PlayerState.NORMAL;
            MainCollider.enabled = State == PlayerState.NORMAL;

        }

        private void LateUpdate()
        {
            CharacterAnimator.transform.localPosition = new Vector3(0, 0, 0);
            CharacterAnimator.transform.localRotation = Quaternion.identity;
        }


        public enum PlayerState
        {
            NORMAL,
            TRANSITION
        }


        //play footsteps
        

        // Start is called before the first frame update
        void Start()
        {
                   }

        public static void RefreshInstance(ref Play player, Play Prefab)
        {
            var position = new Vector3 (Random.Range(5,-5) ,0, Random.Range(-3,3));
            var rotation = Quaternion.identity;
            if (player != null)
            {
                position = player.transform.position;
                rotation = player.transform.rotation;
                PhotonNetwork.Destroy(player.gameObject);
            }

            player = PhotonNetwork.Instantiate(Prefab.gameObject.name, position, rotation).GetComponent<Play>();




          //  player.namapemain.text = PhotonNetwork.NickName;   



        Debug.Log(PhotonNetwork.NickName + " Berhasil Spawn");
        }

       
       



    }
}
