namespace SocobanLevels
{
    partial class SocobanLevels
    {
        private System.ComponentModel.IContainer components = null;

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
            this.SuspendLayout();
            // set window title
            this.Text = "Socoban";
            // use exe icon (set via ApplicationIcon in csproj)
            try
            {
                this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location);
            }
            catch
            {
                // ignore if icon not found
            }
            this.Load += new System.EventHandler(this.SocobanLevelsLoad);
            this.ResumeLayout(false);
        }
    }
}
