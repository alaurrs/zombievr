using System;
using System.Collections;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    
    public AudioSource audioSource;  // Référence à l'AudioSource
    public AudioClip audioClip;      // Clip à jouer

    private void Start()
    {
        this.audioClip = audioSource.clip;
    }

    private IEnumerator OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Zombie OFF");

            Animator zombieAnimator = other.GetComponent<Animator>();
            if (zombieAnimator != null)
            {
                // Attendre que l'animation actuelle soit terminée
                while (zombieAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime < 10)
                {
                    yield return null; // Attendre une frame
                }

                // Une fois que l'animation est terminée, réinitialiser le trigger
                zombieAnimator.ResetTrigger("IsAttacked");
                Debug.Log("Zombie OFF");

            }
        }
    }
    
    public void PlaySound()
    {
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    public void StopSound()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zombie"))
        {
            Debug.Log("Zombie ON");

            Animator zombieAnimator = other.GetComponent<Animator>();
            if (zombieAnimator != null)
            {
                PlaySound();
                // Déclenche l'animation d'attaque
                zombieAnimator.SetTrigger("IsAttacked");
                Debug.Log("Zombie ON");
            }
        }
    }
}