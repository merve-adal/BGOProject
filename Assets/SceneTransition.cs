using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // Sahne geçiþleri için

public class SceneTransition : MonoBehaviour
{
    // Bu fonksiyon sahneyi yüklemek için kullanýlýr
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}

