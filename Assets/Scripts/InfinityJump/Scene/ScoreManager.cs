using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Extensions;

public class ScoreManager : MonoBehaviour
{
    DatabaseReference databaseReference;

    void Start()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => {
            FirebaseApp app = FirebaseApp.DefaultInstance;
            databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        });
    }

    public void SaveScore(string playerName, int score)
    {
        string key = databaseReference.Child("scores").Push().Key;
        ScoreData scoreData = new ScoreData(playerName, score);

        string json = JsonUtility.ToJson(scoreData);
        databaseReference.Child("scores").Child(key).SetRawJsonValueAsync(json);
    }

    public void LoadScores(System.Action<List<ScoreData>> onScoresLoaded)
    {
        databaseReference.Child("scores").GetValueAsync().ContinueWithOnMainThread(task => {
            if (task.IsFaulted)
            {
                Debug.LogError("Error al cargar los puntajes");
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                List<ScoreData> scores = new List<ScoreData>();

                foreach (var child in snapshot.Children)
                {
                    ScoreData scoreData = JsonUtility.FromJson<ScoreData>(child.GetRawJsonValue());
                    scores.Add(scoreData);
                }

                onScoresLoaded(scores);
            }
        });
    }
}

[System.Serializable]
public class ScoreData
{
    public string playerName;
    public int score;

    public ScoreData(string playerName, int score)
    {
        this.playerName = playerName;
        this.score = score;
    }
}



