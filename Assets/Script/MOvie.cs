using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace lerisa
{
    public class MOvie : MonoBehaviour
    {
        public UnityEngine.Video.VideoClip videoClip;
        public GameObject camera;
        public AudioSource audiovideo;
        void Start()
        {
            camera = GameObject.Find("Camera");
            Debug.Log(camera);
   
            var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();
            audiovideo = GetComponent<AudioSource>();

            videoPlayer.playOnAwake = false;
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;
            videoPlayer.targetCameraAlpha = 1F;
            videoPlayer.clip = videoClip;
        //    videoPlayer.source = UnityEngine.Video.VideoSource.Url;
          //  videoPlayer.url = "http://heszhost.com/dashboard/video/IPM.mp4";
            videoPlayer.frame = 100;
            
            //  videoPlayer.pixelAspectRatioNumerator.Equals(0.5f);
       //     videoPlayer.aspectRatio = UnityEngine.Video.VideoAspectRatio.Stretch;
            videoPlayer.isLooping = false;
            videoPlayer.waitForFirstFrame = false;

            videoPlayer.loopPointReached += EndReached;

            videoPlayer.Play();
            audiovideo.enabled = true;

        }

        void EndReached(UnityEngine.Video.VideoPlayer vp)
        {
            Debug.Log("end vid");
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
