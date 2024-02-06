using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Interfejs wyboru samochodu
public class CarSelectorInterface : Interface
{
    Sprite background;

    List<GUI_Object> GUI_Objects;

    Sprite grayCar, greenCar, orangeCar, ursus;

    CarObject loadedCar;

    //Enum reprezentujący samochód
    public enum carType : int
    {
        grayCar = 0,
        greenCar = 1,
        redCar = 2,
        ursus = 3
    }

    carType selectedCar;

    public CarSelectorInterface(CarSimulator feedback) : base(feedback)
    {
        background = new Sprite(feedback.getTexture(resourcesContainer.textureCode.background));

        GUI_Objects = new List<GUI_Object>();

        GUI_Objects.Add(new GUI_grayCarChoseButton(new Vector2f(560, 200), new Vector2f(420, 50), feedback.getFont(), "Szary samochód", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        GUI_Objects.Add(new GUI_greenCarChoseButton(new Vector2f(560, 260), new Vector2f(420, 50), feedback.getFont(), "Zielony samochód", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        GUI_Objects.Add(new GUI_orangeCarChoseButton(new Vector2f(560, 320), new Vector2f(420, 50), feedback.getFont(), "Pomarańczowy samochód", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        GUI_Objects.Add(new GUI_ursusCarChoseButton(new Vector2f(560, 380), new Vector2f(420, 50), feedback.getFont(), "Ursus C-360", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));

        GUI_Objects.Add(new GUI_loadFromSQLDatabase(new Vector2f(900, 500), new Vector2f(290, 50), feedback.getFont(), "Załaduj z bazy SQL", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        GUI_Objects.Add(new GUI_LoadFromXLS(new Vector2f(560, 500), new Vector2f(290, 50), feedback.getFont(), "Załaduj z pliku .xls", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));

        GUI_Objects.Add(new GUI_goBack(new Vector2f(560, 900), new Vector2f(290, 50), feedback.getFont(), "Powrót", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));

        selectedCar = carType.grayCar;

        grayCar = new Sprite(feedback.getTexture(resourcesContainer.textureCode.grayCar));
        greenCar = new Sprite(feedback.getTexture(resourcesContainer.textureCode.greenCar));
        orangeCar = new Sprite(feedback.getTexture(resourcesContainer.textureCode.orangeCar));
        ursus = new Sprite(feedback.getTexture(resourcesContainer.textureCode.ursus));

        Vector2f selectedCarSpritePosition = new Vector2f(1050, 220);
        grayCar.Position = selectedCarSpritePosition;
        greenCar.Position = selectedCarSpritePosition;
        orangeCar.Position = selectedCarSpritePosition;
        ursus.Position = selectedCarSpritePosition;

        loadedCar = new CarObject(new Vector2f(100, 200), new Vector2f(700, 700));
    }

    ~CarSelectorInterface()
    {

    }
    //rysowanie
    public override void Render(RenderTexture drawTexture)
    {
        drawTexture.Draw(background);
        foreach (GUI_Object i in GUI_Objects.ToList())
        {
            i.draw(drawTexture);
        }

        switch (((int)selectedCar))
        {
            case 0:
                drawTexture.Draw(grayCar);
                break;
            case 1:
                drawTexture.Draw(greenCar);
                break;
            case 2:
                drawTexture.Draw(orangeCar);
                break;
            case 3:
                drawTexture.Draw(ursus);
                break;
        }
    }
    //Aktualizacja interfejsu
    public override void Update(List<Keyboard.Key> pressedKeys)
    {
        foreach (GUI_Object i in GUI_Objects.ToList()) //to list eliminuje problem błędu przy dodaniu przycisku
        {
            i.update(Mouse.GetPosition(feedback.window), Mouse.IsButtonPressed(Mouse.Button.Left), pressedKeys);
        }
    }
    //Wybór samochodu
    public void setCar(carType selectedCar)
    {
        this.selectedCar = selectedCar;
    }
    //Informacja zwrotna o powrocie do poprzedniego interfejsu
    public void goBack()
    {
        feedback.driverDataInput();
    }
    //Informacja zwrotna o uruchomieniu symulacji samochodu z załadowanym samochodem
    public void start()
    {
        feedback.start(loadedCar);
    }

    //Ładowanie samochodu z pliku xlsx i tworzenie przycisku "Start" w przypadku sukcesu
    public void loadFromXLSX()
    {
        if (loadedCar.loadFromXLSX(selectedCar, Directory.GetCurrentDirectory() + "\\Resources\\data\\carParameters.xlsx", feedback.getResourceContainer()))
        {
            GUI_Objects.Add(new GUI_InputDriverData(new Vector2f(900, 900), new Vector2f(290, 50), feedback.getFont(), "Dalej", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        }
        //jak zwróci true to znaczy, ze udalo sie zaladowac samochod
    }
}