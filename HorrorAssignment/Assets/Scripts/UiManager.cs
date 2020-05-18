using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiManager : MonoBehaviour
{
    public static UiManager instance;

    public Image caughtImage, loadingScene;
    public float caughtImageAlpha, loadingSceneImage, fadeSpeed = 2f;



    public void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
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
    }
}
