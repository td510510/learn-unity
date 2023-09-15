using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverGroup;
    private AudioSource playAudio;
    private GameObject mainCamera;

    // Start is called before the first frame update
    void Start()
    {
        playAudio = GetComponent<AudioSource>();
        mainCamera = GameObject.Find("Main Camera");
        playAudio = mainCamera.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GameOver()
    {
        gameOverGroup.gameObject.SetActive(true);
        playAudio.Stop();
    }

    public void RestartGame()
    {
        gameOverGroup.gameObject.SetActive(false);
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
    }
}
