using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        }

    public void BeginGame()
    {
        Debug.Log("clisk");
        SceneManager.LoadScene("Level1_Room1");

    }
}
