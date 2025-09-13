using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using GameVanilla.Core;
using GameVanilla.Game.Popups;
using GameVanilla.Game.Scenes;
using System.IO;

public class QuestionDownloader : MonoBehaviour
{
    [SerializeField] TextAsset textAsset = null;
    const string FirstTime= "FirstTime";
    const string GUID = "GUID";
    string _guid;
    List<QuizQuestion> questionData;

    private static QuestionDownloader instance;
    public static QuestionDownloader _instance
    {
        get
        {
            if(instance == null)
            {
                instance = FindFirstObjectByType<QuestionDownloader>();
            }
            return instance;
        }
    }

    private void Awake()
    {
        if(instance !=this && instance != null)
        {
            Destroy(gameObject);
        }
        instance = this;
        DontDestroyOnLoad(gameObject);

        if (PlayerPrefs.GetInt(FirstTime) == 0)
        {
            string newBackstageItemID = System.Guid.NewGuid().ToString();
            PlayerPrefs.SetInt(FirstTime, 1);
            PlayerPrefs.SetString(GUID, newBackstageItemID);
            PlayerPrefs.Save();
        }
        else
        {
            _guid = PlayerPrefs.GetString(GUID);
        }

        //if(Application.internetReachability == NetworkReachability.NotReachable)
        //{
        //    LoadAvailableData();
        //}
        //else
        //{
        //    StartCoroutine(DownloadJSON(url));
       // }
    }

    public void LoadAvailableData()
    {
        var wrapper = JsonUtility.FromJson<QuizQuestionList>(textAsset.text);
        questionData = wrapper.questions;
        foreach (var obj in questionData)
        {
            print(obj.question_paragraph);
        }
    }


    public void DownloadQuestions(int level)
    {
        StartCoroutine(DownloadJSON(level));

    }


    public IEnumerator DownloadJSON(int level)
    {
        int stage = 1;
        if (level <= 20)
        {
            stage = 1;
        }
        else if (level > 20 && level <= 40)
        {
            stage = 2;
        }
        else if (level > 40 && level <= 60)
        {
            stage = 3;
        }
        else if (level > 60 && level <= 80)
        {
            stage = 4;
        }
        else if (level > 80 && level <= 100)
        {
            stage = 5;
        }
        string url = "https://www.enlightenededucators.biz/Pillpusher/wp-json/custom-api/v1/questions?level=" + stage + "&max=50";

        // Replace obsolete WWW with UnityWebRequest
        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.Log("Error downloading JSON: " + request.error);
                //    LoadAvailableData();
                yield break;
            }
            // Deserialize the JSON text into a JSON object using JsonUtility
            string jsonText = request.downloadHandler.text;
            print(jsonText);
            var wrapper = JsonUtility.FromJson<QuizQuestionList>(jsonText);
            questionData = wrapper.questions;
        }
    }

    public void PostAnswer(int answerID, int success , string questionID)
    {
        string uid = PlayerPrefs.GetString(GUID);
       // https://www.enlightenededucators.biz/Pillpusher/wp-json/custom-api/v1/answers?uid=999&quid=1&answid=2&success=1     Dummy Link
        string createLink = "https://www.enlightenededucators.biz/Pillpusher/wp-json/custom-api/v1/answers?uid=" + uid+ "&quid="+questionID+"&answid="+answerID.ToString()+"&success="+success.ToString();
        StartCoroutine(PostRequest(createLink));     
}
    public IEnumerator PostRequest(string url)
    {
            UnityWebRequest request = UnityWebRequest.Get(url);

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + request.downloadHandler.text);
            }
            else
            {
                Debug.Log("Error sending request: " + request.error);
            }
    }




public List<QuizQuestion> GetQuestionList(int map_level =0)
    {
        //if(map_level == 1)
        //return map_Questions_1;
        //if (map_level == 2)
        //    return map_Questions_2;
        //if (map_level == 3)
        //    return map_Questions_3;
        //if (map_level == 4)
        //    return map_Questions_4;
        //if (map_level == 5)
        //    return map_Questions_5;
        return questionData;
    }

}
        [System.Serializable]
    public class QuizQuestion
    {
        public string question_id { get; set; }
        public string category { get; set; }
        public string map_level { get; set; }
        public string question_type { get; set; }
        public string question_paragraph { get; set; }
        public string answer_option_1 { get; set; }
        public string answer_option_1_correct { get; set; }
        public string answer_option_2 { get; set; }
        public string answer_option_2_correct { get; set; }
        public string answer_option_3 { get; set; }
        public string answer_option_3_correct { get; set; }
        public string answer_option_4 { get; set; }
        public string answer_option_4_correct { get; set; }
        public string hint_text { get; set; }
        public string answer_response_paragraph { get; set; }
        public string modified { get; set; }
        public string user { get; set; }
        public string active { get; set; }
    }

[System.Serializable]
public class QuizQuestionList
{
    public List<QuizQuestion> questions;
}
