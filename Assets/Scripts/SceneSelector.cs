using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneSelector : MonoBehaviour
{
    public string level;
    // Start is called before the first frame update
    public void OpenScene() {
        SceneManager.LoadScene(level);
    }
}
