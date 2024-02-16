using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameBehavior : MonoBehaviour
{
    private int _keysCollected = 0;
    private int _currentKeys = 0;
    private int _locksRemaining = 3;
    public string labelText = "Collect all 3 keys to unlock the gate and escape!";
    public int maxKeys = 3;
    public bool showWinScreen = false;
    public bool showLossScreen = false;
    public PlayerBehavior Player;
    private int currentHP;
    public int Keys
    {
        get { return _keysCollected; }

        set 
        { 
            _keysCollected = value;
            
            if (_keysCollected >= maxKeys)
            {
                labelText = "You've found all the keys! Go unlock the gate!";
                
            }
            else
            {
                labelText = "Key found, only " + (maxKeys - _keysCollected) + " more to go.";
            }
        }
        

    }
    public int Locks
    {
        get { return _locksRemaining; }

        set
        {
            _locksRemaining = value;

            if (_locksRemaining == 0)
            {
                
                showWinScreen = true;
                Time.timeScale = 0f;
            }
        }
    }
    public int HP
    {
        get { return currentHP; }

        set
        {
            currentHP = value;
            if (currentHP <= 0)
            {
                labelText = "You died. Try again?";
                showLossScreen = true;
            }
            if (currentHP > 1)
            {
                labelText = "You feel invigorated.";
            }
        }
    }

    public int currentKeys
    {
        get { return _currentKeys; }

        set
        {
            _currentKeys = value;
            if (_currentKeys == 0)
            {
                Player.hasKey = false;
            }
            if(_currentKeys > 0)
            {
                Player.hasKey = true;
            }
        }
    }
    private void OnGUI()
    {
        GUI.Box(new Rect(20,50,150,25), "Keys Collected:" + _keysCollected);

        GUI.Label(new Rect(Screen.width / 2, Screen.height - 50,300,50), labelText);
        if (showLossScreen)
        {
            Cursor.lockState = CursorLockMode.Confined;
            if (GUI.Button(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 50,200,100),"You died."))
            {
                RestartLevel();
            }
        }
        if(showWinScreen) {
            Cursor.lockState = CursorLockMode.Confined;
            if (GUI.Button(new Rect(Screen.width/2 - 100,Screen.height/2 - 50,200,100),"You Escaped!"))
            {
                
                RestartLevel();
            }
        }
    }
    void RestartLevel()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }
}
