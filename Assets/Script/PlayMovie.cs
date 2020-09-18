using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace lerisa
{
    public class PlayMovie : MonoBehaviour
    {
        //public UnityEngine.Video.VideoClip videoClip;
        private UnityEngine.Video.VideoSource videoSource;

        private AudioSource audioSource;

        void Start()
        {
            var videoPlayer = gameObject.AddComponent<UnityEngine.Video.VideoPlayer>();
            var audioSource =  GetComponent<AudioSource>();

            videoPlayer.playOnAwake = false;
            videoPlayer.isLooping = false;
            videoPlayer.waitForFirstFrame = false;
          //  videoPlayer.clip = videoClip;
            videoPlayer.source = VideoSource.Url;
            videoPlayer.url = "http://heszhost.com/dashboard/video/IKLessVoice.mp4";
            videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.MaterialOverride;
            videoPlayer.targetMaterialRenderer = GetComponent<Renderer>();
            videoPlayer.targetMaterialProperty = "_MainTex";
            videoPlayer.audioOutputMode = UnityEngine.Video.VideoAudioOutputMode.AudioSource;
            videoPlayer.SetTargetAudioSource(0, audioSource);

            videoPlayer.Play();

           
            //Play Sound
            audioSource.enabled = true;

        }


    }
}