using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using rlImGui_cs;
using ImGuiNET;

class Program
{
    
    public static int ScreenWidth = 1280;
    public static int ScreenHeight = 720;

    public static Color Blue1 = new Color(0, 45, 45, 255);
    public static Color Blue2 = new Color(0, 65, 65, 255);
    
    private static Camera2D _camera;
    
    private static float _shipAngleDegrees;
    private static float _shipSpeed = 200f;
    private static Vector2 _shipDirection = new(1,0);
    private static Rectangle _shipSourceRect = new(0,0,0,0);
    private static Rectangle _shipDestRect = new(0,0,0,0);
    
    private static Texture2D _texture;
    private static Vector2 _textureOrigin = new();
    private static Vector2 _texturePosition = new();
    private static Vector2 _mousePosition = new();
    
    private static void Main()
    {
        SetConfigFlags(ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow | ConfigFlags.UndecoratedWindow);
        InitWindow(ScreenWidth, ScreenHeight, "SpaceGame RaylibCS");
        //SetTargetFPS(60);

        _camera.Target = _texturePosition;
        _camera.Zoom = 1f;
        _camera.Rotation = 0f;
        
        _texture = LoadTexture("Assets/Textures/ship1.png");
        _texture.Height /= 4;
        _texture.Width /= 4;
        _textureOrigin = new Vector2(_texture.Width / 2f, _texture.Height / 2f);
        _texturePosition = new Vector2(ScreenWidth / 2f, ScreenHeight / 2f);
        
        rlImGui.Setup(true);
        while (!WindowShouldClose())
        {

            if (IsWindowResized())
            {
                ScreenWidth = GetScreenWidth();
                ScreenHeight = GetScreenHeight();
            }
            
            Update();
            BeginDrawing();
            ClearBackground(Blue1);
            Draw2D();
            DrawGUI();
            EndDrawing();
        }
        
        
    }

    private static void ExitApp()
    {
        UnloadTexture(_texture);
        rlImGui.Shutdown();
        CloseWindow();
    }
    
    private static void Update()
    {
        HandleShipMovement();
    }
    
    private static void Draw2D()
    {
        BeginMode2D(_camera);
        DrawTexturePro(_texture, _shipSourceRect, _shipDestRect, _textureOrigin, _shipAngleDegrees, Blue2);
        EndMode2D();
    }

    private static void DrawGUI()
    {
        rlImGui.Begin();
            
        if (ImGui.Begin("Debug window"))
        {
            ImGui.TextUnformatted("FPS: " + GetFPS());
            ImGui.TextUnformatted("Ship position: " + _texturePosition);
            ImGui.TextUnformatted("Ship rotation: " + _shipAngleDegrees);
            ImGui.TextUnformatted("Ship dir: " + _shipDirection);

            if (ImGui.Button("Exit game"))
            {
                ExitApp();
            }
        }
        ImGui.End();
        rlImGui.End();
    }
    
    private static void HandleShipMovement()
    {
        _mousePosition = GetMousePosition();
        
        _shipSourceRect.X = 0;
        _shipSourceRect.Y = 0;
        _shipSourceRect.Width = _texture.Width;
        _shipSourceRect.Height = _texture.Height;
        
        _shipDestRect.X = _texturePosition.X;
        _shipDestRect.Y = _texturePosition.Y;
        _shipDestRect.Width = _texture.Width;
        _shipDestRect.Height = _texture.Height;

        var rads =
            (float)Math.Atan2(_mousePosition.Y - _texturePosition.Y, _mousePosition.X - _texturePosition.X);
        _shipAngleDegrees = rads * (180f / MathF.PI);
        
        

        if (IsKeyDown(KeyboardKey.W))
        {
            _shipDirection.X = (float)Math.Cos(rads);
            _shipDirection.Y = (float)Math.Sin(rads);
            _shipDirection = Vector2.Normalize(_shipDirection); 
            _texturePosition += _shipDirection * _shipSpeed * GetFrameTime();
        }
    }
}