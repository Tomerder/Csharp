using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QcLib;

namespace QCLibTestApp
{
    class Program
    {
        static void Main(string[] args)
        {

            using (QualityCenterManager qcLib = new QualityCenterManager("AROMA", "dery tomer", "", @"http://subs-qc:8080/qcbin/", "AVIONIC"))
            {
                qcLib.ConnectToQC();

                List<Defect> listPRs = new List<Defect>();
                qcLib.GetListOfHandledDefects("", "1_085", ref listPRs);

                qcLib.DisconnectFromQC();
            }

        }
    }
}
