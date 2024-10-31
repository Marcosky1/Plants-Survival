using UnityEngine;
using Firebase.Auth;
using TMPro;
using UnityEngine.SceneManagement;

public class AuthManager : MonoBehaviour
{
    public TMP_InputField emailInput; 
    public TMP_InputField passwordInput;
    public FirebaseAuth auth;

    private void Start()
    {
        auth = FirebaseAuth.DefaultInstance; 
    }

    public async void RegisterUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        try
        {
            var createUserTask = auth.CreateUserWithEmailAndPasswordAsync(email, password);
            var authResult = await createUserTask; 
            FirebaseUser newUser = authResult.User; 

            Debug.Log("Usuario registrado: " + newUser.Email);

            LoadUploadScene();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al registrar usuario: " + e.Message);
        }
    }

    public async void LoginUser()
    {
        string email = emailInput.text;
        string password = passwordInput.text;

        try
        {
            var signInTask = auth.SignInWithEmailAndPasswordAsync(email, password);
            var authResult = await signInTask;
            FirebaseUser user = authResult.User; 

            Debug.Log("Usuario autenticado: " + user.Email);
            LoadUploadScene();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al iniciar sesión: " + e.Message);
        }
    }

    private void LoadUploadScene()
    {
        SceneManager.LoadScene("Album"); 
    }
}





