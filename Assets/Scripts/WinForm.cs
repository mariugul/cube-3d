using System.Drawing;
using System.Windows.Forms;

public class WinForm : Form
{
    public WinForm()
    {
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Location = new Point(0, -uwfHeaderHeight); // Hide header.
        MaximizeBox = false;
        Size = new Size(Screen.PrimaryScreen.WorkingArea.Width, uwfHeaderHeight + 24); // + menu height.
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.Manual;

        uwfMovable = false;

        var itemFile = new ToolStripMenuItem("File");
        var itemEdit = new ToolStripMenuItem("Edit");

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

        var itemFileExit = new ToolStripMenuItem("Exit");

        itemFile.DropDownItems.Add(itemFileOpen);
        itemFile.DropDownItems.Add(itemFileExit);

        var menu = new MenuStrip();
        menu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        menu.AutoSize = false;
        menu.Location = new Point(0, uwfHeaderHeight); // Need to be placed below form's header.
        menu.Width = Width;
        menu.Items.Add(itemFile);
        menu.Items.Add(itemEdit);

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