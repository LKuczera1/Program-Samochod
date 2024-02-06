using OfficeOpenXml;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa samochodu
public class CarObject : GPObject
{
    Vector2f frontAxisPosition, rearAxisPosition;

    double speed, acceleration;

    float distanceBetweenAxis;

    float carDirection, steeringWheel, frontWheelRotation;
    bool turnBackSteeringWheel;
    float wheelRotationDegree; //"Współczynnik obrotu przednich kół, 8-12 dla normalnych samochodów, 5 dla wózków widłowych"


    double throttle = 0.52;

    double engineRPM;
    double carWeight, wheelsRadius;
    double numberOfCylinders, fuelPowerFactor;

    double maxEngineRPM;
    double minEngineRPM;

    double shaftWeight;
    double shaftRadius;

    double clutchLevel;
    double[] gearBoxRatios; //skrzynia biegów = {1,2,3,4,5,R}
    ushort currentTransmissionLevel; //Obecnie wykorzystywany bieg


    double mechanicalResistanceFactor;

    double radiatorFluidTemperature, radiatorEfficiency, engineGeneratedHeat;

    double fuelContainterCapacity, storedFuel, fuelConsumptionPer100KM;

    List<GPObject> objects;

    double distanceBetweenFrontAxisAndFront;


    bool showCollision;



    Vector2f frontRight, frontLeft, rearRight, rearLeft; //bounding box


    Sprite carTire;
    RenderTexture carTexture;
    Sprite carSprite;

    ushort isEngineON, engineStarterPower;
    bool engineStarter;

    double storedEngineOil;
    double oilConsumption;
    double engineHealth;

    double brakePower;
    double currentBrakePower;

    public CarObject(Vector2f size, Vector2f position) : base(size, position)
    {
        //Podstawowa inicjalizacja parametrów samochodu
        wheelRotationDegree = 10;
        distanceBetweenAxis = 120;
        carWeight = 1650;
        wheelsRadius = 0.4;
        numberOfCylinders = 4;
        fuelPowerFactor = 0.1;
        gearBoxRatios = new double[7] { 0, 1, 4, 8, 16, 32, -4 };
        fuelContainterCapacity = 30;
        storedFuel = 10;
        fuelConsumptionPer100KM = 8;
        distanceBetweenFrontAxisAndFront = 100;
        engineStarterPower = 40;

        basicSprite = new Sprite(basicTexture);


        frontAxisPosition = new Vector2f(-2200, -800);
        rearAxisPosition = new Vector2f(-2000, -800);
        speed = 30;
        carDirection = 270;

        acceleration = 0;
        engineRPM = 1000;

        maxEngineRPM = 7000;
        minEngineRPM = 400;
        shaftWeight = 30;
        shaftRadius = 0.1;
        clutchLevel = 1;
        currentTransmissionLevel = 0;
        mechanicalResistanceFactor = 0.003;

        radiatorFluidTemperature = 20;
        radiatorEfficiency = 0.001;
        engineGeneratedHeat = 0.09;



        isEngineON = 0;
        engineStarter = false;

        storedEngineOil = 4;
        oilConsumption = 0.0001;
        engineHealth = 100;

        brakePower = 5000000;
        currentBrakePower = 0;

        showCollision = false;
    }

