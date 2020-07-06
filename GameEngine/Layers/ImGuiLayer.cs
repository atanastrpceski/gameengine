using GameEngine.Core;
using GameEngine.Core.Events;
using ImGuiNET;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Threading;

namespace GameEngine.Layers
{
    public class ImGuiLayer : Layer
    {
        private int s_fontTexture;
        private DateTime _previousFrameStartTime;
        private static double s_desiredFrameLength = 1f / 60.0f;

        public ImGuiLayer() : base("ImGui")
        { 
        
        }

        public unsafe override void OnUpdate()
        {
            _previousFrameStartTime = DateTime.UtcNow;

            RenderFrame();

            DateTime afterFrameTime = DateTime.UtcNow;
            double elapsed = (afterFrameTime - _previousFrameStartTime).TotalSeconds;
            double sleepTime = s_desiredFrameLength - elapsed;
            if (sleepTime > 0.0)
            {
                DateTime finishTime = afterFrameTime + TimeSpan.FromSeconds(sleepTime);
                while (DateTime.UtcNow < finishTime)
                {
                    Thread.Sleep(0);
                }
            }

            base.OnUpdate();
        }
        
        public unsafe override void OnAttach()
        {
            Log.CoreLogger.Info("Attaching Overlay: " + GetName());

            ImGui.LoadDefaultFont();
            SetOpenTKKeyMappings();
            CreateDeviceObjects();
            base.OnAttach();
        }

        public unsafe override void OnDetach()
        {
            Log.CoreLogger.Info("Detaching Overlay: " + GetName());
            base.OnDetach();
        }
        
        public unsafe override void OnEvent(Event @event)
        {
            EventDispatcher<MouseButtonPressedEvent>.Dispatch(@event, OnMouseButtonPressedEvent);
            EventDispatcher<MouseButtonReleasedEvent>.Dispatch(@event, OnMouseButtonReleasedEvent);
            EventDispatcher<MouseMovedEvent>.Dispatch(@event, OnMouseMovedEvent);
            EventDispatcher<MouseScrolledEvent>.Dispatch(@event, OnMouseScrolledEvent);
            EventDispatcher<KeyPressedEvent>.Dispatch(@event, OnKeyPressedEvent);
            EventDispatcher<KeyReleasedEvent>.Dispatch(@event, OnKeyReleasedEvent);
            EventDispatcher<KeyTypedEvent>.Dispatch(@event, OnKeyTypedEvent);
            EventDispatcher<WindowResizeEvent>.Dispatch(@event, OnWindowResizeEvent);

            base.OnEvent(@event);
        }

        private unsafe void OnMouseButtonPressedEvent(MouseButtonPressedEvent e)
        {
            ImGui.GetIO().MouseDown[e.GetMouseButton()] = true;
        }

        private unsafe void OnMouseButtonReleasedEvent(MouseButtonReleasedEvent e)
        {
            ImGui.GetIO().MouseDown[e.GetMouseButton()] = false;
        }

        private unsafe void OnMouseMovedEvent(MouseMovedEvent e)
        {
            ImGui.GetIO().MousePosition = new System.Numerics.Vector2((float)e.GetXOffset(), (float)e.GetYOffset());
        }

        private unsafe void OnWindowResizeEvent(WindowResizeEvent e)
        {
            var io = ImGui.GetIO();
            io.DisplaySize = new System.Numerics.Vector2(e.GetWidth(), e.GetHeight());
            io.DisplayFramebufferScale = new System.Numerics.Vector2(1.0f, 1.0f);
            GL.Viewport(0, 0, e.GetWidth(), e.GetHeight());
        }

        private unsafe void OnKeyTypedEvent(KeyTypedEvent e)
        {
            int keycode = e.GetChar();
            if (keycode > 0 && keycode < 0x10000)
                ImGuiNative.ImGuiIO_AddInputCharacter((ushort)keycode);
        }

        private unsafe void OnMouseScrolledEvent(MouseScrolledEvent e)
        {
            var io = ImGui.GetIO();
            io.MouseWheel += (float)e.GetYOffset();
        }

        private unsafe void OnKeyPressedEvent(KeyPressedEvent e)
        {
            ImGui.GetIO().KeysDown[(int)e.GetKeyCode()] = true;
            UpdateModifiers(e.GetKeyModifiers());
        }

