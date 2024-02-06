using OfficeOpenXml;
using SFML.Graphics;
using SFML.System;
using SFML.Window;

//Klasa interfejsu logowania. Stworzona specjlanie na potrzeby implementacji podstawowej walidacji wprowadzanych danych.
public class DriverDataInterface : Interface
{
    Sprite background;

    List<GUI_Object> GUI_Objects;

    TextInput loginInput;
    TextInput passwordInput;
    TextInput licensePlateInput;

    MessageBox _messageBox;
    Vector2f _messageBoxPosition;
    Vector2f _messageBoxSize;

    public DriverDataInterface(CarSimulator feedback) : base(feedback)
    {
        background = new Sprite(feedback.getTexture(resourcesContainer.textureCode.background));

        _messageBoxSize = new Vector2f(800, 300);
        _messageBoxPosition = new Vector2f((feedback.WindowXresolution - _messageBoxSize.X) / 2, (feedback.WindowYresolution - _messageBoxSize.Y) / 2);

        GUI_Objects = new List<GUI_Object>();

        Vector2f loginInterfaceBoxPosition, loginInterfaceBoxSize;
        loginInterfaceBoxSize = new Vector2f(500, 600);
        loginInterfaceBoxPosition = new Vector2f((feedback.WindowXresolution - loginInterfaceBoxSize.X) / 2, (feedback.WindowYresolution - loginInterfaceBoxSize.Y) / 2);
        float verticalDistanceBetweenGUIelements = 60;

        GUI_Caption loginCaption = new GUI_Caption(feedback.getFont(), "Login:", loginInterfaceBoxPosition, 40, SFML.Graphics.Color.Black);
        GUI_Objects.Add(loginCaption);
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements;

        loginInput = new TextInput(loginInterfaceBoxPosition, new Vector2f(loginInterfaceBoxSize.X, 50), SFML.Graphics.Color.White, SFML.Graphics.Color.Black);
        GUI_Objects.Add(loginInput);
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements;

        GUI_Caption passwordCaption = new GUI_Caption(feedback.getFont(), "Hasło:", loginInterfaceBoxPosition, 40, SFML.Graphics.Color.Black);
        GUI_Objects.Add(passwordCaption);
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements;

        passwordInput = new TextInput(loginInterfaceBoxPosition, new Vector2f(loginInterfaceBoxSize.X, 50), SFML.Graphics.Color.White, SFML.Graphics.Color.Black);
        GUI_Objects.Add(passwordInput);
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements;

        GUI_Caption licensePlateCaption = new GUI_Caption(feedback.getFont(), "Tablica rejestracyjna:", loginInterfaceBoxPosition, 40, SFML.Graphics.Color.Black);
        GUI_Objects.Add(licensePlateCaption);
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements - 20;

        GUI_Caption licensePlateCaptionAdnotation = new GUI_Caption(feedback.getFont(), "Wymagana w przypadku tworzenia nowego prfilu kierowcy", loginInterfaceBoxPosition, 20, SFML.Graphics.Color.Black);
        GUI_Objects.Add(licensePlateCaptionAdnotation);
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements - 20;

        licensePlateInput = new TextInput(loginInterfaceBoxPosition, new Vector2f(loginInterfaceBoxSize.X, 50), SFML.Graphics.Color.White, SFML.Graphics.Color.Black);
        licensePlateInput.caption = "RZ ";
        GUI_Objects.Add(licensePlateInput);
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements * 3;

        GUI_Objects.Add(new GUI_driverDataLoginButton(loginInterfaceBoxPosition, new Vector2f(loginInterfaceBoxSize.X, 50), feedback.getFont(), "Zaloguj", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements;
        GUI_Objects.Add(new GUI_newDriverAccount(loginInterfaceBoxPosition, new Vector2f(loginInterfaceBoxSize.X, 50), feedback.getFont(), "Utwórz nowy profil kierowcy", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));
        loginInterfaceBoxPosition.Y += verticalDistanceBetweenGUIelements;
        GUI_Objects.Add(new GUI_driverDataReturnButton(loginInterfaceBoxPosition, new Vector2f(loginInterfaceBoxSize.X, 50), feedback.getFont(), "Powrot", 35, new Color(250, 250, 250), new Color(220, 220, 220), new Color(190, 190, 190), new Color(0, 0, 0), this));


        ExcelPackage.LicenseContext = LicenseContext.NonCommercial; //ustawienie licencji pakietu EPPlus na Noncommercial
    }

    ~DriverDataInterface()
    {

    }
    //rysowanie interfejsu
    public override void Render(RenderTexture drawTexture)
    {
        drawTexture.Draw(background);
        foreach (GUI_Object i in GUI_Objects.ToList()) //toList eliminuje problem błędu przy dodaniu przycisku
        {
            i.draw(drawTexture);
        }

        if (_messageBox != null) _messageBox.draw(drawTexture);
    }
    //aktualizacja interfejsu
    public override void Update(List<Keyboard.Key> pressedKeys)
    {
        foreach (GUI_Object i in GUI_Objects.ToList()) //to list eliminuje problem błędu przy dodaniu przycisku
        {
            i.update(Mouse.GetPosition(feedback.window), Mouse.IsButtonPressed(Mouse.Button.Left), pressedKeys);
        }

        if (_messageBox != null) _messageBox.update(Mouse.GetPosition(feedback.window), Mouse.IsButtonPressed(Mouse.Button.Left), pressedKeys);
    }
    //powrót do poprzedniego interfejsu
    public void goBack()
    {
        feedback.mainMenu();
    }
    //przejście do interfejsu wyboru samochodów
    public void start()
    {
        feedback.carSelector();
    }

    //Różnice w implementowaniu funkcji i klas związanych z interfejsem wynikają z
    //pogłębiania wiedzy na temat programowania i wdrażania jej w projekt
    void accesGranted()
    {
        feedback.carSelector();
    }

    void showMessageBox(string message)
    {
        _messageBox = new MessageBox(this, message, _messageBoxPosition, _messageBoxSize, feedback.getFont());
    }
    //Funkcje wyświetlające okna z odpowiednim komunikatem
    void accesDenied()
    {
        showMessageBox("Odmowa dostępu");
    }

    void noAccountFound()
    {
        showMessageBox("Nie znalezionio konta o podanych danych logowania");
    }

    void fileReadError(Exception e)
    {
        showMessageBox(e.ToString());
    }

    void similarAccountAlreadyExists(string message)
    {
        showMessageBox(message);
    }

    void succesfullyCreatedNewAccount()
    {
        showMessageBox("Pomyślnie utworzono nowe konto");
    }

    void closeMessageBox()
    {
        _messageBox = null;
        GC.Collect();
    }

    //--------------------klasy wewnętrzne

    //Klasa przycisku powrotu
    class GUI_driverDataReturnButton : GUI_Button
    {
        DriverDataInterface feedback;
        public GUI_driverDataReturnButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, DriverDataInterface feedback)
            : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
        {
            this.feedback = feedback;
        }

        ~GUI_driverDataReturnButton()
        {

        }

        public override void onClick()
        {
            feedback.goBack();
        }
    }
    //Klasa przycisku logowania
    class GUI_driverDataLoginButton : GUI_Button
    {
        DriverDataInterface feedback;
        public GUI_driverDataLoginButton(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, DriverDataInterface feedback)
            : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
        {
            this.feedback = feedback;
        }

        ~GUI_driverDataLoginButton()
        {

        }
        //Wczytywanie danych z bazy w pliku xlsx
        public override void onClick()
        {
            try
            {
                //Zmienna pakietu excel
                ExcelPackage xlsx = new ExcelPackage(new FileInfo(Directory.GetCurrentDirectory() + "\\Resources\\data\\driversData.xlsx"));

                //Zmienna arkusza
                var myWorksheet = xlsx.Workbook.Worksheets.SingleOrDefault(); //Wybor arkusza

                //rozmiar arkusza
                int rows = myWorksheet.Dimension.End.Row; //rozmiar arkusza - rzędy
                int columns = myWorksheet.Dimension.End.Column; //rozmiar arkusza - kolumny

                //początkowy rząd i kolumna
                int row = 2;
                int column = 1;

                bool foundAccount = false;

                //walidacja danych logowania
                while (row <= rows)
                {
                    if (myWorksheet.Cells[row, column].Value == null)
                    {
                        row++;
                        continue;
                    }
                    if (feedback.loginInput.caption == myWorksheet.Cells[row, column].Value.ToString())
                    {
                        column++;
                        if (feedback.passwordInput.caption == myWorksheet.Cells[row, column].Value.ToString())
                        {
                            //Dostęp przyznany

                            xlsx.Dispose();
                            feedback.accesGranted();
                            break;
                        }
                        else
                        {
                            //Odmowa dostępu - Błędne hasło
                            feedback.accesDenied();
                            foundAccount = true;
                            break;
                        }
                    }
                    row++;
                }

                //Po przejsciu pętli: Brak konta o podanym loginie w bazie

                if (!foundAccount) feedback.noAccountFound();
                xlsx.Dispose();
            }
            catch (Exception e)
            {
                feedback.fileReadError(e);
            }

        }
    }
    //Klasa tworzenia nowego konta
    class GUI_newDriverAccount :GUI_Button
    {
        DriverDataInterface feedback;
        public GUI_newDriverAccount(Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor, DriverDataInterface feedback)
            : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
        {
            this.feedback = feedback;
        }

        ~GUI_newDriverAccount()
        {

        }

        public override void onClick()
        {
            if (feedback.loginInput.caption == "" || feedback.passwordInput.caption == "" || feedback.licensePlateInput.caption == "")
            {
                feedback.showMessageBox("Pola nie mogą być puste.");
                return;
            }

            try
            {
                ExcelPackage xlsx = new ExcelPackage(new FileInfo(Directory.GetCurrentDirectory() + "\\Resources\\data\\driversData.xlsx"));

                ExcelWorksheet myWorksheet = xlsx.Workbook.Worksheets.SingleOrDefault(); //Wybor arkusza
                int rows = myWorksheet.Dimension.End.Row; //rozmiar arkusza - rzędy
                int columns = myWorksheet.Dimension.End.Column; //rozmiar arkusza - kolumny

                int row = 2;
                int column = 1;

                bool similarAccountFound = false;

                //walidacja danych logowania
                while (row <= rows)
                {
                    if (myWorksheet.Cells[row, column].Value == null)
                    {
                        row++;
                        continue;
                    }
                    if (feedback.loginInput.caption == myWorksheet.Cells[row, column].Value.ToString() ||
                        feedback.licensePlateInput.caption == myWorksheet.Cells[row, column + 2].Value.ToString())
                    {
                        feedback.similarAccountAlreadyExists("Konto o podanym loginie lub tablicy rejestracyjnej już istnieje.");
                        similarAccountFound = true;
                    }
                    row++;
                }

                if (!similarAccountFound)
                {
                    myWorksheet.Cells[rows + 1, column].Value = feedback.loginInput.caption;
                    myWorksheet.Cells[rows + 1, column + 1].Value = feedback.passwordInput.caption;
                    myWorksheet.Cells[rows + 1, column + 2].Value = feedback.licensePlateInput.caption;
                    xlsx.Save();

                    feedback.succesfullyCreatedNewAccount();
                }


                //Po przejsciu pętli: Brak konta o podanym loginie w bazie
            }
            catch (Exception e)
            {
                feedback.fileReadError(e);
            }
        }
    }
    //Struktura profilu kierowcy - Niewykorzystane
    struct driverAccount
    {
        public string login;
        public string password;
        public string licensePlate;
        public double drivenDistance;
    }
    //Klasa okna powiadomienia
    class MessageBox : GUI_Object
    {
        RectangleShape _box;

        Vector2f _position, _size;

        Font _font;

        Text _text;

        string _caption;

        DriverDataInterface _feedback;

        OKButton _OKButton;

        const int bttnSizeX = 60, bttnSizeY = 40;

        const int maxNumberOfCharactersInRow = 60;

        const int messageBoxOutlineThickness = 4;
        
        public string caption
        {
            get { return _caption; }
            set { _caption = value; }
        }
        public MessageBox(DriverDataInterface feedback, string caption, Vector2f position, Vector2f size, Font font)
        {
            this._caption = caption;
            this._position = position;
            this._size = size;
            this._font = font;
            this._feedback = feedback;

            _box = new RectangleShape();
            _box.FillColor = SFML.Graphics.Color.White;
            _box.OutlineColor = SFML.Graphics.Color.Black;
            _box.OutlineThickness = messageBoxOutlineThickness;
            _box.Size = _size;
            _box.Position = _position;

            if (_caption.Length > maxNumberOfCharactersInRow)
            {
                for (int i = maxNumberOfCharactersInRow; i < _caption.Length; i += maxNumberOfCharactersInRow)
                {
                    for (int j = i; j < caption.Length; j++)
                    {
                        if (_caption[j] == ' ')
                        {
                            _caption = _caption.Insert(j, "\n");
                            break;
                        }
                    }
                }
            }

            _text = new Text(_caption, font);
            _text.Position = _position;
            _text.FillColor = SFML.Graphics.Color.Black;

            _OKButton = new OKButton(this, new Vector2f(_position.X + (_size.X - bttnSizeX) / 2, _position.Y + (_size.Y * (float)0.9) - bttnSizeY), new Vector2f(bttnSizeX, bttnSizeY), _font, "OK", 40, new SFML.Graphics.Color(200, 200, 200), new SFML.Graphics.Color(170, 170, 170), new SFML.Graphics.Color(140, 140, 140), SFML.Graphics.Color.Black);
        }

        ~MessageBox()
        {
            _box = null;
            GC.Collect();
        }
        //Rysowanie okna
        public override void draw(RenderTexture target)
        {
            target.Draw(_box);
            target.Draw(_text);
            _OKButton.draw(target);
        }
        //Aktualizacja okna
        public override void update(Vector2i mousePosition, bool isLMBPressed, List<Keyboard.Key> pressedKeys)
        {
            _OKButton.update(mousePosition, isLMBPressed, pressedKeys);
        }
        //Zamknięcie okna
        void close()
        {
            _feedback.closeMessageBox();
        }
        //Klasa orzycisku
        class OKButton : GUI_Button
        {
            MessageBox _feedback;
            public OKButton(MessageBox feedback, Vector2f position, Vector2f size, Font font, string caption, uint fontSize, Color backgroundColor, Color activeColor, Color onClickColor, Color captionColor) : base(position, size, font, caption, fontSize, backgroundColor, activeColor, onClickColor, captionColor)
            {
                this._feedback = feedback;
            }

            ~OKButton()
            {

            }

            public override void onClick()
            {
                _feedback.close();
            }
        }
    }
}