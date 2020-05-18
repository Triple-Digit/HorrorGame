using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public string nextScene;
    public float waitToLoad = 3f;
    public bool playerCaught, levelEnding;

    private void Awake()
    {
        instance = this;
        playerCaught = false;
        levelEnding = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerCaught()
    {
        StartCoroutine(RestartLevel());
        playerCaught = true;
    }

    public void FinishedLevel()
    {
        StartCoroutine(LoadNextLevel());
        levelEnding = true;
    }

    public IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LoadNextLevel()
    {
        Debug.Log("Finished Level");
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(nextScene);
    }
}
