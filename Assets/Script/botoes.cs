using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botoes : MonoBehaviour
{
    public gamecontroller gamecontroller;
    public int idBotao;
   
    void OnMouseDown()
    {
        if (gamecontroller.gamestate == GameState.RESPONDER)
        {
            gamecontroller.StartCoroutine("responder", idBotao);
        }
    }

}
