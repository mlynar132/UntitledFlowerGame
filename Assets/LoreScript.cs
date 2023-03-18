using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoreScript : MonoBehaviour
{
    [SerializeField] private MainMenu _mainMenu;

    private void StartGame() {
        _mainMenu.StartGame();
    }



}
