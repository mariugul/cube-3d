using UnityEngine;
using System.Drawing;
using System.Windows.Forms;
using TMPro;
using System;
using System.IO;
using System.Diagnostics;
using UnityEngine.Networking.PlayerConnection;
using Unity.API;

public class WinFormToolStrip : Form
{
    // Instances of Unity classes
    private PanelOpener panel;
    private ExportProject export;
    private CubeLayout cube;

    public WinFormToolStrip(PanelOpener panel, ExportProject export, CubeLayout cube)
    {
        this.panel = panel;
        this.export = export;

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

        // Tool strip menu items
        //--------------------------------------------------------------
        var itemFile     = new ToolStripMenuItem("File");
        var itemExport   = new ToolStripMenuItem("Export");
        var itemSettings = new ToolStripMenuItem("Settings");
        var itemDiscord  = new ToolStripMenuItem("Discord");
        var itemYoutube  = new ToolStripMenuItem("YouTube");
        var itemGithub   = new ToolStripMenuItem("GitHub");
        var itemView     = new ToolStripMenuItem("View");

        // Tool strip subitems
        //--------------------------------------------------------------
        var itemExportArduinoProject = new ToolStripMenuItem("Arduino Project");
        var itemExportPatternFile    = new ToolStripMenuItem("Pattern File");
        var itemExportSelectFolder   = new ToolStripMenuItem("Select Default");

        var itemFileAbout   = new ToolStripMenuItem("About");
        var itemFileHotkeys = new ToolStripMenuItem("Hotkeys");
        var itemFileSave    = new ToolStripMenuItem("Save Project");
        var itemFileExit    = new ToolStripMenuItem("Exit");

        var itemViewLayout  = new ToolStripMenuItem("Layout");
        var itemViewPlanes  = new ToolStripMenuItem("Planes");
        var itemViewColumns = new ToolStripMenuItem("Columns");

        // Tried to set button image
        //itemDiscord.BackgroundImage = Image.FromFile(@"C:\GitHub\cube-3d\Assets\Resources\discord.png");

        // On Click Functionality
        //--------------------------------------------------------------

        // On click View Layout
        itemViewLayout.Click += (sender, args) =>
        {
            //cube.TogglePlaneText();
        };
        

        // On click export Arduino Project
        itemExportArduinoProject.Click += (sender, args) =>
        {
            var ofd = new SaveFileDialog();

            ofd.Filter = "Folder Name|*Folder";

            var result = ofd.ShowDialog(); // Saving the result is pointless, it's not async stuff here.
                                           // What you can do is wait for form closing:
            ofd.FormClosed += (o, eventArgs) =>
            {
                if (ofd.DialogResult == DialogResult.OK)
                {
                    UnityEngine.Debug.Log(ofd.FileName);

                    string selectedFolder = ofd.FileName;
                    export.ArduinoGenerate(selectedFolder);
                    MessageBox.Show("Arduino project created successfully at " + ofd.FileName);
                }
            };

        };

        // On click export pattern.h File
        itemExportPatternFile.Click += (sender, args) =>
        {
            var sfd = new SaveFileDialog();

            sfd.Filter = "C Header File|*.h";

            var result = sfd.ShowDialog();

            sfd.FormClosed += (o, eventArgs) =>
            {
                if (sfd.DialogResult == DialogResult.OK)
                {
                    export.PatternFileGenerate(sfd.FileName);
                    MessageBox.Show("The pattern file was created successfully at " + sfd.FileName);
                }
            };
        };

        // On click GitHub
        itemGithub.Click += (sender, args) =>
        {
            Process.Start("https://github.com/mariugul/cube-3d");
        };

        // On click Hotkeys
        itemFileHotkeys.Click += (sender, args) =>
        {
            MessageBox.Show(
                "Generate pattern              Enter\n\n"            +
                "Enable all LEDs               Ctrl + A\n"         +
                "Disable all LEDs              Ctrl + Shift + A\n\n" +
                "Select pattern time           Ctrl + T\n\n"         +
                "Redo generated line       Ctrl + Z\n"         +
                "Delete pattern line           Delete\n");
        };

        // On click Youtube
        itemYoutube.Click += (sender, args) =>
        {
            Process.Start("https://www.youtube.com");
        };


        // On click Discord
        itemDiscord.Click += (sender, args) =>
        {
            Process.Start("https://discord.gg/ZgxjkC2");
        };


        // On click SELECT FOLDER
        itemExportSelectFolder.Click += (sender, args) =>
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

        // On click FILE SAVE
        itemFileSave.Click += (sender, args) =>
        {
            var sfd = new SaveFileDialog();

            sfd.Filter = "C Header File|*.h";

            var result = sfd.ShowDialog();

            sfd.FormClosed += (o, eventArgs) =>
            {
                if (sfd.DialogResult == DialogResult.OK)
                {
                    if (!File.Exists(sfd.FileName))
                    {
                        File.Create(sfd.FileName);
                        MessageBox.Show("The file has been created.");
                    }
                    else
                    {
                        File.Create(sfd.FileName);
                        MessageBox.Show("The file has been overwritten.");
                    }
                }
            };

        };

        // On click ABOUT
        itemFileAbout.Click += (sender, args) =>
        {
            string html_link = "https://github.com/mariugul/cube-3d";
           
            MessageBox.Show(
                "Created by Marius C. K. in 2020 with the Unity game engine.\n\n" +
                "For help or suggestions join the Discord server community. \n" +
                "Video tutorials on usage and the code can be found on YouTube. \n" +
                "The source code of this application and the LED cube is found on GitHub.");
           
        };

        // On click SETTINGS
        itemSettings.Click += (sender, args) => {
            panel.TogglePanel();
            //MessageBox.Show("Settings!");
        };

        // Add items to FILE dropdown menu
        //itemFile.DropDownItems.Add(itemFileSave); // TODO: Wait with this for next release
        itemFile.DropDownItems.Add(itemFileAbout);
        itemFile.DropDownItems.Add(itemFileHotkeys);
        //itemFile.DropDownItems.Add(itemFileExit); // TODO: Add on later release?

        // Add items to EXPORT dropdown menu 
        itemExport.DropDownItems.Add(itemExportArduinoProject);
        itemExport.DropDownItems.Add(itemExportPatternFile);
        //itemExport.DropDownItems.Add(itemExportSelectFolder); // TODO: Wait with this for next release

        // Add items to VIEW dropdown menu
        itemView.DropDownItems.Add(itemViewLayout);
        itemView.DropDownItems.Add(itemViewPlanes);
        itemView.DropDownItems.Add(itemViewColumns);

        // Set up menu strip and anchor at the top
        var menu = new MenuStrip();
        menu.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        menu.AutoSize = false;
        menu.Location = new Point(0, uwfHeaderHeight); // Need to be placed below form's header.
        menu.Width = Width;

        // Add menu strip items
        menu.Items.Add(itemFile);
        menu.Items.Add(itemSettings);
        menu.Items.Add(itemView);
        menu.Items.Add(itemExport);
        menu.Items.Add(itemDiscord);
        menu.Items.Add(itemYoutube);
        menu.Items.Add(itemGithub);

        // Add menu strip
        Controls.Add(menu);
    }

    void Initialize()
    {
        var button = new Button();
        // Set button properties...

        button.Click += (o, a) => panel.TogglePanel();
    }

    // Hide all painting with overriding methods.
    protected override void OnPaint(PaintEventArgs e)
    { }

    protected override void OnPaintBackground(PaintEventArgs e)
    { }

    protected internal override void uwfOnLatePaint(PaintEventArgs e)
    { }
}