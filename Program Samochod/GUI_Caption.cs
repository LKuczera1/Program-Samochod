using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa napisu
public class GUI_Caption : GUI_Object
{
    public Font font;
    public string caption;
    public Vector2f position;
    public uint size;
    public Text text;
    Color captionColor;
    public GUI_Caption(Font font, string caption, Vector2f position, uint fontSize, Color captionColor) : base()
    {
        this.font = font;
        this.caption = caption;
        this.position = position;
        this.size = fontSize;
        this.captionColor = captionColor;

        text = new Text(caption, font, size);
        text.Position = position;
        text.Color = captionColor;
    }

    ~GUI_Caption()
    {

    }

    void setPosition(Vector2f position)
    {
        this.position = position;

        text.Position = position;
    }

    public override void draw(RenderTexture target)
    {
        text.DisplayedString = caption;
        target.Draw(text);
    }
    public override void update(Vector2i mousePosition, bool isLMBPressed, List<Keyboard.Key> pressedKeys) //LMB - Left Mouse Button
    {

    }
}