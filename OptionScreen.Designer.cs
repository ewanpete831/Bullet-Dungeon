
namespace Bullet_Dungeon
{
    partial class OptionScreen
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.endLabel = new System.Windows.Forms.Label();
            this.returnButton = new System.Windows.Forms.Button();
            this.steelButton = new System.Windows.Forms.Button();
            this.instructionLabel = new System.Windows.Forms.Label();
            this.easyLabel = new System.Windows.Forms.Label();
            this.easyButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // endLabel
            // 
            this.endLabel.AutoSize = true;
            this.endLabel.Font = new System.Drawing.Font("Comic Sans MS", 72F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.endLabel.Location = new System.Drawing.Point(449, 26);
            this.endLabel.Name = "endLabel";
            this.endLabel.Size = new System.Drawing.Size(404, 135);
            this.endLabel.TabIndex = 1;
            this.endLabel.Text = "Options";
            // 
            // returnButton
            // 
            this.returnButton.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.returnButton.Location = new System.Drawing.Point(57, 37);
            this.returnButton.Name = "returnButton";
            this.returnButton.Size = new System.Drawing.Size(257, 124);
            this.returnButton.TabIndex = 5;
            this.returnButton.Text = "Return to Menu";
            this.returnButton.UseVisualStyleBackColor = true;
            this.returnButton.Click += new System.EventHandler(this.returnButton_Click);
            // 
            // steelButton
            // 
            this.steelButton.BackColor = System.Drawing.Color.Red;
            this.steelButton.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.steelButton.Location = new System.Drawing.Point(122, 283);
            this.steelButton.Name = "steelButton";
            this.steelButton.Size = new System.Drawing.Size(100, 91);
            this.steelButton.TabIndex = 6;
            this.steelButton.UseVisualStyleBackColor = false;
            this.steelButton.Click += new System.EventHandler(this.steelButton_Click);
            // 
            // instructionLabel
            // 
            this.instructionLabel.AutoSize = true;
            this.instructionLabel.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.instructionLabel.Location = new System.Drawing.Point(295, 290);
            this.instructionLabel.Name = "instructionLabel";
            this.instructionLabel.Size = new System.Drawing.Size(152, 67);
            this.instructionLabel.TabIndex = 7;
            this.instructionLabel.Text = "Steel";
            // 
            // easyLabel
            // 
            this.easyLabel.AutoSize = true;
            this.easyLabel.Font = new System.Drawing.Font("Comic Sans MS", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.easyLabel.Location = new System.Drawing.Point(295, 439);
            this.easyLabel.Name = "easyLabel";
            this.easyLabel.Size = new System.Drawing.Size(279, 67);
            this.easyLabel.TabIndex = 9;
            this.easyLabel.Text = "Easy Mode";
            // 
            // easyButton
            // 
            this.easyButton.BackColor = System.Drawing.Color.Red;
            this.easyButton.Font = new System.Drawing.Font("Comic Sans MS", 26.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.easyButton.Location = new System.Drawing.Point(122, 432);
            this.easyButton.Name = "easyButton";
            this.easyButton.Size = new System.Drawing.Size(100, 91);
            this.easyButton.TabIndex = 8;
            this.easyButton.UseVisualStyleBackColor = false;
            this.easyButton.Click += new System.EventHandler(this.easyButton_Click);
            // 
            // OptionScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.easyLabel);
            this.Controls.Add(this.easyButton);
            this.Controls.Add(this.instructionLabel);
            this.Controls.Add(this.steelButton);
            this.Controls.Add(this.returnButton);
            this.Controls.Add(this.endLabel);
            this.Name = "OptionScreen";
            this.Size = new System.Drawing.Size(1370, 737);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label endLabel;
        private System.Windows.Forms.Button returnButton;
        private System.Windows.Forms.Button steelButton;
        private System.Windows.Forms.Label instructionLabel;
        private System.Windows.Forms.Label easyLabel;
        private System.Windows.Forms.Button easyButton;
    }
}
