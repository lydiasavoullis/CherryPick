using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TextInteractionMethods : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    TextMeshProUGUI attatchedText;
    void Start()
    {
        UpdateText(gameObject.name);
    }
    public void UpdateText(string textVariableName) {
        string textVar = textVariableName.ToLower();
        switch (textVar)
        {
            
            case "funds":
                attatchedText.text = GameManager.Instance.funds.ToString();
                break;

            case "reputation":
                attatchedText.text = GameManager.Instance.reputation.ToString();
                break;

            default:
                //attatchedText.text = GameManager.Instance.funds.ToString();
                break;
        }
        
    }
    
}
