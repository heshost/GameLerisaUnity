using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;


namespace lerisa
{

    public class leaderboardManager : MonoBehaviour
    {
        [System.Serializable]
        public class skorCollection
        {
            public ListScore[] data;
        }

        [System.Serializable]
        public class ListScore
        {
            public string nama_user;
            public int poin;
            public int waktu;

        }

        public skorCollection tabelScore;
        


        private Transform contentLeaderboardPool;   //pool dari content
        private Transform contentLeaderboard;       //content dari leaderboard yang nanti di instantiate tiap array

        private List<Transform> ListScoreTransformList;

        public float tinggiContent;

        public string url;

        void Start()
        {
            contentLeaderboardPool = transform.Find("contentLeaderboardPool");
            contentLeaderboard = contentLeaderboardPool.Find("contentLeaderboard");

            //set false dulu contentnya

            contentLeaderboard.gameObject.SetActive(false);

            // float tinggiContent = 30f;
            StartCoroutine(GetData());


        }


        //narik data json
        public IEnumerator GetData()
        {
            WWWForm form = new WWWForm();
            form.AddField("game", "MyGameName");
            form.AddField("namauser", "namaUser");
          

            using (UnityWebRequest www = UnityWebRequest.Post(url, form))
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

                        Debug.Log("pretest mulai");

                        processJsonData(Pertanyaan);


                        //  soalku = Pertanyaan.Split(new string[] { "iniSoal" }, StringSplitOptions.RemoveEmptyEntries);

                    }
                }
            }
        }

        private void processJsonData(string _dataJson)
        {
            tabelScore = JsonUtility.FromJson<skorCollection>(_dataJson);

            Debug.Log(tabelScore);

            //kalo ikut format JsonData, dibikin variable array penampung tabelScore.data
            // public ListScore[] daftarUser --> initial
            // daftarUser = tabelScore.data;

            //sort by score 

            for (int i = 0; i < tabelScore.data.Length; i++)
            {
                for (int j = i + 1; j < tabelScore.data.Length; j++)
                {
                    if (tabelScore.data[j].poin > tabelScore.data[i].poin)
                    {
                        //swap posisi
                        ListScore tmp = tabelScore.data[i];
                        tabelScore.data[i] = tabelScore.data[j];
                        tabelScore.data[j] = tmp;

                        //Debug.log(tabelscore.data[j].poin + "j ini i" + tabelscore.data[i].poin);
                    }
                    Debug.Log(tabelScore.data[j].poin + "j ini i" + tabelScore.data[i].poin);

                }
                //    debug.log(" ini i " + tabelscore.data[i].poin);
                }

                ListScoreTransformList = new List<Transform>();

            foreach ( ListScore listScore in tabelScore.data) {

                createTableLeaderboard(listScore, contentLeaderboardPool, ListScoreTransformList);
                Debug.Log(listScore.poin);

            }
            
        }



        public void createTableLeaderboard(ListScore listScore, Transform poolContent, List<Transform> transformList)
        {
            Transform dataContent = Instantiate(contentLeaderboard, poolContent);
            RectTransform dataContentReact = dataContent.GetComponent<RectTransform>();
            dataContentReact.anchoredPosition = new Vector2(0, -tinggiContent * transformList.Count);
            dataContent.gameObject.SetActive(true);

            // isi kontent

            int rank = transformList.Count + 1;  // ini ntar ganti index user
            string rankString;
            switch (rank)
            {
                default: rankString = rank + "th"; break;
                case 1: rankString = "1st"; break;
                case 2: rankString = "2nd"; break;
                case 3: rankString = "3rd"; break;
            }

            if(rank == 1)
            {
                // warna paling atas
                dataContent.Find("TextRank").GetComponent<Text>().color = Color.green;
                dataContent.Find("TextName").GetComponent<Text>().color = Color.green;
                dataContent.Find("TextScore").GetComponent<Text>().color = Color.green;
                dataContent.Find("TextTime").GetComponent<Text>().color = Color.green;
            }

            
            switch (rank)
            {
                //warna tropy
                default:
                    dataContent.Find("Trophy").gameObject.SetActive(false);
                    break;
                case 1:
                    dataContent.Find("Trophy").GetComponent<RawImage>().color = Color.yellow;
                    break;
                case 2:
                    dataContent.Find("Trophy").GetComponent<RawImage>().color = Color.magenta;
                    break;
                case 3:
                    dataContent.Find("Trophy").GetComponent<RawImage>().color = Color.red;
                    break;
            }

            var hours = listScore.waktu / 3600;
            var minutes = Mathf.Floor(listScore.waktu / 60) % 60;
            var seconds = listScore.waktu % 60;
          //  rekapWaktu.text = "Waktu: " + string.Format("{0:00} : {1:00}", minutes, seconds);

            dataContent.Find("TextRank").GetComponent<Text>().text = rank.ToString();
            // dataContent.Find("TextRank").GetComponent<Text>().text = rankString;

            dataContent.Find("TextName").GetComponent<Text>().text = listScore.nama_user;
            dataContent.Find("TextScore").GetComponent<Text>().text = listScore.poin.ToString();
            dataContent.Find("TextTime").GetComponent<Text>().text = string.Format("{0:00} : {1:00} : {2:00}", hours, minutes, seconds);

            transformList.Add(contentLeaderboard);
            Debug.Log(dataContent);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }

}