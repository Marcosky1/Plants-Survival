using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Database;
using System;

public class DataBaseManager : MonoBehaviour
{
    [SerializeField] private string UserID;
    [SerializeField] private StudentSO studentSO;

    private DatabaseReference reference;

    // Start is called before the first frame update
    private void Awake()
    {
        UserID = SystemInfo.deviceUniqueIdentifier;
    }

    private void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void UpdloadStudent()
    {
        Student newStudent = studentSO.GetBasicStudentData();

        string json = JsonUtility.ToJson(newStudent);

        reference.Child("Students").Child(UserID).Child(newStudent.nickName).SetRawJsonValueAsync(json);
    }

    private IEnumerable GetFirstName(Action<string> onCallBack) {
        var userNameData = reference.Child("Users").Child(UserID).Child("firstName").GetValueAsync();
        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);
        if(userNameData!=null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }

    private IEnumerable GetLastName(Action<string> onCallBack)
    {
        var userNameData = reference.Child("Users").Child(UserID).Child("lastName").GetValueAsync();
        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);
        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }

    private IEnumerable GetCodeID(Action<string> onCallBack)
    {
        var userNameData = reference.Child("Users").Child(UserID).Child(nameof(User.codeID)).GetValueAsync();
        yield return new WaitUntil(predicate: () => userNameData.IsCompleted);
        if (userNameData != null)
        {
            DataSnapshot snapshot = userNameData.Result;
                                                                                                                                                                                                                                                                                                                                                                        
            onCallBack?.Invoke(snapshot.Value.ToString());
        }
    }   
}

[System.Serializable]
public class Student
{
    public string name;
    public string nickName;
    public int age; 
    public int id;
    public string career;

    public Student(string name, string nickName, int age, int id, string career)
    {
        this.name = name;
        this.nickName = nickName;
        this.age = age;
        this.id = id;
        this.career = career;
    }
}
