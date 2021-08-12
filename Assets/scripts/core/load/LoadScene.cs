using System.Collections;
using System.Collections.Generic;
using Tools;
using UnityEngine;

namespace Global.Load
{
    public class LoadScene : MonoBehaviour
    {
        public void Load(int index)
        {
            SceneLoader.LoadScene(index);
        }
    }
}