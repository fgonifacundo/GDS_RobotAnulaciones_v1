namespace ROTOB_ANULACION
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador requerida.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén utilizando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben eliminar; false en caso contrario.</param>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.lblHora = new System.Windows.Forms.Label();
            this.myTimer = new System.Windows.Forms.Timer(this.components);
            this.txtMensaje = new System.Windows.Forms.TextBox();
            this.cboProcesos = new System.Windows.Forms.ComboBox();
            this.btnProcesar = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblHora
            // 
            this.lblHora.AutoSize = true;
            this.lblHora.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHora.Location = new System.Drawing.Point(174, 9);
            this.lblHora.Name = "lblHora";
            this.lblHora.Size = new System.Drawing.Size(179, 37);
            this.lblHora.TabIndex = 0;
            this.lblHora.Text = "00 : 00 : 00";
            // 
            // myTimer
            // 
            this.myTimer.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // txtMensaje
            // 
            this.txtMensaje.Location = new System.Drawing.Point(6, 91);
            this.txtMensaje.Name = "txtMensaje";
            this.txtMensaje.Size = new System.Drawing.Size(340, 20);
            this.txtMensaje.TabIndex = 3;
            // 
            // cboProcesos
            // 
            this.cboProcesos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProcesos.FormattingEnabled = true;
            this.cboProcesos.Location = new System.Drawing.Point(6, 56);
            this.cboProcesos.Name = "cboProcesos";
            this.cboProcesos.Size = new System.Drawing.Size(244, 21);
            this.cboProcesos.TabIndex = 5;
            // 
            // btnProcesar
            // 
            this.btnProcesar.Location = new System.Drawing.Point(256, 49);
            this.btnProcesar.Name = "btnProcesar";
            this.btnProcesar.Size = new System.Drawing.Size(90, 32);
            this.btnProcesar.TabIndex = 6;
            this.btnProcesar.Text = "Procesar";
            this.btnProcesar.UseVisualStyleBackColor = true;
            this.btnProcesar.Click += new System.EventHandler(this.btnProcesar_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(35, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(381, 118);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnProcesar);
            this.Controls.Add(this.cboProcesos);
            this.Controls.Add(this.txtMensaje);
            this.Controls.Add(this.lblHora);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "Aviso y Anulación de Boletos SABRE";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHora;
        private System.Windows.Forms.Timer myTimer;
        private System.Windows.Forms.TextBox txtMensaje;
        private System.Windows.Forms.ComboBox cboProcesos;
        private System.Windows.Forms.Button btnProcesar;
        private System.Windows.Forms.Button button1;
    }
}

