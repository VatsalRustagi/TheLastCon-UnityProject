using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Threading;
using UnityEngine.SceneManagement;

public class Conversation {

    private int rightAnswer;
    private Queue<string> rightConversation;
    private string[] answerOptions;

    public int RightAnswer 
    {
        get {
            return rightAnswer; 
        }
        private set {
            rightAnswer = value;
        }
    }

    public Queue<string> RightConversation
    {
        get { 
            return rightConversation;
        }
        private set {
            rightConversation = value;
        }
    }

    public string[] AnswerOptions
    {
        get { 
            return answerOptions; 
        }
        private set {
            answerOptions = value;
        }
    }

    public Conversation(int rightAnswer, Queue<string> rightConversation, string[] answerOptions) {
        this.rightAnswer = rightAnswer;
        this.rightConversation = rightConversation;
        this.answerOptions = answerOptions;
    }
}

public class PlayerAtDinnerCon : MonoBehaviour {

    [SerializeField] Button optionOne;
    [SerializeField] Button optionTwo;
    [SerializeField] Button optionThree;
    [SerializeField] Text subtitle;
    [SerializeField] Image woman;



    int currentIndex = 0;

    Conversation[] conversations;

    Button[] buttons;

    string restartText = "You have failed to impress her. Press [Space Bar] to restart this level now.";

    Queue<string> playerDialogues = new Queue<string>(new[]
    {
        "Future Self: So that right there is Marié. Head over and talk to her",
        "Kyle: Ok gotcha, wait here let me get us invited to her place",
        "Use the arrow keys to move towards Marié and initiate a conversation"
    });

    Queue<string> marieDialoagues = new Queue<string>(new[]
    {
        "Kyle: Hey! Can I get you a drink?",
        "Marié: Sure. Surprise me!",
        "Choose a drink to impress Marie"
    });

    bool restartGame = false;
    bool marieConversationStarted = false;

    // Use this for initialization
    void Start () {
        optionOne.onClick.AddListener(() => buttonClicked(1));
        optionTwo.onClick.AddListener(() => buttonClicked(2));
        optionThree.onClick.AddListener(() => buttonClicked(3));
        buttons = new[] { optionOne, optionTwo, optionThree };
        initializeConversations();
        setActiveButtons(false);
        restartGame = false;
    }
	
	// Update is called once per frame
	void Update() {
       
        Vector3 position = GetComponent<UnityEngine.UI.Image>().transform.position;

        if (!isDialogueDone(marieDialoagues) && isDialogueDone(playerDialogues) && !isNearMarie() && !marieConversationStarted) {
            subtitle.text = "Use the arrow keys to move towards Marié and initiate a conversation";
        } else if (!isDialogueDone(marieDialoagues) && isDialogueDone(playerDialogues) && isNearMarie() && !marieConversationStarted) {
            subtitle.text = "You're near Marie, press [Space Bar] to begin conversation";
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            if (restartGame) 
            {
                var nextIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(nextIndex);
            }

            if (!isDialogueDone(playerDialogues))
            {
                subtitle.text = playerDialogues.Dequeue();
            }
            else if (!isDialogueDone(marieDialoagues) && isNearMarie())
            {
                subtitle.text = marieDialoagues.Dequeue();
                marieConversationStarted = true;

                if (subtitle.text.Equals(restartText)) {
                    restartGame = true;
                }

                if (isDialogueDone(marieDialoagues) && conversations.Length > currentIndex && restartGame == false) {
                    setActiveButtons(true);
                }
            }
        }
	}

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(4);
    }

    bool isNearMarie() {
        bool result = (int)Mathf.Abs(transform.position.x - woman.transform.position.x) <= 100;

        return result;
    }

    void buttonClicked(int id) {
        setActiveButtons(false);

        if (conversations.Length <= currentIndex) {
            return;
        }

        if(id == conversations[currentIndex].RightAnswer) {
            marieDialoagues = conversations[currentIndex].RightConversation;
            subtitle.text = marieDialoagues.Dequeue();
            setButtonValues(conversations[currentIndex].AnswerOptions);
            currentIndex++;
        } 
        else {
            subtitle.text = "Hmm...that's an interesting choice";
            marieDialoagues.Enqueue(restartText);
        }
    }

    void setButtonValues(string[] textOptions) {

        if (textOptions.Length != 3) 
        {
            Debug.LogError("textOptions length should be 3");
            return;
        }

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].GetComponentInChildren<Text>().text = textOptions[i];
        }
        
    }

    void setActiveButtons(bool active) {

        foreach (Button button in buttons)
        {
            button.gameObject.SetActive(active);
        }
    }

    bool isDialogueDone(Queue<string> queue) {
        return queue.Count == 0;
    }

    void initializeConversations() {
        Queue<string> conversation1 = new Queue<string>(new[]
        {
            "Marie: Ah good choice! You read my mind monsieur",
            "Kyle: Nah I'm just cool like that",
            "Marie: So what do you do for a living?"
        });
        string[] options1 = { "Software Developer", "Art Gallery Owner", "Stock Broker" };

        Queue<string> conversation4 = new Queue<string>(new[]
        {
            "You done homie"
        });
        Conversation convo1 = new Conversation(2, conversation1, options1);
        Conversation convo4 = new Conversation(2, conversation4,  options1);
        conversations = new[] { convo1, convo4 };
    }
}
