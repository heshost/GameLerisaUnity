using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace lerisa
{
    public class showWanted : MonoBehaviour
    {

        public string urlinfo;
        public MeshRenderer wanted;
        // Start is called before the first frame update
        void Start()
        {
            StartCoroutine(playWanted());

        }

        IEnumerator playWanted()
        {
            using (UnityWebRequest www = UnityWebRequestTexture.GetTexture(urlinfo))
            {
                yield return www.SendWebRequest();

                if (www.isNetworkError || www.isHttpError)
                {
                    Debug.Log(www.error);

                }
                else
                {
                    Texture2D myTexture = DownloadHandlerTexture.GetContent(www);
                    wanted.material.mainTexture = myTexture;

                }

            }

            yield return new WaitForSecondsRealtime(1);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
