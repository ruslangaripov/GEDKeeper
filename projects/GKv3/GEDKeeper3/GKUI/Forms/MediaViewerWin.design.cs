﻿using GKUI.Components;

namespace GKUI.Forms
{
    partial class MediaViewerWin
    {
        private void InitializeComponent()
        {
            SuspendLayout();

            ShowInTaskbar = true;
            Title = "MediaViewerWin";
            Closing += MediaViewerWin_FormClosing;
            KeyDown += MediaViewerWin_KeyDown;

            UIHelper.SetPredefProperties(this, 1030, 580);
            ResumeLayout();
        }
    }
}