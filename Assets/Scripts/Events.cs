using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Events : MonoBehaviour
{
    // Este método se llamará cuando se presione el botón
    public void QuitGame()
    {
        // Si estamos en el editor de Unity, detener la reproducción
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // Si estamos en una compilación, cerrar la aplicación
        Application.Quit();
#endif
    }
}
