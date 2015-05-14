using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoViewer
{
    /// <summary>
    /// 每着棋的结构
    /// 棋盘横纵坐标和黑白
    /// </summary>
    public struct Move
    {
        /// <summary>
        /// 这步棋的纵坐标
        /// </summary>
        public int row;
        /// <summary>
        /// 这步棋的横坐标
        /// </summary>
        public int col;
        /// <summary>
        /// 这步棋的颜色
        /// </summary>
        public bool black;
        
        /// <summary>
        /// 给这步棋赋值
        /// </summary>
        /// <param name="i">纵坐标</param>
        /// <param name="j">横坐标</param>
        /// <param name="black">黑白</param>
        public Move(int i, int j, bool black)
        {
            this.row = i;
            this.col = j;
            this.black = black;
        }
    }
}
