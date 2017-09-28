using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using MazeLib;
using ViewModels;
using System.Windows.Forms;

namespace Views
{

    /// <summary>
    /// Interaction logic for MazeBoard.xaml
    /// </summary>
    public partial class MazeBoard : System.Windows.Controls.UserControl
    {

        public event EventHandler<PlayerMovedEventArgs> playerMoved;

        private double xAdvancment;
        private double yAdvancment;

        public double XAdvancement
        {
            get
            {
                return this.xAdvancment;
            }
        }

        public double YAdvancement
        {
            get
            {
                return this.yAdvancment;
            }
        }
        public MazeBoard()
        {
            InitializeComponent();
        }

        public int Rows
        {
            get { return (int)GetValue(RowsProperty); }
            set { SetValue(RowsProperty, value);
            }
        }

        // Using a DependencyProperty as the backing store for Rows.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty RowsProperty =
            DependencyProperty.Register("Rows", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));



        public int Cols
        {
            get { return (int)GetValue(ColsProperty); }
            set { SetValue(ColsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Cols.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ColsProperty =
            DependencyProperty.Register("Cols", typeof(int), typeof(MazeBoard), new PropertyMetadata(0));



        public string Maze
        {
            get { return (string)GetValue(MazeProperty); }
            set { SetValue(MazeProperty, value); }
        }


        // Using a DependencyProperty as the backing store for Maze.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MazeProperty =
            DependencyProperty.Register("Maze", typeof(string), typeof(MazeBoard), new PropertyMetadata(string.Empty, BuildMaze));



        public Position InitialPos
        {
            get { return (Position)GetValue(InitialPosProperty); }
            set { SetValue(InitialPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for InitialPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty InitialPosProperty =
            DependencyProperty.Register("InitialPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position(0,0)));



        public Position GoalPos
        {
            get { return (Position)GetValue(GoalPosProperty); }
            set { SetValue(GoalPosProperty, value); }
        }

        // Using a DependencyProperty as the backing store for GoalPos.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty GoalPosProperty =
            DependencyProperty.Register("GoalPos", typeof(Position), typeof(MazeBoard), new PropertyMetadata(new Position(0, 0)));



        public string PlayerImageFile
        {
            get { return (string)GetValue(PlayerImageFileProperty); }
            set { SetValue(PlayerImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PlayerImageFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PlayerImageFileProperty =
            DependencyProperty.Register("PlayerImageFile", typeof(string), typeof(MazeBoard), new PropertyMetadata(string.Empty));



        public string ExitImageFile
        {
            get { return (string)GetValue(ExitImageFileProperty); }
            set { SetValue(ExitImageFileProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ExitImageFile.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ExitImageFileProperty =
            DependencyProperty.Register("ExitImageFile", typeof(string), typeof(MazeBoard), new PropertyMetadata(string.Empty));



        private static void BuildMaze(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var myMaze = d as MazeBoard;
            int rowsNum = myMaze.Rows;
            int colsNum = myMaze.Cols;
            string mazeString = (string)e.NewValue;
            string[] mazeTable = mazeString.Split(new string[] { "\r\n" }, StringSplitOptions.None);
            double x = 0;
            double y = 0;
            double recWidth = myMaze.myCanvas.Width / colsNum;
            double recHeight = myMaze.myCanvas.Height / rowsNum;
            myMaze.xAdvancment = recWidth;
            myMaze.yAdvancment = recHeight;
            for (int i = 0; i < rowsNum; ++i)
            {
                for (int j = 0; j < colsNum; ++j)
                {
                    Position p = new Position(i, j);
                    Rectangle r = new Rectangle();
                    r.Width = recWidth;
                    r.Height = recHeight;
                    myMaze.myCanvas.Children.Add(r);

                    if (i == myMaze.InitialPos.Row && j == myMaze.InitialPos.Col)
                    {
                        Image playerImage = myMaze.playerImage;
                        playerImage.Width = recWidth;
                        playerImage.Height = recHeight;
                    }
                    if (i == myMaze.GoalPos.Row && j == myMaze.GoalPos.Col)
                    {
                        Image goalImage = myMaze.goalImage;
                        goalImage.Width = recWidth;
                        goalImage.Height = recHeight;
                        Canvas.SetLeft(goalImage, x);
                        Canvas.SetTop(goalImage, y);
                    }
                    if (mazeTable[i][j] == '1')
                        r.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));


                    Canvas.SetLeft(r, x);
                    Canvas.SetTop(r, y);
                    x += recWidth;
                }
                x = 0;
                y += recHeight;
            }
        }

        public void Window_KeyDown(object sender, EventArgs ea)
        {
            System.Windows.Input.KeyEventArgs e = ea as System.Windows.Input.KeyEventArgs;
            Direction direction;
            switch (e.Key)
            {
                case Key.Left:
                    direction = Direction.Left;
                    break;
                case Key.Right:
                    direction = Direction.Right;
                    break;
                case Key.Up:
                    direction = Direction.Up;
                    break;
                case Key.Down:
                    direction = Direction.Down;
                    break;
                default:
                    return;
            }
            playerMoved?.Invoke(this, new PlayerMovedEventArgs(direction));
        }


    }
}
