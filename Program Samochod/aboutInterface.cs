using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa interfejsu "O Programie"
public class aboutInterface : Interface
{
    List<GUI_Object> GUI_Objects;

    Sprite background;

    Font font;
    public aboutInterface(CarSimulator feedback) : base(feedback)
    {
        font = new Font(Directory.GetCurrentDirectory() + "\\Resources\\Font\\GillSansMT.TTF");

        GUI_Objects = new List<GUI_Object>();

        GUI_Objects.Add(new GUI_aboutInterfaceBackButton(new Vector2f(820, 800), new Vector2f(280, 50), font, "Powrót", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));

        background = new Sprite(feedback.getTexture(resourcesContainer.textureCode.background));

        StreamReader file;
        string line;

        //Wczytywanie opisu z pliku
        try
        {
            file = new StreamReader(Directory.GetCurrentDirectory() + "\\Resources\\data\\aboutProgram.txt");

            line = file.ReadLine();

            int lenghtOfFile = Int32.Parse(line);

            if (lenghtOfFile < 0 || lenghtOfFile > 1000) throw new Exception("an error occured during file reading");

            for (int i = 0; i < lenghtOfFile; i++)
            {
                line = file.ReadLine();

                GUI_Objects.Add(new GUI_Caption(font, line, new Vector2f(450, 280 + (i * 40)), 40, new Color(250, 250, 250)));
            }

        }
        catch (Exception e)
        {

        }
    }

    ~aboutInterface()
    {
        for(int i=0;i< GUI_Objects.Count;i++)
        {
            GUI_Objects[i] = null;
        }
        GUI_Objects = null;
        font = null;
        background = null;
        GC.Collect();
    }
    //Rysowanie interfejsu
    public override void Render(RenderTexture drawTexture)
    {
        drawTexture.Draw(background);
        foreach (GUI_Object i in GUI_Objects)
        {
            i.draw(drawTexture);
        }
    }
    //Aktualizacja interfejsu
    public override void Update(List<Keyboard.Key> pressedKeys)
    {
        foreach (GUI_Object i in GUI_Objects)
        {
            i.update(Mouse.GetPosition(feedback.window), Mouse.IsButtonPressed(Mouse.Button.Left), pressedKeys);
        }
    }
    //Informacja zwrotna o powrocie do menu głównego
    public void goBack()
    {
        feedback.mainMenu();
    }
}