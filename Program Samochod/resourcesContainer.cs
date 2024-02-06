using SFML.Graphics;

//Klasa zasobu graficznego
public class graphicResource
{
    public Texture texture;
    public bool isLoaded;
    public graphicResource()
    {
        isLoaded = false;
    }

    public void load(string path)
    {
        try
        {
            texture = new Texture(path);
        }
        catch (Exception e)
        {
            throw new Exception("Cant load: " + path);
        }
    }

    ~graphicResource()
    {
        texture = null;
    }
}
//Manager zasobów graficznych
public class resourcesContainer
{
    List<graphicResource> graphics;
    List<string> paths;

    Font font;

    const string fontPath = "\\Resources\\Font\\GillSansMT.TTF";

    const string pathToFile = "\\Resources\\data\\graphicsPaths.txt";

    const int maxNumberOfGraphics = 10;

    public enum textureCode : int
    {
        background = 0,
        grayCar = 1,
        greenCar = 2,
        orangeCar = 3,
        ursus = 4,
        carTire = 5
    }

    public resourcesContainer()
    {
        graphics = new List<graphicResource>();
        paths = new List<string>();

        StreamReader file; ;
        string line;

        try
        {
            file = new StreamReader(Directory.GetCurrentDirectory() + pathToFile);
            line = file.ReadLine();

            for (int i = 0; i < maxNumberOfGraphics; i++)
            {
                graphics.Add(new graphicResource());
                if (line != null)
                {
                    paths.Add(new string(Directory.GetCurrentDirectory() + line));
                    line = file.ReadLine();
                }
            }

            file.Close();

            font = new Font(Directory.GetCurrentDirectory() + "\\Resources\\Font\\GillSansMT.TTF");
        }
        catch (Exception e)
        {
            Console.WriteLine("An error ocurred during file reading.");
        }
    }

    ~resourcesContainer()
    {
        for (int i = 0; 0 < maxNumberOfGraphics; i++)
        {
            graphics[i] = null;
        }

        graphics.Clear();
        GC.Collect();
    }

    void loadTexture(textureCode code)
    {
        try
        {
            graphics[((int)code)].load(Directory.GetCurrentDirectory() + paths[((int)code)]);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public Texture getTexture(textureCode code)
    {
        if (((int)code) >= maxNumberOfGraphics)
        {
            throw new Exception("Number out of range");
            return null;
        }

        if (!graphics[((int)code)].isLoaded)
        {
            graphics[((int)code)].load(paths[((int)code)]);
        }

        return graphics[((int)code)].texture;
    }

    public Font getFont()
    {
        return font;
    }
}