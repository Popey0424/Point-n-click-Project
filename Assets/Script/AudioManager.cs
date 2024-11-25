using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public string StopScene;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        // eviter le load du son dans les cha^pitre
        
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == StopScene)
        {
            Destroy(gameObject);
        }
    }

}
