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
            // start centered on screen
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            // prevent maximizing (fixed single border)
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            // load Wall.ico from output directory and set as window icon
            try
            {
                var icoPath = System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Wall.ico");
                if (System.IO.File.Exists(icoPath))
                {
                    this.Icon = new System.Drawing.Icon(icoPath);
                }
                else
                {
                    this.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.Reflection.Assembly.GetEntryAssembly().Location);
                }
            }
            catch
            {
                // ignore if icon not found or load fails
            }
            this.Load += new System.EventHandler(this.SocobanLevelsLoad);
            this.ResumeLayout(false);
        }
    }
}
