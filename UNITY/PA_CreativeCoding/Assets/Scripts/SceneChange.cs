using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    
   
    public void SceneLoad()
    {
        PlayerController PC = GameObject.Find("Player").GetComponent<PlayerController>();
        PC.ResetGameOver();
        SceneManager.LoadScene(SceneName);
    }
}
