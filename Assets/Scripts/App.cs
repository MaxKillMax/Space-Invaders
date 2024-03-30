using CrazyGames;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SI
{
    public class App : MonoBehaviour
    {
        [SerializeField] private AppData _data;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
#if CG
            CrazySDK.Instance.GameplayStart();
#endif

            SceneManager.LoadSceneAsync(1);
        }

        private void OnApplicationQuit()
        {
#if CG
            CrazySDK.Instance.GameplayStop();
#endif
        }
    }
}
