using SFML.Graphics;
using SFML.System;

//Przycisk zamknięcia interfejsu "O programie"
public class GUI_aboutInterfaceBackButton : GUI_Button
{
    aboutInterface feedback;
    public GUI_aboutInterfaceBackButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, aboutInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_aboutInterfaceBackButton()
    {

    }

    public override void onClick()
    {
        feedback.goBack();
    }
}
//Przycisk otworzenia interfejsu "O programie"
public class GUI_aboutInterfaceButton : GUI_Button
{
    MainMenuInterface feedback;
    public GUI_aboutInterfaceButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, MainMenuInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_aboutInterfaceButton()
    {

    }

    public override void onClick()
    {
        feedback.aboutPage();
    }
}