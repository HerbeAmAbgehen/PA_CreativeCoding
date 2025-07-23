using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string SceneName;
    
   
    public void SceneLoad()
    {
        SceneManager.LoadScene(SceneName);
    }
}
