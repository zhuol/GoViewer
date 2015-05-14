using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;


namespace GoViewer
{
    class Board
    {
        /// <summary>
        /// 棋盘矩阵，存黑白空
        /// </summary>
        private bool?[,] grid;
        /// <summary>
        /// 下过的棋的招法集合
        /// </summary>
        public List<Move> Moves;
        /// <summary>
        /// 提子集合
        /// </summary>
        public List<HashSet<Move>> Dead;
        /// <summary>
        /// 当前步数
        /// </summary>
        public int Count;
        /// <summary>
        /// 连通的棋的集合
        /// </summary>
        private List<HashSet<int>> Unions;
        /// <summary>
        /// 落子是否有效
        /// </summary>
        public bool IsValid { set; get; }
        /// <summary>
        /// 是否打劫
        /// </summary>
        public bool isKo { get; set; }
        /// <summary>
        /// 新棋盘
        /// </summary>
        public Board()
        {
            clear();
        }

        /// <summary>
        /// 初始化
        /// </summary>
        public void clear()
        {
            Count = 0;
            Moves = new List<Move>();
            Dead = new List<HashSet<Move>>();
            grid = new bool?[19, 19];
            Unions = new List<HashSet<int>>();
            HashSet<int> tmp = new HashSet<int>();
            for (int i = 0; i < 361; i++)
                tmp.Add(i);
            Unions.Add(tmp);
        }

        /// <summary>
        /// 返回棋盘矩阵状态
        /// </summary>
        /// <param name="i">矩阵纵坐标</param>
        /// <param name="j">矩阵横坐标</param>
        /// <returns></returns>
        public bool? getStone(int i, int j)
        {
            return grid[i, j];
        }

        /// <summary>
        /// 下一子的操作
        /// </summary>
        /// <param name="i">矩阵纵坐标</param>
        /// <param name="j">矩阵横坐标</param>
        /// <param name="isBlack">黑白空</param>
        public void setStone(int i, int j, bool? isBlack)
        {
            IsValid = false;
            isKo = false;

            if (isBlack == null)
            {
                grid[i, j] = null;
                return;
            }
            if (grid[i, j] != null) return;

            //判断能否落子
            bool canPlace = false;
            if (i > 0 && (grid[i - 1, j] == null || (grid[i - 1, j] == isBlack && qi(i - 1, j) != 1))) canPlace = true;
            if (!canPlace && i < 18 && (grid[i + 1, j] == null || (grid[i + 1, j] == isBlack && qi(i + 1, j) != 1))) canPlace = true;
            if (!canPlace && j > 0 && (grid[i, j - 1] == null || (grid[i, j - 1] == isBlack && qi(i, j - 1) != 1))) canPlace = true;
            if (!canPlace && j < 18 && (grid[i, j + 1] == null || (grid[i, j + 1] == isBlack && qi(i, j + 1) != 1))) canPlace = true;

            if (!canPlace && i > 0 && grid[i - 1, j] != null && grid[i - 1, j] != isBlack && qi(i - 1, j) == 1) canPlace = true;
            if (!canPlace && i < 18 && grid[i + 1, j] != null && grid[i + 1, j] != isBlack && qi(i + 1, j) == 1) canPlace = true;
            if (!canPlace && j > 0 && grid[i, j - 1] != null && grid[i, j - 1] != isBlack && qi(i, j - 1) == 1) canPlace = true;
            if (!canPlace && j < 18 && grid[i, j + 1] != null && grid[i, j + 1] != isBlack && qi(i, j + 1) == 1) canPlace = true;

            if (!canPlace) return;

            grid[i, j] = isBlack;

            //更新连通集合和招法集合
            int unionNo = i * 19 + j;
            Unions[0].Remove(unionNo);
            HashSet<int> tmp = new HashSet<int>();
            tmp.Add(unionNo);
            Unions.Add(tmp);
            if (i > 0) connect(i, j, i - 1, j);
            if (i < 18) connect(i, j, i + 1, j);
            if (j > 0) connect(i, j, i, j - 1);
            if (j < 18) connect(i, j, i, j + 1);
            Moves.Add(new Move(i, j, isBlack == true));

            IsValid = true;

            //如有，提死子
            HashSet<Move> dead = new HashSet<Move>();
            if (i > 0 && grid[i - 1, j] != null && grid[i - 1, j] != isBlack && qi(i - 1, j) == 0) remove(i - 1, j, dead, isBlack);
            if (i < 18 && grid[i + 1, j] != null && grid[i + 1, j] != isBlack && qi(i + 1, j) == 0) remove(i + 1, j, dead, isBlack);
            if (j > 0 && grid[i, j - 1] != null && grid[i, j - 1] != isBlack && qi(i, j - 1) == 0) remove(i, j - 1, dead, isBlack);
            if (j < 18 && grid[i, j + 1] != null && grid[i, j + 1] != isBlack && qi(i, j + 1) == 0) remove(i, j + 1, dead, isBlack);
            Dead.Add(dead);
            Count++;

            //判断是否打劫
            if (Dead.Count < 3) return;
            if (Dead[Dead.Count - 1].Count != 1) return;
            if (Dead[Dead.Count - 2].Count != 1) return;

            foreach (var item in Unions)
            {
                if (item.Contains(i * 19 + j) && item.Count != 1) return;
            }
            Move m = Dead[Dead.Count - 1].First();
            if (!m.Equals(Moves[Moves.Count - 2])) return;
            Moves.RemoveAt(Moves.Count - 1);
            Count--;
            isKo = true;
            IsValid = false;
        }

