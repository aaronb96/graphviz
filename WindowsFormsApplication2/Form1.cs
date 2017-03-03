using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GraphVizWrapper;
using GraphVizWrapper.Commands;
using GraphVizWrapper.Queries;
using System.IO;

namespace WindowsFormsApplication2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        string gyujto(Ember peldany)
        {
            if (peldany.MarBejarva)
                return "";

            peldany.MarBejarva = true;

            StringBuilder sb = new StringBuilder();

            if (peldany.parja != null)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(peldany.parja.Nev);
                sb.Append(";");

                sb.Append(gyujto(peldany.parja));
            }
            foreach(Ember e in peldany.gyermeke)
            {
                sb.Append(peldany.Nev);
                sb.Append("->");
                sb.Append(e.Nev);
                sb.Append(";");

                sb.Append(gyujto(e));
            }

            return sb.ToString();

        }



        private void button1_Click(object sender, EventArgs e)
        {
            Ember apa = new Ember("Apa");
            Ember anya = new Ember("Anya");
            Ember gy1 = new Ember("Gy1");
            Ember gy2 = new Ember("Gy2");
            Ember u1 = new Ember("U");
            Ember uu1 = new Ember("uu1");
            Ember uu2 = new Ember("uu2");
            Ember uu3 = new Ember("uu3");

            apa.parja = anya;
            anya.parja = apa;

            apa.gyermeke.Add(gy1);
            apa.gyermeke.Add(gy2);
            anya.gyermeke.Add(gy1);
            anya.gyermeke.Add(gy2);

            gy1.gyermeke.Add(u1);

            u1.gyermeke.Add(uu1);
            u1.gyermeke.Add(uu2);
            u1.gyermeke.Add(uu3);



            string s = gyujto(apa);



            //

            var getStartProcessQuery = new GetStartProcessQuery();
            var getProcessStartInfoQuery = new GetProcessStartInfoQuery();
            var registerLayoutPluginCommand = new RegisterLayoutPluginCommand(getProcessStartInfoQuery, getStartProcessQuery);

            // GraphGeneration can be injected via the IGraphGeneration interface

            var wrapper = new GraphGeneration(getStartProcessQuery,
                                              getProcessStartInfoQuery,
                                              registerLayoutPluginCommand);

            byte[] output = wrapper.GenerateGraph("digraph{"+s+"}", Enums.GraphReturnType.Png);

            using (MemoryStream ms = new MemoryStream(output))
            {
                Image i = Image.FromStream(ms);
                pictureBox1.Image = i;
            }
        }
    }
}
