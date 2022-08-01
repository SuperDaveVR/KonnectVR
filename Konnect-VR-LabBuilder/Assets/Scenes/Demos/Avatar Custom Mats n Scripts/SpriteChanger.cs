using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Audio;

public class SpriteChanger : MonoBehaviour
{
    [SerializeField]
    public GameObject mouthObject;
    [SerializeField]
    public Material defaultMouth;
    [SerializeField]
    public Material openMouth;

    [SerializeField]
    public GameObject eyeLeft;
    [SerializeField]
    public GameObject eyeRight;
    [SerializeField]
    public Material defaultEye;
    [SerializeField]
    public Material blinkEye;

    public Vector3 objectScale;
    public float mouthCounter;
    public float eyeCounter;
    public float eyeTimer;

    public float sensitivity;
    public float loudness = 0;
    private AudioSource _audio;


    void Awake()
    {
        //_audio = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();

        objectScale = transform.localScale;

        eyeTimer = UnityEngine.Random.Range(3.0f, 10.0f);

        /*_audio.clip = Microphone.Start(null, true, 10, 44100);
        _audio.loop = true;
        _audio.mute = false;
        while (!(Microphone.GetPosition(null) > 0)) { }
        _audio.Play();*/
    }

    // Update is called once per frame
    void Update()
    {
        faceSpriteUpdateJunk();

        //if (Input.GetKey(KeyCode.A)) { phoneticState(openMouth); } ///Relic from initial testing
    }
    
    void faceSpriteUpdateJunk()
    {
        if(mouthCounter > 0) { mouthCounter = mouthCounter - (7 * Time.deltaTime); }
        eyeCounter = eyeCounter + (Time.deltaTime);
        if(eyeCounter > 0.2f && eyeCounter < 0.3f) { openEm(eyeLeft, eyeRight); }
        if(eyeCounter > eyeTimer) { blink(eyeLeft, eyeRight); }

        //This section commented out is depreciated...
        //Call avatar's SpriteChanger from OnSpeechDetected in VivoxVoiceManager.
        /*loudness = GetAveragedVolume() * sensitivity;
        if (loudness > 0.1) 
        {
            if (mouthCounter <= 0) { mouthCounter = 1; }
            if (loudness > 2) { loudness = 2; }
            if (loudness < 1) { loudness = 1; }
            phoneticState(openMouth);
            changeMouthSize(loudness);
        }
        else */
        if (mouthCounter <= 0)
        { 
            phoneticState(defaultMouth);
            changeMouthSize(1);
        }
    }

    //Direct response to OnSpeechDetected in VivoxVoiceManager.
    //That invoke will need to get the avatar instacne it's attached to and call this script for it.
    //Not sure how to do that, or calculate loudness from that, at the moment.
    public void OnSpeechDetectedEventResponse() 
    {
        if (mouthCounter <= 0) { mouthCounter = 1; }
        if (loudness > 2) { loudness = 2; }
        if (loudness < 1) { loudness = 1; }
        phoneticState(openMouth);
        changeMouthSize(loudness);
    }

    void phoneticState(Material material)
    {
        MeshRenderer meshRenderer = GetComponent<MeshRenderer>();
        meshRenderer.material = material;
    }

    void changeMouthSize(float modifier)
    {
        transform.localScale = new Vector3(objectScale.x * 1, objectScale.y * 1, objectScale.z * modifier);
    }

    void blink(GameObject eyeL, GameObject eyeR)
    {
        MeshRenderer meshRenderer1 = eyeL.GetComponent<MeshRenderer>();
        meshRenderer1.material = blinkEye;
        MeshRenderer meshRenderer2 = eyeR.GetComponent<MeshRenderer>();
        meshRenderer2.material = blinkEye;
        eyeTimer = UnityEngine.Random.Range(3.0f, 10.0f);
        eyeCounter = 0;
    }
    void openEm(GameObject eyeL, GameObject eyeR)
    {
        MeshRenderer meshRenderer1 = eyeL.GetComponent<MeshRenderer>();
        meshRenderer1.material = defaultEye;
        MeshRenderer meshRenderer2 = eyeR.GetComponent<MeshRenderer>();
        meshRenderer2.material = defaultEye;
    }

    float GetAveragedVolume()
    {
        float[] data = new float[256];
        float a = 0;
        _audio.GetOutputData(data, 0); //
        foreach (float s in data)
        {
            a += Mathf.Abs(s);
        }
        return a / 256;
    }
}
