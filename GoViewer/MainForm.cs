using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoViewer
{
    /// <summary>
    /// 处理鼠标，显示等等
    /// </summary>
    public partial class MainForm : Form
    {
        private BoardPanel boardPanel;
        /// <summary>
        /// 初始化新棋盘
        /// </summary>
        public MainForm()
        {
            InitializeComponent();
            countingMode = false;
            board = new Board();
            this.boardPanel = new BoardPanel();
            //this.boardPanel.Size = new System.Drawing.Size(this.Width - statPanel.Width - 16, this.Height - controlPanel.Height - 64);
            this.boardPanel.Size = new System.Drawing.Size(this.Width - statPanel.Width - 100, this.Height - 100);
            this.boardPanel.Paint += new System.Windows.Forms.PaintEventHandler(this.boardPanel_Paint);
            this.boardPanel.MouseClick += new System.Windows.Forms.MouseEventHandler(this.boardPanel_MouseClick);
            this.boardPanel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.boardPanel_MouseMove);
            this.boardPanel.Resize += new System.EventHandler(this.boardPanel_Resize);
            this.Controls.Add(this.boardPanel);

        }

        /// <summary>
        /// 退出菜单项
        /// </summary>
        private void itemQuit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private Board board;
        private const bool BLACK = true;
        private const bool WHITE = false;

        private int width;
        private int height;
        private int size;
        private int mouseI;
        private int mouseJ;
        private bool mouseIn = false;
        private int timerCount;

        private bool? turn;
        private Image img;

        //棋盘面板重绘，画棋盘棋子
        private void boardPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(img, 0, 0);
            if (board.Count == 0) return;
            drawSign(g, board.Moves[board.Count - 1].row, board.Moves[board.Count - 1].col, board.Moves[board.Count - 1].black);
        }

        /// <summary>
        /// 画棋子
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i">第i行</param>
        /// <param name="j">第j列</param>
        /// <param name="isBlack">true为黑， false为白, null为空</param>
        private void drawStone(Graphics g, int i, int j, bool? isBlack)
        {
            //TODO: Adjust stone postion if click on line or inside square
            if (isBlack == null) return;
            int x = width / 2 - 9 * size + j * size + size / 2;
            int y = height / 2 - 9 * size + i * size + size / 2;

            //设置颜色渐变画棋子
            Brush brush;
            if (isBlack == BLACK)
                brush = new LinearGradientBrush(new Rectangle(x, y, size, size), Color.Gray, Color.Black, 60f);
            else
                brush = new LinearGradientBrush(new Rectangle(x, y, size, size), Color.White, Color.Gray, 60f);

            g.FillEllipse(brush, x, y, size, size);
        }

        /// <summary>
        /// 窗口大小改变重绘
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boardPanel_Resize(object sender, EventArgs e)
        {
            width = boardPanel.Width;
            height = boardPanel.Height;
            size = Math.Min(width, height) / 20;
            img = new Bitmap(width, height);
            drawImage();
            Refresh();
        }

        private bool countingMode;
        /// <summary>
        /// 鼠标点击棋盘响应
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boardPanel_MouseClick(object sender, MouseEventArgs e)
        {
            getMouse(e.X, e.Y, turn);
            if (countingMode)
            {
                if (mouseIn)
                {
                    board.setGridCount(mouseI, mouseJ);
                    JudgeAndRefresh();
                }
            }
            else
            {
                if (mouseIn)                    //鼠标是否点在棋盘范围
                {
                    board.setStone(mouseI, mouseJ, turn);
                    //处理打劫
                    if (board.isKo)
                    {
                        DisplayEnd();
                        mouseIn = false;
                        board.isKo = false;
                        return;
                    }
                    //更新棋盘
                    drawImage();
                    Refresh();
                    //黑白互换
                    if (board.IsValid)
                        if (turn == true) turn = false;
                        else if (turn == false) turn = true;
                }
            }
            mouseIn = false;
        }

        /// <summary>
        /// 测试坐标是否在棋盘范围内
        /// 如在转换成棋盘矩阵坐标并落子
        /// </summary>
        /// <param name="p1">棋盘面板横坐标</param>
        /// <param name="p2">棋盘面板纵坐标</param>
        /// <param name="turn">黑或白或空</param>
        private void getMouse(int p1, int p2, bool? turn)
        {
            mouseI = (p2 - (height / 2 - 9 * size)) / size;
            mouseJ = (p1 - (width / 2 - 9 * size)) / size;
            if (mouseI < 0 || mouseI > 18 || mouseJ < 0 || mouseJ > 18)
            {
                mouseIn = false;
                return;
            }
            mouseIn = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="g"></param>
        /// <param name="i"></param>
        /// <param name="j"></param>
        /// <param name="isBlack"></param>
        private void drawSquare(Graphics g, int i, int j, bool? isBlack)
        {
            if (isBlack == null) return;
            int x = width / 2 - 9 * size + j * size + size / 2 - 5;
            int y = height / 2 - 9 * size + i * size + size / 2 - 5;
            if (isBlack == true)
                g.FillRectangle(Brushes.White, x, y, 10, 10);
            else
                g.FillRectangle(Brushes.Black, x, y, 10, 10);

        }

        private void drawSign(Graphics g, int i, int j, bool? isBlack)
        {
            int x = width / 2 - 9 * size + j * size + size / 2;
            int y = height / 2 - 9 * size + i * size + size / 2;
            Point[] p = { new Point(x, y), new Point(x + size / 2, y), new Point(x, y + size / 2) };
            Brush b = isBlack == true ? Brushes.Yellow : Brushes.Blue;
            g.FillPolygon(b, p);
        }
        /// <summary>
        /// 鼠标在棋盘上移动处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void boardPanel_MouseMove(object sender, MouseEventArgs e)
        {
            //setIndex(e.X, e.Y);
        }

        /// <summary>
        /// 窗口加载后初始化
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_Load(object sender, EventArgs e)
        {
            width = boardPanel.Width;
            height = boardPanel.Height;
            size = Math.Min(width, height) / 20;
            turn = BLACK;
            img = new Bitmap(width, height);
            drawImage();
        }

        /// <summary>
        /// 将棋盘棋子画入位图
        /// </summary>
        private void drawImage()
        {
            //img = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(img);

            //使绘图质量最高，即消除锯齿
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            g.CompositingQuality = CompositingQuality.HighQuality;

            //g.FillRectangle(Brushes.Gray, width / 2 - 9 * size - size / 4 + 3, height / 2 - 9 * size - size / 4 + 3, size * 19 + size / 2, size * 19 + size / 2);           
            //g.FillRectangle(Brushes.Peru, width / 2 - 9 * size - size / 4, height / 2 - 9 * size - size / 4, size * 19 + size / 2, size * 19 + size / 2);
            g.FillRectangle(Brushes.Peru, 0, 0, boardPanel.Width, boardPanel.Height);
            Pen pen = new Pen(Brushes.Black, 1);
            for (int i = 0; i < 19; i++)
            {
                g.DrawLine(pen, width / 2 - 9 * size + i * size, height / 2 - 9 * size,
                    width / 2 - 9 * size + i * size, height / 2 + 9 * size);
                g.DrawLine(pen, width / 2 - 9 * size, height / 2 - 9 * size + i * size,
                    width / 2 + 9 * size, height / 2 - 9 * size + i * size);
            }
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                {
                    g.FillEllipse(Brushes.Black, width / 2 - 9 * size + ((i + 1) * 6 - 3) * size - 4,
                        height / 2 - 9 * size + ((j + 1) * 6 - 3) * size - 4, 8, 8);
                }
            for (int i = 0; i < 19; i++)
                for (int j = 0; j < 19; j++)
                    drawStone(g, i, j, board.getStone(i, j));
        }

        private List<Move> moves;

        /// <summary>
        /// 清除棋盘，设置定时器
        /// 将刚下完的或文件调入的棋谱定时显示
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMode_Click(object sender, EventArgs e)
        {
            moves = new List<Move>(board.Moves);
            board.clear();
            timerCount = 0;
            timerView.Enabled = true;
            drawImage();
            boardPanel.Refresh();

        }

        /// <summary>
        /// 定时器事件操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timerView_Tick(object sender, EventArgs e)
        {
            if (timerCount >= moves.Count)
            {
                timerView.Enabled = false;
                board.Moves = new List<Move>(moves);
                return;
            }
            board.setStone(moves[timerCount].row, moves[timerCount].col, moves[timerCount].black == BLACK);
            timerCount++;
            drawImage();
            boardPanel.Refresh();
        }

        /// <summary>
        /// 打开文件按钮和菜单项操作
        /// 解析部分gib文件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "棋谱文件|*.gib;*.NGF|弈城棋谱|*.gib|新浪棋谱|*.NGF";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (dialog.FileName.ToLower().EndsWith(".gib"))
                    ParseEweiqiFile(dialog);
                else if (dialog.FileName.ToLower().EndsWith(".ngf"))
                    ParseSinaFile(dialog);
            }

            DisplayEnd();
        }

        /// <summary>
        /// 解析新浪棋谱
        /// </summary>
        /// <param name="dialog"></param>
        private void ParseSinaFile(OpenFileDialog dialog)
        {
            FileStream aFile = new FileStream(dialog.FileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);
            board.Moves = new List<Move>();


            string strLine = sr.ReadLine();
            while (strLine != null)
            {
                if (!strLine.StartsWith("PM"))
                {
                    strLine = sr.ReadLine();
                    continue;
                }
                board.Moves.Add(new Move(strLine[7] - 'B', strLine[8] - 'B', strLine[4] == 'B'));
                strLine = sr.ReadLine();
            }
            sr.Close();
        }

        /// <summary>
        /// 解析弈城棋谱文件
        /// </summary>
        /// <param name="dialog"></param>
        private void ParseEweiqiFile(OpenFileDialog dialog)
        {
            FileStream aFile = new FileStream(dialog.FileName, FileMode.Open);
            StreamReader sr = new StreamReader(aFile);
            board.Moves = new List<Move>();


            string strLine = sr.ReadLine();
            while (strLine != null)
            {
                string[] Strs = strLine.Split(' ');
                if (Strs[0] != "STO")
                {
                    strLine = sr.ReadLine();
                    continue;
                }
                board.Moves.Add(new Move(int.Parse(Strs[5]), int.Parse(Strs[4]), Strs[3] == "1"));
                strLine = sr.ReadLine();
            }
            sr.Close();
        }

        /// <summary>
        /// 显示终局
        /// </summary>
        private void DisplayEnd()
        {
            moves = new List<Move>(board.Moves);
            board.clear();
            //drawImage();
            //boardPanel.Refresh();
            foreach (var item in moves)
            {
                board.setStone(item.row, item.col, item.black == BLACK);
            }
            drawImage();
            boardPanel.Refresh();
            board.Moves = new List<Move>(moves);
        }

        /// <summary>
        /// 上一步按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            board.Previous();
            drawImage();
            boardPanel.Refresh();
        }

        /// <summary>
        /// 下一步按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNext_Click(object sender, EventArgs e)
        {
            board.Next();
            drawImage();
            boardPanel.Refresh();
        }

        /// <summary>
        /// 开始按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            board.GotoStart();
            drawImage();
            boardPanel.Refresh();
        }

        /// <summary>
        /// 结束按钮操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnEnd_Click(object sender, EventArgs e)
        {
            DisplayEnd();
        }

        private void btnJudge_Click(object sender, EventArgs e)
        {
            if (!countingMode)
            {
                countingMode = true;
                lblResult.Text = "请确认死子";
                lblResult.Visible = true;
                btnJudge.Text = "确认死子";
                board.initGridCount();
            }
            else
            {
                JudgeAndRefresh();
                lblResult.Text = (board.NoOfBlackWin - 6.5 > 0 ? "黑" : "白") + "胜" + Math.Abs((int)(((float)board.NoOfBlackWin) - 6.5)) + ".5目";
                lblResult.Visible = true;
                btnJudge.Text = "点目";
                countingMode = false;
            }
        }

        private void JudgeAndRefresh()
        {
            board.Judge();
            drawImage();
            Graphics g = Graphics.FromImage(img);
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (board.Result[i, j] == 1) drawSquare(g, i, j, WHITE);
                    if (board.Result[i, j] == 2) drawSquare(g, i, j, BLACK);
                }
            }
            boardPanel.Refresh();
        }


    }
}
