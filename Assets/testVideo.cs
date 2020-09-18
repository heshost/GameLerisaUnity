using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class testVideo : MonoBehaviour
{

    public GameObject camera;
    private VideoPlayer videoPlayer;
    private VideoSource videoSource;

    private AudioSource audioSource;

    public GameObject completePanel;


    public string urlvideo;
    void Start()
    {
        
        Application.runInBackground = true;
        StartCoroutine(playVideo());
        

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

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        
        Debug.Log("end Playing Video");
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
