using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameState
{
    SEQUENCIA,
    RESPONDER,
    NOVA,
    ERRO
}

public class gamecontroller : MonoBehaviour
{
    public GameState gamestate;
    public Text rodadaTxt, qtNotasTxt;

    public Color[] cor;
    public Image[] botoes;
    public GameObject startButton;

    public List<int> cores; //É a sequência de cores geradas
    public int idResposta, qtCores, rodada;

    private AudioSource fonteAudio;
    public AudioClip[] sons;

    void Start()
    {
        fonteAudio = GetComponent<AudioSource>();
        novaRodada(); 
    }

    public void StartGame()
    {
        StartCoroutine("sequencia", qtCores + rodada);
    }

    public void novaRodada() 
    {

      foreach(Image img in botoes)
        {
            img.color = cor[0];
        }
        
        cores.Clear();
        
        startButton.SetActive(true);
        rodadaTxt.text = "Rodada: " + (rodada + 1).ToString();
        qtNotasTxt.text = "Sequencia: " + (qtCores + rodada).ToString();
    }
    IEnumerator sequencia (int qtd)
    {
        startButton.SetActive(false);

        for (int r = qtd; r > 0; r--)
        {
            yield return new WaitForSeconds(0.5f);
            int i = Random.Range(0, botoes.Length);
            botoes[i].color = cor[1];
            fonteAudio.PlayOneShot(sons[i]);

            cores.Add(i);

            yield return new WaitForSeconds(0.5f);
            botoes[i].color = cor[0];
        }
        gamestate = GameState.RESPONDER;
        idResposta = 0;
    } 

    IEnumerator responder(int idBotao) 
    {
            botoes[idBotao].color = cor[1];
            
            if(cores[idResposta] == idBotao) 
            {
                fonteAudio.PlayOneShot(sons[idBotao]);
            }
            else 
            {
                gamestate = GameState.ERRO;
                StartCoroutine("GameOver");
            }
            idResposta += 1;

            if(idResposta == cores.Count) 
            {
                gamestate = GameState.NOVA;
                rodada += 1;
                yield return new WaitForSeconds(1);
                novaRodada();
            }
            yield return new WaitForSeconds(0.3f);
            botoes[idBotao].color = cor[0];
    }
    IEnumerator GameOver() 
    {
        rodada = 0;
        fonteAudio.PlayOneShot(sons[4]);

        yield return new WaitForSeconds(1);
        for (int i = 3; i > 0; i--) 
        {
           foreach(Image img in botoes) 
            {
               img.color = cor[1];
            }
           yield return new WaitForSeconds(0.3f);
           foreach(Image img in botoes) 
           {
               img.color = cor[0];
           }
           yield return new WaitForSeconds(0.3f);
        }
        int idB = 0;
        for(int i = 12; i > 0; i--) 
        {
            botoes[idB].color = cor[1];
            yield return new WaitForSeconds(0.1f);
            botoes[idB].color = cor[0];
            idB += 1; 
            if(idB == 4) 
            {
                idB = 0;
            }
        }
        gamestate = GameState.NOVA;
        novaRodada();
    }
}
