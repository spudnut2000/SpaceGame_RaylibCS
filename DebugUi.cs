using ImGuiNET;
using Raylib_cs;
using rlImGui_cs;

namespace SpaceGame_RaylibCS;

public class DebugUi
{
    public DebugUi() {}

    public void Draw()
    {
        if (ImGui.Begin("Debug window"))
        {
            ImGui.TextUnformatted("FPS: " + Raylib.GetFPS());
            ImGui.TextUnformatted("Ship position: " + Program.Player.Position);
            ImGui.TextUnformatted("Ship rotation: " + Program.Player.Rotation);
            ImGui.TextUnformatted("Ship dir: " + Program.Player.Direction);
            ImGui.Separator();
            if (ImGui.SliderFloat("Speed", ref Program.Player.Speed, 0f, 500f))
            {
                Program.Player.Speed = Math.Clamp(Program.Player.Speed, 0f, 500f);
            }
            if (ImGui.SliderFloat("Damping", ref Program.Player.Damping, 0f, 10f))
            {
                Program.Player.Damping = Math.Clamp(Program.Player.Damping, 0f, 10f);
            }
            ImGui.Separator();
            if (ImGui.Button("Exit game"))
            {
                Program.ExitApp();
            }
        }
        ImGui.End();
    }
}