using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class StartEvent : MonoBehaviour
{
    private AudioManager theAudio;
    private DialogueManager theDial;
    private FadeManager theFade;
    private EventManager theEvent;
    private PlayerController thePlayer;

    [TextArea(1, 2)] [SerializeField] 
    private string[] dials;

    [SerializeField] private string bell;
    [SerializeField] private string mistery;
    [SerializeField] private string floor4;
    
    private Light2D flashLight;
    
    void Start()
    {
        flashLight = FindObjectOfType<Light2D>();
        theAudio = AudioManager.instance;
        theDial = DialogueManager.instance;
        theFade = FadeManager.instance;
        theEvent = EventManager.instance;
        thePlayer = PlayerController.instance;
        if (!theEvent.switches[(int)SwitchType.StartEvent])
        {
            theEvent.isEventIng = true;
            theEvent.switches[(int)SwitchType.StartEvent] = true;
            thePlayer.transform.position = new Vector3(0.5f, -1.5f, transform.position.z);
            StartCoroutine(StartEventCoroutine());
        }
    }

    IEnumerator StartEventCoroutine()
    {
        theFade.FadeIn();
        PlayerController.instance.IsPause = true;
        yield return new WaitForSeconds(2f);
        theAudio.PlaySFX(bell);
        yield return new WaitForSeconds(7.5f);
        theDial.ShowText(dials[0]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        yield return new WaitForSeconds(1f);
        theDial.ShowText(dials[1]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        yield return new WaitForSeconds(1f);
        theDial.ShowText(dials[2]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        yield return new WaitForSeconds(1f);
        theDial.ShowText(dials[3]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        yield return new WaitForSeconds(1f);
        
        flashLight.intensity = 1f;
        yield return new WaitForSeconds(1.5f);
        theAudio.PlaySFX(mistery);
        thePlayer.SetBalloonAnim();
        yield return new WaitForSeconds(2.5f);
        thePlayer.SetPlayerDirAnim("DirX", 1f);
        yield return new WaitForSeconds(1f);
        thePlayer.SetPlayerDirAnim("DirX", -1f);
        yield return new WaitForSeconds(1f);
        thePlayer.SetPlayerDirAnim("DirY", -1f);
        yield return new WaitForSeconds(2f);
        
        
        theAudio.PlayBGM(floor4);
        theDial.ShowText(dials[4]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        yield return new WaitForSeconds(1f);
        theDial.ShowText(dials[5]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        yield return new WaitForSeconds(1f);
        theDial.ShowText(dials[6]);
        yield return new WaitUntil(() => theDial.nextDialogue == true);
        PlayerController.instance.IsPause = false;
        theEvent.isEventIng = false;
    }
}
