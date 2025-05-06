
namespace microsoftEdgeSearch
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;
        private Microsoft.Web.WebView2.WinForms.WebView2 WebView2;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.WebView2 = new Microsoft.Web.WebView2.WinForms.WebView2();
            ((System.ComponentModel.ISupportInitialize)(this.WebView2)).BeginInit();
            this.SuspendLayout();

            // WebView2
            this.WebView2.AllowExternalDrop = true;
            this.WebView2.CreationProperties = null;
            this.WebView2.DefaultBackgroundColor = System.Drawing.Color.White;
            this.WebView2.Dock = System.Windows.Forms.DockStyle.Fill; // Fills the entire form
            this.WebView2.Name = "WebView2";
            this.WebView2.Size = new System.Drawing.Size(784, 461); // Full screen, but size adjusts to form size
            this.WebView2.TabIndex = 0;
            this.WebView2.ZoomFactor = 1D;

            // Form1
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.WebView2);
            this.Name = "Form1";
            this.Text = "Edge Search";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized; // Start in full-screen mode
            ((System.ComponentModel.ISupportInitialize)(this.WebView2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
