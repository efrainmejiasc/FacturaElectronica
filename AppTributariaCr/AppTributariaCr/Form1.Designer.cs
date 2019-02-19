namespace AppTributariaCr
{
    partial class Form1
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
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
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.chkFactura = new System.Windows.Forms.CheckBox();
            this.chkNotaDebito = new System.Windows.Forms.CheckBox();
            this.chkNotaCredito = new System.Windows.Forms.CheckBox();
            this.chkTicket = new System.Windows.Forms.CheckBox();
            this.button2 = new System.Windows.Forms.Button();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.button3 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.timer3 = new System.Windows.Forms.Timer(this.components);
            this.button4 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.Silver;
            this.button1.Location = new System.Drawing.Point(12, 549);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(729, 54);
            this.button1.TabIndex = 0;
            this.button1.Text = "ENVIAR DOCUMENTO ELECTRONICO EMISOR";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.Silver;
            this.richTextBox1.Location = new System.Drawing.Point(12, 12);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(729, 481);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // chkFactura
            // 
            this.chkFactura.AutoSize = true;
            this.chkFactura.Location = new System.Drawing.Point(12, 513);
            this.chkFactura.Name = "chkFactura";
            this.chkFactura.Size = new System.Drawing.Size(76, 17);
            this.chkFactura.TabIndex = 2;
            this.chkFactura.Text = "FACTURA";
            this.chkFactura.UseVisualStyleBackColor = true;
            this.chkFactura.CheckedChanged += new System.EventHandler(this.chkFactura_CheckedChanged);
            // 
            // chkNotaDebito
            // 
            this.chkNotaDebito.AutoSize = true;
            this.chkNotaDebito.Location = new System.Drawing.Point(94, 513);
            this.chkNotaDebito.Name = "chkNotaDebito";
            this.chkNotaDebito.Size = new System.Drawing.Size(99, 17);
            this.chkNotaDebito.TabIndex = 3;
            this.chkNotaDebito.Text = "NOTA DEBITO";
            this.chkNotaDebito.UseVisualStyleBackColor = true;
            this.chkNotaDebito.CheckedChanged += new System.EventHandler(this.chkFactura_CheckedChanged);
            // 
            // chkNotaCredito
            // 
            this.chkNotaCredito.AutoSize = true;
            this.chkNotaCredito.Location = new System.Drawing.Point(199, 513);
            this.chkNotaCredito.Name = "chkNotaCredito";
            this.chkNotaCredito.Size = new System.Drawing.Size(107, 17);
            this.chkNotaCredito.TabIndex = 4;
            this.chkNotaCredito.Text = "NOTA CREDITO";
            this.chkNotaCredito.UseVisualStyleBackColor = true;
            this.chkNotaCredito.CheckedChanged += new System.EventHandler(this.chkFactura_CheckedChanged);
            // 
            // chkTicket
            // 
            this.chkTicket.AutoSize = true;
            this.chkTicket.Location = new System.Drawing.Point(312, 513);
            this.chkTicket.Name = "chkTicket";
            this.chkTicket.Size = new System.Drawing.Size(64, 17);
            this.chkTicket.TabIndex = 5;
            this.chkTicket.Text = "TICKET";
            this.chkTicket.UseVisualStyleBackColor = true;
            this.chkTicket.CheckedChanged += new System.EventHandler(this.chkFactura_CheckedChanged);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.Silver;
            this.button2.Location = new System.Drawing.Point(12, 609);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(729, 54);
            this.button2.TabIndex = 6;
            this.button2.Text = "RESUMEN DE DOCUMENTOS ENVIADOS";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // timer2
            // 
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // button3
            // 
            this.button3.BackColor = System.Drawing.Color.Silver;
            this.button3.Location = new System.Drawing.Point(12, 669);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(729, 54);
            this.button3.TabIndex = 7;
            this.button3.Text = "CONSULTAR COMPROBANTE ";
            this.button3.UseVisualStyleBackColor = false;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(414, 511);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(327, 20);
            this.textBox1.TabIndex = 8;
            // 
            // timer3
            // 
            this.timer3.Tick += new System.EventHandler(this.timer3_Tick);
            // 
            // button4
            // 
            this.button4.BackColor = System.Drawing.Color.Silver;
            this.button4.Location = new System.Drawing.Point(12, 733);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(729, 54);
            this.button4.TabIndex = 9;
            this.button4.Text = "CREAR DOCUMENTO ELECTRONICO";
            this.button4.UseVisualStyleBackColor = false;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(762, 798);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.chkTicket);
            this.Controls.Add(this.chkNotaCredito);
            this.Controls.Add(this.chkNotaDebito);
            this.Controls.Add(this.chkFactura);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.CheckBox chkFactura;
        private System.Windows.Forms.CheckBox chkNotaDebito;
        private System.Windows.Forms.CheckBox chkNotaCredito;
        private System.Windows.Forms.CheckBox chkTicket;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Timer timer3;
        private System.Windows.Forms.Button button4;
    }
}

