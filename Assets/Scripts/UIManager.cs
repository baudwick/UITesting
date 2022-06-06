using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    Text errorMessage;
    GameObject currentInput;
    GameObject go;
    EventSystem eventSystem;
    InputField inputUsername = null;
    InputField inputPassword = null;

    DBManager db = new DBManager();
 
    int currentInputIndex;
    int nextInputIndex;
    int minInputIndex = 1;
    int maxInputIndex;
   
    void InputTabManagement(bool inputTabReverse)
    {
        currentInput = eventSystem.currentSelectedGameObject;
        currentInputIndex = go.transform.Find(currentInput.name).GetSiblingIndex();
        maxInputIndex = go.transform.childCount;

        // Manages direction of input focus
        if(inputTabReverse)
        {
            nextInputIndex = currentInputIndex - 1;
            if (nextInputIndex == 0)
            {
                nextInputIndex = maxInputIndex - 1;
            }
        }
        else
        {
            nextInputIndex = currentInputIndex + 1;
            if (nextInputIndex == 4)
            {
                nextInputIndex = minInputIndex;
            }
        }

        // Manages input focus
        if (nextInputIndex < maxInputIndex && nextInputIndex > 0)
        {
            if (go.transform.GetChild(nextInputIndex).gameObject.GetComponent<InputField>())
            {
                go.transform.GetChild(nextInputIndex).gameObject.GetComponent<InputField>().ActivateInputField();
            }
            else
            {
                go.transform.GetChild(nextInputIndex).gameObject.GetComponent<Button>().Select();
            }
        }        
    }
    public void InputFieldCheckIsEmpty(string inputFieldText)
    {
        currentInput = eventSystem.currentSelectedGameObject;

        // Check to see if input field is empty based on length in string
        if (inputFieldText.Length <= 0)
        {
            errorMessage.enabled = true;

            if (currentInput.name == "InputUsername")
            {
                errorMessage.text = "Username field is empty.";
            }
            else if (currentInput.name == "InputPassword")
            {
                errorMessage.text = "Password field is empty.";
            }
            else
            {
                errorMessage.text = "Something went wrong.";
            }
        }
    }
    public void InputFieldClearErrorMessage()
    {
        errorMessage.enabled = false;
    }

    public void LoginButtonClicked()
    {
        bool isEmpty = ButtonLoginCheckInputFields();
        //bool isAuthorized = LoginAuthorizeUser(inputUsername, inputPassword);
    }

    bool ButtonLoginCheckInputFields()
    {   

        if (inputUsername.text.Length <= 0 || inputPassword.text.Length <= 0)
        {
            errorMessage.enabled = true;
            errorMessage.text = "Username or Password is empty.";
            return false;
        }

        return true;
    }

    // Start is called before the first frame update
    void Start()
    {
        go = this.gameObject;
        errorMessage = GameObject.Find("ErrorMessage").GetComponent<Text>();
        eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        inputUsername = go.transform.Find("InputUsername").GetComponent<InputField>();
        inputPassword = go.transform.Find("InputPassword").GetComponent<InputField>();
    }

    // Update is called once per frame
    void Update()
    {

        if (!Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            InputTabManagement(false);
        }
        else if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            InputTabManagement(true);
        } 
    }
}
