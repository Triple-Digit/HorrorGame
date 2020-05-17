using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public Scene nextScene;
    public float waitToLoad = 3f;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayerCaught()
    {
        StartCoroutine(RestartLevel());
    }

    public void FinishedLevel()
    {
        StartCoroutine(LoadNextLevel());
    }

    public IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public IEnumerator LoadNextLevel()
    {
        yield return new WaitForSeconds(waitToLoad);
        SceneManager.LoadScene(nextScene.name);
    }
}
