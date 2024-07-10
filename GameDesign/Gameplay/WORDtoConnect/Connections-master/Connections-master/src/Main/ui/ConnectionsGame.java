package Main.ui;

import Main.model.Answer;
import Main.model.Group;

import javax.swing.*;
import java.awt.*;
import java.util.ArrayList;
import java.util.Collections;
import java.util.List;
import java.util.Scanner;

public class ConnectionsGame extends JFrame {

    private int mistakesAllowed;
    private List<Answer> selectedOptions;
    public static final int WIDTH = 800;
    public static final int HEIGHT = 800;
    public Scanner input = new Scanner(System.in);
    private List<Group> groups;
    private List<Answer> options;
    private SubmitButton submitButton;
    private Group Oceanography = new Group("Oceanography");
    private Group SpaceExploration = new Group("Space Exploration");
    private Group CulinaryArts = new Group("Culinary Things");
    private Group TechnologyTrends = new Group("Technology Trends");
    private Group NHLTeams = new Group("NHL Teams");
    private Group ThingSaidTwice = new Group("Things said in succession");
    private Group TypesOfShoes = new Group("Types of shoes");
    private Group BlankBall = new Group("_____ ball");
    private Group SpiderStuff = new Group("Spider Things");
    private Group HairBlank = new Group("Hair _____");
    private Group Homophones = new Group("Homophones");
    private Group TVShowComps = new Group("TV Components");
    private Group FighterPlanes = new Group("Fighter Planes");
    private Group GolfStuff = new Group("Things found on a golf course");
    private Group Burgers = new Group("Types of burger");
    private Group TypesOfPhysics = new Group("Fields of Physics");
    private Group Airlines = new Group("Airlines");
    private Group GreekLetters = new Group("Greek Letters");
    private Group AppleDevices = new Group("Apple devices that start with an i");
    private Group CodeLanguages = new Group("Code Languages");
    private List<Group> groupsConstant = new ArrayList<>();

    public ConnectionsGame() {
        super("Connections! (By Jack)");
        setupConstants();
        System.out.println("Do you want to (M)ake your own prompts or use (G)iven ones?");
        String choice = input.nextLine();
        choice = choice.toLowerCase();
        if (choice.equals("m")) {
            groups = setupGroups();
        } else if (choice.equals("g")) {
            groups = randomizeGroups();
        } else {
            System.out.println("Invalid. Restart to try again.");
            System.out.println("Plz try again its really fun! :)");
            dispose();
        }
        groups.get(0).setSolvedColor(new Color(40, 112, 255));
        groups.get(1).setSolvedColor(new Color(243,255,46));
        groups.get(2).setSolvedColor(new Color(56, 201, 31));
        groups.get(3).setSolvedColor(new Color(168, 114, 255));
        options = setupOptions();
        this.mistakesAllowed = 4;
        this.selectedOptions = new ArrayList<>();
        init();
        setDefaultCloseOperation(JFrame.EXIT_ON_CLOSE);
        setLocationRelativeTo(null);
        setVisible(true);
    }

    private List<Group> randomizeGroups() {
        Collections.shuffle(groupsConstant);
        return groupsConstant.subList(0,4);
    }

