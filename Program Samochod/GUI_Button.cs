using SFML.Graphics;
using SFML.System;
using SFML.Window;

//klasa bazowa przycisku
public class GUI_Button : GUI_Caption
{
    Vector2f size;
    Vector2f buttonPosition;
    Color backgroundColor;
    Color activeColor;
    Color onClickColor;
    RectangleShape rectangle;

    enum buttonState : int
    {
        defaultState = 0,
        active = 1,
        clicked = 2
    }

    buttonState state;

    const int captionVerticalOffset = 8;
    public GUI_Button(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor) : base(font, caption, position, fontSize, captionColor)
    {
        this.buttonPosition = position;
        this.size = size;

        this.position.Y += (size.Y / 2 - this.text.GetGlobalBounds().Height / 2 - captionVerticalOffset);
        this.position.X += (size.X / 2 - this.text.GetGlobalBounds().Width / 2);
        text.Position = this.position;

        this.backgroundColor = backgroundColor;
        this.activeColor = activeColor;
        this.onClickColor = onClickColor;

        rectangle = new RectangleShape();
        rectangle.Position = buttonPosition;
        rectangle.Size = size;
        rectangle.FillColor = backgroundColor;

    }

    ~GUI_Button()
    {

    }

    public virtual void onClick()
    {

    }

    public override void draw(RenderTexture target)
    {
        switch (state)
        {
            case buttonState.defaultState:
                rectangle.FillColor = backgroundColor;
                break;
            case buttonState.active:
                rectangle.FillColor = activeColor;
                break;
            case buttonState.clicked:
                rectangle.FillColor = onClickColor;
                break;
        }
        target.Draw(rectangle);
        target.Draw(text);
    }
    public override void update(Vector2i mousePosition, bool isLMBPressed, List<Keyboard.Key> pressedKeys)
    {
        if (mousePosition.X > buttonPosition.X && mousePosition.X < buttonPosition.X + size.X &&
            mousePosition.Y > buttonPosition.Y && mousePosition.Y < buttonPosition.Y + size.Y)
        {
            if (isLMBPressed) state = buttonState.clicked;
            else
            {
                if (state == buttonState.clicked) onClick();
                state = buttonState.active;
            }
        }
        else
        {
            state = buttonState.defaultState;
        }
    }
}