        /// <summary>
        /// 提死子，设置更新
        /// </summary>
        /// <param name="i">矩阵纵坐标</param>
        /// <param name="j">矩阵横坐标</param>
        /// <param name="dead">当前死子集合</param>
        /// <param name="isBlack">黑白</param>
        private void remove(int i, int j, HashSet<Move> dead, bool? isBlack)
        {
            HashSet<int> set = null;
            foreach (HashSet<int> s in Unions)
            {
                if (s.Contains(i * 19 + j))
                {
                    set = s;
                    break;
                }
            }
            foreach (int n in set)
            {
                Unions[0].Add(n);
                int x = n % 19;
                int y = n / 19;
                grid[y, x] = null;
                dead.Add(new Move(y, x, isBlack != true));
            }
            Unions.Remove(set);
        }

        /// <summary>
        /// 返回所在连通集合有多少气
        /// </summary>
        /// <param name="i">矩阵纵坐标</param>
        /// <param name="j">矩阵横坐标</param>
        /// <returns></returns>
        private int qi(int i, int j)
        {
            HashSet<int> set = null;
            foreach (HashSet<int> s in Unions)
            {
                if (s.Contains(i * 19 + j))
                {
                    set = s;
                    break;
                }
            }
            HashSet<Point> qis = new HashSet<Point>();
            foreach (int n in set)
            {
                int x = n % 19;
                int y = n / 19;
                if (x > 0 && grid[y, x - 1] == null) qis.Add(new Point(y, x - 1));
                if (x < 18 && grid[y, x + 1] == null) qis.Add(new Point(y, x + 1));
                if (y > 0 && grid[y - 1, x] == null) qis.Add(new Point(y - 1, x));
                if (y < 18 && grid[y + 1, x] == null) qis.Add(new Point(y + 1, x));

            }
            return qis.Count;
        }

        /// <summary>
        /// 连通两块棋子集合
        /// </summary>
        /// <param name="ai">a块矩阵纵坐标</param>
        /// <param name="aj">a块矩阵横坐标</param>
        /// <param name="bi">b块矩阵纵坐标</param>
        /// <param name="bj">b块矩阵横坐标</param>
        private void connect(int ai, int aj, int bi, int bj)
        {
            if (grid[ai, aj] != grid[bi, bj]) return;
            int a = ai * 19 + aj;
            int b = bi * 19 + bj;
            int x = 0, y = 0;
            for (int i = 0; i < Unions.Count; i++)
            {
                if (Unions[i].Contains(a)) x = i;
                if (Unions[i].Contains(b)) y = i;
            }
            if (x == y) return;
            Unions[x].UnionWith(Unions[y]);
            Unions.RemoveAt(y);
        }

        /// <summary>
        /// 设置棋盘为上一步状态
        /// </summary>
        public void Previous()
        {
            if (Count <= 0) return;
            Count--;
            grid[Moves[Count].row, Moves[Count].col] = null;
            foreach (var item in Dead[Count])
            {
                grid[item.row, item.col] = item.black;
            }
        }

        /// <summary>
        /// 设置棋盘为下一步状态
        /// </summary>
        public void Next()
        {
            if (Count > Moves.Count - 1) return;
            grid[Moves[Count].row, Moves[Count].col] = Moves[Count].black;
            foreach (var item in Dead[Count])
            {
                grid[item.row, item.col] = null;
            }
            Count++;
        }