    private void setupConstants() {
        Oceanography.buildGroupConstant("Coral", "Tide","Seabed","Fish");
        groupsConstant.add(Oceanography);
        SpaceExploration.buildGroupConstant("Rocket", "Galaxy", "Nebula", "Astro");
        groupsConstant.add(SpaceExploration);
        CulinaryArts.buildGroupConstant("Recipe", "Spice", "Utensils", "Cook");
        groupsConstant.add(CulinaryArts);
        TechnologyTrends.buildGroupConstant("Chain","Automation","AI","Crypto");
        groupsConstant.add(TechnologyTrends);
        NHLTeams.buildGroupConstant("Blues", "Senators", "Flames", "Sharks");
        groupsConstant.add(NHLTeams);
        ThingSaidTwice.buildGroupConstant("Hear","Aye","Tut","Ho");
        groupsConstant.add(ThingSaidTwice);
        TypesOfShoes.buildGroupConstant("Dunks","Runners","Joggers","Cowboy");
        groupsConstant.add(TypesOfShoes);
        BlankBall.buildGroupConstant("Base","Basket","Cheese","Chocolate");
        groupsConstant.add(BlankBall);
        SpiderStuff.buildGroupConstant("Web","Silk","Eggs","Venom");
        groupsConstant.add(SpiderStuff);
        HairBlank.buildGroupConstant("Brush","Clip","Tie","Spray");
        groupsConstant.add(HairBlank);
        Homophones.buildGroupConstant("Eight","Flower","Cell","Know");
        groupsConstant.add(Homophones);
        TVShowComps.buildGroupConstant("Pilot","Episode","Finale","Season");
        groupsConstant.add(TVShowComps);
        FighterPlanes.buildGroupConstant("Spitfire","Hurricane","Typhoon","Lightning");
        groupsConstant.add(FighterPlanes);
        GolfStuff.buildGroupConstant("Club","Cart","Ball","Tee");
        groupsConstant.add(GolfStuff);
        Burgers.buildGroupConstant("Whopper","Big Mac","Slider","Smash");
        groupsConstant.add(Burgers);
        TypesOfPhysics.buildGroupConstant("Thermodynamics","Mechanics","Relativity","Quantum");
        groupsConstant.add(TypesOfPhysics);
        Airlines.buildGroupConstant("Delta","Bombardier","Flair","Alaskan");
        groupsConstant.add(Airlines);
        GreekLetters.buildGroupConstant("Zeta","Sigma","Theta","Kappa");
        groupsConstant.add(GreekLetters);
        AppleDevices.buildGroupConstant("Phone","Pad","Mac","Pod");
        groupsConstant.add(AppleDevices);
        CodeLanguages.buildGroupConstant("Python","C","R","Java");
        groupsConstant.add(CodeLanguages);
    }

    private List<Group> setupGroups() {
        List<Group> groups = new ArrayList<>();
        groups.add(setupGroup());
        groups.add(setupGroup());
        groups.add(setupGroup());
        groups.add(setupGroup());
        return groups;
    }

