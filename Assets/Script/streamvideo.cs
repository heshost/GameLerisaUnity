using Photon.Pun;
using Photon.Pun.UtilityScripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using UnityEngine.Video;

namespace lerisa
{
    public class streamvideo : MonoBehaviour
    {
        public GameObject camera;
        [SerializeField] private GameObject bingkai;
        [SerializeField] private GameObject bukaObjek;
      //  [SerializeField] private GameObject test;


        // public VideoClip videoToPlay;

        private VideoPlayer videoPlayer;
        private VideoSource videoSource;

        private AudioSource audioSource;

        public GameObject completePanel;


        public string urlvideo;
        
        public WebData webdata;
        public bool isFirstTime;

        public bool keepWaktu;
        public float waktutotal;
        public Text waktuDisplay;

        public bool kirimdata;

        public int jenisMisi;
        public int kodeSoal;
        public int kodeIndikator;

        public MissionStatus missionStatus;

        void Start()
        {
            Debug.Log(isFirstTime);
            Application.runInBackground = true;
            StartCoroutine(playVideo());
            webdata = FindObjectOfType<WebData>();
            missionStatus = FindObjectOfType<MissionStatus>();

            updateWaktuTest();
        }

        IEnumerator playVideo()
        {
            camera = GameObject.Find("Camera");
            //Add VideoPlayer to the GameObject
            videoPlayer = camera.AddComponent<VideoPlayer>();

            //Add AudioSource
            audioSource = GetComponent<AudioSource>();

            //Disable Play on Awake for both Video and Audio
            videoPlayer.playOnAwake = false;
           

            //We want to play from video clip not from url

            //  videoPlayer.source = VideoSource.VideoClip;

            // Vide clip from Url
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = urlvideo;
                //"http://heszhost.com/dashboard/video/IKLessVoice.mp4";
            //    videoPlayer.url = "https://download.blender.org/peach/bigbuckbunny_movies/BigBuckBunny_640x360.m4v";

           



            //Set Audio Output to AudioSource
            videoPlayer.audioOutputMode = VideoAudioOutputMode.AudioSource;
            videoPlayer.controlledAudioTrackCount = 1;

            //Assign the Audio from Video to AudioSource to be played
            videoPlayer.EnableAudioTrack(0, true);
            videoPlayer.SetTargetAudioSource(0, audioSource);

            //Set video To Play then prepare Audio to prevent Buffering
            //  videoPlayer.clip = videoToPlay;
            videoPlayer.Prepare();

            //Wait until video is prepared
            WaitForSeconds waitTIme = new WaitForSeconds(1);
            while (!videoPlayer.isPrepared)
            {
                yield return waitTIme;

                break;
            }

            Debug.Log("Done Preparing Video");

            //Assign the Texture from Video to RawImage to be displayed
            //   image.texture = videoPlayer.texture;

            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            videoPlayer.targetCameraAlpha = 1F;
            videoPlayer.frame = 100;

            videoPlayer.isLooping = false;
            videoPlayer.waitForFirstFrame = false;

            //   videoPlayer.isLooping = true;

            //Play Video
            videoPlayer.Play();


            yield return new WaitForSecondsRealtime(3);
            //Play Sound
            audioSource.enabled = true;

            Debug.Log("Playing Video");
            if (!videoPlayer.isPlaying)
            {
                Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
                yield return null;
                Debug.Log("lagi ga diputar");
            }

            


            Debug.Log("Done Playing Video");

            yield return new WaitForSecondsRealtime(5);

            videoPlayer.loopPointReached += EndReached;

            videoPlayer.errorReceived += delegate (VideoPlayer videoPlayer, string message)
            {
                Debug.LogWarning("[VideoPlayer] Play Movie Error: " + message);
                Handheld.PlayFullScreenMovie(videoPlayer.url, Color.black, FullScreenMovieControlMode.CancelOnInput, FullScreenMovieScalingMode.AspectFit);
            };

        }

        

        public void ShowFloatingText()
        {
            int nontonVideo = 200;

            StartCoroutine(missionStatus.ShowFloatingVideo());
          //  var go = Instantiate(floatingTextPrefabs, transform.position, Quaternion.identity, transform);
          //  Debug.Log(go);
         //   go.GetComponent<Text>().text = "+" + nontonVideo.ToString();
            Debug.Log("tampil point tiap klik");

            PhotonNetwork.LocalPlayer.AddScore(nontonVideo);
           
            Debug.LogWarning("Video Time: " + Mathf.FloorToInt((float)videoPlayer.time));
          
            string namaPlayer = PhotonNetwork.LocalPlayer.NickName;
            int newPoin = PhotonNetwork.LocalPlayer.GetScore();

            //kirim nilai ke server
            StartCoroutine(webdata.updatePoin(namaPlayer, newPoin));
            Debug.Log("Data Terkirim ke Server");
            Debug.Log("Poin Terbaru: "+ newPoin);
        }

        void EndReached(UnityEngine.Video.VideoPlayer vp)
        {
           //s test.SetActive(true);

            ShowFloatingText();
            // vp.playbackSpeed = vp.playbackSpeed / 10.0F;
            Debug.Log("end Playing Video");
            //  vp.playOnAwake = true;
            //  vp.isLooping = true;

            //image.texture = vp.texture;
            StartCoroutine(completeTest());

            isFirstTime = true;
        }

        public IEnumerator completeTest()
        {

            stopUpdateWaktu();

            int waktu = Mathf.RoundToInt(waktutotal);
            //  int waktu = 90;
            string namaPlayer = PhotonNetwork.LocalPlayer.NickName;
            
           

            if (!kirimdata)
            {
                //Kirim data waktu ke Server function
                StartCoroutine(webdata.updateWaktu(namaPlayer, kodeIndikator, jenisMisi, waktu));

                kirimdata = true;

                StartCoroutine(missionStatus.ShowCheklisVideo());
            }


            yield return new WaitForSecondsRealtime(2);
            Debug.Log("Video Segera ditutup");
            bukaObjek.SetActive(false);
            bingkai.gameObject.SetActive(false);
           
            //completePanel.SetActive(true);

        }

        private void updateWaktuTest()
        {
            keepWaktu = true;
            var minutes = Mathf.Floor(waktutotal / 60);
            var seconds = waktutotal % 60;
            waktuDisplay.text = string.Format("{0:00} : {1:00}", minutes, seconds);
        }

        float stopUpdateWaktu()
        {
            keepWaktu = false;
            return waktutotal;
        }


        // Update is called once per frame
        void Update()
        {
            waktutotal += Time.deltaTime;

            if (keepWaktu)                  //boolean is true
            {
                updateWaktuTest();
            }

            if (isFirstTime)
            {
                Debug.Log(isFirstTime);
                StartCoroutine(completeTest());
            }

        }
    }
}
