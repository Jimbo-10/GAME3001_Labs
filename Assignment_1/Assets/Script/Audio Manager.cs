using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioClip m_Clip;
    AudioSource m_audioSource;
    // Start is called before the first frame update

    void Start()
    {
        m_audioSource = GetComponent<AudioSource>();

        m_audioSource.clip = m_Clip;
        m_audioSource.Play();

        DontDestroyOnLoad(this.gameObject);
    }   
}
