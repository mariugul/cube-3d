using System.Drawing;
using System.Windows.Forms;

public class WinForm : Form
{
    public WinForm()
    {
        // Form settings to make it invisible and anchored at the top
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Location = new Point(0, -uwfHeaderHeight); // Hide header.
        MaximizeBox = false;
        Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, uwfHeaderHeight + 24); // + menu height.
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.Manual;

        uwfMovable = false;

        // Tool strip menu items
        var itemFile     = new ToolStripMenuItem("File");
        var itemExport   = new ToolStripMenuItem("Export");
        var itemSettings = new ToolStripMenuItem("Settings");
        var itemAbout    = new ToolStripMenuItem("About");

        // Tool strip subitems
        var itemFileOpen = new ToolStripMenuItem("Open");
        itemFileOpen.Click += (sender, args) =>
        {
            var ofd = new OpenFileDialog();
            var result = ofd.ShowDialog(); // Saving the result is pointless, it's not async stuff here.
                                           // What you can do is wait for form closing:
            ofd.FormClosed += (o, eventArgs) =>
            {
                if (ofd.DialogResult == DialogResult.OK)
                {
                    UnityEngine.Debug.Log(ofd.FileName);
                }
            };
        };

        var itemFileExit   = new ToolStripMenuItem("Exit");

        // Add items to dropdown menu
        itemFile.DropDownItems.Add(itemFileOpen);
        itemFile.DropDownItems.Add(itemFileExit);

        // Set up menu strip and anchor at the top
        var menu = new MenuStrip();
        menu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        menu.AutoSize = false;
        menu.Location = new Point(0, uwfHeaderHeight); // Need to be placed below form's header.
        menu.Width = Width;

        // Add menu strip items
        menu.Items.Add(itemFile);
        menu.Items.Add(itemExport);
        menu.Items.Add(itemSettings);
        menu.Items.Add(itemAbout);

        // Add menu strip
        Controls.Add(menu);
    }

    // Hide all painting with overriding methods.
    protected override void OnPaint(PaintEventArgs e)
    { }

    protected override void OnPaintBackground(PaintEventArgs e)
    { }

    protected internal override void uwfOnLatePaint(PaintEventArgs e)
    { }
}