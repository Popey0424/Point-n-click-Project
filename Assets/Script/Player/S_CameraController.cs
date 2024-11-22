using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; // Pour charger des sc�nes

public class S_CameraController : MonoBehaviour
{
    [Header("Settings Camera")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("SceneTarget")]
    [SerializeField] private Image imageFade; 
    [SerializeField] private List<SceneChoice> scenes; 

    [Header("Interaction")]
    [SerializeField] private bool hasInteraction;

    public S_Chapter S_Chapter;

    

   
    void Start()
    {
        
    }


    void Update()
    {
        
    }

   
    public void OnClickChoiceArrow(int choice)
    {
        if(hasInteraction == true)
        {
            Debug.Log("Necessite Item");
            if (S_Chapter.CompleteIterraction == true)
            {

                if (choice >= 0 && choice < scenes.Count)
                {
                    string nextScene = scenes[choice].NextScene;
                    Debug.Log($"Chargement de la sc�ne : {nextScene}");
                    StartCoroutine(LoadScene(nextScene));
                }
                else
                {
                    Debug.LogWarning("Choix invalide !");
                }
            }
            else
            {
                S_Chapter.StartMindDialogue(1);
            }
            
            
            
        }
        else
        {
            Debug.Log("Stop");
        }
       
    }


    private IEnumerator LoadScene(string sceneName)
    {
        if (imageFade != null)
        {
         
            imageFade.gameObject.SetActive(true);
            for (float t = 0; t < 1; t += Time.deltaTime)
            {
                Color color = imageFade.color;
                color.a = t;
                imageFade.color = color;
                yield return null;
            }
        }

 
        SceneManager.LoadScene(sceneName);
    }

    [System.Serializable]
    public class SceneChoice
    {
        public string NextScene; 
        public int sceneInteraction; 
    }
}