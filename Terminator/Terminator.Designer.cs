namespace Terminator
{
    partial class Terminator
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido del método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Terminator));
            this.gl = new OpenTK.GLControl();
            this.tmrRedraw = new System.Windows.Forms.Timer(this.components);
            this.tmrTime = new System.Windows.Forms.Timer(this.components);
            this.lbl_time1_num1 = new System.Windows.Forms.Label();
            this.lbl_time1_num4 = new System.Windows.Forms.Label();
            this.lbl_time1_num7 = new System.Windows.Forms.Label();
            this.lbl_time1_num2 = new System.Windows.Forms.Label();
            this.lbl_time1_num5 = new System.Windows.Forms.Label();
            this.lbl_time1_num6 = new System.Windows.Forms.Label();
            this.lbl_time1_num3 = new System.Windows.Forms.Label();
            this.lbl_time2_num1 = new System.Windows.Forms.Label();
            this.lbl_time2_num4 = new System.Windows.Forms.Label();
            this.lbl_time2_num7 = new System.Windows.Forms.Label();
            this.lbl_time2_num2 = new System.Windows.Forms.Label();
            this.lbl_time2_num6 = new System.Windows.Forms.Label();
            this.lbl_time2_num5 = new System.Windows.Forms.Label();
            this.lbl_time2_num3 = new System.Windows.Forms.Label();
            this.picturCredits = new System.Windows.Forms.PictureBox();
            this.lbl_score = new System.Windows.Forms.Label();
            this.lbl_bullets = new System.Windows.Forms.Label();
            this.lbl_statusBar = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picturCredits)).BeginInit();
            this.SuspendLayout();
            // 
            // gl
            // 
            this.gl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.gl.AutoSize = true;
            this.gl.BackColor = System.Drawing.Color.Black;
            this.gl.Cursor = System.Windows.Forms.Cursors.Default;
            this.gl.Location = new System.Drawing.Point(0, -1);
            this.gl.Name = "gl";
            this.gl.Size = new System.Drawing.Size(1266, 648);
            this.gl.TabIndex = 0;
            this.gl.VSync = false;
            this.gl.Load += new System.EventHandler(this.gl_Load);
            this.gl.Paint += new System.Windows.Forms.PaintEventHandler(this.gl_Paint);
            this.gl.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.keyboard);
            this.gl.MouseDown += new System.Windows.Forms.MouseEventHandler(this.gl_MouseDown);
            // 
            // tmrRedraw
            // 
            this.tmrRedraw.Enabled = true;
            this.tmrRedraw.Interval = 5;
            this.tmrRedraw.Tick += new System.EventHandler(this.tmrRedraw_Tick);
            // 
            // tmrTime
            // 
            this.tmrTime.Enabled = true;
            this.tmrTime.Interval = 1000;
            this.tmrTime.Tick += new System.EventHandler(this.tmrTime_Tick);
            // 
            // lbl_time1_num1
            // 
            this.lbl_time1_num1.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time1_num1.Location = new System.Drawing.Point(15, 44);
            this.lbl_time1_num1.Name = "lbl_time1_num1";
            this.lbl_time1_num1.Size = new System.Drawing.Size(16, 4);
            this.lbl_time1_num1.TabIndex = 3;
            // 
            // lbl_time1_num4
            // 
            this.lbl_time1_num4.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time1_num4.Location = new System.Drawing.Point(15, 15);
            this.lbl_time1_num4.Name = "lbl_time1_num4";
            this.lbl_time1_num4.Size = new System.Drawing.Size(16, 4);
            this.lbl_time1_num4.TabIndex = 3;
            // 
            // lbl_time1_num7
            // 
            this.lbl_time1_num7.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time1_num7.Location = new System.Drawing.Point(15, 29);
            this.lbl_time1_num7.Name = "lbl_time1_num7";
            this.lbl_time1_num7.Size = new System.Drawing.Size(16, 5);
            this.lbl_time1_num7.TabIndex = 3;
            // 
            // lbl_time1_num2
            // 
            this.lbl_time1_num2.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time1_num2.Location = new System.Drawing.Point(15, 30);
            this.lbl_time1_num2.Name = "lbl_time1_num2";
            this.lbl_time1_num2.Size = new System.Drawing.Size(4, 18);
            this.lbl_time1_num2.TabIndex = 3;
            // 
            // lbl_time1_num5
            // 
            this.lbl_time1_num5.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time1_num5.Location = new System.Drawing.Point(27, 15);
            this.lbl_time1_num5.Name = "lbl_time1_num5";
            this.lbl_time1_num5.Size = new System.Drawing.Size(4, 18);
            this.lbl_time1_num5.TabIndex = 3;
            // 
            // lbl_time1_num6
            // 
            this.lbl_time1_num6.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time1_num6.Location = new System.Drawing.Point(27, 30);
            this.lbl_time1_num6.Name = "lbl_time1_num6";
            this.lbl_time1_num6.Size = new System.Drawing.Size(4, 18);
            this.lbl_time1_num6.TabIndex = 3;
            // 
            // lbl_time1_num3
            // 
            this.lbl_time1_num3.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time1_num3.Location = new System.Drawing.Point(15, 15);
            this.lbl_time1_num3.Name = "lbl_time1_num3";
            this.lbl_time1_num3.Size = new System.Drawing.Size(4, 18);
            this.lbl_time1_num3.TabIndex = 3;
            // 
            // lbl_time2_num1
            // 
            this.lbl_time2_num1.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time2_num1.Location = new System.Drawing.Point(35, 44);
            this.lbl_time2_num1.Name = "lbl_time2_num1";
            this.lbl_time2_num1.Size = new System.Drawing.Size(16, 4);
            this.lbl_time2_num1.TabIndex = 3;
            // 
            // lbl_time2_num4
            // 
            this.lbl_time2_num4.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time2_num4.Location = new System.Drawing.Point(35, 15);
            this.lbl_time2_num4.Name = "lbl_time2_num4";
            this.lbl_time2_num4.Size = new System.Drawing.Size(16, 4);
            this.lbl_time2_num4.TabIndex = 3;
            // 
            // lbl_time2_num7
            // 
            this.lbl_time2_num7.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time2_num7.Location = new System.Drawing.Point(35, 29);
            this.lbl_time2_num7.Name = "lbl_time2_num7";
            this.lbl_time2_num7.Size = new System.Drawing.Size(16, 5);
            this.lbl_time2_num7.TabIndex = 3;
            // 
            // lbl_time2_num2
            // 
            this.lbl_time2_num2.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time2_num2.Location = new System.Drawing.Point(35, 30);
            this.lbl_time2_num2.Name = "lbl_time2_num2";
            this.lbl_time2_num2.Size = new System.Drawing.Size(4, 18);
            this.lbl_time2_num2.TabIndex = 3;
            // 
            // lbl_time2_num6
            // 
            this.lbl_time2_num6.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time2_num6.Location = new System.Drawing.Point(47, 30);
            this.lbl_time2_num6.Name = "lbl_time2_num6";
            this.lbl_time2_num6.Size = new System.Drawing.Size(4, 18);
            this.lbl_time2_num6.TabIndex = 3;
            // 
            // lbl_time2_num5
            // 
            this.lbl_time2_num5.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time2_num5.Location = new System.Drawing.Point(47, 15);
            this.lbl_time2_num5.Name = "lbl_time2_num5";
            this.lbl_time2_num5.Size = new System.Drawing.Size(4, 18);
            this.lbl_time2_num5.TabIndex = 3;
            // 
            // lbl_time2_num3
            // 
            this.lbl_time2_num3.BackColor = System.Drawing.Color.RoyalBlue;
            this.lbl_time2_num3.Location = new System.Drawing.Point(35, 15);
            this.lbl_time2_num3.Name = "lbl_time2_num3";
            this.lbl_time2_num3.Size = new System.Drawing.Size(4, 18);
            this.lbl_time2_num3.TabIndex = 3;
            // 
            // picturCredits
            // 
            this.picturCredits.BackColor = System.Drawing.Color.Transparent;
            this.picturCredits.Image = global::Terminator.Properties.Resources.creditsLabel;
            this.picturCredits.Location = new System.Drawing.Point(286, 10);
            this.picturCredits.Name = "picturCredits";
            this.picturCredits.Size = new System.Drawing.Size(712, 607);
            this.picturCredits.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.picturCredits.TabIndex = 4;
            this.picturCredits.TabStop = false;
            this.picturCredits.Visible = false;
            // 
            // lbl_score
            // 
            this.lbl_score.AutoSize = true;
            this.lbl_score.BackColor = System.Drawing.Color.Transparent;
            this.lbl_score.Font = new System.Drawing.Font("More than Enough", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_score.ForeColor = System.Drawing.Color.White;
            this.lbl_score.Image = global::Terminator.Properties.Resources.label2;
            this.lbl_score.Location = new System.Drawing.Point(1157, 619);
            this.lbl_score.Name = "lbl_score";
            this.lbl_score.Size = new System.Drawing.Size(93, 23);
            this.lbl_score.TabIndex = 3;
            this.lbl_score.Text = "Puntos: 0";
            // 
            // lbl_bullets
            // 
            this.lbl_bullets.AutoSize = true;
            this.lbl_bullets.BackColor = System.Drawing.Color.Transparent;
            this.lbl_bullets.Font = new System.Drawing.Font("More than Enough", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_bullets.ForeColor = System.Drawing.Color.White;
            this.lbl_bullets.Image = global::Terminator.Properties.Resources.label2;
            this.lbl_bullets.Location = new System.Drawing.Point(3, 619);
            this.lbl_bullets.Name = "lbl_bullets";
            this.lbl_bullets.Size = new System.Drawing.Size(84, 23);
            this.lbl_bullets.TabIndex = 3;
            this.lbl_bullets.Text = "Balas: 10";
            // 
            // lbl_statusBar
            // 
            this.lbl_statusBar.BackColor = System.Drawing.Color.Transparent;
            this.lbl_statusBar.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_statusBar.ForeColor = System.Drawing.Color.Black;
            this.lbl_statusBar.Image = global::Terminator.Properties.Resources.statusBar2;
            this.lbl_statusBar.Location = new System.Drawing.Point(3, 620);
            this.lbl_statusBar.Name = "lbl_statusBar";
            this.lbl_statusBar.Size = new System.Drawing.Size(1267, 25);
            this.lbl_statusBar.TabIndex = 3;
            // 
            // Cubux
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1264, 647);
            this.Controls.Add(this.picturCredits);
            this.Controls.Add(this.lbl_score);
            this.Controls.Add(this.lbl_bullets);
            this.Controls.Add(this.lbl_statusBar);
            this.Controls.Add(this.lbl_time2_num3);
            this.Controls.Add(this.lbl_time1_num3);
            this.Controls.Add(this.lbl_time2_num5);
            this.Controls.Add(this.lbl_time1_num5);
            this.Controls.Add(this.lbl_time2_num6);
            this.Controls.Add(this.lbl_time1_num6);
            this.Controls.Add(this.lbl_time2_num2);
            this.Controls.Add(this.lbl_time1_num2);
            this.Controls.Add(this.lbl_time2_num7);
            this.Controls.Add(this.lbl_time1_num7);
            this.Controls.Add(this.lbl_time2_num4);
            this.Controls.Add(this.lbl_time2_num1);
            this.Controls.Add(this.lbl_time1_num4);
            this.Controls.Add(this.lbl_time1_num1);
            this.Controls.Add(this.gl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Cubux";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Cubux";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.picturCredits)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private OpenTK.GLControl gl;
        private System.Windows.Forms.Label lbl_time1_num1;
        private System.Windows.Forms.Label lbl_time1_num4;
        private System.Windows.Forms.Label lbl_time1_num7;
        private System.Windows.Forms.Label lbl_time1_num2;
        private System.Windows.Forms.Label lbl_time1_num5;
        private System.Windows.Forms.Label lbl_time1_num6;
        private System.Windows.Forms.Label lbl_time1_num3;
        private System.Windows.Forms.Label lbl_time2_num1;
        private System.Windows.Forms.Label lbl_time2_num4;
        private System.Windows.Forms.Label lbl_time2_num7;
        private System.Windows.Forms.Label lbl_time2_num2;
        private System.Windows.Forms.Label lbl_time2_num6;
        private System.Windows.Forms.Label lbl_time2_num5;
        private System.Windows.Forms.Label lbl_time2_num3;
        private System.Windows.Forms.Label lbl_score;
        private System.Windows.Forms.Label lbl_bullets;
        private System.Windows.Forms.Label lbl_statusBar;
        public System.Windows.Forms.Timer tmrRedraw;
        public System.Windows.Forms.Timer tmrTime;
        private System.Windows.Forms.PictureBox picturCredits;
    }
}

