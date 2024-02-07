using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    [SerializeField] private AudioSource m_AudioSource;
    [SerializeField] private AudioClip m_Clip;
    // Start is called before the first frame update

    void Start()
    {

        m_AudioSource.clip = m_Clip;
        m_AudioSource.Play();

        DontDestroyOnLoad(gameObject);
    }
   
}
