using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TournamentSimulation.TournamentStrategies;
using TournamentSimulation.MatchStrategies;
using System.Threading;
using MathNet.Numerics.LinearAlgebra;
using System.Reflection;

namespace TournamentSimulation.StudyRunner
{
    public partial class Form1 : Form
    {
        #region Fields and Properties
        private int _numberOfTournamentIterations;
        private int _currentIteration;
        private int _numberOfCompetitors;
        private DateTime _startTime;
        private List<string> _methodsToExecute;
        private bool _isStop;
        private Dictionary<string, double> _studyDeterminants;
        #endregion

        private delegate void SetTextCallback(string text);
        private delegate void SetIterationLabelCallback(string text);

        #region Constructors
        public Form1()
        {
            InitializeComponent();

            DisplayTournamentStrategies();
        }
        #endregion

        private void btnRunStudies_Click(object sender, EventArgs e)
        {
            _numberOfTournamentIterations = Convert.ToInt32(txtIterations.Text);
            _numberOfCompetitors = Convert.ToInt32(txtCompetitors.Text);

            txtResults.Clear();

            ListBox.SelectedObjectCollection selectedMethods = lstTournamentStrategyMethods.SelectedItems;

            _methodsToExecute = new List<string>();
            foreach (string item in selectedMethods)
            {
                _methodsToExecute.Add(item);
            }

            // start a new thread so the results can be displayed while the studies are being run
            ThreadStart job = new ThreadStart(RunStudies);
            Thread thread = new Thread(job);
            thread.Start();
        }

        private void RunStudies()
        {
            _isStop = false;
            _studyDeterminants = new Dictionary<string, double>();

            // TODO: move the study methods to a separate class to get it out of Form1
            // TODO: make the study methods internal, rather than public

            foreach (string methodToExecute in _methodsToExecute)
            {
                this.GetType().GetMethod(string.Format("Study{0}", methodToExecute)).Invoke(this, null);

                if (_isStop)
                    break;
            }

            AppendResultsText("\r\n\r\n===============================================================================================");
            AppendResultsText("\r\nDone");
        }

        #region Tournament Strategies

        public void StudyIterateCompetitors_Random()
        {
            for (int i = 2; i <= _numberOfCompetitors; i++)
            {
                List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(i);

                TournamentStrategy tournamentStrategy = new RandomTS();
                MatchStrategy matchStrategy = new SimpleRandomMS();

                RunStudy(competitors, tournamentStrategy, matchStrategy);
            }
        }

        public void StudyIterateCompetitors_Rr_1_FirstTo2_CompetitorNumberSensitivity()
        {
            for (int i = 2; i <= _numberOfCompetitors; i++)
            {
                List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(i);

                TournamentStrategy tournamentStrategy = new RrTS(1, 2);
                MatchStrategy matchStrategy = new SimpleRandomMS();

                RunStudy(competitors, tournamentStrategy, matchStrategy);
            }
        }

        public void StudyIterateCompetitors_Rr_1_FirstTo2_CompetitorSeedSensitivity()
        {
            for (int i = 2; i <= _numberOfCompetitors; i++)
            {
                List<Competitor> competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomRatings(_numberOfCompetitors);

                TournamentStrategy tournamentStrategy = new RrTS(1, 2);
                MatchStrategy matchStrategy = new SimpleRandomMS();

                RunStudy(competitors, tournamentStrategy, matchStrategy);
            }
        }

