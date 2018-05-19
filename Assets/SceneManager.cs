using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SceneManager : MonoBehaviour {

    public void ChangeScene(int scene)
    {
        switch (scene)
        {
            case 0:
                UnityEngine.SceneManagement.SceneManager.LoadScene(0);
                break;
            case 1:
                UnityEngine.SceneManagement.SceneManager.LoadScene(1);
                break;
        }
    }
}
