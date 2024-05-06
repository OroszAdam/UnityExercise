using UnityEngine;

public class GameController : MonoBehaviour {
    public static GameController Instance;

    public GameObject capsule1;
    public GameObject capsule2;

    void Awake() 
    {
        if (Instance == null) 
        {
            Instance = this;
        }
    }
}
