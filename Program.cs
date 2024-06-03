using System.Numerics;
using Raylib_cs;
using static Raylib_cs.Raylib;
using rlImGui_cs;
using ImGuiNET;
using SpaceGame_RaylibCS;

class Program
{
    
    public static int ScreenWidth = 1280;
    public static int ScreenHeight = 720;

    public static Color Blue1 = new Color(0, 45, 45, 255);
    public static Color Blue2 = new Color(0, 65, 65, 255);
    
    public static Player Player;
    
    private static Camera2D _camera;


    private static Intro _intro;
    private static DebugUi _debugUi;
    
    public static void ExitApp()
    {
        Player.Unload();
        rlImGui.Shutdown();
        CloseWindow();
    }
    
    
    private static void Main()
    {
        SetConfigFlags(ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow);
        InitWindow(ScreenWidth, ScreenHeight, "SpaceGame RaylibCS");
        SetTargetFPS(60);

        _debugUi = new DebugUi();
        Player = new Player();
        _intro = new Intro();
        

        _camera.Target = new Vector2(0, 0);
        _camera.Zoom = 1f;
        _camera.Rotation = 0f;

        
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
    
    private static void Update()
    {
        _intro.Update();
        
        Player.Update();
    }
    
    private static void Draw2D()
    {
        BeginMode2D(_camera);

        _intro.Draw();
        Player.Draw();
        
        EndMode2D();
    }

    private static void DrawGUI()
    {
        rlImGui.Begin();
        
        _debugUi.Draw();
        
        rlImGui.End();
    }
}