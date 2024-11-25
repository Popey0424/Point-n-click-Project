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
    
        
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == StopScene)
        {
            Destroy(gameObject);
        }
    }

}
