namespace KOAR_File_Editor
{
    partial class frmBXml
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.rtbXml = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbXml
            // 
            this.rtbXml.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbXml.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbXml.Location = new System.Drawing.Point(0, 0);
            this.rtbXml.Name = "rtbXml";
            this.rtbXml.Size = new System.Drawing.Size(800, 450);
            this.rtbXml.TabIndex = 0;
            this.rtbXml.Text = "";
            this.rtbXml.WordWrap = false;
            // 
            // frmBXml
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtbXml);
            this.Location = new System.Drawing.Point(0, 0);
            this.Name = "frmBXml";
            this.Text = "frmBXml";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbXml;
    }
}