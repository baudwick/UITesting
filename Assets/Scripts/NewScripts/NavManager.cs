using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class NavManager : MonoBehaviour
{
    EventSystem _eventSystem;
    GameObject _uiSystem;
    GameObject _currentInput;
    int _currentInputIndex;

    void InputTabManagement(bool inputTabReverse)
    {
        int nextInputIndex;
        int maxInputIndex = _uiSystem.transform.childCount;
        int minInputIndex = 0;

        _currentInput = _eventSystem.currentSelectedGameObject;
        _currentInputIndex = _uiSystem.transform.Find(_currentInput.name).GetSiblingIndex();

        // Manages direction of input focus
        if (inputTabReverse)
        {
            nextInputIndex = (_currentInputIndex == minInputIndex) ? maxInputIndex - 1 : _currentInputIndex - 1;
        }
        else
        {
            nextInputIndex = (_currentInputIndex + 1 == maxInputIndex) ? minInputIndex : _currentInputIndex + 1;
        }

        // Check current input field, if empty display error message
        if (_uiSystem.transform.GetChild(_currentInputIndex).gameObject.GetComponent<InputField>())
        {
            InputFieldCheckIsEmpty();
        }

        // Manages input focus
        if (_uiSystem.transform.GetChild(nextInputIndex).gameObject.GetComponent<InputField>())
        {
            _uiSystem.transform.GetChild(nextInputIndex).gameObject.GetComponent<InputField>().ActivateInputField();
        }
        else if (_uiSystem.transform.GetChild(nextInputIndex).gameObject.GetComponent<Button>())
        {
            _uiSystem.transform.GetChild(nextInputIndex).gameObject.GetComponent<Button>().Select();
        }
    }

    // Checks for empty input fields
    public void InputFieldCheckIsEmpty()
    {
        _currentInput = _eventSystem.currentSelectedGameObject;
        _currentInputIndex = _uiSystem.transform.Find(_currentInput.name).GetSiblingIndex();
        _uiSystem.transform.Find("LoginButton").transform.Find("ErrorMessage").GetComponent<Text>().enabled = false;

        if (_uiSystem.transform.GetChild(_currentInputIndex).GetComponent<InputField>().text.Length <= 0)
        {
            _uiSystem.transform.GetChild(_currentInputIndex).transform.Find("ErrorMessage").GetComponent<Text>().enabled = true;
        }
        else
        {
            _uiSystem.transform.GetChild(_currentInputIndex).transform.Find("ErrorMessage").GetComponent<Text>().enabled = false;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        _eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
        _uiSystem = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.Tab))
        {
            InputTabManagement(true);
        }
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            InputTabManagement(false);
        }
        else if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
    }
}