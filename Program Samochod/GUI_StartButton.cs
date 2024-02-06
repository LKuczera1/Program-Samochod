using SFML.Graphics;
using SFML.System;

//Klasa przycisku otworzenia interfejsu wyboru pojazdu
public class GUI_carSelectorButton : GUI_Button
{
    MainMenuInterface feedback;
    public GUI_carSelectorButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, MainMenuInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_carSelectorButton()
    {

    }

    public override void onClick()
    {
        feedback.driverData();
    }
}
//Klasa przycisku rozpoczęcia rozgrywki
public class GUI_StartButton : GUI_Button
{
    DriverDataInterface feedback;
    public GUI_StartButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, DriverDataInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_StartButton()
    {

    }

    public override void onClick()
    {
        feedback.start();
    }
}

//Klasa przycisku interfejsu danych kierowcy
public class GUI_InputDriverData : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_InputDriverData(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_InputDriverData()
    {

    }

    public override void onClick()
    {
        feedback.start();
    }
}