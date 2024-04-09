using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Dataflow;
using System.Windows.Forms;

namespace _13._4
{
    public partial class Form1 : Form
    {
        TransformBlock<int, int> multiplyBlock = new TransformBlock<int, int>(item => item* 2);
        ActionBlock<int> displayBlock;
        int counter = 10;
        public Form1()
        {
            InitializeComponent();
            var options = new ExecutionDataflowBlockOptions
            {
                TaskScheduler = TaskScheduler.FromCurrentSynchronizationContext(),
            };
            displayBlock = new ActionBlock<int>(
             result => listBox1.Items.Add(result), options);
            multiplyBlock.LinkTo(displayBlock);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            multiplyBlock.Post(counter++);
        }
    }
}
