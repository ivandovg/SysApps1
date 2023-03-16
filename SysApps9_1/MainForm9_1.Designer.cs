namespace SysApps9_1
{
    partial class MainForm9_1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.btnSetHook = new System.Windows.Forms.Button();
            this.btnUnsetHook = new System.Windows.Forms.Button();
            this.lsbHistory = new System.Windows.Forms.ListBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.SuspendLayout();
            // 
            // btnSetHook
            // 
            this.btnSetHook.Location = new System.Drawing.Point(12, 19);
            this.btnSetHook.Name = "btnSetHook";
            this.btnSetHook.Size = new System.Drawing.Size(75, 23);
            this.btnSetHook.TabIndex = 0;
            this.btnSetHook.Text = "Set Hook";
            this.btnSetHook.UseVisualStyleBackColor = true;
            this.btnSetHook.Click += new System.EventHandler(this.btnSetHook_Click);
            // 
            // btnUnsetHook
            // 
            this.btnUnsetHook.Enabled = false;
            this.btnUnsetHook.Location = new System.Drawing.Point(93, 19);
            this.btnUnsetHook.Name = "btnUnsetHook";
            this.btnUnsetHook.Size = new System.Drawing.Size(75, 23);
            this.btnUnsetHook.TabIndex = 1;
            this.btnUnsetHook.Text = "Unset Hook";
            this.btnUnsetHook.UseVisualStyleBackColor = true;
            this.btnUnsetHook.Click += new System.EventHandler(this.btnUnsetHook_Click);
            // 
            // lsbHistory
            // 
            this.lsbHistory.FormattingEnabled = true;
            this.lsbHistory.Location = new System.Drawing.Point(12, 48);
            this.lsbHistory.Name = "lsbHistory";
            this.lsbHistory.Size = new System.Drawing.Size(300, 277);
            this.lsbHistory.TabIndex = 2;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // MainForm9_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(324, 341);
            this.Controls.Add(this.lsbHistory);
            this.Controls.Add(this.btnUnsetHook);
            this.Controls.Add(this.btnSetHook);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm9_1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Test Hook System";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnSetHook;
        private System.Windows.Forms.Button btnUnsetHook;
        private System.Windows.Forms.ListBox lsbHistory;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
    }
}

