using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using OpenTK.Graphics.OpenGL;
using OpenTK;
using System.IO;
using Glu = OpenTK.Graphics.Glu;
using System.Drawing.Imaging;


namespace Terminator
{
    public partial class Terminator : Form
    {
        int time, bullets, score;
        float zoom = 90;
        Engine.Camera camare;
        Engine.Light ligth;
        Engine.Maps map;
        Engine.Sound sound1;
        Engine.Sound sound2;
        Engine.Resources resource;
        int level;
        Point pointer_current, pointer_previous;
        List<Int32> GList;
        List<Objects.Plane> templates;
        List<Objects.FirstPerson> firstPerson = new List<Objects.FirstPerson>();
        bool isStatusBarVisibility = false;
        bool isTransparency = false;
        bool isLine = false;
        bool isPause = false;
        bool loaded = false;
        int efecto;
        double quality = Engine.Constant.QUALITY_PLANE_HIGH;

        public Terminator()
        {
            // Pantalla completa
            this.TopMost = true;
            this.FormBorderStyle = FormBorderStyle.None;
            this.Width = Screen.PrimaryScreen.Bounds.Width;
            this.Height = Screen.PrimaryScreen.Bounds.Height;            
            //--
            InitializeComponent();
        }

        private void gl_Paint(object sender, PaintEventArgs e)
        {
            // Preconfiguracion
            if (!loaded) 
                return;
            if (GList == null)
                return;
            loaded = false;
            //--


            // Perspectivas
            GL.Viewport(0, 0, gl.Size.Width - 1, gl.Size.Height - 1);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Glu.Perspective(zoom, ((gl.Size.Width - 1) / (gl.Size.Height - 1)), 0.01f, 10000f);
            GL.MatrixMode(MatrixMode.Modelview);
            //--


            // Camara
            GL.LoadIdentity();
            Glu.LookAt(camare.position[0], camare.position[1], camare.position[2], camare.view[0], camare.view[1], camare.view[2], 0, 0.1f, 0);
            //--


            // Luz
            ligth.positionLigth[0] = (float) camare.position[0];
            ligth.positionLigth[1] = (float)camare.position[1];
            ligth.positionLigth[2] = (float)camare.position[2];
            ligth.directionLigth[0] = (float)(camare.view[0] - camare.position[0]);
            ligth.directionLigth[1] = (float)(camare.view[1] - camare.position[1]);
            ligth.directionLigth[2] = (float)(camare.view[2] - camare.position[2]);
            ligth.directionLigth = Engine.OperationMatrixVector.normalize(ligth.directionLigth);

            GL.Light(LightName.Light0, LightParameter.Position, ligth.positionLigth);
            GL.Light(LightName.Light0, LightParameter.SpotDirection, ligth.directionLigth);
            //--


            // Profundidad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //--


            // Truco de transparencia y paredes
            if (isTransparency)
                GL.BlendFunc(BlendingFactorSrc.One, BlendingFactorDest.One);
            else
                GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);

            if (isLine)
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Line);
            else
                GL.PolygonMode(MaterialFace.FrontAndBack, PolygonMode.Fill);
            //--


            // Objetos
            GL.PushMatrix();

            try
            {
                for (int i = 0; i < 9; i++)
                {
                    GL.Disable(EnableCap.Fog);
                    GL.Disable(EnableCap.Lighting);
                    GL.CallList(GList[i]);
                }
            }
            catch { }

            if (ligth.turnOn)
                GL.Enable(EnableCap.Lighting);

            GL.Enable(EnableCap.Fog);
            if (camare.position[1] == Engine.Constant.CAMERA_HEIGHT)
                GL.Disable(EnableCap.Fog);
            try
            {
                for (int i = 9; i < GList.Count - firstPerson.Count; i++)
                {
                    GL.CallList(GList[i]);
                }
            }
            catch { }

            GL.PopMatrix();

            if (!camare.levelCompleted)
            {
                GL.Disable(EnableCap.DepthTest);
                GL.MatrixMode(MatrixMode.Projection);
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.Ortho(0, gl.Size.Width - 1, 0, gl.Size.Height - 1, -1, 1);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.CallList(efecto);
                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PopMatrix();
                GL.Enable(EnableCap.DepthTest);

                GL.Disable(EnableCap.Lighting);
                GL.Disable(EnableCap.DepthTest);
                GL.MatrixMode(MatrixMode.Projection);
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.Ortho(0, gl.Size.Width - 1, 0, gl.Size.Height - 1, -1, 1);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.CallList(GList.Count - 1);
                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PopMatrix();
                GL.Enable(EnableCap.DepthTest);
            }

            if (isPause)
            {
                GL.Disable(EnableCap.Lighting);
                GL.Disable(EnableCap.DepthTest);
                GL.MatrixMode(MatrixMode.Projection);
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.Ortho(0, gl.Size.Width - 1, 0, gl.Size.Height - 1, -1, 1);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PushMatrix();
                GL.LoadIdentity();
                GL.CallList(GList.Count - 2);
                GL.MatrixMode(MatrixMode.Projection);
                GL.PopMatrix();
                GL.MatrixMode(MatrixMode.Modelview);
                GL.PopMatrix();
                GL.Enable(EnableCap.DepthTest);
            }
            

            GL.Finish();
            //--


