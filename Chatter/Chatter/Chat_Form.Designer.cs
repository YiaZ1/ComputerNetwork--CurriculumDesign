namespace Chatter
{
    partial class Chat_Form
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
            this.ClientRTB_msg = new System.Windows.Forms.RichTextBox();
            this.ClintRTB_snd = new System.Windows.Forms.RichTextBox();
            this.ChatBT_Send = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.ChatLB_list = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClientRTB_msg
            // 
            this.ClientRTB_msg.BackColor = System.Drawing.SystemColors.Window;
            this.ClientRTB_msg.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClientRTB_msg.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.ClientRTB_msg.Font = new System.Drawing.Font("微软雅黑", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClientRTB_msg.Location = new System.Drawing.Point(9, 11);
            this.ClientRTB_msg.Name = "ClientRTB_msg";
            this.ClientRTB_msg.Size = new System.Drawing.Size(428, 267);
            this.ClientRTB_msg.TabIndex = 0;
            this.ClientRTB_msg.TabStop = false;
            this.ClientRTB_msg.Text = "";
            // 
            // ClintRTB_snd
            // 
            this.ClintRTB_snd.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ClintRTB_snd.Font = new System.Drawing.Font("宋体", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ClintRTB_snd.Location = new System.Drawing.Point(9, 295);
            this.ClintRTB_snd.Name = "ClintRTB_snd";
            this.ClintRTB_snd.Size = new System.Drawing.Size(428, 73);
            this.ClintRTB_snd.TabIndex = 1;
            this.ClintRTB_snd.Text = "";
            // 
            // ChatBT_Send
            // 
            this.ChatBT_Send.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChatBT_Send.Location = new System.Drawing.Point(335, 376);
            this.ChatBT_Send.Name = "ChatBT_Send";
            this.ChatBT_Send.Size = new System.Drawing.Size(94, 24);
            this.ChatBT_Send.TabIndex = 2;
            this.ChatBT_Send.Text = "发送";
            this.ChatBT_Send.UseVisualStyleBackColor = true;
            this.ChatBT_Send.Click += new System.EventHandler(this.ChatBT_Send_Click);
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(235, 376);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(94, 24);
            this.button2.TabIndex = 3;
            this.button2.Text = "关闭";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // ChatLB_list
            // 
            this.ChatLB_list.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ChatLB_list.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ChatLB_list.FormattingEnabled = true;
            this.ChatLB_list.ItemHeight = 21;
            this.ChatLB_list.Location = new System.Drawing.Point(456, 11);
            this.ChatLB_list.Name = "ChatLB_list";
            this.ChatLB_list.Size = new System.Drawing.Size(142, 357);
            this.ChatLB_list.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe Print", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label3.Location = new System.Drawing.Point(477, 371);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(95, 37);
            this.label3.TabIndex = 6;
            this.label3.Text = "chatter";
            // 
            // Chat_Form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(610, 407);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.ChatLB_list);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.ChatBT_Send);
            this.Controls.Add(this.ClintRTB_snd);
            this.Controls.Add(this.ClientRTB_msg);
            this.ForeColor = System.Drawing.SystemColors.WindowText;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "Chat_Form";
            this.Text = "Chat_Form";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Chat_Form_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox ClientRTB_msg;
        private System.Windows.Forms.RichTextBox ClintRTB_snd;
        private System.Windows.Forms.Button ChatBT_Send;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ListBox ChatLB_list;
        private System.Windows.Forms.Label label3;
    }
}