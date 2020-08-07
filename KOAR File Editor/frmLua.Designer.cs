namespace KOAR_File_Editor {
    partial class frmLua {
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
            this.rtbBytecode = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // rtbBytecode
            // 
            this.rtbBytecode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbBytecode.Font = new System.Drawing.Font("Courier New", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.rtbBytecode.Location = new System.Drawing.Point(0, 0);
            this.rtbBytecode.Name = "rtbBytecode";
            this.rtbBytecode.Size = new System.Drawing.Size(800, 450);
            this.rtbBytecode.TabIndex = 1;
            this.rtbBytecode.Text = "";
            this.rtbBytecode.WordWrap = false;
            // 
            // frmLua
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.rtbBytecode);
            this.Name = "frmLua";
            this.Text = "frmLua";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtbBytecode;
    }
}