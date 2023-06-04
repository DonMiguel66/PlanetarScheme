using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Views
{
    public class StartSceneUIView : MonoBehaviour
    {
        [SerializeField] private Button _start;
        [SerializeField] private Button _exit;

        private void Awake()
        {
            _start.onClick.AddListener(()=> SceneManager.LoadScene("Scenes/MainScene"));
            _exit.onClick.AddListener(Application.Quit);
        }
    }
}