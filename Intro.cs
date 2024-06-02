using Raylib_cs;

namespace SpaceGame_RaylibCS;

public class Intro
{
    public bool Finished = false;
    
    private int _logoPositionX = Program.ScreenWidth/2 - 128;
    private int _logoPositionY = Program.ScreenHeight/2 - 128;

    private int _framesCounter = 0;
    private int _lettersCount = 0;

    private int _topSideRecWidth = 16;
    private int _leftSideRecHeight = 16;

    private int _bottomSideRecWidth = 16;
    private int _rightSideRecHeight = 16;

    private int _state = 0;
    private float _alpha = 1f;

    private string text = "raylib";
    
    
    public Intro() {}

    public void Update()
    {
        if (_state == 0)
        {
            _framesCounter++;

            if (_framesCounter == 120)
            {
                _state = 1;
                _framesCounter = 0;
            }
        }
        else if (_state == 1)
        {
            _topSideRecWidth += 4;
            _leftSideRecHeight += 4;

            if (_topSideRecWidth == 256) _state = 2;
        }
        else if (_state == 2)
        {
            _bottomSideRecWidth += 4;
            _rightSideRecHeight += 4;

            if (_bottomSideRecWidth == 256) _state = 3;
        }
        else if (_state == 3)
        {
            _framesCounter++;

            if (_framesCounter % 12 == 0)
            {
                _lettersCount++;
                _framesCounter = 0;
            }

            if (_lettersCount >= text.Length)
            {
                _alpha -= 0.02f;

                if (_alpha <= 0f)
                {
                    _alpha = 0f;
                    Finished = true;
                }
            }
        }
    }

    public void Draw()
    {
        if (_state == 0)
        {
            if ((_framesCounter / 15) % 2 == 0) Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, 16, Color.Black);
        }
        else if (_state == 1)
        {
            Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRecWidth, 16, Color.Black);
            Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, _leftSideRecHeight, Color.Black);
        }
        else if (_state == 2)
        {
            Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRecWidth, 16, Color.Black);
            Raylib.DrawRectangle(_logoPositionX, _logoPositionY, 16, _leftSideRecHeight, Color.Black);
            
            Raylib.DrawRectangle(_logoPositionX + 240, _logoPositionY, 16, _rightSideRecHeight, Color.Black);
            Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 240, _bottomSideRecWidth, 16, Color.Black);
        }
        else if (_state == 3)
        {
            unsafe
            {
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY, _topSideRecWidth, 16, Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 16, 16, _leftSideRecHeight - 32, Raylib.Fade(Color.Black, _alpha));

                Raylib.DrawRectangle(_logoPositionX + 240, _logoPositionY + 16, 16, _rightSideRecHeight - 32, Raylib.Fade(Color.Black, _alpha));
                Raylib.DrawRectangle(_logoPositionX, _logoPositionY + 240, _bottomSideRecWidth, 16, Raylib.Fade(Color.Black, _alpha));

                Raylib.DrawRectangle(Raylib.GetScreenWidth()/2 - 112, Raylib.GetScreenHeight()/2 - 112, 224, 224, Raylib.Fade(Color.RayWhite, _alpha));



                if (_lettersCount <= text.Length)
                {
                    Raylib.DrawText(text.Substring(0, _lettersCount), Raylib.GetScreenWidth()/2 - 44, 
                        Raylib.GetScreenHeight()/2 + 48, 50, Raylib.Fade(Color.Black, _alpha));
                }

            }
        }
    }
}