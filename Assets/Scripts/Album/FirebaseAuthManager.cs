using Firebase;
using Firebase.Auth;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Extensions;

public class FirebaseAuthManager : MonoBehaviour
{
    public TMP_InputField emailInput;
    public TMP_InputField passwordInput;
    public TextMeshProUGUI feedbackText;

    private FirebaseAuth auth;
    private FirebaseUser user;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
    }

    public void RegisterUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.CreateUserWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                AuthResult result = task.Result; 
                user = result.User; 
                Debug.Log("Usuario registrado: " + user.Email);
                feedbackText.text = "Registro exitoso!";
            }
            else
            {
                Debug.LogError("Error en el registro: " + task.Exception);
                feedbackText.text = "Error: " + task.Exception?.Message;
            }
        });
    }

    public void LoginUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        auth.SignInWithEmailAndPasswordAsync(email, password).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted && !task.IsFaulted)
            {
                AuthResult result = task.Result; 
                user = result.User; 
                Debug.Log("Usuario logeado: " + user.Email);
                feedbackText.text = "Inicio de sesión exitoso!";
            }
            else
            {
                Debug.LogError("Error al iniciar sesión: " + task.Exception);
                feedbackText.text = "Error: " + task.Exception?.Message;
            }
        });
    }

    public void SignOutUser()
    {
        auth.SignOut();
        feedbackText.text = "Sesión cerrada.";
    }
}




