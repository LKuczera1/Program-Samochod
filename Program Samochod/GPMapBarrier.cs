using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa krawędzi mapy
public class GPMapBarrier : GPObject
{
    bool showCollision;
    public GPMapBarrier(Vector2f position, Vector2f size) : base(size, position)
    {
        showCollision = false;
    }

    ~GPMapBarrier()
    {

    }

    //Rysowanie
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
    }
    //Aktualizacja
    public override void Update(List<Keyboard.Key> pressedKeys)
    {

    }
    //Zwraca true, jeżeli obiekt wywoła unieruchomienie samochodu
    public override bool onCollision()
    {
        return true;
    }
}