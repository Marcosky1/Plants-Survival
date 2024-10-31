using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Storage;
using System.Threading.Tasks;
using TMPro;

public class ImageUploadManager : MonoBehaviour
{
    public RawImage previewImage;
    public TMP_Text statusText; 
    private FirebaseStorage storage;
    private Texture2D selectedImage; 

    private void Start()
    {
        storage = FirebaseStorage.DefaultInstance;
    }

    public void SelectImage()
    {
        NativeGallery.Permission permission = NativeGallery.GetImageFromGallery((path) =>
        {
            if (path != null)
            {
                Texture2D texture = NativeGallery.LoadImageAtPath(path, 512);
                if (texture != null)
                {
                    selectedImage = texture;
                    previewImage.texture = texture;
                    statusText.text = "Imagen seleccionada.";
                }
                else
                {
                    statusText.text = "No se pudo cargar la imagen.";
                }
            }
            else
            {
                statusText.text = "No se seleccionó ninguna imagen.";
            }
        });
    }

    public async void UploadImage()
    {
        if (selectedImage == null)
        {
            statusText.text = "Por favor, selecciona una imagen antes de subirla.";
            return;
        }

        byte[] imageBytes = selectedImage.EncodeToPNG();
        string fileName = "user_" + System.Guid.NewGuid().ToString() + ".png"; 
        StorageReference storageRef = storage.GetReference($"uploads/{fileName}");

        try
        {
            var uploadTask = storageRef.PutBytesAsync(imageBytes);
            await uploadTask;

            var downloadUrlTask = storageRef.GetDownloadUrlAsync();
            System.Uri downloadUrl = await downloadUrlTask;

            statusText.text = "Imagen subida exitosamente. URL: " + downloadUrl;
        }
        catch (System.Exception e)
        {
            Debug.LogError("Error al subir la imagen: " + e.Message);
            statusText.text = "Error al subir la imagen.";
        }
    }
}
