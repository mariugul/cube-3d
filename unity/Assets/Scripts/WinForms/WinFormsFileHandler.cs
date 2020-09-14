using System;
using System.Drawing;
using System.Windows.Forms;

/// <summary>
/// This script uses WinForms to get access to 
/// the File Dialog that is native to the Windows OS. 
/// </summary>
public class WinFormsFileHandler : Form
{
    // Instances of Unity classes
    private ExportProject export;

    public WinFormsFileHandler(ExportProject export)
    {
        this.export = export;

        // Listen for button click event
        EventHandler.Request += ButtonHandler;

        // Form settings 
        //--------------------------------------------------------------
        Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        FormBorderStyle = FormBorderStyle.FixedSingle;
        Location = new Point(0, -uwfHeaderHeight); // Hide header.
        MaximizeBox = false;
        Size = new Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, uwfHeaderHeight + 24); // + menu height.
        SizeGripStyle = SizeGripStyle.Hide;
        StartPosition = FormStartPosition.Manual;
        uwfMovable = false;
    }

    void ButtonHandler(string button)
    {
        if (button == "Arduino Project")
        {
            ExportArduinoProject();
        }

        else if (button == "Atmel Studio Project")
        {
            ExportAtmelProject();
        }

        else if (button == "Pattern File")
        {
            ExportPatternFile();
        }
    }

    void ExportAtmelProject()
    {

    }

    void ExportArduinoProject()
    {
        var ofd = new SaveFileDialog();

        // Initial directory
        string initDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        ofd.InitialDirectory = initDir;

        ofd.Filter = "Folder Name|*Folder";
        
        var result = ofd.ShowDialog(); // Saving the result is pointless, it's not async stuff here.

        ofd.FormClosed += (o, eventArgs) =>
        {
            if (ofd.DialogResult == DialogResult.OK)
            {
                UnityEngine.Debug.Log(ofd.FileName);
                
                string selectedFolder = ofd.FileName;
                if (export.ArduinoProjectGenerate(selectedFolder))
                    MessageBox.Show("Arduino project created successfully at\n" + ofd.FileName);
                else
                    MessageBox.Show("Something went wrong, project not created.\nThe folder might exist already.\n\nPlease try again.");
            }
        };
    }

    void SelectFolder()
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
    }

    void ExportPatternFile()
    {
        var sfd = new SaveFileDialog();

        // Initial directory
        string initDir = System.Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        sfd.InitialDirectory = initDir;
        
        sfd.Filter = "C Header File|*.h";

        var result = sfd.ShowDialog();

        sfd.FormClosed += (o, eventArgs) =>
        {
            if (sfd.DialogResult == DialogResult.OK)
            {
                if (export.PatternFileGenerate(sfd.FileName))
                    MessageBox.Show("The pattern file was created successfully at\n" + sfd.FileName);
                else
                    MessageBox.Show("Something went wrong, file not created.\nThe file might exist already or you did not write a filename.\n\nPlease try again.");
            }
        };
    }

    // Hide all painting with overriding methods.
    protected override void OnPaint(PaintEventArgs e)
    { }

    protected override void OnPaintBackground(PaintEventArgs e)
    { }

    protected internal override void uwfOnLatePaint(PaintEventArgs e)
    { }
}