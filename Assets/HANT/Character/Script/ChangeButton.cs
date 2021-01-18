using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeButton : MonoBehaviour
{
    AudioSource audioSource;
    //public AudioClip Cancel;

    private bool OnStartButton;
    private bool OnExpalanationButton;
    private bool OnOptionButton;
    private bool OnBuckButton;    

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    public void OnStartButtonClick()
    {
        OnStartButton = true;
        StartCoroutine("SE");      
    }

    public void OnExplanationButtonClick()
    {
        OnExpalanationButton = true;
        StartCoroutine("SE");
    }

    public void OnOptionButtonClick()
    {
        OnOptionButton = true;
        StartCoroutine("SE");
    }

    public void BuckButtonClick()
    {
        OnBuckButton = true;
        StartCoroutine("SE");        
    }
    IEnumerator SE()
    {
        if (OnStartButton)
        {
            //audioSource.PlayOneShot();
            OnStartButton = false;
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene("Stage");
        }

        if (OnExpalanationButton)
        {
            //audioSource.PlayOneShot();
            OnExpalanationButton = false;
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene("Explanation");
        }

        if (OnOptionButton)
        {
            //audioSource.PlayOneShot();
            OnOptionButton = false;
            yield return new WaitForSeconds(0.2f);
            SceneManager.LoadScene("Option");
        }

        if (OnBuckButton)
        {
            //audioSource.PlayOneShot(Cancel);
            OnBuckButton = false;
            yield return new WaitForSeconds(0.1f);
            SceneManager.LoadScene("Title");
        }


    }
}
