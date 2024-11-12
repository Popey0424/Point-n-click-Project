using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class ChapterScene : MonoBehaviour
{
    [Header("Chapter Settings")]
    [SerializeField] private Image imageFade;
    [SerializeField] private string goToScene;

    private void Start()
    {
        imageFade.DOFade(0, 2.9f).OnComplete(ResetFade);
    }
    private void ResetFade()
    {
        StartCoroutine(LaunchChapter());
    }

    IEnumerator LaunchChapter()
    {
        yield return new WaitForSeconds(3f);
        imageFade.DOFade(1, 2.5f).OnComplete(FadeCompleted);
    }

    private void FadeCompleted()
    {
        SceneManager.LoadScene(goToScene);
        Debug.Log("SceneLancer");
    }


    
}
