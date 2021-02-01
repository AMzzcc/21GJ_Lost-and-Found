using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class startBtn : MonoBehaviour
{
    public GameObject noticePanel;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(OpenNotice);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OpenNotice()
    {
        noticePanel.SetActive(true);
    }
}
