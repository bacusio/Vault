package Main.ui;

import Main.model.Answer;
import Main.model.Group;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class OptionButton implements ActionListener {

    private ConnectionsGame game;
    private Font font;
    private JButton button;
    private boolean selected;
    private Answer answer;
    private Boolean selectable;

    public OptionButton(ConnectionsGame game, JComponent parent, Answer answer) {
        this.game = game;
        this.font = new Font(Font.SANS_SERIF, 1, 18);
        this.selected = false;
        this.answer = answer;
        this.selectable = true;
        createButton(parent, this.answer.getAnswer());
        this.answer.addButton(this);
    }

    private void createButton(JComponent parent, String desc) {
        button = new JButton(desc);
        button.setBackground(new Color(180, 180, 180));
        button.setFont(font);
        button.addActionListener(this);
        parent.add(button);
    }

    public Boolean getSelected() {
        return selected;
    }

    public void select() {
        selected = true;
        button.setBackground(new Color(117, 117, 117));
    }

    public void deselect() {
        selected = false;
        button.setBackground(new Color(180,180,180));
    }

    public Answer getAnswer() {
        return answer;
    }

    public boolean getSelectable() {
        return selectable;
    }

    public void setSelectable(Group group) {
        selectable = false;
        button.setBackground(group.getSolvedColor());
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        if (getSelectable()) {
            game.select(this);
        }
    }
}
