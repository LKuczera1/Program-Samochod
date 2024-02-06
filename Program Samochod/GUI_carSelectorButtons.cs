using SFML.Graphics;
using SFML.System;

//Przycisk wyboru szarego samochodu
public class GUI_grayCarChoseButton : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_grayCarChoseButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_grayCarChoseButton()
    {

    }

    public override void onClick()
    {
        feedback.setCar(CarSelectorInterface.carType.grayCar);
    }
}
//Przycisk wyboru zielnego samochodu
public class GUI_greenCarChoseButton : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_greenCarChoseButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_greenCarChoseButton()
    {

    }

    public override void onClick()
    {
        feedback.setCar(CarSelectorInterface.carType.greenCar);
    }
}
//Przycisk wyboru pomarańczowego samochodu
public class GUI_orangeCarChoseButton : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_orangeCarChoseButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_orangeCarChoseButton()
    {

    }

    public override void onClick()
    {
        feedback.setCar(CarSelectorInterface.carType.redCar);
    }
}
//Przycisk wyboru pojazdu Ursus C360
public class GUI_ursusCarChoseButton : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_ursusCarChoseButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_ursusCarChoseButton()
    {

    }

    public override void onClick()
    {
        feedback.setCar(CarSelectorInterface.carType.ursus);
    }
}
//Przycisk ładowania z bazy SQL - Nie zaimplementowano
public class GUI_loadFromSQLDatabase : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_loadFromSQLDatabase(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_loadFromSQLDatabase()
    {

    }

    public override void onClick()
    {
        feedback.setCar(CarSelectorInterface.carType.ursus);
    }
}

//Przycisk ładowania z pliku XLSX
public class GUI_LoadFromXLS : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_LoadFromXLS(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_LoadFromXLS()
    {

    }

    public override void onClick()
    {
        feedback.loadFromXLSX();
    }
}

//Przycisk powrotu
public class GUI_goBack : GUI_Button
{
    CarSelectorInterface feedback;
    public GUI_goBack(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, CarSelectorInterface feedback)
        : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
    {
        this.feedback = feedback;
    }

    ~GUI_goBack()
    {

    }

    public override void onClick()
    {
        feedback.goBack();
    }
}