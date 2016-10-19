using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class BookScript : MonoBehaviour {

    private Animator anim;
    public GameObject book;
    public GameObject openButton;
    public GameObject wholeContent;
    public GameObject levelContent;
    public GameObject SettingsContent;
    public GameObject AchievementContent;
    public GameObject moreContent;

    private bool isPlayButtonPressed = false;
    private bool isSettingsButtonPressed = false;
    private bool isAchieveButtoPressed = false;
    private bool isMoreButtonPressed = false;

    private CanvasGroup levelContentGroup;

    // Use this for initialization
    void Start () {
        Time.timeScale = 1f;
        anim = book.GetComponentInChildren<Animator>();
        levelContentGroup = wholeContent.GetComponent<CanvasGroup>();
        wholeContent.active = false;
        levelContent.active = false;
        SettingsContent.active = false;
        AchievementContent.active = false;
        moreContent.active = false;
    }

    void FadeIn()
    {
        StartCoroutine(DoFade());
    }

    //updates alpha (fade in)
    IEnumerator DoFade()
    {
        while (levelContentGroup.alpha <= 1)
        {
            levelContentGroup.alpha += Time.deltaTime;
            yield return null;
        }
        levelContentGroup.interactable = false;
        yield return null;
    }

    //for very first time (pressed play button)
    public void openBook () {
        anim.SetInteger("bookState", 1);
        FadeIn();
        wholeContent.active = true;
        openButton.active = false;
        levelContent.active = true;
        moreContent.active = false;
        isPlayButtonPressed = true;
    }

    public void showLevelContent()
    {
        //check to stop replaying animation on button pressed 
        if (!isPlayButtonPressed) { anim.Play("turn page", -1, 0f); }
        isPlayButtonPressed = true;

        //content switch
        levelContent.active = true;
        SettingsContent.active = false;
        AchievementContent.active = false;
        moreContent.active = false;

        //reset if pressed state 
        isSettingsButtonPressed = false;
        isAchieveButtoPressed = false;
        isMoreButtonPressed = false;

}

    public void showSettingsContent()
    {
        //check to stop replaying animation on button pressed 
        if (!isSettingsButtonPressed) { anim.Play("turn page", -1, 0f); }
        isSettingsButtonPressed = true;

        //content switch
        levelContent.active = false;
        SettingsContent.active = true;
        AchievementContent.active = false;
        moreContent.active = false;

        //reset if pressed state 
        isPlayButtonPressed = false;
        isAchieveButtoPressed = false;
        isMoreButtonPressed = false;
    }


    public void showArchievementContent()
    {
        //check to stop replaying animation on button pressed 
        if (!isAchieveButtoPressed) { anim.Play("turn page", -1, 0f); }
        isAchieveButtoPressed = true;

        //content switch
        levelContent.active = false;
        SettingsContent.active = false;
        AchievementContent.active = true;
        moreContent.active = false;

        //reset if pressed state 
        isPlayButtonPressed = false;
        isSettingsButtonPressed = false;
        isMoreButtonPressed = false;
    }

    public void showMoreContent()
    {
        //check to stop replaying animation on button pressed 
        if (!isMoreButtonPressed) { anim.Play("turn page", -1, 0f); }
        isMoreButtonPressed = true;

        //content switch
        levelContent.active = false;
        SettingsContent.active = false;
        AchievementContent.active = false;
        moreContent.active = true;

        //reset if pressed state 
        isPlayButtonPressed = false;
        isSettingsButtonPressed = false;
        isAchieveButtoPressed = false;
    }

    public void loadLevel(string lvlName)
    {
        SceneManager.LoadScene(lvlName);
    }

}
