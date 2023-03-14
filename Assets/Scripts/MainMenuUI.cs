using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class MainMenuUI : MonoBehaviour
{
    [SerializeField] private Button playButton, quitButton;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(() => Loader.LoadScene(Loader.Scene.GameScene));
        quitButton.onClick.AddListener(() => Application.Quit());
    }

}
