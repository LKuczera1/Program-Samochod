using SFML.Graphics;
using SFML.Window;

//Klasa bazowa interfejsów
public abstract class Interface
{
    public CarSimulator feedback;
    public Interface(CarSimulator feedback)
    {
        this.feedback = feedback;
    }
    public abstract void Render(RenderTexture drawTexture);
    public abstract void Update(List<Keyboard.Key> pressedKeys);
}
