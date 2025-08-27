using UnityEngine;
using TMPro; // Important: This namespace is needed for Text Mesh Pro classes

public class InputFieldTextColorChanger : MonoBehaviour
{
    // A public reference to the TMP_InputField component.
    public TMP_InputField myInputField;

    [SerializeField]
    private Color newColor;

    // This method will change the color of the text inside the input field.
    public void SetInputTextColor(Color newColor)
    {
        // First, check if the input field reference is set to avoid errors.
        if (myInputField != null)
        {
            // Access the 'textComponent' property of the TMP_InputField.
            // This property is a reference to the TMP_Text component that displays the text.
            TMP_Text textComponent = myInputField.textComponent;

            // Check if the text component exists before changing its color.
            if (textComponent != null)
            {
                // Set the color of the text component.
                textComponent.color = newColor;
            }
        }
    }

    // You can call this method from other scripts or a UI event.
    // For example, from an OnValueChanged event in the Inspector.
    void Update()
    {
        // Example: Change color to red if the input text is "invalid".
        // You could also hook this up to a button's OnClick() event.
        if (myInputField.text == "invalid")
        {
            SetInputTextColor(Color.red);
        }
    }
}