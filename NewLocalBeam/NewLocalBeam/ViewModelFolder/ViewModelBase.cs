using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NewLocalBeam.ModelFolder;

namespace NewLocalBeam.ViewModelFolder
{
    class ViewModelBase
    {
        public Points pt = new Points();
        public Command command { get; set; }

        public ViewModelBase()
        {
            this.command = new Command(this);
        }

        public double txtP1X
        {
            get{ return pt.P1_x;}
            set{ pt.P1_x = value; }
        }

        public double txtP1Y
        {
            get { return pt.P1_y; }
            set { pt.P1_y = value; }
        }


        public double txtP2X
        {
            get { return pt.P2_x; }
            set { pt.P2_x = value; }
        }


        public double txtP3Y
        {
            get { return pt.P3_y; }
            set { pt.P3_y = value; }
        }


        public string txtP1String
        {
            get { return pt.P1_string; }
            set { pt.P1_string= value; }
        }


        public string txtP2String
        {
            get { return pt.P2_string; }
            set { pt.P2_string = value; }
        }


        public string txtP3String
        {
            get { return pt.P3_string; }
            set { pt.P3_string = value; }
        }


        public void RunMethod()
        {
            Console.WriteLine("RunMethod");
        }

    }
}
