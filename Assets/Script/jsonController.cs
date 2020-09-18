using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class jsonController : MonoBehaviour
{

    public string url = "http://leatonm.net//CandlepinData/jsonfile.json";
    public string url1 = "http://heszhost.com/dashboard/dataJsonOrang.json";
    public string url2 = "http://heszhost.com/dashboard/dataJson.json";
    public string url3 = "http://heszhost.com/dashboard/dataAsli.json";
    public string url4 = "http://heszhost.com/dashboard/pages/postTest.php";

    public JsonSaya punyaSaya;

    public dataJasonOrang orangPunya;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetData());
    }

    public IEnumerator GetData()
    {
        using (UnityWebRequest www = UnityWebRequest.Get(url4))
        {
            yield return www.SendWebRequest();

            if (www.isNetworkError || www.isHttpError)
            {
                Debug.Log(www.error);

            }
            else
            {
                if (www.isDone)
                {

                   string Pertanyaan = www.downloadHandler.text;

                    Debug.Log(Pertanyaan);

                    processJsonData(Pertanyaan);



                    //  soalku = Pertanyaan.Split(new string[] { "iniSoal" }, StringSplitOptions.RemoveEmptyEntries);

                }
            }
        }
    }
    private void processJsonData(string _dataJson)
    {
       punyaSaya = JsonUtility.FromJson<JsonSaya>(_dataJson);
        Debug.Log(punyaSaya.data);

     //   orangPunya = JsonUtility.FromJson<dataJasonOrang>(_dataJson);
      //  Debug.Log(orangPunya.playerName);
    }
}
