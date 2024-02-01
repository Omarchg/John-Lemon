using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Observer : MonoBehaviour
{
    public Transform player;
    public GameEnding gameEnding;
    bool m_IsPlayerInRange;
    float m_TimerCaught;
    float m_TimerExclamacion;
    bool exclamacionworking;
    bool exclamacionaudioisplayed;


    void OnTriggerEnter (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    void OnTriggerExit (Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
            m_TimerCaught = 0f;
            
        }
    }

    void Update ()
    {
        if (m_IsPlayerInRange)
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if(Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    exclamacionworking = true;
                    m_TimerCaught += Time.deltaTime;
                    if (m_TimerCaught > 2f) 
                    {
                        exclamacionaudioisplayed = false; //para que no suene cuando pierdes
                        gameEnding.CaughtPlayer();
                    }
                    
                }
            }
        }

        if (exclamacionworking)
        { 
            if(!exclamacionaudioisplayed) //audio de la exclamacion
            {
                gameEnding.tindeck.Play();
                exclamacionaudioisplayed = true;
            }
            gameEnding.exclamacion.SetActive(true);
            gameEnding.exclamacion.transform.position = transform.position;
            m_TimerExclamacion += Time.deltaTime;
            if (m_TimerExclamacion > 2f)  //dos segundos para que desaparezca la exclamacion
            {
                gameEnding.exclamacion.SetActive(false);
                exclamacionworking = false;
                m_TimerExclamacion = 0f;
                exclamacionaudioisplayed = false;
            }
            
            
        }
    }

    
}