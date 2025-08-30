namespace CanvasTest.Features.LoadController
{
    using Unity.VisualScripting;
    using UnityEngine;
    using UnityEngine.SceneManagement;

    public sealed class LoadSceneController : MonoBehaviour
    {
        private static bool isMenuScene = false;
     
        void Update()
        {
            if (isMenuScene && Input.anyKeyDown){
                isMenuScene = !isMenuScene;
                SceneManager.LoadScene(isMenuScene ? "Menu" : "Game");
            }
        }
    }
}

