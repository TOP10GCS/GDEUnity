using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerCustom : MonoBehaviour
{

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void GotoOffice()
    {
        SceneManager.LoadScene("OfficeVR");
    }

    public void GotoTheather()
    {
        SceneManager.LoadScene("TheatherVR");
    }
}