    //Wczytywanie parametrów samochodu z pliku xlsx
    public bool loadFromXLSX(CarSelectorInterface.carType chosenCar, string path, resourcesContainer resources)
    {

        try
        {
            ExcelPackage xlsx = new ExcelPackage(new FileInfo(path));

            var myWorksheet = xlsx.Workbook.Worksheets.FirstOrDefault(); //Wybor arkusza
            var rows = myWorksheet.Dimension.End.Row; //ostatniRzad
            var columns = myWorksheet.Dimension.End.Column; //ostatnia kolumna

            int row = 2;
            int column = ((int)(2 + chosenCar));

            wheelRotationDegree = float.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            distanceBetweenAxis = float.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            carWeight = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            wheelsRadius = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            numberOfCylinders = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            fuelPowerFactor = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            gearBoxRatios = new double[7];
            gearBoxRatios[1] = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            gearBoxRatios[2] = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            gearBoxRatios[3] = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            gearBoxRatios[4] = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            gearBoxRatios[5] = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            gearBoxRatios[6] = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            fuelContainterCapacity = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            storedFuel = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            fuelConsumptionPer100KM = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            distanceBetweenFrontAxisAndFront = double.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;
            engineStarterPower = ushort.Parse(myWorksheet.Cells[row, column].Value.ToString());
            row++;

            basicTexture = null;

            switch (((int)chosenCar))
            {
                case 0:
                    basicTexture = new Texture(resources.getTexture(resourcesContainer.textureCode.grayCar));
                    break;
                case 1:
                    basicTexture = new Texture(resources.getTexture(resourcesContainer.textureCode.greenCar));
                    break;
                case 2:
                    basicTexture = new Texture(resources.getTexture(resourcesContainer.textureCode.orangeCar));
                    break;
                case 3:
                    basicTexture = new Texture(resources.getTexture(resourcesContainer.textureCode.ursus));
                    break;
            }

            basicSprite = null;
            basicSprite = new Sprite(basicTexture);

            carTire = null;
            carTire = new Sprite(new Texture(resources.getTexture(resourcesContainer.textureCode.carTire)));
            basicSprite.Origin = new Vector2f(50, 45);
            carTire.Origin = new Vector2f(5, 15);

            xlsx.Dispose();

            return true;
        }
        catch (Exception e)
        {

        }

        return false;
    }

    ~CarObject()
    {

    }

    //funkcje zwracające wybrane parametry
    public double getSpeed()
    {
        return Math.Abs(speed);
    }

    public double getRPMvalue()
    {
        return engineRPM;
    }

    public double getRadiatorFluidTemperature()
    {
        return radiatorFluidTemperature;
    }

    public double getStoredFuel()
    {
        return storedFuel;
    }

    public double getFuelContainerCapacity()
    {
        return fuelContainterCapacity;
    }

    //pobieranie listy obiektow, z ktorymi samochod moze kolidowac
    public void setListOfObjects(List<GPObject> objects)
    {
        this.objects = objects;
    }

    //rysowanie samochodu
    public override void Render(RenderTexture drawTexture, Camera targetScene)
    {

        carTire.Rotation = frontWheelRotation + carDirection;
        carTire.Position = targetScene.getRelativePosition(frontAxisPosition + RotateVector(new Vector2f(0, 45), carDirection - 90));
        drawTexture.Draw(carTire);
        carTire.Position = targetScene.getRelativePosition(frontAxisPosition + RotateVector(new Vector2f(0, 45), carDirection - 270));
        drawTexture.Draw(carTire);

        basicSprite.Rotation = carDirection;
        basicSprite.Position = targetScene.getRelativePosition(frontAxisPosition);
        basicSprite.Texture.Smooth = true;
        drawTexture.Draw(basicSprite);

        //rysowanie boundingBoxu samochodu
        if (showCollision)
        {
            RectangleShape rect = new RectangleShape(new Vector2f(2, 2));
            rect.FillColor = new Color(220, 230, 20);
            rect.Position = targetScene.getRelativePosition(frontRight);
            drawTexture.Draw(rect);
            rect.Position = targetScene.getRelativePosition(frontLeft);
            drawTexture.Draw(rect);
            rect.Position = targetScene.getRelativePosition(rearRight);
            drawTexture.Draw(rect);
            rect.Position = targetScene.getRelativePosition(rearLeft);
            drawTexture.Draw(rect);
            rect.Position = targetScene.getRelativePosition(position);
            drawTexture.Draw(rect);
        }
    }

    //Funkcja obracająca wektor względem pkt (0,0) o wybrany kąt
    public Vector2f RotateVector(Vector2f vector, float angle)
    {
        //     |cos(angle) -sin(angle)| |x|
        // v = |                      |*| |
        //     |sin(angle)  cos(angle)| |y|

        angle = ((float)(angle * Math.PI / 180));

        return new Vector2f(((float)Math.Cos(angle)) * vector.X - ((float)Math.Sin(angle)) * vector.Y, ((float)Math.Sin(angle)) * vector.X + ((float)Math.Cos(angle)) * vector.Y);
    }

    public Vector2f getCarPosition()
    {
        return frontAxisPosition;
    }

