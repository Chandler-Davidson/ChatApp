namespace ChatApp
{
    partial class ClientForm
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
            this.MessageLogTextBox = new System.Windows.Forms.RichTextBox();
            this.ConnectedUserTextBox = new System.Windows.Forms.RichTextBox();
            this.MessageTextBox = new System.Windows.Forms.RichTextBox();
            this.SendButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // MessageLogTextBox
            // 
            this.MessageLogTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageLogTextBox.Location = new System.Drawing.Point(0, 0);
            this.MessageLogTextBox.Name = "MessageLogTextBox";
            this.MessageLogTextBox.ReadOnly = true;
            this.MessageLogTextBox.Size = new System.Drawing.Size(458, 350);
            this.MessageLogTextBox.TabIndex = 0;
            this.MessageLogTextBox.TabStop = false;
            this.MessageLogTextBox.Text = "";
            // 
            // ConnectedUserTextBox
            // 
            this.ConnectedUserTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ConnectedUserTextBox.Location = new System.Drawing.Point(464, 0);
            this.ConnectedUserTextBox.Name = "ConnectedUserTextBox";
            this.ConnectedUserTextBox.ReadOnly = true;
            this.ConnectedUserTextBox.Size = new System.Drawing.Size(131, 350);
            this.ConnectedUserTextBox.TabIndex = 1;
            this.ConnectedUserTextBox.TabStop = false;
            this.ConnectedUserTextBox.Text = "Connected Users: \n";
            // 
            // MessageTextBox
            // 
            this.MessageTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MessageTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.MessageTextBox.Location = new System.Drawing.Point(12, 356);
            this.MessageTextBox.Multiline = false;
            this.MessageTextBox.Name = "MessageTextBox";
            this.MessageTextBox.Size = new System.Drawing.Size(446, 31);
            this.MessageTextBox.TabIndex = 0;
            this.MessageTextBox.Text = "";
            // 
            // SendButton
            // 
            this.SendButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.SendButton.Location = new System.Drawing.Point(464, 356);
            this.SendButton.Name = "SendButton";
            this.SendButton.Size = new System.Drawing.Size(75, 31);
            this.SendButton.TabIndex = 1;
            this.SendButton.Text = "Send";
            this.SendButton.UseVisualStyleBackColor = true;
            this.SendButton.Click += new System.EventHandler(this.SendButton_Click);
            // 
            // ClientForm
            // 
            this.AcceptButton = this.SendButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(607, 399);
            this.Controls.Add(this.SendButton);
            this.Controls.Add(this.MessageTextBox);
            this.Controls.Add(this.ConnectedUserTextBox);
            this.Controls.Add(this.MessageLogTextBox);
            this.Name = "ClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ClientForm";
            this.Load += new System.EventHandler(this.ClientForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox MessageLogTextBox;
        private System.Windows.Forms.RichTextBox ConnectedUserTextBox;
        private System.Windows.Forms.RichTextBox MessageTextBox;
        private System.Windows.Forms.Button SendButton;
    }
}