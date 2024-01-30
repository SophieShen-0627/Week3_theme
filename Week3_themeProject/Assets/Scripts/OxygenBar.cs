using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    public GameObject OxygenIcon;
    [SerializeField] AudioClip BubbleSound;
    [SerializeField] List<GameObject> Icons = new List<GameObject>();
    [SerializeField] ParticleSystem BubbleBroke;
    private GameObject[] IconArray;
    private int LastIconNum = 0;


    // Start is called before the first frame update
    void Start()
    {

        for (int i = 0; i < PlayerParameters.instance.PlayerMaxOxygen; i++)
        {
            Debug.Log("generate oxygen bubble");
            GameObject icon = Instantiate<GameObject>(OxygenIcon, transform);
            Icons.Add(icon);
        }

        IconArray = Icons.ToArray();
        LastIconNum = PlayerParameters.instance.PlayerMaxOxygen - 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerParameters.instance.PlayerCurrentOxygen > 0)
        {
            OxygenCalculator();
        }
    }

    private void OxygenCalculator()
    {

        int CurrentIconNum =  Mathf.FloorToInt(PlayerParameters.instance.PlayerCurrentOxygen);

        if (CurrentIconNum == PlayerParameters.instance.PlayerMaxOxygen) CurrentIconNum = PlayerParameters.instance.PlayerMaxOxygen - 1;
        GameObject CurrentIcon = IconArray[CurrentIconNum];

        for (int i = CurrentIconNum + 1; i < PlayerParameters.instance.PlayerMaxOxygen; i++)
        {
            IconArray[i].GetComponent<Image>().color = new Color(1, 1, 1, 0);
        }
        float CurrentAlpha = PlayerParameters.instance.PlayerCurrentOxygen - CurrentIconNum;

        CurrentIcon.GetComponent<Image>().color = new Color(1, 1, 1, CurrentAlpha);

        if (CurrentIconNum < LastIconNum)
        {
            GetComponent<AudioSource>().PlayOneShot(BubbleSound);
            Instantiate<ParticleSystem>(BubbleBroke, IconArray[LastIconNum].transform.position, Quaternion.identity);
            LastIconNum -= 1;
        }
    }

}
