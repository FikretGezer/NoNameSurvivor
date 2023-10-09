using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

namespace FikretGezer
{
    public class MainMenuScript : MonoBehaviour
    {
        [SerializeField] private GameObject b;
        public void SetActiveButton()
        {
            b.SetActive(true);
        }
        public void LoadTheGame()
        {
            SceneManager.LoadScene("Main Game");
        }
    }
}
