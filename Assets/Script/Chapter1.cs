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
    [SerializeField] private GameObject MotherKilling;
    private PlayerMovement playermovement;

    private void Start()
    {
        RaycastImage.gameObject.SetActive(false);
        FadeImage.gameObject.SetActive(false);
    }

    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    EndOfChapterOne();
        //}
    }

    public void EndOfChapterOne()
    {
        Debug.Log("Changement de Chapitre");
        RaycastImage.gameObject.SetActive(true);
        FadeImage.gameObject.SetActive(true);

      
        MoveMotherToPlayer();

        FadeImage.DOFade(1, 2.9f).OnComplete(FadeComplete);
    }

    private void MoveMotherToPlayer()
    {
        if (MotherKilling != null && player != null)
        {
        
            Vector3 playerPosition = player.transform.position;

            MotherKilling.transform.DOMove(playerPosition, 5.0f).SetEase(Ease.Linear)
                .OnComplete(() => Debug.Log("MotherKilling est arrivé à la position du joueur."));
        }
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