            // Posconfiguracion
            try { gl.SwapBuffers();}
            catch { }
            loaded = true;
            //--
        }

        private void gl_template()
        {
            // Perspectivas
            GL.Viewport(0, 0, gl.Size.Width - 1, gl.Size.Height - 1);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadIdentity();
            Glu.Perspective(zoom, ((gl.Size.Width - 1) / (gl.Size.Height - 1)), 0.01f, 10000f);
            GL.MatrixMode(MatrixMode.Modelview);
            //--


            // Camara
            GL.LoadIdentity();
            Glu.LookAt(camare.position[0], camare.position[1], camare.position[2], camare.view[0], camare.view[1], camare.view[2], 0, 0.1f, 0);
            //--


            // Luz
            GL.Disable(EnableCap.Lighting);
            //--


            // Profundidad
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            //--


            // Objetos
            GL.PushMatrix();
            try
            {
                for (int i = 0; i < GList.Count; i++)
                {
                    GL.Disable(EnableCap.Fog);
                    GL.Disable(EnableCap.Lighting);
                    GL.CallList(GList[i]);
                }
            }
            catch { }
            GL.Finish();
            //--


            // Posconfiguracion
            try { gl.SwapBuffers(); }
            catch { }
            //--
        }


        private void gl_Load(object sender, EventArgs e)
        {
            // Profundidad
            GL.ClearColor(Color.Black);
            GL.ClearDepth(1.0);
            GL.Enable(EnableCap.DepthTest);
            GL.DepthFunc(DepthFunction.Lequal);
            //--


            // Tipo de Material
            float[] mat_ambient = { 0.3945f, 0.1718f, 0.1289f, 1.0f };
            float[] mat_diffuse = { 0.8231f, 0.67f, 0.574f, 1.0f };
            float[] mat_specula = { 0.8125f, 0.6406f, 0.5898f, 1.0f };
            float[] mat_shinine = { 128.0f };

            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Ambient, mat_ambient);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Diffuse, mat_diffuse);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Specular, mat_specula);
            GL.Material(MaterialFace.FrontAndBack, MaterialParameter.Shininess, mat_shinine);
            GL.Enable(EnableCap.ColorMaterial);
            GL.ShadeModel(ShadingModel.Smooth);
            //GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Specular);
            //GL.ColorMaterial(MaterialFace.FrontAndBack, ColorMaterialParameter.Emission);
            //--


            // Textura
            GL.Enable(EnableCap.Blend);
            GL.Enable(EnableCap.Texture2D);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
            //--


            // Camara
            camare = new Engine.Camera(0, 0, -10, 0, 0, -1, 4, 2.0, 0.2, 0.2, 0.5);
            //--


            // Recursos (para mejorar el rendimiento)
            resource = new Engine.Resources();
            //--


            // Sonido
            sound1 = new Engine.Sound();
            sound2 = new Engine.Sound();
            //--


            // Antialiasing
            GL.Enable(EnableCap.LineSmooth);
            GL.Hint(HintTarget.LineSmoothHint, HintMode.DontCare);
            //--


            // Luz
            //GL.Hint(HintTarget.PerspectiveCorrectionHint, HintMode.Nicest); Mejora perspectiva supuestamente, para mi NO
            float[] ligthColor = { 1.0f, 1.0f, 1.0f, 1.0f };
            ligth = new Engine.Light();


            float[] LightAmb = { 0.4f, 0.4f, 0.4f, 1.0f};
            float[] LightDif = { 1.0f, 1.0f, 1.0f, 1.0f};
            float[] LightSpc = { 1.0f, 1.0f, 1.0f, 1.0f};

            GL.Light(LightName.Light0, LightParameter.Ambient, LightAmb);
            GL.Light(LightName.Light0, LightParameter.Diffuse, LightDif);
            GL.Light(LightName.Light0, LightParameter.Specular, LightSpc);
            GL.Light(LightName.Light0, LightParameter.SpotCutoff, 20);
            GL.Light(LightName.Light0, LightParameter.ConstantAttenuation, 0.6f);
            GL.Enable(EnableCap.Light0);
            //--


            // Label
            labelHide();
            //--


            // Tiempo y creditos (labels)
            picturCredits.Left = (gl.Size.Width / 2) - (picturCredits.Width / 2);
            picturCredits.Top = 70;

            lbl_time1_num1.Left = (gl.Size.Width / 2) + lbl_time1_num1.Left - 35;
            lbl_time1_num2.Left = (gl.Size.Width / 2) + lbl_time1_num2.Left - 35;
            lbl_time1_num3.Left = (gl.Size.Width / 2) + lbl_time1_num3.Left - 35;
            lbl_time1_num4.Left = (gl.Size.Width / 2) + lbl_time1_num4.Left - 35;
            lbl_time1_num5.Left = (gl.Size.Width / 2) + lbl_time1_num5.Left - 35;
            lbl_time1_num6.Left = (gl.Size.Width / 2) + lbl_time1_num6.Left - 35;
            lbl_time1_num7.Left = (gl.Size.Width / 2) + lbl_time1_num7.Left - 35;

            lbl_time2_num1.Left = (gl.Size.Width / 2) + lbl_time2_num1.Left - 35;
            lbl_time2_num2.Left = (gl.Size.Width / 2) + lbl_time2_num2.Left - 35;
            lbl_time2_num3.Left = (gl.Size.Width / 2) + lbl_time2_num3.Left - 35;
            lbl_time2_num4.Left = (gl.Size.Width / 2) + lbl_time2_num4.Left - 35;
            lbl_time2_num5.Left = (gl.Size.Width / 2) + lbl_time2_num5.Left - 35;
            lbl_time2_num6.Left = (gl.Size.Width / 2) + lbl_time2_num6.Left - 35;
            lbl_time2_num7.Left = (gl.Size.Width / 2) + lbl_time2_num7.Left - 35;
            //--


            // Balas y puntos
            lbl_statusBar.Top = gl.Size.Height - lbl_statusBar.Height - 1;
            lbl_statusBar.Width = gl.Size.Width;
            lbl_bullets.Top = gl.Size.Height - lbl_bullets.Height - 1;
            lbl_bullets.Left = 0;
            lbl_score.Top = gl.Size.Height - lbl_score.Height - 1;
            lbl_score.Left = gl.Size.Width - lbl_score.Width - 1;
            //--


            // Variables del jugador
            time = Engine.Constant.TIME;
            bullets = Engine.Constant.BULLETS;
            score = 0;
            //


            // Primera persona
            Objects.FirstPerson fp;

            fp = new Objects.FirstPerson(@"Weapon\weapon1.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"Weapon\weaponFire1.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"Weapon\weapon2.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"Weapon\weaponFire2.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"Weapon\weapon3.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"Weapon\weaponFire3.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"Weapon\satelliteView.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"UserInterface\pause.png", gl);
            firstPerson.Add(fp);
            fp = new Objects.FirstPerson(@"Weapon\mira.png", gl);
            firstPerson.Add(fp);
            //--


            // Activar Normalizacion
            GL.Enable(EnableCap.Normalize);
            //--


            // Iniciar sonido
            sound1.play("init.wav", true);
            //--


            // Preconfiguracion de presentacion y nivel
            level = -1;
            tmrRedraw.Stop();
            mainMenu();
            drawTemplate();
            ligth.turnOn = false;
            camare.levelCompleted = true;
            //--
        }

        private void tmrRedraw_Tick(object sender, EventArgs e)
        {
            gl_Paint(gl, null);
            mouse();

            if (!camare.levelCompleted)
            {
                Engine.Effects.enemies(map.enemys);
            }
        }


        private void tmrTime_Tick(object sender, EventArgs e)
        {
            int num1, num2;

            if (!camare.levelCompleted)
            {
                num1 = time / 10;
                num2 = time % 10;

                led(num1, lbl_time1_num1, lbl_time1_num2, lbl_time1_num3, lbl_time1_num4, lbl_time1_num5, lbl_time1_num6, lbl_time1_num7);
                led(num2, lbl_time2_num1, lbl_time2_num2, lbl_time2_num3, lbl_time2_num4, lbl_time2_num5, lbl_time2_num6, lbl_time2_num7);

                if (time < 15)
                {
                    if (time % 2 == 0)
                    {
                        lbl_time1_num1.BackColor = System.Drawing.Color.Red;
                        lbl_time1_num2.BackColor = System.Drawing.Color.Red;
                        lbl_time1_num3.BackColor = System.Drawing.Color.Red;
                        lbl_time1_num4.BackColor = System.Drawing.Color.Red;
                        lbl_time1_num5.BackColor = System.Drawing.Color.Red;
                        lbl_time1_num6.BackColor = System.Drawing.Color.Red;
                        lbl_time1_num7.BackColor = System.Drawing.Color.Red;
                        lbl_time2_num1.BackColor = System.Drawing.Color.Red;
                        lbl_time2_num2.BackColor = System.Drawing.Color.Red;
                        lbl_time2_num3.BackColor = System.Drawing.Color.Red;
                        lbl_time2_num4.BackColor = System.Drawing.Color.Red;
                        lbl_time2_num5.BackColor = System.Drawing.Color.Red;
                        lbl_time2_num6.BackColor = System.Drawing.Color.Red;
                        lbl_time2_num7.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        lbl_time1_num1.BackColor = System.Drawing.Color.Yellow;
                        lbl_time1_num2.BackColor = System.Drawing.Color.Yellow;
                        lbl_time1_num3.BackColor = System.Drawing.Color.Yellow;
                        lbl_time1_num4.BackColor = System.Drawing.Color.Yellow;
                        lbl_time1_num5.BackColor = System.Drawing.Color.Yellow;
                        lbl_time1_num6.BackColor = System.Drawing.Color.Yellow;
                        lbl_time1_num7.BackColor = System.Drawing.Color.Yellow;
                        lbl_time2_num1.BackColor = System.Drawing.Color.Yellow;
                        lbl_time2_num2.BackColor = System.Drawing.Color.Yellow;
                        lbl_time2_num3.BackColor = System.Drawing.Color.Yellow;
                        lbl_time2_num4.BackColor = System.Drawing.Color.Yellow;
                        lbl_time2_num5.BackColor = System.Drawing.Color.Yellow;
                        lbl_time2_num6.BackColor = System.Drawing.Color.Yellow;
                        lbl_time2_num7.BackColor = System.Drawing.Color.Yellow;
                    }
                }

                if (time > 0)
                    time--;
                else
                {
                    camare.levelCompleted = true;
                    // Mira
                    labelHide();
                    //--
                    camare.position[0] = 0;
                    camare.position[1] = 0;
                    camare.position[2] = -10;
                    camare.view[0] = 0;
                    camare.view[1] = 0;
                    camare.view[2] = -1;
                    // Poner sonido final
                    sound1.play("gameOver.wav", false);
                    //--
                    ligth.turnOn = false;

                    templates = new List<Objects.Plane>();
                    templates.Add(Objects.Template.levelCompleted("gameOver.jpg"));
                    ligth.turnOn = false;
                    drawTemplate();

                    level = -2;
                    System.Threading.Thread.Sleep(Engine.Constant.DELAY);
                    sound1.play("init.wav", true);

                    tmrRedraw.Stop();
                    Cursor.Show();
                }
            }
        }

        public void led(int num, Label lbl1, Label lbl2, Label lbl3, Label lbl4, Label lbl5, Label lbl6, Label lbl7)
        {
            switch (num)
            {
                case 0:
                    lbl1.Show();
                    lbl2.Show();
                    lbl3.Show();
                    lbl4.Show();
                    lbl5.Show();
                    lbl6.Show();
                    lbl7.Hide();
                    break;
                case 1:
                    lbl1.Hide();
                    lbl2.Hide();
                    lbl3.Hide();
                    lbl4.Hide();
                    lbl5.Show();
                    lbl6.Show();
                    lbl7.Hide();
                    break;
                case 2:
                    lbl1.Show();
                    lbl2.Show();
                    lbl3.Hide();
                    lbl4.Show();
                    lbl5.Show();
                    lbl6.Hide();
                    lbl7.Show();
                    break;
                case 3:
                    lbl1.Show();
                    lbl2.Hide();
                    lbl3.Hide();
                    lbl4.Show();
                    lbl5.Show();
                    lbl6.Show();
                    lbl7.Show();
                    break;
                case 4:
                    lbl1.Hide();
                    lbl2.Hide();
                    lbl3.Show();
                    lbl4.Hide();
                    lbl5.Show();
                    lbl6.Show();
                    lbl7.Show();
                    break;
                case 5:
                    lbl1.Show();
                    lbl2.Hide();
                    lbl3.Show();
                    lbl4.Show();
                    lbl5.Hide();
                    lbl6.Show();
                    lbl7.Show();
                    break;
                case 6:
                    lbl1.Show();
                    lbl2.Show();
                    lbl3.Show();
                    lbl4.Show();
                    lbl5.Hide();
                    lbl6.Show();
                    lbl7.Show();
                    break;
                case 7:
                    lbl1.Hide();
                    lbl2.Hide();
                    lbl3.Hide();
                    lbl4.Show();
                    lbl5.Show();
                    lbl6.Show();
                    lbl7.Hide();
                    break;
                case 8:
                    lbl1.Show();
                    lbl2.Show();
                    lbl3.Show();
                    lbl4.Show();
                    lbl5.Show();
                    lbl6.Show();
                    lbl7.Show();
                    break;
                case 9:
                    lbl1.Hide();
                    lbl2.Hide();
                    lbl3.Show();
                    lbl4.Show();
                    lbl5.Show();
                    lbl6.Show();
                    lbl7.Show();
                    break;
            }
        }

        public void startLevel()
        {
            // Siguiente nivel
            level++;
            //--


            // Inicio mapa
            map = new Engine.Maps(level, quality);
            //--


            // Niebla
            GL.Fog(FogParameter.FogMode, (int)FogMode.Exp2);
            GL.Fog(FogParameter.FogColor, map.fogColor);
            GL.Fog(FogParameter.FogDensity, map.fogDensity);
            GL.Hint(HintTarget.FogHint, HintMode.Nicest);
            GL.Fog(FogParameter.FogStart, 1.0f);
            GL.Fog(FogParameter.FogEnd, 10.0f);
            GL.Enable(EnableCap.Fog);
            //--


            // Dibujo mapa
            drawMap();
            //--


            // Preconfigura la posición inicial del personaje y vuelvo por defecto nivelCompletado
            camare.position[0] = map.position[0];
            camare.position[1] = map.position[1];
            camare.position[2] = map.position[2];
            camare.view[0] = map.view[0];
            camare.view[1] = map.view[1];
            camare.view[2] = map.view[2];
            camare.levelCompleted = false;
            ligth.turnOn = true;
            //--


            // Iniciar sonido
            sound1.play("music" + level + ".wav", true);
            //--


            // Mira
            labelShow();
            //--


            // Tiempo
            time = Engine.Constant.TIME;
            //--

            // Color de LED por defecto
            lbl_time1_num1.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time1_num2.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time1_num3.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time1_num4.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time1_num5.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time1_num6.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time1_num7.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time2_num1.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time2_num2.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time2_num3.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time2_num4.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time2_num5.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time2_num6.BackColor = System.Drawing.Color.RoyalBlue;
            lbl_time2_num7.BackColor = System.Drawing.Color.RoyalBlue;
            //--

            // Inicio ciclo y escondo mouse
            if (level == 1)
            {
                tmrRedraw.Start();
                Cursor.Hide();
            }
            //--
        }

        public void drawMap()
        {
            double loadedIn = 0.0, loadedOut;
            int auxTextureId;

            templates = new List<Objects.Plane>();
            templates.Add(Objects.Template.levelCompleted("loadingGame.jpg"));
            templates.Add(Objects.Template.levelCompleted("barContainerLoading.png", 16, 1.5, 2, 16, 0));
            templates.Add(Objects.Template.levelCompleted("barLoading.png", 0, 1.5, 2, 16, 0));
            drawTemplate();

            // Calculando la cantidad de items a cargar
            loadedOut = map.planes.Count + map.photos.Count + map.boxs.Count + map.enemys.Count + firstPerson.Count;
            loadedOut = 16 / loadedOut;
            //--

            foreach (Objects.Plane plane in map.planes)
            {
                loadedIn = barLoading(loadedIn, loadedOut);
                GL.NewList(GList.Count, ListMode.Compile);

                auxTextureId = resource.getTextureId(plane.textureName);
                if (auxTextureId != -1)
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                else
                {
                    auxTextureId = Engine.Texture.LoadTexture(plane.textureName, plane.repeat, plane.rotate);
                    resource.setTextureId(plane.textureName, auxTextureId);
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                }

                GL.Color3(plane.color);
                foreach (Objects.Rectangle rectangle in plane.rectangles)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < rectangle.points.Count; i++)
                    {
                        if (i == 0)
                            GL.TexCoord2(0, 0);
                        if (i == 1)
                            GL.TexCoord2(1, 0);
                        if (i == 2)
                            GL.TexCoord2(1, 1);
                        if (i == 3)
                            GL.TexCoord2(0, 1);

                        double[] vector = { rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2] };
                        vector = Engine.OperationMatrixVector.normalize(vector);
                        GL.Normal3(vector);

                        GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                    }
                    GL.End();
                }
                GL.EndList();
                GList.Add(GList.Count);
            }

            foreach (Objects.Photo photo in map.photos)
            {
                loadedIn = barLoading(loadedIn, loadedOut);
                GL.NewList(GList.Count, ListMode.Compile);

                auxTextureId = resource.getTextureId(photo.textureName);
                if (auxTextureId != -1)
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                else
                {
                    auxTextureId = Engine.Texture.LoadTexture(photo.textureName, false, false);
                    resource.setTextureId(photo.textureName, auxTextureId);
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                }

                GL.Color3(Color.Transparent);
                GL.Translate(photo.position[0], photo.position[1], photo.position[2]);
                foreach (Objects.Rectangle rectangle in photo.rectangles)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < rectangle.points.Count; i++)
                    {
                        if (i == 0)
                            GL.TexCoord2(0, 0);
                        if (i == 1)
                            GL.TexCoord2(1, 0);
                        if (i == 2)
                            GL.TexCoord2(1, 1);
                        if (i == 3)
                            GL.TexCoord2(0, 1);

                        double[] vector = { rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2] };
                        vector = Engine.OperationMatrixVector.normalize(vector);
                        GL.Normal3(vector);

                        GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                    }
                    GL.End();
                }
                GL.Translate(-photo.position[0], -photo.position[1], -photo.position[2]);
                GL.EndList();
                GList.Add(GList.Count);
            }

            foreach (Objects.Box box in map.boxs)
            {
                loadedIn = barLoading(loadedIn, loadedOut);
                GL.NewList(GList.Count, ListMode.Compile);

                auxTextureId = resource.getTextureId(box.textureName);
                if (auxTextureId != -1)
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                else
                {
                    auxTextureId = Engine.Texture.LoadTexture(box.textureName, false, false);
                    resource.setTextureId(box.textureName, auxTextureId);
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                }

                GL.Color3(Color.Transparent);
                GL.Translate(box.position[0], box.position[1], box.position[2]);
                foreach (Objects.Rectangle rectangle in box.rectangles)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < rectangle.points.Count; i++)
                    {
                        if (i == 0)
                            GL.TexCoord2(0, 0);
                        if (i == 1)
                            GL.TexCoord2(1, 0);
                        if (i == 2)
                            GL.TexCoord2(1, 1);
                        if (i == 3)
                            GL.TexCoord2(0, 1);

                        double[] vector = { rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2] };
                        vector = Engine.OperationMatrixVector.normalize(vector);
                        GL.Normal3(vector);

                        GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                    }
                    GL.End();
                }
                GL.Translate(-box.position[0], -box.position[1], -box.position[2]);
                GL.EndList();
                GList.Add(GList.Count);
            }

            foreach (Objects.Enemy enemy in map.enemys)
            {
                loadedIn = barLoading(loadedIn, loadedOut);
                enemy.indexList = GList.Count;
                GL.NewList(GList.Count, ListMode.Compile);
                GL.LoadName(enemy.indexList);

                auxTextureId = resource.getTextureId(enemy.textureName);
                if (auxTextureId != -1)
                {
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                    enemy.textureId = auxTextureId;
                }
                else
                {
                    auxTextureId = enemy.textureId = Engine.Texture.LoadTexture(enemy.textureName, false, false);
                    resource.setTextureId(enemy.textureName, auxTextureId);
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                }

                GL.Color3(Color.Transparent);
                GL.Translate(enemy.position[0], enemy.position[1], enemy.position[2]);
                foreach (Objects.Rectangle rectangle in enemy.rectangles)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < rectangle.points.Count; i++)
                    {
                        if (i == 0)
                            GL.TexCoord2(0, 0);
                        if (i == 1)
                            GL.TexCoord2(1, 0);
                        if (i == 2)
                            GL.TexCoord2(1, 1);
                        if (i == 3)
                            GL.TexCoord2(0, 1);

                        double[] vector = { rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2] };
                        vector = Engine.OperationMatrixVector.normalize(vector);
                        GL.Normal3(vector);

                        GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                    }
                    GL.End();
                }
                GL.Translate(-enemy.position[0], -enemy.position[1], -enemy.position[2]);
                GL.EndList();
                GList.Add(GList.Count);
            }

            efecto = GList.Count;
            foreach (Objects.FirstPerson fp in firstPerson)
            {
                loadedIn = barLoading(loadedIn, loadedOut);
                GL.NewList(GList.Count, ListMode.Compile);

                auxTextureId = resource.getTextureId(fp.textureName);
                if (auxTextureId != -1)
                {
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                    fp.textureId = auxTextureId;
                }
                else
                {
                    auxTextureId = fp.textureId = Engine.Texture.LoadTexture(fp.textureName, false, false);
                    resource.setTextureId(fp.textureName, auxTextureId);
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                }

                GL.Color3(Color.Transparent);
                GL.Translate(fp.position[0], fp.position[1], fp.position[2]);
               
                GL.Begin(BeginMode.Polygon);
                for (int i = 0; i < fp.r.points.Count; i++)
                {
                    if (i == 0)
                        GL.TexCoord2(0, 0);
                    if (i == 1)
                        GL.TexCoord2(1, 0);
                    if (i == 2)
                        GL.TexCoord2(1, 1);
                    if (i == 3)
                        GL.TexCoord2(0, 1);

                    GL.Vertex3(fp.r.points[i][0], fp.r.points[i][1], fp.r.points[i][2]);
                }
                GL.End();

                GL.Translate(-fp.position[0], -fp.position[1], -fp.position[2]);
                GL.EndList();
                GList.Add(GList.Count);
            }

            GL.DeleteLists(1, 3);

            loaded = true;
        }

        public double barLoading(double loadedIn, double loadedOut)
        {
            int auxTextureId;
            loadedIn += loadedOut;
            Objects.Plane template = Objects.Template.levelCompleted("barLoading.png", loadedIn, 1.5, 2, 16, 0);


            GL.NewList(3, ListMode.Compile);
            GL.LoadName(3);

            auxTextureId = resource.getTextureId(template.textureName);
            if (auxTextureId != -1)
            {
                GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
            }
            else
            {
                auxTextureId = Engine.Texture.LoadTexture(template.textureName, template.repeat, template.rotate);
                resource.setTextureId(template.textureName, auxTextureId);
                GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
            }

            GL.Color3(template.color);
            foreach (Objects.Rectangle rectangle in template.rectangles)
            {
                GL.Begin(BeginMode.Polygon);
                for (int i = 0; i < rectangle.points.Count; i++)
                {
                    if (i == 0)
                        GL.TexCoord2(0, 0);
                    if (i == 1)
                        GL.TexCoord2(1, 0);
                    if (i == 2)
                        GL.TexCoord2(1, 1);
                    if (i == 3)
                        GL.TexCoord2(0, 1);

                    GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                }
                GL.End();
            }
            GL.EndList();


            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);
            GL.PushMatrix();
            try
            {
                GL.CallList(GList[1]);
                GL.CallList(GList[2]);
                GL.CallList(GList[3]);
            }
            catch { }
            GL.Finish();
            try { gl.SwapBuffers(); }
            catch { }

            return loadedIn;
        }

        public void drawTemplate()
        {
            int auxTextureId;
            GList = new List<Int32>();
            GList.Add(0);

            foreach (Objects.Plane template in templates)
            {
                GL.NewList(GList.Count, ListMode.Compile);
                GL.LoadName(GList.Count);

                auxTextureId = resource.getTextureId(template.textureName);
                if (auxTextureId != -1)
                {
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                }
                else
                {
                    auxTextureId = Engine.Texture.LoadTexture(template.textureName, template.repeat, template.rotate);
                    resource.setTextureId(template.textureName, auxTextureId);
                    GL.BindTexture(TextureTarget.Texture2D, auxTextureId);
                }

                GL.Color3(template.color);
                foreach (Objects.Rectangle rectangle in template.rectangles)
                {
                    GL.Begin(BeginMode.Polygon);
                    for (int i = 0; i < rectangle.points.Count; i++)
                    {
                        if (i == 0)
                            GL.TexCoord2(0, 0);
                        if (i == 1)
                            GL.TexCoord2(1, 0);
                        if (i == 2)
                            GL.TexCoord2(1, 1);
                        if (i == 3)
                            GL.TexCoord2(0, 1);

                        GL.Vertex3(rectangle.points[i][0], rectangle.points[i][1], rectangle.points[i][2]);
                    }
                    GL.End();
                }
                GL.EndList();
                GList.Add(GList.Count);
            }

            gl_template();
        }

        private void mouse()
        {
            pointer_current = System.Windows.Forms.Cursor.Position;

            if (!camare.levelCompleted && !isPause)
            {
                if (pointer_previous.X > pointer_current.X)
                    camare.lookLeftMouse(pointer_previous.X - pointer_current.X);
                else if (pointer_previous.X < pointer_current.X)
                    camare.lookRightMouse(pointer_current.X - pointer_previous.X);
                if (pointer_previous.Y > pointer_current.Y)
                    camare.lookUpMouse(pointer_previous.Y - pointer_current.Y);
                else if (pointer_previous.Y < pointer_current.Y)
                    camare.lookDownMouse(pointer_current.Y - pointer_previous.Y);
            }

            System.Windows.Forms.Cursor.Position = new Point(gl.Size.Width / 2, gl.Size.Height / 2);
            
            pointer_previous = System.Windows.Forms.Cursor.Position;
        }

        private void keyboard(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyData == Engine.Control.exit)
                this.Close();
            
            if (!camare.levelCompleted)
            {
                if (e.KeyData == Engine.Control.pause)
                {
                    isPause = !isPause;
                    if (isPause)
                    {
                        tmrTime.Stop();
                        labelHide();
                    }
                    else
                    {
                        tmrTime.Start();
                        labelShow();
                    }
                }
                else if (!isPause)
                {
                    // Trucos
                    if (e.KeyData == (Keys.Alt | Keys.F1))
                        camare.levelCompleted = true;
                    else if (e.KeyData == (Keys.Alt | Keys.C))
                        camare.isCollision = !camare.isCollision;
                    else if (e.KeyData == (Keys.Alt | Keys.L))
                        ligth.turnOn = !ligth.turnOn;
                    else if (e.KeyData == (Keys.Alt | Keys.T))
                    {
                        isTransparency = !isTransparency;
                        ligth.turnOn = true;
                    }
                    else if (e.KeyData == (Keys.Alt | Keys.P))
                    {
                        isLine = !isLine;
                        ligth.turnOn = !isLine;
                    }
                    //--


                    if (camare.position[1] != Engine.Constant.CAMERA_HEIGHT)
                    {
                        if (e.KeyData == Engine.Control.moveForward)
                            camare.moveForward(map.collisions);
                        else if (e.KeyData == Engine.Control.moveBack)
                            camare.moveBack(map.collisions);
                        else if (e.KeyData == Engine.Control.moveLeft)
                            camare.moveLeft(map.collisions);
                        else if (e.KeyData == Engine.Control.moveRight)
                            camare.moveRight(map.collisions);
                    }
                    if (e.KeyData == Engine.Control.lookUp)
                        camare.lookUpKey();
                    else if (e.KeyData == Engine.Control.lookDown)
                        camare.lookDownKey();
                    else if (e.KeyData == Engine.Control.lookLeft)
                        camare.lookLeftKey();
                    else if (e.KeyData == Engine.Control.lookRight)
                        camare.lookRightKey();
                    else if (e.KeyData == Engine.Control.statusBar)
                    {
                        if (isStatusBarVisibility)
                        {
                            lbl_bullets.Hide();
                            lbl_score.Hide();
                            lbl_statusBar.Hide();
                        }
                        else
                        {
                            lbl_bullets.Show();
                            lbl_score.Show();
                            lbl_statusBar.Show();
                        }
                        isStatusBarVisibility = !isStatusBarVisibility;
                    }
                    else if (e.KeyData == Engine.Control.weaponsReload)
                    {
                        bullets = Engine.Constant.BULLETS;
                        lbl_bullets.Text = "Balas: " + bullets;
                        sound2.play("weaponsReloadIn.wav", false);

                    }
                    else if (e.KeyData == Engine.Control.restart)
                    {
                        camare.position[0] = map.position[0];
                        camare.position[1] = Engine.Constant.PLAYER_HEIGHT;
                        camare.position[2] = map.position[2];
                        camare.view[0] = map.view[0];
                        camare.view[1] = map.view[1];
                        camare.view[2] = map.view[2];
                    }
                    else if (e.KeyData == Engine.Control.satelliteView)
                    {
                        sound2.play("satelliteView.wav", false);
                        camare.position[1] = Engine.Constant.CAMERA_HEIGHT;
                        ligth.turnOn = false;
                        efecto = GList.Count - 3;
                    }
                    else if (e.KeyData == Engine.Control.weapon1)
                    {
                        sound2.play("changeWeapon1.wav", false);
                        camare.position[1] = Engine.Constant.PLAYER_HEIGHT;
                        ligth.turnOn = true;
                        efecto = GList.Count - 9;
                    }
                    else if (e.KeyData == Engine.Control.weapon2)
                    {
                        sound2.play("changeWeapon2.wav", false);
                        camare.position[1] = Engine.Constant.PLAYER_HEIGHT;
                        ligth.turnOn = true;
                        efecto = GList.Count - 7;
                    }
                    else if (e.KeyData == Engine.Control.weapon3)
                    {
                        sound2.play("changeWeapon3.wav", false);
                        camare.position[1] = Engine.Constant.PLAYER_HEIGHT;
                        ligth.turnOn = true;
                        efecto = GList.Count - 5;
                    }
                }
            }
            else if (e.KeyData == Engine.Control.go)
            {
                if (level == -2)
                {
                    level = -1;
                    mainMenu();
                    ligth.turnOn = false;
                    isLine = false;
                    drawTemplate();
                    level = -1;
                    picturCredits.Visible = false;
                }
                else if (level == -1)
                {
                    templates = new List<Objects.Plane>();
                    templates.Add(Objects.Template.levelCompleted("instructionsGame.jpg"));
                    templates.Add(Objects.Template.levelCompleted("buttonReturn.png", true, 7.2, 18.8));
                    templates.Add(Objects.Template.levelCompleted("buttonContinue.png", true, 10.2, 18.8));
                    ligth.turnOn = false;
                    drawTemplate();
                    level = 0;
                }
                else
                {
                    ligth.turnOn = false;
                    startLevel();
                }
            }            

            if (camare.levelCompleted)
            {
                // Labels y zoom
                labelHide();
                zoom = 90;
                //--
                camare.position[0] = 0;
                camare.position[1] = 0;
                camare.position[2] = -10;
                camare.view[0] = 0;
                camare.view[1] = 0;
                camare.view[2] = -1;

                if (level >= 1 && level <= 3)
                {
                    sound1.play("levelCompleted.wav", false);
                    templates = new List<Objects.Plane>();
                    templates.Add(Objects.Template.levelCompleted("levelCompleted" + level + ".jpg"));
                    ligth.turnOn = false;
                    drawTemplate();
                }
                else if (level == 4)
                {
                    tmrRedraw.Stop();
                    Cursor.Show();

                    sound1.play("end.wav", true);
                    templates = new List<Objects.Plane>();
                    templates.Add(Objects.Template.levelCompleted("completeGame.jpg"));
                    ligth.turnOn = false;
                    drawTemplate();
                    System.Threading.Thread.Sleep(Engine.Constant.DELAY);

                    templates = new List<Objects.Plane>();
                    templates.Add(Objects.Template.levelCompleted("credits.jpg"));
                    picturCredits.Visible = true;
                    ligth.turnOn = false;
                    drawTemplate();

                    level = -2;

                }    
            }
        }

        private void gl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !camare.levelCompleted && !isPause)
            {
                sound2.play("weaponZoom.wav", false);
                if (zoom == 90)
                    zoom = 40;
                else
                    zoom = 90;
            }
            if (e.Button == MouseButtons.Left && camare.position[1] != Engine.Constant.CAMERA_HEIGHT && !isPause)
            {
                if (bullets > 0)
                {
                    if (!camare.levelCompleted)
                    {
                        // Efecto disparo
                        efecto++;
                        gl_Paint(gl, null);
                        efecto--;
                        //--

                        // Disminuir balas y puntos
                        bullets--;
                        lbl_bullets.Text = "Balas: " + bullets;

                        score -= 5;
                        lbl_score.Text = "Puntos: " + score;
                        lbl_score.Left = gl.Size.Width - lbl_score.Width - 1;
                        //--
                        sound2.play("weaponFire.wav", false);
                    }
                }
                else if (!camare.levelCompleted)
                {
                    sound2.play("weaponNoBullets.wav", false);
                    return;
                }

                int seleccionado = 0;
                double minZ = 0;
                int idx = 0;
                int[] selectBuff = new int[512];
                int[] Viewports = new int[4];

                loaded = false;
                GL.SelectBuffer(512, selectBuff);
                GL.GetInteger(GetPName.Viewport, Viewports);
                GL.RenderMode(RenderingMode.Select);
                GL.InitNames();
                GL.PushName(-1);

                //camara aplicar
                int[] Viewports2 = new int[4];
                GL.GetInteger(GetPName.Viewport, Viewports2);
                GL.MatrixMode(MatrixMode.Projection);
                GL.LoadIdentity();
                Glu.PickMatrix(e.X, (Viewports2[3] - e.Y), 1, 1, Viewports2);
                Glu.Perspective(zoom, ((gl.Size.Width - 1) / (gl.Size.Height - 1)), 0.01f, 10000f);
                GL.MatrixMode(MatrixMode.Modelview);
                GL.LoadIdentity();
                Glu.LookAt(camare.position[0], camare.position[1], camare.position[2], camare.view[0], camare.view[1], camare.view[2], 0, 1, 0);
                //camara aplicar

                GL.PushMatrix();
                for (int i = 1; i < GList.Count; i++)
                    GL.CallList(GList[i]);
                GL.PopMatrix();
                GL.Flush();
                long hits = GL.RenderMode(RenderingMode.Render);
                if (!(hits == 0))
                {
                    minZ = 2147483647;
                    idx = 0;
                    seleccionado = 0;

                    int NameNos;
                    for (int i = 0; i < hits; i++)
                    {
                        NameNos = selectBuff[idx];

                        if ((selectBuff[idx + 1] < minZ) && (NameNos > 0))
                        {
                            minZ = selectBuff[idx + 1];
                            seleccionado = selectBuff[idx + 3];
                        }
                        idx = idx + 3 + NameNos;
                    }

                }
                else
                {
                    seleccionado = -1;
                }
                if (seleccionado > 1)
                {
                    if (!camare.levelCompleted  && bullets > 0)
                    {
                        int i;

                        GL.DeleteLists(seleccionado, 1);

                        for (i = 0; i < map.enemys.Count; i++)
                        {
                            if (seleccionado == map.enemys[i].indexList)
                                break;
                        }
                        if (!map.enemys[i].isDead)
                        {
                            map.enemys[i].isDead = true;
                            // Eliminar objeto
                            //GL.DeleteLists(map.enemys[i].indexList, 1);
                            map.collisions.RemoveAt(map.enemys[i].indexCollision);
                            //map.enemys.RemoveAt(i);
                            for (; i < map.enemys.Count; i++)
                            {
                                map.enemys[i].indexCollision--;
                            }
                            //--

                            // Aumentar puntos
                            score += 15;
                            lbl_score.Text = "Puntos: " + score;
                            lbl_score.Left = gl.Size.Width - lbl_score.Width - 1;
                            //--
                        }
                    }
                    else if (camare.levelCompleted)
                    {
                        if (level == -1)
                        {
                            switch (seleccionado)
                            {
                                case 2:
                                    sound2.play("mouseClick.wav", false);
                                    templates = new List<Objects.Plane>();
                                    templates.Add(Objects.Template.levelCompleted("instructionsGame.jpg"));
                                    templates.Add(Objects.Template.levelCompleted("buttonReturn.png", true, 7.2, 18.8));
                                    templates.Add(Objects.Template.levelCompleted("buttonContinue.png", true, 10.2, 18.8));
                                    drawTemplate();
                                    level = 0;
                                    break;
                                case 3:
                                    sound2.play("mouseClick.wav", false);
                                    mainMenu();
                                    templates.Add(Objects.Template.levelCompleted("labelOption.png", 3.2, 11, 2.5, 5, 0.05));
                                    templates.Add(Objects.Template.levelCompleted("buttonQualityVeryLow.png", true, 2.9, 6.5));
                                    templates.Add(Objects.Template.levelCompleted("buttonQualityLow.png", true, 2.9, 8.5));
                                    templates.Add(Objects.Template.levelCompleted("buttonQualityMedia.png", true, 2.9, 10.5));
                                    templates.Add(Objects.Template.levelCompleted("buttonQualityHigh.png", true, 2.9, 12.5));
                                    templates.Add(Objects.Template.levelCompleted("buttonQualityVeryHigh.png", true, 2.9, 14.5));

                                    drawTemplate();
                                    break;
                                case 4:
                                    sound2.play("mouseClick.wav", false);
                                    templates = new List<Objects.Plane>();
                                    templates.Add(Objects.Template.levelCompleted("credits.jpg"));
                                    picturCredits.Visible = true;
                                    drawTemplate();

                                    level = -2;
                                    break;
                                case 5:
                                    sound2.play("mouseClick.wav", false);
                                    this.Close();
                                    break;
                                case 7:
                                    sound2.play("mouseClick.wav", false);
                                    quality = Engine.Constant.QUALITY_PLANE_VERY_LOW;
                                    mainMenu();
                                    drawTemplate();
                                    break;
                                case 8:
                                    sound2.play("mouseClick.wav", false);
                                    quality = Engine.Constant.QUALITY_PLANE_LOW;
                                    mainMenu();
                                    drawTemplate();
                                    break;
                                case 9:
                                    sound2.play("mouseClick.wav", false);
                                    quality = Engine.Constant.QUALITY_PLANE_MEDIA;
                                    mainMenu();
                                    drawTemplate();
                                    break;
                                case 10:
                                    sound2.play("mouseClick.wav", false);
                                    quality = Engine.Constant.QUALITY_PLANE_HIGH;
                                    mainMenu();
                                    drawTemplate();
                                    break;
                                case 11:
                                    sound2.play("mouseClick.wav", false);
                                    quality = Engine.Constant.QUALITY_PLANE_VERY_HIGH;
                                    mainMenu();
                                    drawTemplate();
                                    break;
                            }
                        }
                        else if (level == 0)
                        {
                            switch (seleccionado)
                            {
                                case 2:
                                    sound2.play("mouseClick.wav", false);
                                    mainMenu();
                                    drawTemplate();
                                    level = -1;
                                    break;
                                case 3:
                                    sound2.play("mouseClick.wav", false);
                                    startLevel();
                                    break;
                            }
                        }
                    }
                }

                loaded = true;
            }
        }

        private void mainMenu()
        {
            templates = new List<Objects.Plane>();
            templates.Add(Objects.Template.levelCompleted("startGame.jpg"));
            templates.Add(Objects.Template.levelCompleted("buttonPlay.png", true, 0, 4));
            templates.Add(Objects.Template.levelCompleted("buttonOption.png", true, 0, 6));
            templates.Add(Objects.Template.levelCompleted("buttonCredits.png", true, 0, 8));
            templates.Add(Objects.Template.levelCompleted("buttonExit.png", true, 0, 10));

            loaded = true;
        }

        private void labelShow()
        {
            if (isStatusBarVisibility)
            {
                lbl_bullets.Show();
                lbl_score.Show();
                lbl_statusBar.Show();
            }

            lbl_time1_num1.Show();
            lbl_time1_num2.Show();
            lbl_time1_num3.Show();
            lbl_time1_num4.Show();
            lbl_time1_num5.Show();
            lbl_time1_num6.Show();
            lbl_time1_num7.Show();


            lbl_time2_num1.Show();
            lbl_time2_num2.Show();
            lbl_time2_num3.Show();
            lbl_time2_num4.Show();
            lbl_time2_num5.Show();
            lbl_time2_num6.Show();
            lbl_time2_num7.Show();
        }

        private void labelHide()
        {
            lbl_bullets.Hide();

            lbl_score.Hide();

            lbl_statusBar.Hide();

            lbl_time1_num1.Hide();
            lbl_time1_num2.Hide();
            lbl_time1_num3.Hide();
            lbl_time1_num4.Hide();
            lbl_time1_num5.Hide();
            lbl_time1_num6.Hide();
            lbl_time1_num7.Hide();


            lbl_time2_num1.Hide();
            lbl_time2_num2.Hide();
            lbl_time2_num3.Hide();
            lbl_time2_num4.Hide();
            lbl_time2_num5.Hide();
            lbl_time2_num6.Hide();
            lbl_time2_num7.Hide();
        }
    }
}