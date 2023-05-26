using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Typer : MonoBehaviour
{
    public TextMeshProUGUI textMeshPro;
    public static string NPC_Chat_text;
    private int parser;
    // Start is called before the first frame update
    void Start()
    {
        textMeshPro = GetComponent<TextMeshProUGUI>();
        textMeshPro.text = "";
        NPC_Chat_text = "";
        parser = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(parser < NPC_Chat_text.Length)
        {
            textMeshPro.text += NPC_Chat_text[parser];
            parser += 1;
        }
    }

    public void Reset()
    {
        NPC_Chat_text = "";
        parser = 0;
    }
}
