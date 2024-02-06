using SFML.Graphics;
using SFML.System;
using SFML.Window;

//klasa mapy/pola manewrowego rozgrywki
public class maneuveringArea : GPObject
{
    public maneuveringArea(Vector2f size, Vector2f position) : base(size, position)
    {
        basicTexture = new Texture(Directory.GetCurrentDirectory() + "\\Resources\\Graphics\\maneuveringArea.png");
        basicSprite = new Sprite(basicTexture);
    }

    ~maneuveringArea()
    {

    }


    public override void Render(RenderTexture drawTexture, Camera targetScene)
    {
        basicSprite.Position = targetScene.getRelativePosition(position);
        drawTexture.Draw(basicSprite);
    }
    public override void Update(List<Keyboard.Key> pressedKeys)
    {

    }
}