    //Zwraca kąt między wektorem a i wektorem b względem pkt (0,0)
    public float GetAngleBetweenVectors(Vector2f a, Vector2f b)
    {
        return (float)((Math.Atan2(a.X, a.Y) - Math.Atan2(b.X, b.Y)) * 180 / Math.PI);
    }

    public ushort getCurrentTransmissionLevel()
    {
        return currentTransmissionLevel;
    }
    //Aktualizacja obiektu samochód
    public override void Update(List<Keyboard.Key> pressedKeys)
    {
        //Tymczasowe zmienne związane z kolizją
        Vector2f temp = frontAxisPosition, tempFrontAxis = frontAxisPosition, tempRearAxis = rearAxisPosition;
        float tempDirection = carDirection;

        //Obliczanie tymczasowych zmiennych
        tempFrontAxis += RotateVector(new Vector2f(0, ((float)speed)), carDirection + frontWheelRotation);
        tempDirection += GetAngleBetweenVectors(temp - rearAxisPosition, tempFrontAxis - rearAxisPosition);
        tempRearAxis += RotateVector(new Vector2f(0, ((float)speed)), tempDirection);

        //bounding box
        frontRight = new Vector2f(-size.X / 2, (float)distanceBetweenFrontAxisAndFront);
        frontLeft = new Vector2f(size.X / 2, (float)distanceBetweenFrontAxisAndFront);
        rearRight = new Vector2f(frontRight.X, frontRight.Y - size.Y);
        rearLeft = new Vector2f(frontLeft.X, frontLeft.Y - size.Y);

        frontRight = RotateVector(frontRight, tempDirection);
        frontLeft = RotateVector(frontLeft, tempDirection);
        rearRight = RotateVector(rearRight, tempDirection);
        rearLeft = RotateVector(rearLeft, tempDirection);

        position = new Vector2f((tempRearAxis.X + tempFrontAxis.X) / 2, (tempRearAxis.Y + tempFrontAxis.Y) / 2);


        frontRight.X += position.X;
        frontRight.Y += position.Y;

        frontLeft.X += position.X;
        frontLeft.Y += position.Y;


        rearRight.X += position.X;
        rearRight.Y += position.Y;

        rearLeft.X += position.X;
        rearLeft.Y += position.Y;


        //Sprawdzanie kolizji
        bool hasCollided = false;
        foreach (GPObject i in objects)
        {
            if (i.isColliding(frontRight, frontLeft, rearRight, rearLeft))
            {
                if (i.onCollision()) hasCollided = true;
            }
        }

        if (!hasCollided)
        {
            frontAxisPosition = tempFrontAxis;
            rearAxisPosition = tempRearAxis;
            carDirection = tempDirection;
        }
        else
        {
            speed = 0;
        }

        //obliczanie pozycji tylnej osi
        temp = RotateVector(new Vector2f(0, distanceBetweenAxis), carDirection);
        rearAxisPosition = frontAxisPosition - temp;

        //Symulacja kierownicy    
        frontWheelRotation = steeringWheel / wheelRotationDegree;

        //Pętla odpowiedzialna za obsługę klawiatury
        foreach (Keyboard.Key key in pressedKeys)
        {
            //skręt w lewo
            if (key == Keyboard.Key.Left)
            {
                if (steeringWheel > -450)
                {
                    steeringWheel -= 20;
                }
                turnBackSteeringWheel = false;
            }

            //skręt w prawo
            if (key == Keyboard.Key.Right)
            {
                if (steeringWheel < 450)
                {
                    steeringWheel += 20;
                }
                turnBackSteeringWheel = false;
            }

            //Przyśpieszanie - Otwarcie przepustnicy
            if (key == Keyboard.Key.Up)
            {
                if (throttle < 1) throttle += 0.01;
                continue;
            }

            //Zwalnianie - Zamknięcie przepustnicy
            if (key == Keyboard.Key.Down)
            {
                if (throttle > 0) throttle -= 0.01;
                continue;
            }

            //Obsługa skrzyni biegów
            if (key == Keyboard.Key.Tilde || key == Keyboard.Key.N)
            {
                currentTransmissionLevel = 0;
            }
            else if (key == Keyboard.Key.Num1)
            {
                currentTransmissionLevel = 1;
            }
            else if (key == Keyboard.Key.Num2)
            {
                currentTransmissionLevel = 2;
            }
            else if (key == Keyboard.Key.Num3)
            {
                currentTransmissionLevel = 3;
            }
            else if (key == Keyboard.Key.Num4)
            {
                currentTransmissionLevel = 4;
            }
            else if (key == Keyboard.Key.Num5)
            {
                currentTransmissionLevel = 5;
            }
            else if (key == Keyboard.Key.Num6 || key == Keyboard.Key.R)
            {
                currentTransmissionLevel = 6;
            }

            //Obsługa rozrusznika
            if (key == Keyboard.Key.E)
            {
                isEngineON = 1;
                engineStarter = true;
            }
            else
            {
                engineStarter = false;
            }

            //Obsługa klawiszy odpowiedzialnych za hamowanie
            //Występuje problem z wyłączaniem hamulca w przypadku wciśnięcia dodatkowego klawisza
            if (key == Keyboard.Key.Space)
            {
                currentBrakePower = brakePower;
            }
            else
            {
                //currentBrakePower = 0;
            }
        }

        if (pressedKeys.Count == 0)
        {
            engineStarter = false;
            currentBrakePower = 0;
        }

        if (turnBackSteeringWheel) steeringWheel *= ((float)0.9);
        turnBackSteeringWheel = true;

        //Obliczanie mocno uproszczonej fizyki samochodu
        double wheelsRPM = (speed * 1000 / 60) / (2 * Math.PI * wheelsRadius);

        //obliczanie siły generowanej przez silnik w niutonach
        double generatedPower = engineRPM * throttle * numberOfCylinders * fuelPowerFactor * isEngineON - (Math.Pow(100.0 - engineHealth, 2));
        engineHealth -= 0.001;

        double shaftMomentOfInertia = shaftWeight * shaftRadius * shaftRadius / 2;
        double wheelsMomentOfInertia = carWeight * wheelsRadius * wheelsRadius / 2;

        //symulacja gaśniecia silnika
        if (engineRPM < minEngineRPM) generatedPower *= engineRPM / minEngineRPM * engineRPM / minEngineRPM;

        //symulacja dławienia silnika
        if (engineRPM > maxEngineRPM) generatedPower *= 0.5;

        //Symulacja rozrusznika
        if (engineStarter) generatedPower += engineStarterPower;


        //symulacja zużywania paliwa
        if (storedFuel <= 0) generatedPower = 0.1;
        else if (engineRPM > 0)
        {
            storedFuel -= throttle * numberOfCylinders * fuelConsumptionPer100KM / 100000;
        }

        engineRPM += ((1 / Math.Sqrt(shaftMomentOfInertia / generatedPower) * 38) - engineRPM) * 0.2;
        //0.2 - Współczynnik prędkosci zmiany obrotów(Rozpędzania sie silnika)

        if (double.IsNaN(engineRPM) || engineRPM < 50) engineRPM = 0;

        double outputTorque = generatedPower * shaftRadius * gearBoxRatios[currentTransmissionLevel] * clutchLevel;
        double outputRPM = engineRPM / gearBoxRatios[currentTransmissionLevel];

        //przyspieszenie kątowe kół samochodu
        double angularVelocity = (wheelsRPM * 360 / 60) / 180 * Math.PI;

        double torqueOfDrivingCar = angularVelocity * wheelsMomentOfInertia;
        double torqueSum = outputTorque - torqueOfDrivingCar;

        //obliczanie przyśpieszenia - moment obrotowy / średnica kół / (waga pojazd + siła hamowania)
        acceleration += (torqueSum / wheelsRadius) / (carWeight + currentBrakePower);

        //hamowanie
        if (currentBrakePower != 0) acceleration -= speed * 0.04;

        //Symulacja nagrzewania się silnika
        if (engineRPM > 0)
        {
            radiatorFluidTemperature += engineGeneratedHeat;
            radiatorFluidTemperature -= radiatorFluidTemperature * radiatorEfficiency;
        }
        else
        {
            radiatorFluidTemperature -= radiatorFluidTemperature * radiatorEfficiency / 10;
        }

        //Dodanie przyśpieszenia do prędkości
        speed += acceleration;
        acceleration = 0;
    }
}