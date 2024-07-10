package Main.ui;

import javax.swing.*;
import java.awt.*;
import java.awt.event.ActionEvent;
import java.awt.event.ActionListener;

public class DeselectButton implements ActionListener {

    private ConnectionsGame game;
    private Font font;
    private JButton button;

    public DeselectButton(ConnectionsGame game, JComponent parent) {
        this.game = game;
        this.font = new Font(Font.SANS_SERIF, 1, 25);
        createButton(parent);
    }

    private void createButton(JComponent parent) {
        button = new JButton("Deselect All");
        button.setBackground(new Color(180,180,180));
        button.setFont(font);
        button.addActionListener(this);
        parent.add(button);
    }

    @Override
    public void actionPerformed(ActionEvent e) {
        game.deselectAll();
    }
}
