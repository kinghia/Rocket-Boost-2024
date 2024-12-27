using UnityEngine;
using UnityEngine.InputSystem;

public class QuitGame : MonoBehaviour
{
    void Update()
    {
        if(Keyboard.current.escapeKey.isPressed)
        {
            Debug.Log("Da Quit thanh cong");
            Application.Quit();
        }
    }
}
