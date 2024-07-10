package Main.ui;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class SubmitButton implements ActionListener {

    private ConnectionsGame game;
    private Font font;
    private JButton button;
    private boolean ready;

    public SubmitButton(ConnectionsGame game, JComponent parent) {
        this.game = game;
        this.font = new Font(Font.SANS_SERIF, 1, 25);
        this.ready = false;
        createButton(parent);
    }

    private void createButton(JComponent parent) {
        button = new JButton("SUBMIT");
        button.setBackground(new Color(180,180,180));
        button.setFont(font);
        button.addActionListener(this);
        parent.add(button);
    }

    public Boolean getReady() {
        return ready;
    }

    public void Ready() {
        ready = true;
        button.setBackground(new Color(17, 255, 21));
    }

    public void unReady() {
        ready = false;
        button.setBackground(new Color(180,180,180));
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        game.submit();
    }
}
