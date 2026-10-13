using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Put this on a checkpoint trigger. Assign a simple quiz panel and buttons in the Inspector.
// This sample is a starter; add more GES JHS questions as you build the game.
public class LearningCheckpoint : MonoBehaviour
{
    [System.Serializable]
    public class Question
    {
        public string subject = "Mathematics";
        [TextArea] public string prompt = "What is 25% of 200?";
        public string[] answers = { "25", "50", "75", "100" };
        [Range(0, 3)] public int correctAnswerIndex = 1;
        [TextArea] public string explanation = "25% of 200 is 50.";
    }

    public Question question = new Question();
    public GameObject quizPanel;
    public TMP_Text subjectLabel;
    public TMP_Text questionLabel;
    public TMP_Text feedbackLabel;
    public Button[] answerButtons = new Button[4];
    public TMP_Text[] answerLabels = new TMP_Text[4];
    public Button continueButton;

    private bool completed;
    private RaceGameManager manager;

    private void Start()
    {
        manager = FindFirstObjectByType<RaceGameManager>();
        if (quizPanel != null) quizPanel.SetActive(false);
        if (continueButton != null) continueButton.onClick.AddListener(CloseQuiz);
        for (int i = 0; i < answerButtons.Length; i++)
        {
            int captured = i;
            if (answerButtons[i] != null)
                answerButtons[i].onClick.AddListener(() => ChooseAnswer(captured));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (completed || !other.CompareTag("Player")) return;
        completed = true;
        OpenQuiz();
    }

    private void OpenQuiz()
    {
        if (quizPanel == null)
        {
            if (manager != null) manager.SetStatus("Checkpoint reached! Create and assign a quiz panel in the Inspector.");
            return;
        }

        quizPanel.SetActive(true);
        if (subjectLabel != null) subjectLabel.text = question.subject;
        if (questionLabel != null) questionLabel.text = question.prompt;
        if (feedbackLabel != null) feedbackLabel.text = "Choose an answer.";
        if (continueButton != null) continueButton.gameObject.SetActive(false);

        for (int i = 0; i < answerButtons.Length; i++)
        {
            bool hasAnswer = i < question.answers.Length;
            if (answerButtons[i] != null) answerButtons[i].gameObject.SetActive(hasAnswer);
            if (hasAnswer && i < answerLabels.Length && answerLabels[i] != null)
                answerLabels[i].text = question.answers[i];
            if (answerButtons[i] != null) answerButtons[i].interactable = true;
        }
        Time.timeScale = 0f;
    }

    private void ChooseAnswer(int index)
    {
        bool correct = index == question.correctAnswerIndex;
        if (feedbackLabel != null)
            feedbackLabel.text = correct
                ? "Correct! " + question.explanation
                : "Not quite. " + question.explanation;

        if (correct && manager != null) manager.AnswerCorrectly();

        for (int i = 0; i < answerButtons.Length; i++)
            if (answerButtons[i] != null) answerButtons[i].interactable = false;

        if (continueButton != null) continueButton.gameObject.SetActive(true);
    }

    private void CloseQuiz()
    {
        Time.timeScale = 1f;
        if (quizPanel != null) quizPanel.SetActive(false);
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        if (Time.timeScale == 0f) Time.timeScale = 1f;
    }
}