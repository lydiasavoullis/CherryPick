using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EmailController : MonoBehaviour
{
    [SerializeField]
    GameObject veraEmail;
    [SerializeField]
    GameObject optionsButtons;
    [SerializeField]
    GameObject replyEmail;
    [SerializeField]
    GameObject spaceMessage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame) {
            veraEmail.SetActive(true);
            spaceMessage.SetActive(false);
            ShowButtons(optionsButtons);
        }
    }
    public IEnumerator GiveOptions(GameObject buttons) {
        yield return new WaitForSeconds(2f);
        buttons.SetActive(true);

    }
    public void ShowButtons(GameObject buttons)
    {
        StartCoroutine(GiveOptions(buttons));

    }
}
