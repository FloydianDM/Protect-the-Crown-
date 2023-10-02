using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace ProtectTheCrown
{
    public class GameManager : MonoBehaviour
    {
        public void LoseGame()
        {
            SceneManager.LoadScene(0);
        }
    }
}

