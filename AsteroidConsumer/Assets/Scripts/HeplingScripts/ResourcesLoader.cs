using UnityEngine;

namespace TimB
{
    public class ResourcesLoader
    {
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        public static GameObject LoadGameObject(string path)
        {
            GameObject responseGo = Resources.Load(path) as GameObject;
            return responseGo;
        }
    }
}
