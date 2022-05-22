/**
 * SERIAL PORT APPLICATION 
 * @author Janis Olehnovics <janis.olehnovics@mapon.com>
 * v.0.01
 */


using System;
using System.Collections.Generic; // serial port
using System.Collections.Specialized;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Management;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions; // get/post
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

/*
 * v00.01.01 (07.12.2013): first realease 
 * v0 - major version 
 * .01 - functionality related version number 
 * .01 - bugfix related version number
 */

namespace SerialTerminal
{
    public partial class FormTerminal : Form
    {
        public FormTerminal()
        {
            InitializeComponent();
        }

    }
}
