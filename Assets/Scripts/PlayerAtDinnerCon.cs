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

    Queue<string> marieDialoagues = new Queue<string>(new[]
    {
        "Connor: Hey! Can I get you a drink?",
        "Marié: Sure. Surprise me!",
        "Choose a drink to impress Marié"
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

        if (!isDialogueDone(marieDialoagues) && !isNearMarie() && !marieConversationStarted) {
            subtitle.text = "You're at Alinéa now. Move towards Marié and initiate a conversation.";
        } else if (!isDialogueDone(marieDialoagues) && isNearMarie() && !marieConversationStarted) {
            subtitle.text = "You're near Marié, press [Space Bar] to talk to her";
        }

        if (Input.GetKeyUp(KeyCode.Space)) 
        {
            if (restartGame) 
            {
                var nextIndex = SceneManager.GetActiveScene().buildIndex;
                SceneManager.LoadScene(nextIndex);
            }

            if (!isDialogueDone(marieDialoagues) && isNearMarie())
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

        if(currentIndex >= conversations.Length && isDialogueDone(marieDialoagues)) {
            var nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadScene(nextIndex);
        }
	}

    IEnumerator waiter()
    {
        yield return new WaitForSecondsRealtime(4);
    }

    bool isNearMarie() {
        var width = GetComponent<UnityEngine.UI.Image>().sprite.rect.width;
        bool result = (int)Mathf.Abs(transform.position.x - woman.transform.position.x) <= width + 50;

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
            subtitle.text = "Hmm...that's interesting I guess";
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
        Queue<string> convoQueue1 = new Queue<string>(new[]
        {
            "Marié: Ah good choice! You read my mind monsieur",
            "Connor: Glad you liked it!",
            "Marié: So what do you do for a living?"
        });
        string[] options1 = { "Software Developer", "Stock Broker", "Art Gallery Owner" }; // Correct answer is 3

        Queue<string> convoQueue2 = new Queue<string>(new[]
        {
            "Marié: Really? I'm an art collector myself.",
            "Connor: Wow that is interesting, you should visit our gallery sometime.",
            "Marié: I will surely, who's your favorite artist?"
        });
        string[] options2 = { "Billy Bob", "Vincent Van Gogh", "Banksy" }; // Correct answer is 2

        Queue<string> convoQueue3 = new Queue<string>(new[]
        {
            "Marié: Mine too! I have the 'Wheat Field with Cypresses' at my house.",
            "Connor: No way... That is mine and my brother's favorite painting of Van Gogh.",
            "Marié: Why don't you gentlemen come to my house and I'll show you my art collection.",
            "Connor: We would love to!",
            "..."
        });

        Conversation convo1 = new Conversation(2, convoQueue1, options1);
        Conversation convo2 = new Conversation(3, convoQueue2, options2);
        Conversation convo3 = new Conversation(2, convoQueue3, options1);

        conversations = new[] { convo1, convo2, convo3 };
    }
}
