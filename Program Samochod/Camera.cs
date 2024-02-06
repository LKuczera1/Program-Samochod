using SFML.Graphics;
using SFML.System;

//Klasa Kamera - Odpowiedzialna za wyznaczenie rysowanego obszaru
public class Camera
{
    RenderTexture scene;
    Vector2f size, position;
    public Camera(Vector2f size, Vector2f position)
    {
        this.position = position;
        this.size = size;
        scene = new RenderTexture(((uint)size.X), ((uint)size.Y));
    }

    public RenderTexture GetScene()
    {
        return scene;
    }

    public Vector2f getRelativePosition(Vector2f position) //Zwraca pozycje obiektu względem pozycji kamery
    {
        return (this.position - position);
    }

    public void Display() //Niezbędne przed "Narysowaniem obrazu z kamery"
    {
        scene.Display();
    }

    public void setPosition(Vector2f position)
    {
        this.position = position;
    }

    ~Camera()
    {
        scene = null;
    }
}