    private Group setupGroup() {
        System.out.println("Group theme: ");
        Group group = new Group(input.nextLine());
        System.out.println("First answer: ");
        Answer firstAnswer = new Answer(input.nextLine());
        group.setCorrectAnswer(firstAnswer);
        System.out.println("Second answer: ");
        Answer secondAnswer = new Answer(input.nextLine());
        group.setCorrectAnswer(secondAnswer);
        System.out.println("Third answer: ");
        Answer thirdAnswer = new Answer(input.nextLine());
        group.setCorrectAnswer(thirdAnswer);
        System.out.println("Forth answer: ");
        Answer forthAnswer = new Answer(input.nextLine());
        group.setCorrectAnswer(forthAnswer);
        System.out.println("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
        return group;
    }

    private List<Answer> setupOptions() {
        List<Answer> allAnswers = new ArrayList<>();
        for (Group group : groups) {
            allAnswers.addAll(group.getCorrectAnswers());
        }
        Collections.shuffle(allAnswers);
        return allAnswers;
    }

    private void init() {
        setLayout(new BorderLayout());
        setMinimumSize(new Dimension(WIDTH, HEIGHT));
        JPanel optionsArea = new JPanel(new GridLayout(4,4,15,15));
        add(optionsArea, BorderLayout.CENTER);
        for (Answer answer : options) {
            new OptionButton(this, optionsArea, answer);
        }
        JPanel buttonArea = new JPanel(new FlowLayout(FlowLayout.CENTER));
        submitButton = new SubmitButton(this, buttonArea);
        SoFarButton soFarButton = new SoFarButton(this, buttonArea);
        DeselectButton deselectButton = new DeselectButton(this,buttonArea);
        add(buttonArea, BorderLayout.SOUTH);
    }

    public void select(OptionButton optionButton) {
        if (!optionButton.getSelected() && selectedOptions.size() != 4) {
            optionButton.select();
            selectedOptions.add(optionButton.getAnswer());
            if (selectedOptions.size() == 4) {
                submitButton.Ready();
            }
        } else if (selectedOptions.size() == 4 && !optionButton.getSelected()) {
            JOptionPane.showMessageDialog(null, "You have already selected 4 options!");
        } else {
            optionButton.deselect();
            selectedOptions.remove(optionButton.getAnswer());
            submitButton.unReady();
        }
    }

    public void submit() {
        if (submitButton.getReady()) {
            if (checkFirstGroup()) {
                correctAnswer(groups.get(0));
            } else if (checkSecondGroup()) {
                correctAnswer(groups.get(1));
            } else if (checkThirdGroup()) {
                correctAnswer(groups.get(2));
            } else if (checkForthGroup()) {
                correctAnswer(groups.get(3));
            } else {
                mistakesAllowed--;
                JOptionPane.showMessageDialog(null, "Incorrect\nYou have " + mistakesAllowed
                        + " incorrect attempts left.");
            }
            checkGameOver();
        }
    }

    private void checkGameOver() {
        String Answers = "Answers:\n";
        for (Group group : groups) {
            Answers += "- " + group.getTopic() + ": \n";
            for (Answer answer : group.getCorrectAnswers()) {
                Answers += "       -- " + answer.getAnswer() + "\n";
            }
        }
        if (mistakesAllowed == 0) {
            JOptionPane.showMessageDialog(null, "Game over! :/");
            JOptionPane.showMessageDialog(null, Answers);
            dispose();
        } else if (checkAllSolved()) {
            JOptionPane.showMessageDialog(null, "Correct! Well done!");
        }
    }

    private boolean checkAllSolved() {
        for (Group group : groups) {
            if (!group.getSolved()) {
                return false;
            }
        }
        return true;
    }

    private void correctAnswer(Group group) {
        for (Answer ans : selectedOptions) {
            ans.solved(group);
        }
        group.solve();
        JOptionPane.showMessageDialog(null, "Correct! The group was " + group.getTopic() + ".");
        selectedOptions.removeAll(selectedOptions);
        submitButton.unReady();
    }

    private boolean checkForthGroup() {
        for (Answer ans : selectedOptions) {
            if (!groups.get(3).getCorrectAnswers().contains(ans)) {
                return false;
            }
        }
        return true;
    }

    private boolean checkThirdGroup() {
        for (Answer ans : selectedOptions) {
            if (!groups.get(2).getCorrectAnswers().contains(ans)) {
                return false;
            }
        }
        return true;
    }

    private boolean checkSecondGroup() {
        for (Answer ans : selectedOptions) {
            if (!groups.get(1).getCorrectAnswers().contains(ans)) {
                return false;
            }
        }
        return true;
    }

    private boolean checkFirstGroup() {
        for (Answer ans : selectedOptions) {
            if (!groups.get(0).getCorrectAnswers().contains(ans)) {
                return false;
            }
        }
        return true;
    }

    public void soFar() {
        String soFar = "Answers gotten so far:\n";
        for (Group group : groups) {
            if (group.getSolved()) {
                soFar += " - " + group.getTopic() + "\n";
                for (Answer answer : group.getCorrectAnswers()) {
                    soFar += "       -- " + answer.getAnswer() + "\n";
                }
            }
        }
        JOptionPane.showMessageDialog(null, soFar);
    }

    public void deselectAll() {
        for (Answer button : selectedOptions) {
            button.deselect();
        }
        selectedOptions.removeAll(selectedOptions);
        submitButton.unReady();
    }
}
