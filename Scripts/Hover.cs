using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject restart;
    public GameObject mainRestart;
    public GameObject pause;
    public GameObject volume;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        if (restart!=null)
        {
            restart.gameObject.GetComponent<Animator>().SetBool("hover", true);
        }
        
        if (mainRestart!=null)
        {
            mainRestart.gameObject.GetComponent<Animator>().SetBool("restarthover", true);
        }
        if (pause!=null)
        {
            pause.gameObject.GetComponent<Animator>().SetBool("pause", true);
        }
        if (volume!=null)
        {
            volume.gameObject.GetComponent<Animator>().SetBool("volume", true);
        }

    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        if (restart != null)
        {
            restart.gameObject.GetComponent<Animator>().SetBool("hover", false);
        }

        if (mainRestart != null)
        {
            mainRestart.gameObject.GetComponent<Animator>().SetBool("restarthover", false);
        }
        if (pause != null)
        {
            pause.gameObject.GetComponent<Animator>().SetBool("pause", false);
        }
        if (volume != null)
        {
            volume.gameObject.GetComponent<Animator>().SetBool("volume", false);
        }
    }
}
