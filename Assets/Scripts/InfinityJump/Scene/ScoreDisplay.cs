using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase.Database;
using Firebase.Extensions;

public class ScoreDisplay : MonoBehaviour
{
    public TMP_Text scoresText; 
    private List<ScoreData> scoresList = new List<ScoreData>();

    void Start()
    {
        GetScoresFromFirebase();
    }

    void GetScoresFromFirebase()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.GetReference("scores");
        reference.GetValueAsync().ContinueWithOnMainThread(task =>
        {
            if (task.IsFaulted)
            {
            }
            else if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (var child in snapshot.Children)
                {
                    ScoreData scoreData = JsonUtility.FromJson<ScoreData>(child.GetRawJsonValue());
                    scoresList.Add(scoreData);
                }

                DisplayScores();
            }
        });
    }

    void DisplayScores()
    {
        scoresText.text = "";
        scoresList.Sort((x, y) => y.score.CompareTo(x.score)); 

        for (int i = 0; i < Mathf.Min(5, scoresList.Count); i++)
        {
            scoresText.text += $"{scoresList[i].playerName}: {scoresList[i].score}\n";
        }
    }
}

