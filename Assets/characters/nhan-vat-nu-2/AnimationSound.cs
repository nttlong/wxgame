using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : MonoBehaviour
{
    private AudioSource footstepAudio;
    BaseCharacter character;
    //public Transform charater;
    // Start is called before the first frame update
    void Start()
    {
        if (footstepAudio == null) footstepAudio = GetComponent<AudioSource>();
        character = GetComponentInParent<BaseCharacter>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnHitGround(MotionEnum motion)
    {

        if (footstepAudio == null) return;
        if (character == null) return;
        //if (footstepAudio.isPlaying)
        //{
        //    return;
        //}
        if (character.currentStatus == motion)
        {
            footstepAudio.pitch = Random.Range(0.85f, 1.15f); // Làm tiếng bước chân tự nhiên
            footstepAudio.PlayOneShot(footstepAudio.clip);
        }
        //}
        //Debug.Log("On hit ground :" + motion.ToString() + ";" + chracter.currentStatus.ToString());
        


        //Debug.Log(chracter.currentStatus);
    }
}
