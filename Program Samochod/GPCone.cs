using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa pachołka
public class GPCone : GPObject
{
    Vector2f center;

    Sprite flippedCone;
    bool isflipped;
    double rotation;

    bool showCollision;

    public GPCone(Vector2f position, Vector2f size) : base(size, position)
    {
        center = ((position * -1) + size / 2);
        basicTexture = new Texture(Directory.GetCurrentDirectory() + "\\Resources\\Graphics\\cone.png");
        flippedCone = new Sprite(new Texture(Directory.GetCurrentDirectory() + "\\Resources\\Graphics\\flippedCone.png"));
        basicSprite = new Sprite(basicTexture);

        this.size.X = -basicTexture.Size.X;
        this.size.Y = -basicTexture.Size.Y;

        flippedCone.Origin = new Vector2f(flippedCone.Texture.Size.X, flippedCone.Texture.Size.Y) / 2;

        showCollision = false;
    }

    ~GPCone()
    {

    }

    public override void Render(RenderTexture drawTexture, Camera targetScene)
    {
        if (showCollision)
        {
            RectangleShape rect = new RectangleShape(size * -1);
            rect.Position = targetScene.getRelativePosition(position);
            rect.FillColor = new Color(255, 255, 255, 0);
            rect.OutlineColor = new Color(255, 255, 25);
            rect.OutlineThickness = 2;
            drawTexture.Draw(rect);
        }


        if (isflipped)
        {
            flippedCone.Position = targetScene.getRelativePosition(position);
            flippedCone.Rotation = (float)rotation;
            drawTexture.Draw(flippedCone);
            return;
        }

        basicSprite.Position = targetScene.getRelativePosition(position);
        drawTexture.Draw(basicSprite);
    }
    public override void Update(List<Keyboard.Key> pressedKeys)
    {

    }

    void flipTheCone(Vector2f carPosition)
    {
        rotation = ((Math.Atan2(carPosition.X, carPosition.Y) - Math.Atan2(position.X, position.Y + 100)) * 180 / Math.PI);
        isflipped = true;
    }

    public override bool isColliding(Vector2f carCorner1, Vector2f carCorner2, Vector2f carCorner3, Vector2f carCorner4)
    {
        //Podział samochodu na dwa trójkąty prostokątne, a nastepnie sprawdzanie czy pachołek
        //koliduje z jednym z trójkątów. Niestety metoda jest niedopracowana co powoduje
        //"Sporadyczne samoistne" wywracanie się pachołka lub brak kolizji

        Vector2f carPosition = (carCorner1 + carCorner2 + carCorner3 + carCorner4) / 4;
        Vector2f temp = (carPosition - center) * (float)0.12; //Zwiększenie czułości na kolizje

        double factor1 = (carCorner1.X * (carCorner3.Y - carCorner1.Y) + (center.Y + temp.Y - carCorner1.Y) * (carCorner3.X - carCorner1.X) - (center.X + temp.X) * (carCorner3.Y - carCorner1.Y)) / ((carCorner2.Y - carCorner1.Y) * (carCorner3.X - carCorner1.X) - (carCorner2.X - carCorner1.X) * (carCorner3.Y - carCorner1.Y));
        double factor2 = (center.Y + temp.Y - carCorner1.Y - factor1 * (carCorner2.Y - carCorner1.Y)) / (carCorner3.Y - carCorner1.Y);

        factor1 = Math.Asin(factor1);
        factor2 = Math.Asin(factor2);

        if ((factor1 >= 0) && (factor2 >= 0))
        {
            flipTheCone(carPosition);
            return true;
        }

        double factor3 = (carCorner1.X * (carCorner3.Y - carCorner1.Y) + (center.Y + temp.Y - carCorner1.Y) * (carCorner3.X - carCorner1.X) - (center.X + temp.X) * (carCorner3.Y - carCorner1.Y)) / ((carCorner4.Y - carCorner1.Y) * (carCorner3.X - carCorner1.X) - (carCorner4.X - carCorner1.X) * (carCorner3.Y - carCorner1.Y));
        double factor4 = (center.Y + temp.Y - carCorner1.Y - factor3 * (carCorner4.Y - carCorner1.Y)) / (carCorner3.Y - carCorner1.Y);

        factor3 = Math.Asin(factor3);
        factor4 = Math.Asin(factor4);

        if ((factor3 >= 0) && (factor4 >= 0))
        {
            flipTheCone(carPosition);
            return true;
        }

        return false;
    }

    public override bool onCollision()
    {
        isflipped = true;
        return false;//zwraca prawde jeśli obiekt spowoduje unieruchomienie samochodu
    }
}