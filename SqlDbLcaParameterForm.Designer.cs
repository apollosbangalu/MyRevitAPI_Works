namespace TestWorks
{
    partial class SqlDbLcaParameterForm
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
            this.btn_CreateEnvImpactIndicator = new System.Windows.Forms.Button();
            this.btn_SetEnvImpactIndicatorValue = new System.Windows.Forms.Button();
            this.btn_CreateEnvironmentalImpactValue = new System.Windows.Forms.Button();
            this.btn_SetEnvironmentalImpactValue = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_CreateEnvImpactIndicator
            // 
            this.btn_CreateEnvImpactIndicator.Location = new System.Drawing.Point(36, 12);
            this.btn_CreateEnvImpactIndicator.Name = "btn_CreateEnvImpactIndicator";
            this.btn_CreateEnvImpactIndicator.Size = new System.Drawing.Size(155, 51);
            this.btn_CreateEnvImpactIndicator.TabIndex = 0;
            this.btn_CreateEnvImpactIndicator.Text = "Create Environmental Impact Indicator(s)";
            this.btn_CreateEnvImpactIndicator.UseVisualStyleBackColor = true;
            this.btn_CreateEnvImpactIndicator.Click += new System.EventHandler(this.btn_CreateLca_Indicator_Click);
            // 
            // btn_SetEnvImpactIndicatorValue
            // 
            this.btn_SetEnvImpactIndicatorValue.Location = new System.Drawing.Point(36, 79);
            this.btn_SetEnvImpactIndicatorValue.Name = "btn_SetEnvImpactIndicatorValue";
            this.btn_SetEnvImpactIndicatorValue.Size = new System.Drawing.Size(155, 51);
            this.btn_SetEnvImpactIndicatorValue.TabIndex = 3;
            this.btn_SetEnvImpactIndicatorValue.Text = "Set Environmental Impact Indicator(s) Value";
            this.btn_SetEnvImpactIndicatorValue.UseVisualStyleBackColor = true;
            this.btn_SetEnvImpactIndicatorValue.Click += new System.EventHandler(this.btn_SetLcaIndicatorValue_Click);
            // 
            // btn_CreateEnvironmentalImpactValue
            // 
            this.btn_CreateEnvironmentalImpactValue.Location = new System.Drawing.Point(37, 149);
            this.btn_CreateEnvironmentalImpactValue.Name = "btn_CreateEnvironmentalImpactValue";
            this.btn_CreateEnvironmentalImpactValue.Size = new System.Drawing.Size(154, 55);
            this.btn_CreateEnvironmentalImpactValue.TabIndex = 4;
            this.btn_CreateEnvironmentalImpactValue.Text = "Create Environmental Impact Value(s)";
            this.btn_CreateEnvironmentalImpactValue.UseVisualStyleBackColor = true;
            this.btn_CreateEnvironmentalImpactValue.Click += new System.EventHandler(this.btn_CreateLcaParamValue2_Click);
            // 
            // btn_SetEnvironmentalImpactValue
            // 
            this.btn_SetEnvironmentalImpactValue.Location = new System.Drawing.Point(38, 225);
            this.btn_SetEnvironmentalImpactValue.Name = "btn_SetEnvironmentalImpactValue";
            this.btn_SetEnvironmentalImpactValue.Size = new System.Drawing.Size(153, 52);
            this.btn_SetEnvironmentalImpactValue.TabIndex = 5;
            this.btn_SetEnvironmentalImpactValue.Text = "Set Environmental Impact Value(s)";
            this.btn_SetEnvironmentalImpactValue.UseVisualStyleBackColor = true;
            this.btn_SetEnvironmentalImpactValue.Click += new System.EventHandler(this.btn_SetLcaParamValue2_Click);
            // 
            // SqlDbLcaParameterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(230, 291);
            this.Controls.Add(this.btn_SetEnvironmentalImpactValue);
            this.Controls.Add(this.btn_CreateEnvironmentalImpactValue);
            this.Controls.Add(this.btn_SetEnvImpactIndicatorValue);
            this.Controls.Add(this.btn_CreateEnvImpactIndicator);
            this.Name = "SqlDbLcaParameterForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LCA PLUGIN Form";
            this.Load += new System.EventHandler(this.Lca_Plugin_Form_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_CreateEnvImpactIndicator;
        private System.Windows.Forms.Button btn_SetEnvImpactIndicatorValue;
        private System.Windows.Forms.Button btn_CreateEnvironmentalImpactValue;
        private System.Windows.Forms.Button btn_SetEnvironmentalImpactValue;
    }
}