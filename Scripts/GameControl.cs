using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class GameControl : MonoBehaviour
{
    int clickedValue;
    GameObject objectValue;
    GameObject clickedObjectValue;
    public Sprite defaultSprite;
    public Image[] images;
    public Image[] mainImage;
    public GameObject[] objects;
    public AudioSource[] gameSounds;
    public TextMeshProUGUI counterText;
    public GameObject[] volumes;
    public GameObject mainVolume;
    public GameObject scorePanel;
    public TextMeshProUGUI buttonClickedText;
    public TextMeshProUGUI scoreText;
    bool isMute;
    bool isPause;
    float time = 0;
    float minute;
    float second;
    int imagesControl;
    int buttonClicked;

    void Start()
    {
        clickedValue = 0;

        List<GameObject> objectList = objects.ToList();
        foreach (var item in mainImage)
        {
            int randomizeObjects = Random.Range(0, objectList.Count);
            item.transform.position = objectList[randomizeObjects].transform.position;
            objectList.RemoveAt(randomizeObjects);
        }
        Time.timeScale = 1f;

        scorePanel.SetActive(false);
        imagesControl = images.Length;

    }

    void Update()
    {

        Debug.Log(imagesControl);
        Debug.Log(buttonClicked);
        CounterFNC();
        if (scorePanel.activeSelf == true)
            Time.timeScale = 0;
        else
            Time.timeScale = 1;


        if (imagesControl <= 0)
        {
            scorePanel.SetActive(true);
            scoreText.text = counterText.text;
            buttonClickedText.text = buttonClicked.ToString();
        }



    }
    public void ObjectsFNC(GameObject objects)
    {
        objectValue = objects;


    }
    public void ButtonClickFNC(int value)
    {

        if (clickedValue == 0)
        {
            clickedValue = value;
            clickedObjectValue = objectValue;
            clickedObjectValue.SetActive(false);
            buttonClicked++;

        }
        else
        {
            StartCoroutine(ControlFNC(value));
        }
    }
    public void PauseFNC()
    {
        isPause = !isPause;
        if (isPause)
        {
            Debug.Log("Oyun Durduruldu");
            scorePanel.SetActive(true);
            foreach (var item in images)
            {
                item.GetComponent<Image>().raycastTarget = false;
            }
            foreach (var item in gameSounds)
            {
                item.GetComponent<AudioSource>().enabled = false;
            }
        }
        else
        {
            scorePanel.SetActive(false);
            foreach (var item in images)
            {
                item.GetComponent<Image>().raycastTarget = true;
            }
            foreach (var item in gameSounds)
            {
                item.GetComponent<AudioSource>().enabled = true;
            }
        }


    }
    public void RestartFNC()
    {
        time = 0;
        imagesControl = images.Length;
        scorePanel.gameObject.SetActive(false);
        isPause = false;
        foreach (var image in images)
        {
            clickedValue = 0;
            clickedObjectValue = null;
            image.gameObject.SetActive(true);
        }
        List<GameObject> objectList = objects.ToList();
        foreach (var item in mainImage)
        {
            item.gameObject.SetActive(true);
            int randomizeObjects = Random.Range(0, objectList.Count);
            item.transform.position = objectList[randomizeObjects].transform.position;
            objectList.RemoveAt(randomizeObjects);
        }
       
    }
    IEnumerator ControlFNC(int value)
    {
        if (clickedObjectValue != null && objectValue != null)
        {

            if (clickedValue == value)
            {
                Debug.Log("Eþleþti");
                clickedValue = 0;
                objectValue.SetActive(false);
                foreach (var item in images)
                {
                    item.raycastTarget = false;
                }
                yield return new WaitForSeconds(1);
                foreach (var item in images)
                {
                    item.raycastTarget = true;
                }
                gameSounds[1].Play();
                objectValue.transform.parent.gameObject.SetActive(false);
                clickedObjectValue.transform.parent.gameObject.SetActive(false);
                clickedObjectValue = null;
                imagesControl -= images.Length / 30;
                buttonClicked++;
            }
            else
            {

                Debug.Log("Eþleþmedi");
                clickedValue = 0;
                objectValue.SetActive(false);
                foreach (var item in images)
                {
                    item.raycastTarget = false;
                }
                yield return new WaitForSeconds(1);
                foreach (var item in images)
                {
                    item.raycastTarget = true;
                }
                gameSounds[2].Play();
                clickedObjectValue.SetActive(true);
                objectValue.SetActive(true);
                clickedObjectValue = null;
                buttonClicked++;

            }
        }


    }


    void CounterFNC()
    {
        time += Time.deltaTime;
        minute = Mathf.FloorToInt(time / 60);
        second = Mathf.FloorToInt(time % 60);

        counterText.text = string.Format("{0:00}:{1:00}", minute, second).ToString();
    }
    public void VolumeFNC()
    {


        isMute = !isMute;

        if (isMute)
        {

            mainVolume.GetComponent<Image>().sprite = volumes[1].GetComponent<SpriteRenderer>().sprite;
            foreach (var item in gameSounds)
            {
                item.GetComponent<AudioSource>().enabled = false;
            }
        }
        else
        {
            mainVolume.GetComponent<Image>().sprite = volumes[0].GetComponent<SpriteRenderer>().sprite;
            foreach (var item in gameSounds)
            {
                item.GetComponent<AudioSource>().enabled = true;
            }
        }



    }


}
