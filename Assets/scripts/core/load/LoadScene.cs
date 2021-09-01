using Tools;
using UnityEngine;
using UnityEngine.UI;

namespace Global.Load
{
    public class LoadScene : MonoBehaviour
    {
        [SerializeField] private Button button;

        public void Load(int index)
        {
            SceneLoader.LoadScene(index);
        }
    }
}