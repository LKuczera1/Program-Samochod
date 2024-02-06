using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa obiektu rozgrywki (GamePlay Object)
//Z tej klasy dziedziczą wszystkie inne klasy obiektów rozgrywki
public abstract class GPObject : Obstacle
{
    public Texture basicTexture;
    public Sprite basicSprite;
    public GPObject(Vector2f size, Vector2f position) : base(position, size)
    {

    }
    ~GPObject()
    {
        basicTexture = null;
        basicSprite = null;
        GC.Collect();
    }
    public abstract void Render(RenderTexture drawTexture, Camera targetScene);
    public abstract void Update(List<Keyboard.Key> pressedKeys);
}
