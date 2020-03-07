namespace BusinessDeskTop.Formularios
{
    partial class QueryCompany
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgv = new System.Windows.Forms.DataGridView();
            this.button1 = new System.Windows.Forms.Button();
            this.name = new System.Windows.Forms.TextBox();
            this.rifi = new System.Windows.Forms.TextBox();
            this.mail = new System.Windows.Forms.TextBox();
            this.numero = new System.Windows.Forms.TextBox();
            this.tlf = new System.Windows.Forms.TextBox();
            this.estado = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).BeginInit();
            this.SuspendLayout();
            // 
            // dgv
            // 
            this.dgv.AllowUserToAddRows = false;
            this.dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgv.Location = new System.Drawing.Point(0, 0);
            this.dgv.Name = "dgv";
            this.dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgv.Size = new System.Drawing.Size(888, 280);
            this.dgv.TabIndex = 0;
            this.dgv.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgv_CellContentDoubleClick);
            this.dgv.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgv_ColumnHeaderMouseClick);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 294);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(225, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Empresas";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // name
            // 
            this.name.Location = new System.Drawing.Point(480, 301);
            this.name.Name = "name";
            this.name.Size = new System.Drawing.Size(371, 20);
            this.name.TabIndex = 2;
            // 
            // rifi
            // 
            this.rifi.Location = new System.Drawing.Point(480, 327);
            this.rifi.Name = "rifi";
            this.rifi.Size = new System.Drawing.Size(371, 20);
            this.rifi.TabIndex = 3;
            // 
            // mail
            // 
            this.mail.Location = new System.Drawing.Point(480, 353);
            this.mail.Name = "mail";
            this.mail.Size = new System.Drawing.Size(371, 20);
            this.mail.TabIndex = 4;
            // 
            // numero
            // 
            this.numero.Location = new System.Drawing.Point(480, 405);
            this.numero.Name = "numero";
            this.numero.Size = new System.Drawing.Size(142, 20);
            this.numero.TabIndex = 6;
            // 
            // tlf
            // 
            this.tlf.Location = new System.Drawing.Point(480, 379);
            this.tlf.Name = "tlf";
            this.tlf.Size = new System.Drawing.Size(371, 20);
            this.tlf.TabIndex = 7;
            // 
            // estado
            // 
            this.estado.FormattingEnabled = true;
            this.estado.Location = new System.Drawing.Point(480, 431);
            this.estado.Name = "estado";
            this.estado.Size = new System.Drawing.Size(142, 21);
            this.estado.TabIndex = 13;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(367, 327);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(20, 13);
            this.label5.TabIndex = 18;
            this.label5.Text = "Nit";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(367, 405);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Numero de Equipos";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(367, 379);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 13);
            this.label3.TabIndex = 16;
            this.label3.Text = "Telefono";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(367, 353);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(32, 13);
            this.label2.TabIndex = 15;
            this.label2.Text = "Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(367, 304);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Nombre Empresa";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(370, 431);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(40, 13);
            this.label6.TabIndex = 19;
            this.label6.Text = "Estado";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(637, 426);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(214, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "Actualizar";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // QueryCompany
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 478);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.estado);
            this.Controls.Add(this.tlf);
            this.Controls.Add(this.numero);
            this.Controls.Add(this.mail);
            this.Controls.Add(this.rifi);
            this.Controls.Add(this.name);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.dgv);
            this.Name = "QueryCompany";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Empresas ";
            this.Load += new System.EventHandler(this.QueryCompany_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgv)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dgv;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox name;
        private System.Windows.Forms.TextBox rifi;
        private System.Windows.Forms.TextBox mail;
        private System.Windows.Forms.TextBox numero;
        private System.Windows.Forms.TextBox tlf;
        private System.Windows.Forms.ComboBox estado;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button2;
    }
}