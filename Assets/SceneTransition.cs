using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne ge�i�leri i�in

public class SceneTransition : MonoBehaviour
{
    // Bu fonksiyon sahneyi y�klemek i�in kullan�l�r
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

