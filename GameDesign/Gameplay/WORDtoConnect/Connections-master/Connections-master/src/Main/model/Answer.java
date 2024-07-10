package Main.model;

import Main.ui.OptionButton;

public class Answer {

    private String answer;
    private Group group;
    private OptionButton optionButton;

    public Answer(String desc) {
        this.answer = desc;
        this.group = null;
    }

    public String getAnswer() {
        return answer;
    }

    public void setGroup(Group answer) {
        if (group == null) {
            group = answer;
        }
    }

    public void solved(Group group) {
        optionButton.setSelectable(group);
    }

    public void deselect() {
        optionButton.deselect();
    }

    public void addButton(OptionButton optionButton) {
        this.optionButton = optionButton;
    }
}
