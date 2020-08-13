using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

public class WinFormSettings : Form
{
    public WinFormSettings ()
    {
        // Form settings to make it invisible and anchored at the top
        //Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        //FormBorderStyle = FormBorderStyle.FixedSingle;
        //Location = new Point(0, -uwfHeaderHeight); // Hide header.
        MaximizeBox = false;
        //Size = new Size(System.Windows.Forms.Screen.PrimaryScreen.WorkingArea.Width, uwfHeaderHeight + 24); // + menu height.
        //SizeGripStyle = SizeGripStyle.Hide;
        //StartPosition = FormStartPosition.Manual;

        //uwfMovable = false;
    }
}
