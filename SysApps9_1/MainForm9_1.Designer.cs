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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm9_1));
            this.btnSetHook = new System.Windows.Forms.Button();
            this.btnUnsetHook = new System.Windows.Forms.Button();
            this.lsbHistory = new System.Windows.Forms.ListBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.btnUnhookMouse = new System.Windows.Forms.Button();
            this.btnHookMouse = new System.Windows.Forms.Button();
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
            this.lsbHistory.Size = new System.Drawing.Size(372, 277);
            this.lsbHistory.TabIndex = 2;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipText = "Key Locker is working!!!";
            this.notifyIcon1.BalloonTipTitle = "Key Locker";
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "Key Locker";
            this.notifyIcon1.Visible = true;
            this.notifyIcon1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon1_MouseClick);
            // 
            // btnUnhookMouse
            // 
            this.btnUnhookMouse.Enabled = false;
            this.btnUnhookMouse.Location = new System.Drawing.Point(305, 19);
            this.btnUnhookMouse.Name = "btnUnhookMouse";
            this.btnUnhookMouse.Size = new System.Drawing.Size(75, 23);
            this.btnUnhookMouse.TabIndex = 4;
            this.btnUnhookMouse.Text = "Unset Hook";
            this.btnUnhookMouse.UseVisualStyleBackColor = true;
            this.btnUnhookMouse.Click += new System.EventHandler(this.btnUnhookMouse_Click);
            // 
            // btnHookMouse
            // 
            this.btnHookMouse.Location = new System.Drawing.Point(207, 19);
            this.btnHookMouse.Name = "btnHookMouse";
            this.btnHookMouse.Size = new System.Drawing.Size(92, 23);
            this.btnHookMouse.TabIndex = 3;
            this.btnHookMouse.Text = "Mouse Hook";
            this.btnHookMouse.UseVisualStyleBackColor = true;
            this.btnHookMouse.Click += new System.EventHandler(this.btnHookMouse_Click);
            // 
            // MainForm9_1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(396, 341);
            this.Controls.Add(this.btnUnhookMouse);
            this.Controls.Add(this.btnHookMouse);
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
        private System.Windows.Forms.Button btnUnhookMouse;
        private System.Windows.Forms.Button btnHookMouse;
    }
}