        private unsafe void OnKeyReleasedEvent(KeyReleasedEvent e)
        {
            ImGui.GetIO().KeysDown[(int)e.GetKeyCode()] = false;
            UpdateModifiers(e.GetKeyModifiers());
        }
        private static unsafe void UpdateModifiers(KeyModifiers e)
        {
            var io = ImGui.GetIO();
            io.AltPressed = e == KeyModifiers.Alt;
            io.CtrlPressed = e == KeyModifiers.Control;
            io.ShiftPressed = e == KeyModifiers.Shift;
        }

        private static unsafe void SetOpenTKKeyMappings()
        {
            var io = ImGui.GetIO();

            io.KeyMap[GuiKey.Tab] = (int)Key.Tab;
            io.KeyMap[GuiKey.LeftArrow] = (int)Key.Left;
            io.KeyMap[GuiKey.RightArrow] = (int)Key.Right;
            io.KeyMap[GuiKey.UpArrow] = (int)Key.Up;
            io.KeyMap[GuiKey.DownArrow] = (int)Key.Down;
            io.KeyMap[GuiKey.PageUp] = (int)Key.PageUp;
            io.KeyMap[GuiKey.PageDown] = (int)Key.PageDown;
            io.KeyMap[GuiKey.Home] = (int)Key.Home;
            io.KeyMap[GuiKey.End] = (int)Key.End;
            io.KeyMap[GuiKey.Delete] = (int)Key.Delete;
            io.KeyMap[GuiKey.Backspace] = (int)Key.BackSpace;
            io.KeyMap[GuiKey.Enter] = (int)Key.Enter;
            io.KeyMap[GuiKey.Escape] = (int)Key.Escape;
            io.KeyMap[GuiKey.A] = (int)Key.A;
            io.KeyMap[GuiKey.C] = (int)Key.C;
            io.KeyMap[GuiKey.V] = (int)Key.V;
            io.KeyMap[GuiKey.X] = (int)Key.X;
            io.KeyMap[GuiKey.Y] = (int)Key.Y;
            io.KeyMap[GuiKey.Z] = (int)Key.Z;
        }

