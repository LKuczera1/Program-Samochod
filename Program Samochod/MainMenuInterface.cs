using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Interfejs menu głównego
public class MainMenuInterface : Interface
{
    List<GUI_Object> GUI_Objects;

    Sprite background;

    Font font;
    public MainMenuInterface(CarSimulator feedback) : base(feedback)
    {
        font = new Font(Directory.GetCurrentDirectory() + "\\Resources\\Font\\GillSansMT.TTF");

        GUI_Objects = new List<GUI_Object>();

        GUI_Objects.Add(new GUI_carSelectorButton(new Vector2f(820, 500), new Vector2f(280, 50), font, "Rozpocznij", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        GUI_Objects.Add(new GUI_aboutInterfaceButton(new Vector2f(820, 600), new Vector2f(280, 50), font, "O Programie", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        GUI_Objects.Add(new GUI_exitProgram(new Vector2f(820, 700), new Vector2f(280, 50), font, "Wyjscie", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));

        background = new Sprite(feedback.getTexture(resourcesContainer.textureCode.background));
    }

    ~MainMenuInterface()
    {

    }
    public override void Render(RenderTexture drawTexture)
    {
        drawTexture.Draw(background);
        foreach (GUI_Object i in GUI_Objects)
        {
            i.draw(drawTexture);
        }
    }
    public override void Update(List<Keyboard.Key> pressedKeys)
    {
        foreach (GUI_Object i in GUI_Objects)
        {
            i.update(Mouse.GetPosition(feedback.window), Mouse.IsButtonPressed(Mouse.Button.Left), pressedKeys);
        }
    }


    public void driverData()
    {
        feedback.driverDataInput();
    }

    public void aboutPage()
    {
        feedback.aboutPage();
    }

    void exit()
    {
        feedback.exit();
    }

    //--------------klasy wewnętrzne

    class GUI_exitProgram : GUI_Button
    {
        MainMenuInterface feedback;
        public GUI_exitProgram(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, MainMenuInterface feedback)
            : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
        {
            this.feedback = feedback;
        }

        ~GUI_exitProgram()
        {

        }

        public override void onClick()
        {
            feedback.exit();
        }
    }
}