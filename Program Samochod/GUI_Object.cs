using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa bazowa obiektów interfejsu
public class GUI_Object
{
    public GUI_Object()
    {

    }

    ~GUI_Object()
    {

    }
    public virtual void draw(RenderTexture target)
    {

    }

    public virtual void update(Vector2i mousePosition, bool isLMBPressed, List<Keyboard.Key> pressedKeys)
    {

    }
}