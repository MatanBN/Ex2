using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SearchAlgorithmsLib;
namespace ServerSide
{
    class MyView : IView
    {
        IController c;
        public MyView(IController c)
        {
            this.c = c;
        }
        public void start()
        {
            //...
            //c.calculate(someProblem);
        }
        public void displaySolution(Solution s)
        {/*...*/}
    }

}
