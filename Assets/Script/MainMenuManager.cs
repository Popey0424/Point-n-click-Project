using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{

    [Header("Menus")]
    public GameObject SettingsMenu;

    public GameObject WarningLeaveMenu;
    public GameObject CreditsMenu;


    //[Header("Animation")]
    //public Animator LeaveMenu;
    //public Animator OpenMinMenu;

    [SerializeField] private Image _imageFade;
    public KeyCode LeaveSettings;
    private bool IsInMainMenu;
    bool OpenMini = false;

    //[Header("RaycastImage")]
    //public GameObject RaycastLeaveGame;

    private void Start()
    {

        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        IsInMainMenu = true;
        //RaycastLeaveGame.SetActive(false);


    }

    private void Update()
    {
        Debug.Log(IsInMainMenu);

        if (SettingsMenu.activeSelf && Input.GetKeyDown(LeaveSettings))
        {
            OnClickBackSettings();
   

            IsInMainMenu = true;
        }
        if (CreditsMenu.activeSelf && Input.GetKeyDown(LeaveSettings))
        {

        }
    }

    //Start Game
    public void StartGame()
    {
        _imageFade.gameObject.SetActive(true);
        _imageFade.DOFade(1, 2.9f).OnComplete(FadeComplete);
    }

    private void FadeComplete()
    {
        SceneManager.LoadScene(3);
    }

    //Settings
    public void OpenSettings()
    {
        _imageFade.gameObject.SetActive(true);
        _imageFade.DOFade(1, 1f).OnComplete(FadeSettingsComplete);
    }

    public void FadeSettingsComplete()
    {
        SettingsMenu.SetActive(true);
        IsInMainMenu = false;
        _imageFade.DOFade(0, 1f).OnComplete(ResetFade);
    }

    public void OnClickBackSettings()
    {
        _imageFade.gameObject.SetActive(true);
        _imageFade.DOFade(1, 1f).OnComplete(FadeMenuComplete);
    }
    public void FadeMenuComplete()
    {
        SettingsMenu.SetActive(false);
        CreditsMenu.SetActive(false);
        IsInMainMenu = true;
        _imageFade.DOFade(0, 1).OnComplete(ResetFade);
    }

    // MiniMenu
    //public void OpenMiniMenu()
    //{
    //    if (!OpenMini)
    //    {
    //        OpenMinMenu.SetTrigger("open");
    //    }
    //}

    //public void CloseMiniMenu()
    //{
    //    OpenMinMenu.SetTrigger("close");
    //}

    //Credits
    public void OnClickCredits()
    {
        _imageFade.gameObject.SetActive(true);
        _imageFade.DOFade(1, 1f).OnComplete(FadeCreditsComplete);
    }

    public void FadeCreditsComplete()
    {
        CreditsMenu.SetActive(true);
        IsInMainMenu = false;
        _imageFade.DOFade(0, 1).OnComplete(ResetFade);
    }

    public void OnClickLeaveCreditos()
    {
        _imageFade.gameObject.SetActive(true);
        _imageFade.DOFade(1, 1f).OnComplete(FadeMenuComplete);
    }

    //Quit Game
    public void OnClickLeaveGame()
    {
        Animator animator_LeaveGame = WarningLeaveMenu.GetComponent<Animator>();

        if (animator_LeaveGame != null)
        {
            bool IsOpen = animator_LeaveGame.GetBool("IsOpen");
            animator_LeaveGame.SetBool("IsOpen", true);
        }
        //RaycastLeaveGame.SetActive(true);
    }

    public void OnClickYes()
    {
        CloseGame();
    }

    public void OnCLickNo()
    {
        Animator animator_LeaveGame = WarningLeaveMenu.GetComponent<Animator>();
        if (animator_LeaveGame != null)
        {
            bool IsOpen = animator_LeaveGame.GetBool("IsOpen");
            animator_LeaveGame.SetBool("IsOpen", false);
        }
        //RaycastLeaveGame.SetActive(false);
    }

    public void CloseGame()
    {
        Application.Quit();
    }

    //Reset Fade
    public void ResetFade()
    {
        _imageFade.gameObject.SetActive(false);
    }

}