namespace TestWorks
{
    partial class SQLDoorsForm
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
            this.btn_TableCreate = new System.Windows.Forms.Button();
            this.btn_ExportDoorData = new System.Windows.Forms.Button();
            this.btn_ImportDoorData = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_TableCreate
            // 
            this.btn_TableCreate.Location = new System.Drawing.Point(67, 29);
            this.btn_TableCreate.Name = "btn_TableCreate";
            this.btn_TableCreate.Size = new System.Drawing.Size(221, 43);
            this.btn_TableCreate.TabIndex = 0;
            this.btn_TableCreate.Text = "Create SQL Table";
            this.btn_TableCreate.UseVisualStyleBackColor = true;
            this.btn_TableCreate.Click += new System.EventHandler(this.btn_TableCreate_Click);
            // 
            // btn_ExportDoorData
            // 
            this.btn_ExportDoorData.Location = new System.Drawing.Point(67, 93);
            this.btn_ExportDoorData.Name = "btn_ExportDoorData";
            this.btn_ExportDoorData.Size = new System.Drawing.Size(221, 43);
            this.btn_ExportDoorData.TabIndex = 1;
            this.btn_ExportDoorData.Text = "Export Door Data";
            this.btn_ExportDoorData.UseVisualStyleBackColor = true;
            this.btn_ExportDoorData.Click += new System.EventHandler(this.btn_ExportDoorData_Click);
            // 
            // btn_ImportDoorData
            // 
            this.btn_ImportDoorData.Location = new System.Drawing.Point(67, 159);
            this.btn_ImportDoorData.Name = "btn_ImportDoorData";
            this.btn_ImportDoorData.Size = new System.Drawing.Size(221, 43);
            this.btn_ImportDoorData.TabIndex = 2;
            this.btn_ImportDoorData.Text = "Import Door Data";
            this.btn_ImportDoorData.UseVisualStyleBackColor = true;
            this.btn_ImportDoorData.Click += new System.EventHandler(this.btn_ImportDoorData_Click);
            // 
            // SQLDoorsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(371, 229);
            this.Controls.Add(this.btn_ImportDoorData);
            this.Controls.Add(this.btn_ExportDoorData);
            this.Controls.Add(this.btn_TableCreate);
            this.Name = "SQLDoorsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SQLDoorsForm";
            this.Load += new System.EventHandler(this.SQLDoorsForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_TableCreate;
        private System.Windows.Forms.Button btn_ExportDoorData;
        private System.Windows.Forms.Button btn_ImportDoorData;
    }
}