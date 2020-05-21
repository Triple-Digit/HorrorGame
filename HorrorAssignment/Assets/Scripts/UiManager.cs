using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Image caughtImage, loadingScene;
    public float caughtImageAlpha, loadingSceneImage, fadeSpeed = 0.5f, fadeTextSpeed;

    public Text tutorialText, interactableText;
    public Color fadeInColor, fadeOutColor;
    public bool showInteractableText;
    


    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        showInteractableText = false;
        //interactableTextColor = interactableText.color;
        //tutorialTextColor = tutorialText.color;
        
        interactableText.color = fadeOutColor;
        if(tutorialText != null)
        {
            Invoke("FadeTutorialText", 3f);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!GameManager.instance.playerCaught)
        {
            caughtImage.color = new Color(caughtImage.color.r, caughtImage.color.g, caughtImage.color.b, Mathf.MoveTowards(caughtImage.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        else
        {
            caughtImage.color = new Color(caughtImage.color.r, caughtImage.color.g, caughtImage.color.b, Mathf.MoveTowards(caughtImage.color.a, 0.5f, fadeSpeed * Time.deltaTime));
        }
        if (!GameManager.instance.levelEnding)
        {
            loadingScene.color = new Color(loadingScene.color.r, loadingScene.color.g, loadingScene.color.b, Mathf.MoveTowards(loadingScene.color.a, 0f, fadeSpeed * Time.deltaTime));
        }
        else
        {
            loadingScene.color = new Color(loadingScene.color.r, loadingScene.color.g, loadingScene.color.b, Mathf.MoveTowards(loadingScene.color.a, 0.5f, fadeSpeed * Time.deltaTime));
        }
        if(!showInteractableText)
        {
            interactableText.color = fadeOutColor;
        }
        else
        {
            return;
        }

    }

    public void FadeTutorialText()
    {
        CallFadeOutText(tutorialText);
    }

    public void CallFadeOutText(Text textToFadeOut)
    {

        StartCoroutine(FadeOutText(textToFadeOut));
    }

    public void CallFadeInText(Text textToFadeIn)
    {
        
        StartCoroutine(FadeInText(textToFadeIn));
        showInteractableText = true;
    }

    IEnumerator FadeOutText(Text textType)
    {
        while (textType.color.a > 0)
        {
            textType.color = Color.Lerp(textType.color, fadeOutColor, fadeTextSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator FadeInText(Text textType)
    {
        while(textType.color.a != 1)
        {
            textType.color = Color.Lerp(textType.color, fadeInColor, fadeTextSpeed * Time.deltaTime);
            yield return null;
        }
    }
}
