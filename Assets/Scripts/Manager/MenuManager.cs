using UnityEngine;
using UnityEngine.SceneManagement;

namespace NSFWMiniJam3.Manager
{
    public class MenuManager : MonoBehaviour
    {
        public void Play()
        {
            SceneManager.LoadScene("Main");
        }
    }
}
