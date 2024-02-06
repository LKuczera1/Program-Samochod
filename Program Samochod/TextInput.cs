using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa pola tekstowego
public class TextInput : GUI_Object
{
    Vector2f position;
    Vector2f size;

    GUI_Caption text;
    SFML.Graphics.Color FillColor, BordersColor;
    RectangleShape inputArea;
    RectangleShape pointer;

    bool isSelected;

    //wykorzystanie własciwości get/set

    public float _maxNumberOfCharacters;
    public float maxNumberOfCharacters
    {
        get { return _maxNumberOfCharacters; }
        set { _maxNumberOfCharacters = value; }
    }

    public string caption
    {
        get { return text.caption; }
        set { text.caption = value; }
    }

    public TextInput(Vector2f position, Vector2f size, SFML.Graphics.Color FillColor, SFML.Graphics.Color BordersColor)
    {
        text = new GUI_Caption(new SFML.Graphics.Font(Directory.GetCurrentDirectory() + "\\Resources\\Font\\GillSansMT.TTF"), "", position, 40, SFML.Graphics.Color.Black);
        inputArea = new RectangleShape();
        pointer = new RectangleShape();
        this.FillColor = FillColor;
        this.BordersColor = BordersColor;
        this.position = position;
        this.size = size;

        //Przyjęto, że średnia długośc znaku to 20 jednostek
        _maxNumberOfCharacters = size.X / 20;

        inputArea.Position = position;
        inputArea.OutlineColor = BordersColor;
        inputArea.FillColor = FillColor;
        inputArea.Size = size;

        pointer.Size = new Vector2f(0, size.Y - 12);
        pointer.OutlineThickness = 2;
        pointer.OutlineColor = SFML.Graphics.Color.Black;

        isSelected = false;
    }

    ~TextInput()
    {
        text = null;
        inputArea = null;

        GC.Collect();
    }
    public override void draw(RenderTexture target)
    {
        target.Draw(inputArea);
        text.draw(target);

        if (isSelected)
        {
            if (DateTime.Now.Second % 2 == 0) target.Draw(pointer);
            pointer.Position = new Vector2f(text.text.GetGlobalBounds().Width + text.text.GetGlobalBounds().Left + 4, position.Y + 6);
        }
    }

    public override void update(Vector2i mousePosition, bool isLMBPressed, List<Keyboard.Key> pressedKeys)
    {
        if (mousePosition.X > position.X && mousePosition.X < position.X + size.X &&
            mousePosition.Y > position.Y && mousePosition.Y < position.Y + size.Y)
        {
            if (isLMBPressed)
            {
                isSelected = true;
                pointer.Position = new Vector2f(text.text.GetGlobalBounds().Width + text.text.GetGlobalBounds().Left + 4, position.Y + 6);
            }

        }
        else
        {
            if (isLMBPressed)
            {
                isSelected = false;
            }
        }


        bool shiftPressed = false;

        while (pressedKeys.Count != 0 && isSelected)
        {

            char character = new char();
            bool inputKeyDetected = false;


            if (Keyboard.IsKeyPressed(Keyboard.Key.LShift) || Keyboard.IsKeyPressed(Keyboard.Key.RShift))
            {
                shiftPressed = true;
            }

            //tłumaczenie z enum Key na typ char
            if (pressedKeys[0] >= Keyboard.Key.A && pressedKeys[0] <= Keyboard.Key.Z && text.caption.Length < maxNumberOfCharacters)
            {
                if (shiftPressed) character = ((char)(pressedKeys[0] + 65));
                else character = ((char)(pressedKeys[0] + 97));

                inputKeyDetected = true;
            }
            else if (pressedKeys[0] >= Keyboard.Key.Num0 && pressedKeys[0] <= Keyboard.Key.Num9 && text.caption.Length < maxNumberOfCharacters)
            {
                character = ((char)(pressedKeys[0] + 22));
                inputKeyDetected = true;
            }
            else if (pressedKeys[0] == Keyboard.Key.BackSpace)
            {
                if (text.caption.Length > 0) text.caption = text.caption.Remove(text.caption.Length - 1);
            }
            else if (pressedKeys[0] == Keyboard.Key.Space)
            {
                if (text.caption.Length > 0) text.caption += " ";
            }

            if (inputKeyDetected) text.caption += character;

            pressedKeys.RemoveAt(0);
        }
    }
}