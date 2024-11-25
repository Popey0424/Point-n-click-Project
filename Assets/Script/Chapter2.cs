using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Chapter2 : MonoBehaviour
{
    [SerializeField] private Image FadeImage;
    [SerializeField] private Image RaycastImage;
    [SerializeField] private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        RaycastImage.gameObject.SetActive(false);
        FadeImage.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void EndOfChapterTwo()
    {
        Debug.Log("Fin du niveau deux");
        RaycastImage.gameObject.SetActive(true);
        FadeImage.gameObject.SetActive(true);
        FadeImage.DOFade(1, 2.9f).OnComplete(FadeComplete);
    }

    private void FadeComplete()
    {
        StartCoroutine(LaunchNextChapter());
    }

    IEnumerator LaunchNextChapter()
    {
        audioSource.Play();
        
        
        yield return new WaitForSeconds(1f);
        SwitchScene();
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("OutroScene");
        Debug.Log("Chapitre 2");
    }
}
