using SFML.System;

//Klasa licznika klatek na sekunde
public class FPSCounter
{
    int targetFPS;
    double frameTime;
    Clock deltaTimeClock;
    double deltaTime;
    double sleepTime;

    public FPSCounter(int targetFPS)
    {
        this.targetFPS = targetFPS;
        if (targetFPS != 0) frameTime = (1.0 / targetFPS) * 1000;
        deltaTimeClock = new Clock();
    }
    //Aktualizacja licznika
    public void Update()
    {
        deltaTime = deltaTimeClock.ElapsedTime.AsMilliseconds();
        sleepTime = (frameTime - deltaTime);

        //Opóźnienie działania programu w celu osiągnięcia zadanej ilości klatek na sekunde
        if (sleepTime > 0) Thread.Sleep((int)sleepTime);
        deltaTimeClock.Restart();
    }
    //Funkcja zwracająca różnice czasu pomiędzy klatkami
    public double getDeltaTime()
    {
        return deltaTime;
    }
}