        private unsafe void CreateDeviceObjects()
        {
            IO io = ImGui.GetIO();

            // Build texture atlas
            FontTextureData texData = io.FontAtlas.GetTexDataAsAlpha8();

            // Create OpenGL texture
            s_fontTexture = GL.GenTexture();
            GL.BindTexture(TextureTarget.Texture2D, s_fontTexture);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)All.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)All.Linear);
            GL.TexImage2D(
                TextureTarget.Texture2D,
                0,
                PixelInternalFormat.Alpha,
                texData.Width,
                texData.Height,
                0,
                PixelFormat.Alpha,
                PixelType.UnsignedByte,
                new IntPtr(texData.Pixels));

            // Store the texture identifier in the ImFontAtlas substructure.
            io.FontAtlas.SetTexID(s_fontTexture);

            // Cleanup (don't clear the input data if you want to append new fonts later)
            //io.Fonts->ClearInputData();
            io.FontAtlas.ClearTexData();
            GL.BindTexture(TextureTarget.Texture2D, 0);
        }

        private unsafe void RenderFrame()
        {
            IO io = ImGui.GetIO();
            io.DisplaySize = new System.Numerics.Vector2(Application.GetWindow().GetWidth(), Application.GetWindow().GetHeight());
            io.DisplayFramebufferScale = new System.Numerics.Vector2(1.0f);
            io.DeltaTime = (1f / 60f);

            ImGui.NewFrame();

            PreRenderFrame();
            UpdateRenderState();

            ImGui.Render();

            DrawData* data = ImGui.GetDrawData();
            RenderImDrawData(data);
        }

        private unsafe void PreRenderFrame()
        {
            
        }

        protected unsafe void UpdateRenderState()
        {
            ImGuiNative.igGetStyle()->WindowRounding = 0;
            ImGuiNative.igGetStyle()->ColumnsMinSpacing = 1;
            var windowSize = new System.Numerics.Vector2(400, 400);
            ImGui.SetNextWindowSize(windowSize, SetCondition.Always);
            ImGui.SetNextWindowPosCenter(SetCondition.Always);
            ImGui.BeginWindow("Demo Window", WindowFlags.AlwaysAutoResize);
            //WindowFlags.NoResize | WindowFlags.NoTitleBar | WindowFlags.NoMove | WindowFlags.ShowBorders | WindowFlags.MenuBar | WindowFlags.NoScrollbar

            ImGui.EndWindow();
        }

        private unsafe void RenderImDrawData(DrawData* draw_data)
        {
            // Rendering
            int display_w, display_h;
            display_w = Application.GetWindow().GetWidth();
            display_h = Application.GetWindow().GetHeight();

            var clear_color = new System.Numerics.Vector4(114f / 255f, 144f / 255f, 154f / 255f, 1.0f);
            GL.Viewport(0, 0, display_w, display_h);
            GL.ClearColor(clear_color.X, clear_color.Y, clear_color.Z, clear_color.W);
            GL.Clear(ClearBufferMask.ColorBufferBit);

            // We are using the OpenGL fixed pipeline to make the example code simpler to read!
            // Setup render state: alpha-blending enabled, no face culling, no depth testing, scissor enabled, vertex/texcoord/color pointers.
            int last_texture;
            GL.GetInteger(GetPName.TextureBinding2D, out last_texture);
            GL.PushAttrib(AttribMask.EnableBit | AttribMask.ColorBufferBit | AttribMask.TransformBit);
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactor.SrcAlpha, BlendingFactor.OneMinusSrcAlpha);
            GL.Disable(EnableCap.CullFace);
            GL.Disable(EnableCap.DepthTest);
            GL.Enable(EnableCap.ScissorTest);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            GL.EnableClientState(ArrayCap.ColorArray);
            GL.Enable(EnableCap.Texture2D);

            GL.UseProgram(0);

            // Handle cases of screen coordinates != from framebuffer coordinates (e.g. retina displays)
            IO io = ImGui.GetIO();
            ImGui.ScaleClipRects(draw_data, io.DisplayFramebufferScale);

            // Setup orthographic projection matrix
            GL.MatrixMode(MatrixMode.Projection);
            GL.PushMatrix();
            GL.LoadIdentity();
            GL.Ortho(
                0.0f,
                io.DisplaySize.X / io.DisplayFramebufferScale.X,
                io.DisplaySize.Y / io.DisplayFramebufferScale.Y,
                0.0f,
                -1.0f,
                1.0f);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PushMatrix();
            GL.LoadIdentity();

            // Render command lists

            for (int n = 0; n < draw_data->CmdListsCount; n++)
            {
                DrawList* cmd_list = draw_data->CmdLists[n];
                byte* vtx_buffer = (byte*)cmd_list->VtxBuffer.Data;
                ushort* idx_buffer = (ushort*)cmd_list->IdxBuffer.Data;

                DrawVert vert0 = *((DrawVert*)vtx_buffer);
                DrawVert vert1 = *(((DrawVert*)vtx_buffer) + 1);
                DrawVert vert2 = *(((DrawVert*)vtx_buffer) + 2);

                GL.VertexPointer(2, VertexPointerType.Float, sizeof(DrawVert), new IntPtr(vtx_buffer + DrawVert.PosOffset));
                GL.TexCoordPointer(2, TexCoordPointerType.Float, sizeof(DrawVert), new IntPtr(vtx_buffer + DrawVert.UVOffset));
                GL.ColorPointer(4, ColorPointerType.UnsignedByte, sizeof(DrawVert), new IntPtr(vtx_buffer + DrawVert.ColOffset));

                for (int cmd_i = 0; cmd_i < cmd_list->CmdBuffer.Size; cmd_i++)
                {
                    DrawCmd* pcmd = &(((DrawCmd*)cmd_list->CmdBuffer.Data)[cmd_i]);
                    if (pcmd->UserCallback != IntPtr.Zero)
                    {
                        throw new NotImplementedException();
                    }
                    else
                    {
                        GL.BindTexture(TextureTarget.Texture2D, pcmd->TextureId.ToInt32());
                        GL.Scissor(
                            (int)pcmd->ClipRect.X,
                            (int)(io.DisplaySize.Y - pcmd->ClipRect.W),
                            (int)(pcmd->ClipRect.Z - pcmd->ClipRect.X),
                            (int)(pcmd->ClipRect.W - pcmd->ClipRect.Y));
                        ushort[] indices = new ushort[pcmd->ElemCount];
                        for (int i = 0; i < indices.Length; i++) { indices[i] = idx_buffer[i]; }
                        GL.DrawElements(PrimitiveType.Triangles, (int)pcmd->ElemCount, DrawElementsType.UnsignedShort, new IntPtr(idx_buffer));
                    }
                    idx_buffer += pcmd->ElemCount;
                }
            }

            // Restore modified state
            GL.DisableClientState(ArrayCap.ColorArray);
            GL.DisableClientState(ArrayCap.TextureCoordArray);
            GL.DisableClientState(ArrayCap.VertexArray);
            GL.BindTexture(TextureTarget.Texture2D, last_texture);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.PopMatrix();
            GL.MatrixMode(MatrixMode.Projection);
            GL.PopMatrix();
            GL.PopAttrib();
        }
    }
}
