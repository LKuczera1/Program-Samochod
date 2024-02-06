using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Interfejs rozgrywki
public class GameplayInterface : Interface
{
    Camera gameplayCamera;
    Sprite gameplaySprite;

    CarObject car;
    maneuveringArea map;

    Sprite carInterface, bigPointer, smallPointer, currentGearPointer;

    double carFuelContainerCapacity;

    List<GPObject> objects;

    Vector2f screenResolution;
    public GameplayInterface(CarSimulator Feedback, CarObject car) : base(Feedback)
    {
        screenResolution = new Vector2f(1920, 1080);
        this.car = car;
        carFuelContainerCapacity = car.getFuelContainerCapacity();
        carInterface = new Sprite(new Texture(Directory.GetCurrentDirectory() + "\\Resources\\Graphics\\carInterface.png"));
        bigPointer = new Sprite(new Texture(Directory.GetCurrentDirectory() + "\\Resources\\Graphics\\carBigPtr.png"));
        smallPointer = new Sprite(new Texture(Directory.GetCurrentDirectory() + "\\Resources\\Graphics\\carFuelTempPtr.png"));
        currentGearPointer = new Sprite(new Texture(Directory.GetCurrentDirectory() + "\\Resources\\Graphics\\transmissionLever.png"));
        carInterface.Position = new Vector2f(screenResolution.X - carInterface.Texture.Size.X, 0);
        bigPointer.Origin = new Vector2f(132, 19);
        smallPointer.Origin = new Vector2f(71, 13);
        currentGearPointer.Origin = new Vector2f(30, 30);
        gameplayCamera = new Camera(new Vector2f(screenResolution.X - 350, screenResolution.Y - 80), new Vector2f(0, 0));
        gameplaySprite = new Sprite(gameplayCamera.GetScene().Texture);
        map = new maneuveringArea(new Vector2f(2400, 1800), new Vector2f(0, 0));

        objects = new List<GPObject>();

        //Granice Mapy
        objects.Add(new GPMapBarrier(new Vector2f(2940, 490), new Vector2f(2416, 84)));
        objects.Add(new GPMapBarrier(new Vector2f(3045, 2320), new Vector2f(84, 1820)));
        objects.Add(new GPMapBarrier(new Vector2f(2960, 2440), new Vector2f(2416, 84)));
        objects.Add(new GPMapBarrier(new Vector2f(500, 2340), new Vector2f(84, 1820)));

        //Pachołki
        objects.Add(new GPCone(new Vector2f(2000, 750), new Vector2f(20, 20)));
        objects.Add(new GPCone(new Vector2f(2200, 710), new Vector2f(20, 20)));
        objects.Add(new GPCone(new Vector2f(2300, 600), new Vector2f(20, 20)));
        objects.Add(new GPCone(new Vector2f(2500, 850), new Vector2f(20, 20)));

        //Przekazanie klasie samochód listy obiektów do uwzględnienia w kolizji
        car.setListOfObjects(objects);

    }
    
    //Rysowanie interfejsu
    public override void Render(RenderTexture drawTexture)
    {
        map.Render(gameplayCamera.GetScene(), gameplayCamera);
        car.Render(gameplayCamera.GetScene(), gameplayCamera);

        drawTexture.Draw(carInterface);
        bigPointer.Position = new Vector2f(174 + carInterface.Position.X, 172);
        bigPointer.Rotation = (float)(-45.0 + car.getSpeed() / 240.0 * 270.0);
        drawTexture.Draw(bigPointer);
        bigPointer.Position = new Vector2f(174 + carInterface.Position.X, 466);
        bigPointer.Rotation = (float)(-45.0 + car.getRPMvalue() / 7000 * 270.0);
        drawTexture.Draw(bigPointer);
        
        //Rysowanie elementów graficznego interfejsu użytkownika
        double tempVariable = car.getRadiatorFluidTemperature();
        if (tempVariable >= 50)
        {
            smallPointer.Position = new Vector2f(87 + carInterface.Position.X, 674);
            smallPointer.Rotation = (float)((tempVariable - 50) / 80 * 180.0);
            drawTexture.Draw(smallPointer);
        }
        else
        {
            smallPointer.Position = new Vector2f(87 + carInterface.Position.X, 674);
            smallPointer.Rotation = (float)(0);
            drawTexture.Draw(smallPointer);
        }

        smallPointer.Position = new Vector2f(262 + carInterface.Position.X, 674);
        smallPointer.Rotation = (float)(car.getStoredFuel() / carFuelContainerCapacity * 180.0);
        drawTexture.Draw(smallPointer);

        switch (car.getCurrentTransmissionLevel())
        {
            case 0:
                currentGearPointer.Position = new Vector2f(1750, 910);
                break;
            case 1:
                currentGearPointer.Position = new Vector2f(1658, 810);
                break;
            case 2:
                currentGearPointer.Position = new Vector2f(1658, 1005);
                break;
            case 3:
                currentGearPointer.Position = new Vector2f(1750, 810);
                break;
            case 4:
                currentGearPointer.Position = new Vector2f(1750, 1005);
                break;
            case 5:
                currentGearPointer.Position = new Vector2f(1842, 810);
                break;
            case 6:
                currentGearPointer.Position = new Vector2f(1846, 1005);
                break;
        }

        drawTexture.Draw(currentGearPointer);

        foreach (GPObject i in objects)
        {
            i.Render(gameplayCamera.GetScene(), gameplayCamera);
        }

        gameplayCamera.Display();
        drawTexture.Draw(gameplaySprite);
    }
    //Aktualizacja interfejsu
    public override void Update(List<Keyboard.Key> pressedKeys)
    {
        car.Update(pressedKeys);
        gameplayCamera.setPosition(car.getCarPosition() + new Vector2f((screenResolution.X - 350) / 2, (screenResolution.Y - 80) / 2));

        foreach (GPObject i in objects)
        {
            i.Update(null);
        }
    }
}
