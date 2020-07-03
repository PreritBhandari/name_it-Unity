using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InputButton : MonoBehaviour
{
    public string Alphabet;
    private Button Button;
    private Text ButtonLabel;

    // Start is called before the first frame update
    void Start()
    {
        Button = this.GetComponent<Button>();
        ButtonLabel = Button?.GetComponentInChildren<Text>();

        if(!string.IsNullOrEmpty(Alphabet))
            ButtonLabel.text = Alphabet;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
