using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TimB
{
    public class StartupManager : MonoBehaviour
    {

        // Use this for initialization
        private IEnumerator Start()
        {
            while (!LocalizationManager.instance.GetIsReady())
            {
                yield return null;
            }

            SceneManager.LoadScene("MenuScreen");
        }

    }
}