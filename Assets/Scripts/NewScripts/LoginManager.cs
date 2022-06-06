using UnityEngine;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public string username = null;
    public string password = null;
    public bool isAuthorized = false;

    GameObject _uiSystem;
    Button _loginButton;
    DBManager _dbManager;

    void LoginCheckUserAuthorization()
    {
        username = _uiSystem.transform.Find("InputUsername").GetComponent<InputField>().text;
        password = _uiSystem.transform.Find("InputPassword").GetComponent<InputField>().text;
        isAuthorized = _dbManager.GetUserAuthorization(username, password);
        
        if(isAuthorized)
        {
            _uiSystem.SetActive(false);
            GameObject.Find("WelcomeMessage").transform.Find("Text").GetComponent<Text>().enabled = true;
            GameObject.Find("WelcomeMessage").transform.Find("Text").GetComponent<Text>().text = "Welcome!";
        }
        else
        {
            if (username.Length <= 0 || password.Length <= 0)
            {
                _loginButton.transform.Find("ErrorMessage").GetComponent<Text>().enabled = true;
                _loginButton.transform.Find("ErrorMessage").GetComponent<Text>().text = "Username or Password is empty.";
            }
            else
            {
                _loginButton.transform.Find("ErrorMessage").GetComponent<Text>().enabled = true;
                _loginButton.transform.Find("ErrorMessage").GetComponent<Text>().text = "Username or password was incorrect.";
            }
            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        _uiSystem = GameObject.Find("UISystem");
        _loginButton = _uiSystem.transform.Find("LoginButton").GetComponent<Button>();
        _dbManager = this.GetComponent<DBManager>();
        _loginButton.onClick.AddListener(LoginCheckUserAuthorization);
    }

    // Update is called once per frame
    void Update()
    {
    }
}
