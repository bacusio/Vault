package Main.model;

import java.awt.*;
import java.util.ArrayList;
import java.util.List;

public class Group {

    private List<Answer> correctAnswers;
    private String topic;
    private Boolean solved;
    private Color solvedColor;

    public Group(String topic) {
        this.topic = topic;
        this.correctAnswers = new ArrayList<>();
        this.solved = false;
    }

    public String getTopic() {
        return topic;
    }

    public void setCorrectAnswer(Answer answer) {
        correctAnswers.add(answer);
        answer.setGroup(this);
    }

    public List<Answer> getCorrectAnswers() {
        return correctAnswers;
    }

    public Boolean getSolved() {
        return solved;
    }

    public void solve() {
        solved = true;
    }

    public void setSolvedColor(Color color) {
        this.solvedColor = color;
    }

    public Color getSolvedColor() {
        return solvedColor;
    }

    public void buildGroupConstant(String ans1, String ans2, String ans3, String ans4) {
        Answer answer1 = new Answer(ans1);
        Answer answer2 = new Answer(ans2);
        Answer answer3 = new Answer(ans3);
        Answer answer4 = new Answer(ans4);
        this.setCorrectAnswer(answer1);
        this.setCorrectAnswer(answer2);
        this.setCorrectAnswer(answer3);
        this.setCorrectAnswer(answer4);
    }
}
