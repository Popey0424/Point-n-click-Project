using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class S_CameraController : MonoBehaviour
{
    [Header("Settings Camera")]
    [SerializeField] private float moveSpeed = 10f;

    [Header("SceneTarget")]
    [SerializeField] private Image imageFadeIn; 
    [SerializeField] private Image imageFadeOut; 
    [SerializeField] private List<SceneChoice> scenes; 

    [Header("Interaction")]
    [SerializeField] private bool hasInteraction;

    [Header("Audio Switch")]
    [SerializeField] private AudioSource transitionAudio;


    public S_Chapter S_Chapter;

    

   
    void Start()
    {
        imageFadeIn.gameObject.SetActive(false);
        imageFadeOut.gameObject.SetActive(true);
        StartFadeOut();
    }


    void Update()
    {
        
    }


    public void OnClickChoiceArrow(int choice)
    {
        if (hasInteraction == true)
        {
  
            if (S_Chapter.CompleteIterraction == true)
            {
              
                if (choice >= 0 && choice < scenes.Count && scenes[choice].IsPossible == true)
                {
                    transitionAudio.Play();
                    string nextScene = scenes[choice].NextScene;
                    Debug.Log($"Chargement de la scène : {nextScene}");
                    StartCoroutine(LoadScene(nextScene));
                    
                }
                else
                {
                    Debug.Log("Manque Scene");
                }
            }
            else
            {
                S_Chapter.StartMindDialogue(scenes[choice].IdDialogueMind);
            }
        }
        else if (scenes[choice].IsPossible == false)
        {

            S_Chapter.StartMindDialogue(scenes[choice].IdDialogueMind);
        }
        else
        {

            if (choice >= 0 && choice < scenes.Count && choice == scenes[choice].SceneInteraction)
            {
  
                string nextScene = scenes[choice].NextScene;

                StartCoroutine(LoadScene(nextScene));
            }
            else
            {
                Debug.Log("Manque Scene");
            }
        }
        
    }


    private IEnumerator LoadScene(string sceneName)
    {
        
        StartFadeIn();
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene(sceneName);


    }

    [System.Serializable]
    public class SceneChoice
    {
        public string NextScene; 
        public int SceneInteraction;
        public bool IsPossible;
        public int IdDialogueMind;
    }

    public void StartFadeIn()
    {
        imageFadeIn.gameObject.SetActive (true);
        imageFadeIn.DOFade(1, 2.9f);
    }

    public void StartFadeOut()
    {
        imageFadeOut.gameObject.SetActive (true);
        imageFadeOut.DOFade(0, 1.5f).OnComplete(ResetFade);
    }
    public void ResetFade()
    {
        imageFadeOut.gameObject.SetActive(false);
    }

    
}