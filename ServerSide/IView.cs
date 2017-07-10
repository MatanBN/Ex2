using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
namespace ServerSide
{
    public interface IView
    {
        void start();
        void displaySolution(Solution s);

    }
}
