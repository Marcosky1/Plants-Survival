using Firebase.Storage;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Extensions;


public class FirebaseStorageManager : MonoBehaviour
{
    public RawImage displayImage;
    public TextMeshProUGUI feedbackText;

    private FirebaseStorage storage;
    private StorageReference storageRef;
    private Firebase.Auth.FirebaseAuth auth;
    private Firebase.Auth.FirebaseUser user;

    void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
        storageRef = storage.RootReference;
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;
        user = auth.CurrentUser;
    }

    public void SelectAndUploadImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
                if (texture != null)
                {
                    byte[] imageBytes = texture.EncodeToPNG();
                    string fileName = "miImagen.png"; 
                    UploadImage(imageBytes, fileName);
                }
            }
        });
    }

    public void UploadImage(byte[] imageBytes, string fileName)
    {
        if (user == null)
        {
            feedbackText.text = "Inicia sesión para subir imágenes.";
            return;
        }

        var userFolder = storageRef.Child(user.UserId);
        var imageRef = userFolder.Child(fileName);

        imageRef.PutBytesAsync(imageBytes).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                feedbackText.text = "Imagen subida con éxito.";
            }
            else
            {
                feedbackText.text = "Error al subir imagen: " + task.Exception?.Message;
            }
        });
    }

    public void DownloadImage(string fileName)
    {
        if (user == null)
        {
            feedbackText.text = "Inicia sesión para descargar imágenes.";
            return;
        }

        var userFolder = storageRef.Child(user.UserId);
        var imageRef = userFolder.Child(fileName);

        imageRef.GetBytesAsync(1024 * 1024).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(task.Result);
                displayImage.texture = texture;
                feedbackText.text = "Imagen descargada.";
            }
            else
            {
                feedbackText.text = "Error al descargar imagen: " + task.Exception?.Message;
            }
        });
    }
}

