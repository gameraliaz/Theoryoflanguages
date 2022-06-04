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
using System.Windows.Shapes;
using Theoryoflanguages;

namespace UI
{
    /// <summary>
    /// Interaction logic for WinCreateMachine.xaml
    /// </summary>
    public partial class WinCreateMachine : Window
    {
        public List<q> Q { get; set; }
        public List<BSigma> Sigma { get; set; }
        public List<SDelta> Delta { get; set; }
        public q StartState { get; set; }
        public List<q> FinalStates { get; set; }
        public WinCreateMachine()
        {
            InitializeComponent();
            Q=new List<q>();
            dgQ.ItemsSource = Q;
            Sigma = new List<BSigma>();
            dgSigma.ItemsSource = Sigma;
            Delta = new List<SDelta>();
            dgDelta.ItemsSource = Delta;
            StartState = new q();
            FinalStates = new List<q>();
            dgFinalStates.ItemsSource = FinalStates;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            q nq=new q();
            nq.Name = NodetoQ.Text.Trim();
            foreach(var q in Q)
            {
                if(q.Name==nq.Name)
                {
                    MessageBox.Show("this node is already in Q");
                    return;
                }
            }
            Q.Add(nq);
            dgQ.Items.Refresh();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            try
            {
                StartState = (q)dgQ.SelectedItem;
                txtStartNode.Text = StartState.ToString();
            }
            catch
            {
                MessageBox.Show("Please select a node from a Q datagrid (above)");
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            try
            {
                FinalStates.Add((q)dgQ.SelectedItem);
                dgFinalStates.Items.Refresh();
            }
            catch
            {
                MessageBox.Show("Please select a node from a Q datagrid (above)");
            }
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            BSigma ns = new BSigma();
            ns.ReadChar = chartoSigma.Text.Trim().ToCharArray()[0];
            Sigma.Add(ns);
            dgSigma.Items.Refresh();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            try
            {
                destinitionNode.Text = ((q)dgQ.SelectedItem).ToString();
            }
            catch
            {
                MessageBox.Show("Please select a node from a Q datagrid (above)");
            }
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            try
            {
                letter.Text = ((BSigma)dgSigma.SelectedItem).ReadChar.ToString();
            }
            catch
            {
                MessageBox.Show("Please select a letter from a Sigma datagrid (above)");
            }
        }

        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            try
            {
                originNode.Text = ((q)dgQ.SelectedItem).ToString();
            }
            catch
            {
                MessageBox.Show("Please select a node from a Q datagrid (above)");
            }
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            SDelta nd=new SDelta();
            nd.ReadChar=letter.Text.ToCharArray()[0];
            q oq = new q();
            q dq = new q();
            oq.Name=originNode.Text;
            nd.OriState = oq;
            dq.Name=destinitionNode.Text;
            nd.DesState = dq;
            Delta.Add(nd);
            dgDelta.Items.Refresh();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            bool typeDFA = true;
            if (Delta.Count == (Sigma.Count * Q.Count))
            {
                foreach (var q in Q)
                {
                    List<BSigma> qs = new List<BSigma>();
                    foreach(var d in Delta)
                    {
                        if(d.OriState.Name==q.Name)
                        {
                            foreach(var sigm in qs)
                            {
                                if(sigm.ReadChar==d.ReadChar)
                                {
                                    typeDFA = false;
                                    break;
                                }
                            }
                            if (!typeDFA)
                                break;
                            BSigma bsim=new BSigma();
                            bsim.ReadChar = d.ReadChar;
                            qs.Add(bsim);
                        }
                    }
                    if (!typeDFA)
                        break;
                    if(qs.Count==Sigma.Count)
                    {
                        foreach(var ns in qs)
                        {
                            bool hsig = false;
                            foreach(var os in Sigma)
                            {
                                if(os.ReadChar==ns.ReadChar)
                                {
                                    hsig= true;
                                    break;
                                }
                            }
                            if(!hsig)
                            {
                                typeDFA= false;
                                break;
                            }
                        }
                    }
                    else
                    {
                        typeDFA=false;
                        break;
                    }
                }
            }
            else
                typeDFA=false;


            if(typeDFA)
            {
                DFA dfa = new DFA(Q, Sigma, Delta, StartState, FinalStates);
                foreach (Window window in Application.Current.Windows.OfType<MainWindow>())
                {
                    ((MainWindow)window).DFAs.Add(dfa);
                    ((MainWindow)window).dgDFAs.Items.Refresh();
                }
            }
            else
            {
                NFA nfa = new NFA(Q, Sigma, Delta, StartState, FinalStates);
                foreach (Window window in Application.Current.Windows.OfType<MainWindow>())
                {
                    ((MainWindow)window).NFAs.Add(nfa);
                    ((MainWindow)window).dgNFAs.Items.Refresh();
                }
            }
        }
    }
}
