using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;
    public static UIManager Instance => _instance;
    
    public Canvas canvas;

    public RootOperationPanel rootOperationPanel;
    public MessageDialog messageDialog;
    
    // Start is called before the first frame update
    void Start()
    {
        if (_instance == null)
            _instance = this;
        else
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowUIMessage(string message)
    {
        messageDialog.ShowMessage(message);
    }
}
