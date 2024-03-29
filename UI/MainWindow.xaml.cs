﻿using System;
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
using Theoryoflanguages;

namespace UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public List<DFA> DFAs { get; set; }
        public List<NFA> NFAs { get; set; }
        public MainWindow()
        {
            InitializeComponent();
            DFA dfa ;
            NFA nfa ;
            List<q> Q = new List<q>();
            for(int i=0;i<5;i++)
            {
                q qn=new q();
                qn.Name = "q" + i;
                Q.Add(qn);
            }
            List<BSigma> Sigma = new List<BSigma>();
            BSigma s=new BSigma();
            s.ReadChar = 'a';
            Sigma.Add(s);
            BSigma s2 = new BSigma();
            s2.ReadChar = 'b';
            Sigma.Add(s2);
            List<SDelta> Delta = new List<SDelta>();
            for(int i=0;i<10;i++)
                Delta.Add(new SDelta());
            Delta[0].ReadChar = 'a';
            Delta[0].OriState = Q[0];
            Delta[0].DesState = Q[4];
            Delta[1].ReadChar = 'b';
            Delta[1].OriState = Q[0];
            Delta[1].DesState = Q[1];
            Delta[2].ReadChar = 'a';
            Delta[2].OriState = Q[1];
            Delta[2].DesState = Q[4];
            Delta[3].ReadChar = 'b';
            Delta[3].OriState = Q[1];
            Delta[3].DesState = Q[2];
            Delta[4].ReadChar = 'a';
            Delta[4].OriState = Q[2];
            Delta[4].DesState = Q[3];
            Delta[5].ReadChar = 'b';
            Delta[5].OriState = Q[2];
            Delta[5].DesState = Q[4];
            Delta[6].ReadChar = 'a';
            Delta[6].OriState = Q[3];
            Delta[6].DesState = Q[4];
            Delta[7].ReadChar = 'b';
            Delta[7].OriState = Q[3];
            Delta[7].DesState = Q[3];
            Delta[8].ReadChar = 'a';
            Delta[8].OriState = Q[4];
            Delta[8].DesState = Q[4];
            Delta[9].ReadChar = 'b';
            Delta[9].OriState = Q[4];
            Delta[9].DesState = Q[4];
            List<q> FinalStates = Q.GetRange(3,1);
            dfa = new DFA(Q, Sigma, Delta, Q[0], FinalStates);


            // nfa
            List<SDelta> Delta2 = new List<SDelta>();
            for (int i = 0; i < 8; i++)
                Delta2.Add(new SDelta());
            Delta2[0].ReadChar = 'a';
            Delta2[0].OriState = Q[0];
            Delta2[0].DesState = Q[1];
            Delta2[1].ReadChar = 'λ';
            Delta2[1].OriState = Q[0];
            Delta2[1].DesState = Q[3];
            Delta2[2].ReadChar = 'λ';
            Delta2[2].OriState = Q[1];
            Delta2[2].DesState = Q[4];
            Delta2[3].ReadChar = 'b';
            Delta2[3].OriState = Q[1];
            Delta2[3].DesState = Q[2];
            Delta2[4].ReadChar = 'a';
            Delta2[4].OriState = Q[2];
            Delta2[4].DesState = Q[3];
            Delta2[5].ReadChar = 'λ';
            Delta2[5].OriState = Q[4];
            Delta2[5].DesState = Q[3];
            Delta2[6].ReadChar = 'a';
            Delta2[6].OriState = Q[4];
            Delta2[6].DesState = Q[4];
            Delta2[7].ReadChar = 'b';
            Delta2[7].OriState = Q[3];
            Delta2[7].DesState = Q[3];
            nfa = new NFA(Q, Sigma, Delta2, Q[0], FinalStates);

            DFAs = new List<DFA>();
            NFAs = new List<NFA>();
            DFAs.Add(dfa);
            NFAs.Add(nfa);

            dgDFAs.ItemsSource = DFAs;
            dgNFAs.ItemsSource = NFAs;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            WinCreateMachine winCreateMachine = new WinCreateMachine();
            winCreateMachine.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string sentence=tbSentence.Text.Trim();
            MessageBox.Show(NFAs[dgNFAs.SelectedIndex].Read(sentence) ? "Yes this is readable by this machine!" : "No this is not readable by this machine!");
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DFA dfa = NFAs[dgNFAs.SelectedIndex].ToDFA();
            DFAs.Add(dfa);
            dgDFAs.Items.Refresh();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            NFAs.Remove(NFAs[dgNFAs.SelectedIndex]);
            dgNFAs.Items.Refresh();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            string sentence = tbSentence2.Text.Trim();
            MessageBox.Show(DFAs[dgDFAs.SelectedIndex].Read(sentence) ? "Yes this is readable by this machine!" : "No this is not readable by this machine!");
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            DFAs.Remove(DFAs[dgDFAs.SelectedIndex]);
            dgDFAs.Items.Refresh();
        }
    }
}
