using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    //Resets Game Over conditions and loads target scene, actually not sure if this is still used anywhere

    public string SceneName;
    
    public void SceneLoad()
    {
        PlayerController PC = GameObject.Find("Player").GetComponent<PlayerController>();
        PC.ResetGameOver();
        SceneManager.LoadScene(SceneName);
    }
}
