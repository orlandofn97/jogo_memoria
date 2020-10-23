using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startGame : MonoBehaviour
{
    public gamecontroller gamecontroller;
    

    void OnMouseDown()
    {
        gamecontroller.StartGame();
    }
}
