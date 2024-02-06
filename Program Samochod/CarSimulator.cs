using SFML.Graphics;
using SFML.Window;

//Główna klasa programu
public class CarSimulator
{
    uint _WindowX, _WindowY;
    public uint WindowXresolution { get { return _WindowX; } }
    public uint WindowYresolution { get { return _WindowY; } }


    public RenderWindow window;
    public RenderTexture RenderedTexture;
    Sprite windowSprite;

    FPSCounter FPS;

    List<Keyboard.Key> pressedKeys;

    resourcesContainer resContainer;

    Interface currentInterface;

    CarObject car;
    public CarSimulator()
    {
        //Inicjalizacja okna
        _WindowX = 1920;
        _WindowY = 1080;
        window = new RenderWindow(new VideoMode(_WindowX, _WindowY, 32), "Car Simulator", SFML.Window.Styles.Fullscreen);
        window.SetVisible(true);

        //inicjalizacja wydarzeń
        window.Closed += new EventHandler(OnClosed);
        window.KeyPressed += OnKeyPressed;
        window.KeyReleased += onKeyReleased;

        RenderedTexture = new RenderTexture(_WindowX, _WindowY);
        windowSprite = new Sprite(RenderedTexture.Texture);
        pressedKeys = new List<Keyboard.Key>();

        //Licznik klatek na sekunde
        FPS = new FPSCounter(60);
        
        //Inicjalizacja klasy, która domyślnie miała być odpowiedzialna za przechowywanie wszystkich zasobów graficznych
        resContainer = new resourcesContainer();

        //Inicjalizacja interfejsu menu głównego
        currentInterface = new MainMenuInterface(this);

        //Uruchomienie programu
        Run();
    }

    ~CarSimulator()
    {
        window = null;
        GC.Collect();
    }

    void OnClosed(object sender, EventArgs events)
    {
        window.Close();
    }

    //Główna pętla programu
    public void Run()
    {
        while (window.IsOpen)
        {
            window.DispatchEvents();
            if (currentInterface == null) window.Close();
            RenderedTexture.Clear(new Color(100, 100, 100));
            FPS.Update();
            Update();
            Render();
            window.Display();
        }
    }

    //Rysowanie interfejsu
    void Render()
    {
        currentInterface.Render(RenderedTexture);
        RenderedTexture.Display();
        window.Draw(windowSprite);
    }
    //Aktualizacja interfejsu
    void Update()
    {
        currentInterface.Update(pressedKeys);
        //pressedKeys.Clear();
    }
    //Obsługa klawiatury
    public void OnKeyPressed(object sender, KeyEventArgs Key)
    {
        foreach (Keyboard.Key k in pressedKeys)
        {
            if (k == Key.Code) return;
        }
        pressedKeys.Add(Key.Code);
    }

    public void onKeyReleased(object sender, KeyEventArgs Key)
    {
        pressedKeys.Remove(Key.Code);
    }

    public Texture getTexture(resourcesContainer.textureCode code)
    {
        return resContainer.getTexture(code);
    }

    public Font getFont()
    {
        return resContainer.getFont();
    }
    //Inicjalizacja poszczególnych interfejsów
    public void start(CarObject car)
    {
        currentInterface = null;
        this.car = car;
        GC.Collect();
        if (car != null) currentInterface = new GameplayInterface(this, car);
    }
    public void carSelector()
    {
        currentInterface = null;
        GC.Collect();
        currentInterface = new CarSelectorInterface(this);
    }

    public void driverDataInput()
    {
        currentInterface = null;
        GC.Collect();
        currentInterface = new DriverDataInterface(this);
    }

    public void mainMenu()
    {
        currentInterface = null;
        GC.Collect();
        currentInterface = new MainMenuInterface(this);
    }

    public void aboutPage()
    {
        currentInterface = null;
        GC.Collect();
        currentInterface = new aboutInterface(this);
    }

    public resourcesContainer getResourceContainer()
    {
        return resContainer;
    }

    public void exit()
    {
        window.Close();
    }
}