        /// <summary>
        /// 清空棋盘，回到开始
        /// </summary>
        public void GotoStart()
        {
            Count = 0;
            grid = new bool?[19, 19];

        }

        public int[,] Result;
        public int NoOfBlackWin;
        private bool?[,] gridCount;

        public void setGridCount(int i, int j)
        {
            if (grid[i, j] != null)
            {
                HashSet<int> set = null;
                foreach (HashSet<int> s in Unions)
                {
                    if (s.Contains(i * 19 + j))
                    {
                        set = s;
                        break;
                    }
                }

                bool? tmp = gridCount[i, j];
                foreach (var n in set)
                {
                    int x = n % 19;
                    int y = n / 19;
                    gridCount[y, x] = tmp == null ? grid[i, j] : null;
                }
            }
        }

        public void initGridCount()
        {
            gridCount = new bool?[19, 19];
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    gridCount[i, j] = grid[i, j];
                }
            }
        }

        public void Judge()
        {
            Result = new int[19, 19];
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (Result[i, j] != 0) continue;
                    if (gridCount[i, j] != null) continue;
                    HashSet<Point> tmp = new HashSet<Point>();
                    bool? isBlack = null;
                    bool decided = false;
                    isBlack = dfs(i, j, tmp, ref isBlack, ref decided);
                    foreach (var item in tmp)
                    {
                        if (isBlack == null) Result[item.X, item.Y] = 3;
                        else Result[item.X, item.Y] = isBlack == true ? 1 : 2;
                    }
                }
            }
            int numbersOfBlack = 0;
            int numbersOfWhite = 0;
            for (int i = 0; i < 19; i++)
            {
                for (int j = 0; j < 19; j++)
                {
                    if (gridCount[i, j] == null)
                    {
                        if (Result[i, j] == 1) numbersOfBlack++;
                        if (Result[i, j] == 2) numbersOfWhite++;
                    }
                    else if (gridCount[i, j] == true) numbersOfBlack++;
                    else numbersOfWhite++;
                }
            }
            NoOfBlackWin = numbersOfBlack - numbersOfWhite;
            if (Moves[Moves.Count - 1].black) NoOfBlackWin--;
        }

        private bool? dfs(int i, int j, HashSet<Point> tmp, ref bool? isBlack, ref bool decided)
        {
            tmp.Add(new Point(i, j));
            if (i > 0 && gridCount[i - 1, j] != null)
                if (decided)
                {
                    if (isBlack != null) isBlack = isBlack == gridCount[i - 1, j] ? isBlack : null;
                }
                else
                {
                    isBlack = gridCount[i - 1, j];
                    decided = true;
                }
            if (i < 18 && gridCount[i + 1, j] != null)
                if (decided)
                {
                    if (isBlack != null) isBlack = isBlack == gridCount[i + 1, j] ? isBlack : null;
                }
                else
                {
                    isBlack = gridCount[i + 1, j];
                    decided = true;
                }
            if (j > 0 && gridCount[i, j - 1] != null)
                if (decided)
                {
                    if (isBlack != null) isBlack = isBlack == gridCount[i, j - 1] ? isBlack : null;
                }
                else
                {
                    isBlack = gridCount[i, j - 1];
                    decided = true;
                }
            if (j < 18 && gridCount[i, j + 1] != null)
                if (decided)
                {
                    if (isBlack != null) isBlack = isBlack == gridCount[i, j + 1] ? isBlack : null;
                }
                else
                {
                    isBlack = gridCount[i, j + 1];
                    decided = true;
                }

            if (i > 0 && gridCount[i - 1, j] == null && !tmp.Contains(new Point(i - 1, j))) dfs(i - 1, j, tmp, ref isBlack, ref decided);
            if (i < 18 && gridCount[i + 1, j] == null && !tmp.Contains(new Point(i + 1, j))) dfs(i + 1, j, tmp, ref isBlack, ref decided);
            if (j > 0 && gridCount[i, j - 1] == null && !tmp.Contains(new Point(i, j - 1))) dfs(i, j - 1, tmp, ref isBlack, ref decided);
            if (j < 18 && gridCount[i, j + 1] == null && !tmp.Contains(new Point(i, j + 1))) dfs(i, j + 1, tmp, ref isBlack, ref decided);

            return isBlack;
        }
    }

}