        public void StudyRr_2_FirstTo2()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRr_1_FirstTo2()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(1, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        public void StudyRr_1_FirstTo2_ReasonableGuessCompetitors()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetReasonableGuessCompetitors_8();

            TournamentStrategy tournamentStrategy = new RrTS(1, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        public void StudyKo()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new KoTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        public void StudyKo_Dominant2In16()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithTwoDominants_16();

            TournamentStrategy tournamentStrategy = new KoTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        public void StudyRr2_Dominant2In16()
        {
            var competitors = Helpers.CompetitorListHelper.GetCompetitorsWithTwoDominants_16();

            TournamentStrategy tournamentStrategy = new RrTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        public void StudyKo_RandomSeeding()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetCompetitorsWithRandomizedSeeds(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new KoTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRr_1()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(1, 1);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRr_2()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRr_2_5()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(2.5);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        public void StudyRr_2_1()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(2.1);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRr_3()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(3);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRr_10()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrTS(10);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRrKoSfFi()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrKoSfFiTS(2.0, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRrKoSf5PfFi()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);
            

            TournamentStrategy tournamentStrategy = new RrKoSf5PfFiTS(2.0, 2, 2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRrKoSfPfFi()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrKoSfPfFiTS(2.0, 2, 2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRrKoSfPfFiRrTS_ReferenceFormat()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrKoSfPfFiRrTS(2.0, 2, 2, 2, 2.0);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        public void StudyRrRrSf5KoFiPf()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrRrSf5KoFiPfTS(2.0, 1.0, 2, 2, 1.0);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRrKo1()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrKo1TS(2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }
        
        public void StudyRrKo1a()
        {
            List<Competitor> competitors = Helpers.CompetitorListHelper.GetEvenlySpacedCompetitors(_numberOfCompetitors);

            TournamentStrategy tournamentStrategy = new RrKo1aTS(2, 2);
            MatchStrategy matchStrategy = new SimpleRandomMS();

            RunStudy(competitors, tournamentStrategy, matchStrategy);
        }

        #endregion

        private void RunStudy(List<Competitor> competitors, TournamentStrategy tournamentStrategy, MatchStrategy matchStrategy)
        {
            RunStudy(competitors, tournamentStrategy, matchStrategy, false);
        }

        private void RunStudy(List<Competitor> competitors, TournamentStrategy tournamentStrategy, MatchStrategy matchStrategy, bool isJumbleCompetitorSeeds)
        {
            _currentIteration = 0;
            _startTime = DateTime.Now;

            Study study = new Study(tournamentStrategy, matchStrategy, chkIsLowMemoryMode.Checked);
            study.Iterated += new IteratedEventHandler(StudyIterated);

            if (!string.IsNullOrEmpty(txtResults.Text.Trim()))
                AppendResultsText("\r\n\r\n");

            AppendResultsText("===============================================================================================");

            try
            {
                study.Run(competitors, _numberOfTournamentIterations, isJumbleCompetitorSeeds);

                AppendResultsText(study.StrategyInformation);
                if (chkShowStudyStatistics.Checked) AppendResultsText(study.ResultsStatistics);
                if (chkShowStudyTransformationMatrix.Checked) AppendResultsText(study.CombinedTournamentTransformationMatrixForDisplay.ToString());
                if (chkShowCompetitorInfo.Checked) AppendResultsText(study.CompetitorInformation);
                if (chkShowResultsFrequenciesGraph.Checked) AppendResultsText(study.ResultsFrequenciesGraph);
                if (chkShowRawTournamentResults.Checked) AppendResultsText(study.RawTournamentResultsForDisplay);
                if (chkShowRunTime.Checked) AppendResultsText(string.Format("\r\n\r\nRun Time: {0}", DateTime.Now.Subtract(_startTime)));
            }
            catch (Exception ex)
            {
                AppendResultsText(string.Format("\r\n\r\nTournament Strategy: {0}", study.TournamentStrategyForDisplay));
                AppendResultsText(string.Format("\r\n\r\nCould not run this study. Error: {0}", ex.Message));
            }
            finally
            {
                study = null;
            }
        }

        private void StudyIterated(object sender, IteratedEventArgs e)
        {
            _currentIteration = e.IterationNumber;

            SetIterationLabel(_currentIteration.ToString());
        }

        private void AppendResultsText(string text)
        {
            if (this.txtResults.InvokeRequired)
            {
                SetTextCallback setTextCallback = new SetTextCallback(AppendResultsText);
                this.Invoke(setTextCallback, new object[] { text });
            }
            else
            {
                txtResults.Text += text;
                
                txtResults.Refresh();
            }
        }

        private void SetIterationLabel(string text)
        {
            if (this.lblCurrentIteration.InvokeRequired)
            {
                SetIterationLabelCallback setIterationLabelCallback = new SetIterationLabelCallback(SetIterationLabel);
                this.Invoke(setIterationLabelCallback, new object[] { text });
            }
            else
            {
                lblCurrentIteration.Text = _currentIteration.ToString();
                lblCurrentIteration.Refresh();
            }
        }

        private void txtResults_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A))
            {
                this.txtResults.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
                base.OnKeyDown(e);
        }

        private void DisplayTournamentStrategies()
        {
            MethodInfo[] methodInfos = this.GetType().GetMethods();

            MethodInfo[] studyMethodInfos = methodInfos.Where(x => x.Name.StartsWith("Study")).OrderBy(x => x.Name).ToArray<MethodInfo>();

            lstTournamentStrategyMethods.Items.AddRange(studyMethodInfos.Select(x => x.Name.Replace("Study", "")).ToArray<string>());
        }

        private void btnStop_Click(object sender, EventArgs e)
        {
            _isStop = true;
        }

        private void chkIsLowMemoryMode_CheckedChanged(object sender, EventArgs e)
        {
            //if (chkIsLowMemoryMode.Checked)
            //{
            //    chkShowStudyTransformationMatrix.Checked = false;
            //}

            //chkShowStudyTransformationMatrix.Enabled = !chkIsLowMemoryMode.Checked;
        }
    }
}