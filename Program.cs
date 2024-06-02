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
    
    private static Camera2D _camera;

    private static Player _player;

    private static Intro _intro;
    
    
    private static void Main()
    {
        SetConfigFlags(ConfigFlags.Msaa4xHint | ConfigFlags.VSyncHint | ConfigFlags.ResizableWindow);
        InitWindow(ScreenWidth, ScreenHeight, "SpaceGame RaylibCS");
        SetTargetFPS(60);
        
        _player = new Player();
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

    private static void ExitApp()
    {
        _player.Unload();
        rlImGui.Shutdown();
        CloseWindow();
    }
    
    private static void Update()
    {
        _intro.Update();
        
        _player.Update();
    }
    
    private static void Draw2D()
    {
        BeginMode2D(_camera);

        _intro.Draw();
        
        _player.Draw();
        
        EndMode2D();
    }

    private static void DrawGUI()
    {
        rlImGui.Begin();
            
        if (ImGui.Begin("Debug window"))
        {
            ImGui.TextUnformatted("FPS: " + GetFPS());
            ImGui.TextUnformatted("Ship position: " + _player.Position);
            ImGui.TextUnformatted("Ship rotation: " + _player.Rotation);
            ImGui.TextUnformatted("Ship dir: " + _player.Direction);
            ImGui.Separator();
            if (ImGui.SliderFloat("Speed", ref _player.Speed, 0f, 500f))
            {
                _player.Speed = Math.Clamp(_player.Speed, 0f, 500f);
            }
            ImGui.Separator();
            if (ImGui.Button("Exit game"))
            {
                ExitApp();
            }
        }
        ImGui.End();
        rlImGui.End();
    }
}