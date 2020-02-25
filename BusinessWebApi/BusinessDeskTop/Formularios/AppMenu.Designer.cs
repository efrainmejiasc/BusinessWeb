namespace BusinessDeskTop.Formularios
{
    partial class AppMenu
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
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.empresasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.agregarEmpresaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actualizarListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.subirListaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.Gray;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.empresasToolStripMenuItem,
            this.listasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(685, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // empresasToolStripMenuItem
            // 
            this.empresasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.agregarEmpresaToolStripMenuItem});
            this.empresasToolStripMenuItem.Name = "empresasToolStripMenuItem";
            this.empresasToolStripMenuItem.Size = new System.Drawing.Size(69, 20);
            this.empresasToolStripMenuItem.Text = "Empresas";
            // 
            // agregarEmpresaToolStripMenuItem
            // 
            this.agregarEmpresaToolStripMenuItem.Name = "agregarEmpresaToolStripMenuItem";
            this.agregarEmpresaToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            this.agregarEmpresaToolStripMenuItem.Text = "Agregar Empresa";
            this.agregarEmpresaToolStripMenuItem.Click += new System.EventHandler(this.agregarEmpresaToolStripMenuItem_Click);
            // 
            // listasToolStripMenuItem
            // 
            this.listasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.actualizarListaToolStripMenuItem,
            this.subirListaToolStripMenuItem});
            this.listasToolStripMenuItem.Name = "listasToolStripMenuItem";
            this.listasToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.listasToolStripMenuItem.Text = "Listas";
            // 
            // actualizarListaToolStripMenuItem
            // 
            this.actualizarListaToolStripMenuItem.Name = "actualizarListaToolStripMenuItem";
            this.actualizarListaToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.actualizarListaToolStripMenuItem.Text = "Actualizar Lista";
            this.actualizarListaToolStripMenuItem.Click += new System.EventHandler(this.actualizarListaToolStripMenuItem_Click);
            // 
            // subirListaToolStripMenuItem
            // 
            this.subirListaToolStripMenuItem.Name = "subirListaToolStripMenuItem";
            this.subirListaToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            this.subirListaToolStripMenuItem.Text = "Subir Lista";
            this.subirListaToolStripMenuItem.Click += new System.EventHandler(this.subirListaToolStripMenuItem_Click);
            // 
            // AppMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 329);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "AppMenu";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Menu principal";
            this.Load += new System.EventHandler(this.AppMenu_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem empresasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem agregarEmpresaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem listasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actualizarListaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem subirListaToolStripMenuItem;
    }
}