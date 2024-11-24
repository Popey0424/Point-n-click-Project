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
            Debug.Log("Nécessite un item");
            if (S_Chapter.CompleteIterraction == true)
            {
                //hasInteraction = false;
                Debug.Log("dezde");
                if (choice >= 0 && choice < scenes.Count && scenes[choice].IsPossible == true)
                {
                    string nextScene = scenes[choice].NextScene;
                    Debug.Log($"Chargement de la scène : {nextScene}");
                    StartCoroutine(LoadScene(nextScene));
                    
                }
                else
                {
                    Debug.Log("Choix invalide !");
                }
            }
            else
            {
                S_Chapter.StartMindDialogue(scenes[choice].idDialogueMind);
            }
        }
        else if (scenes[choice].IsPossible == false)
        {
            Debug.Log("Impossible d'aller par la");
            S_Chapter.StartMindDialogue(scenes[choice].idDialogueMind);
        }
        else
        {
            Debug.Log("SwitchScene");
            if (choice >= 0 && choice < scenes.Count && choice == scenes[choice].sceneInteraction)
            {
                Debug.Log("Trouvé !");
                string nextScene = scenes[choice].NextScene;
                Debug.Log($"Chargement de la scène : {nextScene}");
                StartCoroutine(LoadScene(nextScene));
            }
            else
            {
                Debug.LogWarning("Choix invalide !");
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
        public int sceneInteraction;
        public bool IsPossible;
        public int idDialogueMind;
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