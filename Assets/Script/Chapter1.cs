using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class Chapter1 : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Image FadeImage;
    [SerializeField] private Image RaycastImage;
    [SerializeField] private AudioSource audioSource;


    private void Start()
    {
        RaycastImage.gameObject.SetActive(false);
        FadeImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            EndOfChapterOne();
        }
    }

    private void EndOfChapterOne()
    {
        Debug.Log("Changement de Chapitre");
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
        yield return new WaitForSeconds(3f);
        SwitchScene();


    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("Chapter2");
        Debug.Log("Chapitre 2");
    }
}
