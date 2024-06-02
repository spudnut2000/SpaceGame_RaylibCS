using System.Numerics;
using Raylib_cs;

namespace SpaceGame_RaylibCS;

public class Player
{

    public Texture2D Texture;
    public Vector2 Position = new();
    public float Rotation = 0f;
    public Vector2 Velocity = new();
    public Vector2 Direction = new(1,0);
    public float Speed = 200f;
    public Vector2 Damping = new(0.95f, 0.95f);
    
    private Vector2 _textureOrigin;
    private Rectangle _sourceRect = new(0,0,0,0);
    private Rectangle _destRect = new(0,0,0,0);

    public Player()
    {
        Texture = Raylib.LoadTexture("Assets/Textures/ship1.png");
        Texture.Height /= 4;
        Texture.Width /= 4;
        _textureOrigin = new Vector2(Texture.Width / 2f, Texture.Height / 2f);
        Position = new Vector2(Program.ScreenWidth / 2f, Program.ScreenHeight / 2f);
    }

    public void Update()
    {
        HandleRotation();
        HandlePosition();
    }

    public void Draw()
    {
        Raylib.DrawTexturePro(Texture, _sourceRect, _destRect, _textureOrigin, Rotation, Program.Blue2);
    }
    
    public void Unload()
    {
        Raylib.UnloadTexture(Texture);
    }
    
    private void HandlePosition()
    {
        _sourceRect.X = 0;
        _sourceRect.Y = 0;
        _sourceRect.Width = Texture.Width;
        _sourceRect.Height = Texture.Height;
        
        _destRect.X = Position.X;
        _destRect.Y = Position.Y;
        _destRect.Width = Texture.Width;
        _destRect.Height = Texture.Height;
        
        Position += Velocity * Raylib.GetFrameTime();

        if (Raylib.IsKeyDown(KeyboardKey.W))
        {
            var rads = Rotation * (Math.PI / 180);
            Direction.X = (float)Math.Cos(rads);
            Direction.Y = (float)Math.Sin(rads);
            Direction = Vector2.Normalize(Direction);
            
            Velocity += Direction * Speed * Raylib.GetFrameTime();
        }
        else
        {
            Velocity = Vector2.Lerp(Velocity, Vector2.Zero, Damping.X * Raylib.GetFrameTime());
        }
    }
    
    private void HandleRotation()
    {
        var rads = Math.Atan2(Raylib.GetMousePosition().Y - Position.Y, Raylib.GetMousePosition().X - Position.X);
        var degs = rads * (180 / Math.PI);
        if (degs < 0) degs += 360;
        if (Rotation < 0) Rotation += 360;
        var diff = degs - Rotation;
        if (Math.Abs(diff) > 180)
        {
            if (diff > 0) Rotation += 360;
            else degs += 360;
        }
        Rotation = float.Lerp(Rotation, (float)degs, 2f * Raylib.GetFrameTime());
        if (Rotation >= 360) Rotation -= 360;
    }
}
