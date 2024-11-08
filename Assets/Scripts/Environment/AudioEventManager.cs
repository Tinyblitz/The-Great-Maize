using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AudioEventManager : MonoBehaviour
{

    public EventSound3D eventSound3DPrefab;

    public AudioClip[] boxAudio = null;
    //public AudioClip playerAudio;
    //public AudioClip gruntAudio;

    private UnityAction<Vector3, float> boxCollisionEventListener;
    //private UnityAction<Vector3, float> playerEventListener;

    void Awake() {

        boxCollisionEventListener = new UnityAction<Vector3, float>(boxCollisionEventHandler);
        //playerEventListener = new UnityAction<Vector3, float>(playerEventHandler);
    }


    // Use this for initialization
    void Start()
    {}


    void OnEnable() {

        EventManager.StartListening<CratesCollisionEvent, Vector3, float>(boxCollisionEventListener);
        //EventManager.StartListening<PlayerEvent, Vector3, float>(playerEventListener);
    }

    void OnDisable() {

        EventManager.StopListening<CratesCollisionEvent, Vector3, float>(boxCollisionEventListener);
        //EventManager.StopListening<PlayerEvent, Vector3, float>(playerEventListener);
    }

    void boxCollisionEventHandler(Vector3 worldPos, float impactForce) {
        //AudioSource.PlayClipAtPoint(this.boxAudio, worldPos);

        const float halfSpeedRange = 0.2f;

        EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

        snd.audioSrc.clip = this.boxAudio[Random.Range(0, boxAudio.Length)];

        snd.audioSrc.pitch = Random.Range(1f - halfSpeedRange, 1f + halfSpeedRange);

        snd.audioSrc.minDistance = Mathf.Lerp(1f, 8f, impactForce / 200f);
        snd.audioSrc.maxDistance = 100f;

        snd.audioSrc.Play();
    }

    //void playerEventHandler(Vector3 worldPos, float collisionMagnitude)
    //{
    //    //AudioSource.PlayClipAtPoint(this.explosionAudio, worldPos, 1f);

    //    if (collisionMagnitude > 300f)
    //    {

    //        EventSound3D snd = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

    //        snd.audioSrc.clip = this.playerAudio;

    //        snd.audioSrc.minDistance = 5f;
    //        snd.audioSrc.maxDistance = 100f;

    //        snd.audioSrc.Play();

    //        if (collisionMagnitude > 500f)
    //        {

    //            EventSound3D snd2 = Instantiate(eventSound3DPrefab, worldPos, Quaternion.identity, null);

    //            snd2.audioSrc.clip = this.gruntAudio;

    //            snd2.audioSrc.minDistance = 5f;
    //            snd2.audioSrc.maxDistance = 100f;

    //            snd2.audioSrc.Play();
    //        }
    //    }
    //}


}
