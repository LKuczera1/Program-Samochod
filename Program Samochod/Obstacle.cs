using SFML.System;

//Klasa bazowa obiektów z kolizją
public class Obstacle
{
    public Vector2f position, size;

    public Obstacle(Vector2f position, Vector2f size)
    {
        this.position = position * -1;
        this.size = size;
    }

    ~Obstacle()
    {

    }

    internal bool internalIsColliding(Vector2f point)
    {
        return (point.X >= position.X && point.X <= position.X + size.X && point.Y >= position.Y && point.Y <= position.Y + size.Y);
    }
    public virtual bool isColliding(Vector2f carCorner1, Vector2f carCorner2, Vector2f carCorner3, Vector2f carCorner4)
    {
        return internalIsColliding(carCorner1) || internalIsColliding(carCorner2) || internalIsColliding(carCorner3) || internalIsColliding(carCorner4);
    }

    public virtual bool onCollision()
    {
        return true;//returns true if obstacle causes immobility to car
    }
}