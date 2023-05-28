namespace RomanCourseWork
{
    partial class MainForm
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
            this.outListBox = new System.Windows.Forms.ListBox();
            this.loadButton = new System.Windows.Forms.Button();
            this.sortButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // outListBox
            // 
            this.outListBox.FormattingEnabled = true;
            this.outListBox.Location = new System.Drawing.Point(18, 15);
            this.outListBox.Name = "outListBox";
            this.outListBox.Size = new System.Drawing.Size(293, 316);
            this.outListBox.TabIndex = 0;
            // 
            // loadButton
            // 
            this.loadButton.Font = new System.Drawing.Font("Constantia", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.loadButton.Location = new System.Drawing.Point(317, 12);
            this.loadButton.Name = "loadButton";
            this.loadButton.Size = new System.Drawing.Size(163, 45);
            this.loadButton.TabIndex = 1;
            this.loadButton.Text = "Загрузить данные";
            this.loadButton.UseVisualStyleBackColor = true;
            this.loadButton.Click += new System.EventHandler(this.loadButton_Click);
            // 
            // sortButton
            // 
            this.sortButton.Font = new System.Drawing.Font("Constantia", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.sortButton.Location = new System.Drawing.Point(317, 63);
            this.sortButton.Name = "sortButton";
            this.sortButton.Size = new System.Drawing.Size(163, 45);
            this.sortButton.TabIndex = 2;
            this.sortButton.Text = "Отсортировать данные";
            this.sortButton.UseVisualStyleBackColor = true;
            this.sortButton.Click += new System.EventHandler(this.sortButton_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(492, 351);
            this.Controls.Add(this.sortButton);
            this.Controls.Add(this.loadButton);
            this.Controls.Add(this.outListBox);
            this.Name = "MainForm";
            this.Text = "MainForm";
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Button sortButton;

        private System.Windows.Forms.Button loadButton;

        private System.Windows.Forms.ListBox outListBox;

        #endregion
    }
}