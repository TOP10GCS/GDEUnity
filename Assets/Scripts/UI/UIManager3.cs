using UnityEngine;
using UnityEngine.SceneManagement;
public class UIManager3 : MonoBehaviour
{
    
    public void GotoTheatherUI()
    {
        SceneManager.LoadScene("UI");
    }

    public void GotoOfficeUI()
    {
        SceneManager.LoadScene("UIInterview");
    }
}
