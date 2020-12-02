using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class JumpTextController : MonoBehaviour
{
    private static JumpTextController instance = null;
    public static JumpTextController Instance => instance;

    private void Awake()
    {
        instance = this;
    }

    [SerializeField] private GameObject screenMessage = null;
    [SerializeField] private Transform worldCanvas = null;

    [SerializeField] TextMeshProUGUI worldMessageText = null;
    [SerializeField] TextMeshProUGUI screenMessageText = null;

    [SerializeField] private Color startColor = Color.red;
    [SerializeField] private Color endColor = new Color(Color.white.r, Color.white.g, Color.white.b, 0);

    [SerializeField] private float animateTime = 2.0f;
    [SerializeField] private float speed = 5.0f;

    private void Start()
    {
        screenMessage.SetActive(false);
    }

    public static void PlayScreenMessage(string msg) 
    {
        if (instance)
            instance.DoMsg(msg);
    }

    public static void PlayWorldJumpMessageAt(string msg, Vector3 pos)
    {
        if (instance)
            instance.DoJumpMsgAt(msg, pos);
    }

    private void DoMsg(string msg) 
    {
        screenMessageText.text = msg;

        StartCoroutine(DoScreenMessage());
    }

    private void DoJumpMsgAt (string msg, Vector3 pos)
    {
        worldCanvas.Rotate(Vector3.up, 
            Quaternion.Angle(
               Camera.main.transform.rotation,
                transform.rotation));

        worldMessageText.text = msg;
        worldCanvas.position = pos + new Vector3(0, 1, 0);

        StartCoroutine(DoWorldMessage());
    }

    private IEnumerator DoWorldMessage() 
    {
        worldMessageText.gameObject.SetActive(true);
        

        float countdown = animateTime;
        while (countdown > 0)
        {
            worldCanvas.Translate(Vector3.up * speed * Time.deltaTime);
            countdown -= Time.deltaTime;
            yield return new WaitForSeconds(Time.deltaTime);
        }
        worldMessageText.gameObject.SetActive(false);
    }

    private IEnumerator DoScreenMessage() 
    {
        screenMessage.SetActive(true);

        screenMessageText.color = startColor;
       
        //Show message at full alpha for this time
        yield return new WaitForSeconds(animateTime);

        float DEC_ALPHA = .5f; //time over which alpha will be dec
        float countdown = DEC_ALPHA;
        while (countdown > 0) 
        {
              screenMessageText.color = 
              new Color(screenMessageText.color.r,
                screenMessageText.color.g,
                screenMessageText.color.b, 
                screenMessageText.color.a - Time.deltaTime/DEC_ALPHA);

            countdown -= Time.deltaTime;
            yield return null;
        }
        screenMessage.SetActive(false);
    }
}
