using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoViewer
{
    class BoardPanel : Panel
    {
        public BoardPanel()
        {
            this.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
            | System.Windows.Forms.AnchorStyles.Left)
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.Cursor = System.Windows.Forms.Cursors.Hand;
            this.Location = new System.Drawing.Point(0, 25);
            this.Name = "boardPanel";
            //this.Size = new System.Drawing.Size(1018, 785);
            this.TabIndex = 3;

            //设置双缓冲，去掉闪烁
            this.SetStyle(ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            this.UpdateStyles();
        }
    }
}
