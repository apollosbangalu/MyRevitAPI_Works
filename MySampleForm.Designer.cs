namespace TestWorks
{
    partial class MySampleForm
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
            this.Wall_Count = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Wall_Count
            // 
            this.Wall_Count.Location = new System.Drawing.Point(23, 23);
            this.Wall_Count.Name = "Wall_Count";
            this.Wall_Count.Size = new System.Drawing.Size(198, 70);
            this.Wall_Count.TabIndex = 0;
            this.Wall_Count.Text = "Wall_Count";
            this.Wall_Count.UseVisualStyleBackColor = true;
            this.Wall_Count.Click += new System.EventHandler(this.Wall_Count_Click);
            // 
            // MySampleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(249, 123);
            this.Controls.Add(this.Wall_Count);
            this.Name = "MySampleForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "MySampleForm";
            this.Load += new System.EventHandler(this.MySampleForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Wall_Count;
    }
}