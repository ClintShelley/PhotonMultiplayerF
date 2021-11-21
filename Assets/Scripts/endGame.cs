using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using Photon.Pun;
using System.IO;
using Com.MyCompany.MyGame;

public class endGame : MonoBehaviour
{
    [SerializeField] AudioClip WINNERclip;
    private AudioSource audioSource;
    public TextMeshProUGUI MyUitext;

    // Start is called before the first frame update
    void Start()
    {
        MyUitext.gameObject.SetActive(false);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        MyUitext.gameObject.SetActive(true);
        audioSource.PlayOneShot(WINNERclip);
    }
}
