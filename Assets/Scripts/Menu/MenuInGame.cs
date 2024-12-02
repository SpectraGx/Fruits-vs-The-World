using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuInGame : MonoBehaviour
{
    public void Play(string name)
    {
        SceneManager.LoadScene(name);
    }
}
