namespace TournamentSimulation.StudyRunner
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnRunStudies = new System.Windows.Forms.Button();
            this.txtResults = new System.Windows.Forms.TextBox();
            this.txtIterations = new System.Windows.Forms.TextBox();
            this.lblIterationsLabel = new System.Windows.Forms.Label();
            this.lblCurrentIteration = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtCompetitors = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.chkShowResultsFrequenciesGraph = new System.Windows.Forms.CheckBox();
            this.chkShowRawTournamentResults = new System.Windows.Forms.CheckBox();
            this.chkShowCompetitorInfo = new System.Windows.Forms.CheckBox();
            this.chkShowRunTime = new System.Windows.Forms.CheckBox();
            this.chkShowStudyStatistics = new System.Windows.Forms.CheckBox();
            this.chkShowStudyTransformationMatrix = new System.Windows.Forms.CheckBox();
            this.lstTournamentStrategyMethods = new System.Windows.Forms.ListBox();
            this.btnStop = new System.Windows.Forms.Button();
            this.chkIsLowMemoryMode = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // btnRunStudies
            // 
            this.btnRunStudies.Location = new System.Drawing.Point(12, 14);
            this.btnRunStudies.Name = "btnRunStudies";
            this.btnRunStudies.Size = new System.Drawing.Size(75, 23);
            this.btnRunStudies.TabIndex = 4;
            this.btnRunStudies.Text = "Run Studies";
            this.btnRunStudies.UseVisualStyleBackColor = true;
            this.btnRunStudies.Click += new System.EventHandler(this.btnRunStudies_Click);
            // 
            // txtResults
            // 
            this.txtResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtResults.Location = new System.Drawing.Point(12, 41);
            this.txtResults.Multiline = true;
            this.txtResults.Name = "txtResults";
            this.txtResults.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtResults.Size = new System.Drawing.Size(740, 470);
            this.txtResults.TabIndex = 5;
            this.txtResults.WordWrap = false;
            this.txtResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtResults_KeyDown);
            // 
            // txtIterations
            // 
            this.txtIterations.Location = new System.Drawing.Point(216, 16);
            this.txtIterations.Name = "txtIterations";
            this.txtIterations.Size = new System.Drawing.Size(100, 20);
            this.txtIterations.TabIndex = 1;
            this.txtIterations.Text = "100";
            // 
            // lblIterationsLabel
            // 
            this.lblIterationsLabel.AutoSize = true;
            this.lblIterationsLabel.Location = new System.Drawing.Point(322, 19);
            this.lblIterationsLabel.Name = "lblIterationsLabel";
            this.lblIterationsLabel.Size = new System.Drawing.Size(49, 13);
            this.lblIterationsLabel.TabIndex = 3;
            this.lblIterationsLabel.Text = "iterations";
            // 
            // lblCurrentIteration
            // 
            this.lblCurrentIteration.AutoSize = true;
            this.lblCurrentIteration.Location = new System.Drawing.Point(550, 19);
            this.lblCurrentIteration.Name = "lblCurrentIteration";
            this.lblCurrentIteration.Size = new System.Drawing.Size(13, 13);
            this.lblCurrentIteration.TabIndex = 4;
            this.lblCurrentIteration.Text = "0";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(456, 19);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Current Iteration: ";
            // 
            // txtCompetitors
            // 
            this.txtCompetitors.Location = new System.Drawing.Point(93, 16);
            this.txtCompetitors.Name = "txtCompetitors";
            this.txtCompetitors.Size = new System.Drawing.Size(40, 20);
            this.txtCompetitors.TabIndex = 0;
            this.txtCompetitors.Text = "8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(139, 19);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "competitors";
            // 
            // chkShowResultsFrequenciesGraph
            // 
            this.chkShowResultsFrequenciesGraph.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowResultsFrequenciesGraph.AutoSize = true;
            this.chkShowResultsFrequenciesGraph.Checked = true;
            this.chkShowResultsFrequenciesGraph.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowResultsFrequenciesGraph.Location = new System.Drawing.Point(758, 110);
            this.chkShowResultsFrequenciesGraph.Name = "chkShowResultsFrequenciesGraph";
            this.chkShowResultsFrequenciesGraph.Size = new System.Drawing.Size(154, 17);
            this.chkShowResultsFrequenciesGraph.TabIndex = 2;
            this.chkShowResultsFrequenciesGraph.Text = "Results Frequencies Graph";
            this.chkShowResultsFrequenciesGraph.UseVisualStyleBackColor = true;
            // 
            // chkShowRawTournamentResults
            // 
            this.chkShowRawTournamentResults.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowRawTournamentResults.AutoSize = true;
            this.chkShowRawTournamentResults.Location = new System.Drawing.Point(758, 133);
            this.chkShowRawTournamentResults.Name = "chkShowRawTournamentResults";
            this.chkShowRawTournamentResults.Size = new System.Drawing.Size(146, 17);
            this.chkShowRawTournamentResults.TabIndex = 3;
            this.chkShowRawTournamentResults.Text = "Raw Tournament Results";
            this.chkShowRawTournamentResults.UseVisualStyleBackColor = true;
            // 
            // chkShowCompetitorInfo
            // 
            this.chkShowCompetitorInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowCompetitorInfo.AutoSize = true;
            this.chkShowCompetitorInfo.Checked = true;
            this.chkShowCompetitorInfo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowCompetitorInfo.Location = new System.Drawing.Point(758, 87);
            this.chkShowCompetitorInfo.Name = "chkShowCompetitorInfo";
            this.chkShowCompetitorInfo.Size = new System.Drawing.Size(97, 17);
            this.chkShowCompetitorInfo.TabIndex = 8;
            this.chkShowCompetitorInfo.Text = "Competitor Info";
            this.chkShowCompetitorInfo.UseVisualStyleBackColor = true;
            // 
            // chkShowRunTime
            // 
            this.chkShowRunTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowRunTime.AutoSize = true;
            this.chkShowRunTime.Checked = true;
            this.chkShowRunTime.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowRunTime.Location = new System.Drawing.Point(758, 156);
            this.chkShowRunTime.Name = "chkShowRunTime";
            this.chkShowRunTime.Size = new System.Drawing.Size(72, 17);
            this.chkShowRunTime.TabIndex = 9;
            this.chkShowRunTime.Text = "Run Time";
            this.chkShowRunTime.UseVisualStyleBackColor = true;
            // 
            // chkShowStudyStatistics
            // 
            this.chkShowStudyStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowStudyStatistics.AutoSize = true;
            this.chkShowStudyStatistics.Checked = true;
            this.chkShowStudyStatistics.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowStudyStatistics.Location = new System.Drawing.Point(758, 41);
            this.chkShowStudyStatistics.Name = "chkShowStudyStatistics";
            this.chkShowStudyStatistics.Size = new System.Drawing.Size(98, 17);
            this.chkShowStudyStatistics.TabIndex = 10;
            this.chkShowStudyStatistics.Text = "Study Statistics";
            this.chkShowStudyStatistics.UseVisualStyleBackColor = true;
            // 
            // chkShowStudyTransformationMatrix
            // 
            this.chkShowStudyTransformationMatrix.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.chkShowStudyTransformationMatrix.AutoSize = true;
            this.chkShowStudyTransformationMatrix.Checked = true;
            this.chkShowStudyTransformationMatrix.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowStudyTransformationMatrix.Location = new System.Drawing.Point(758, 64);
            this.chkShowStudyTransformationMatrix.Name = "chkShowStudyTransformationMatrix";
            this.chkShowStudyTransformationMatrix.Size = new System.Drawing.Size(134, 17);
            this.chkShowStudyTransformationMatrix.TabIndex = 11;
            this.chkShowStudyTransformationMatrix.Text = "Study Transform Matrix";
            this.chkShowStudyTransformationMatrix.UseVisualStyleBackColor = true;
            // 
            // lstTournamentStrategyMethods
            // 
            this.lstTournamentStrategyMethods.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lstTournamentStrategyMethods.FormattingEnabled = true;
            this.lstTournamentStrategyMethods.Location = new System.Drawing.Point(758, 182);
            this.lstTournamentStrategyMethods.Name = "lstTournamentStrategyMethods";
            this.lstTournamentStrategyMethods.ScrollAlwaysVisible = true;
            this.lstTournamentStrategyMethods.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstTournamentStrategyMethods.Size = new System.Drawing.Size(383, 303);
            this.lstTournamentStrategyMethods.TabIndex = 12;
            // 
            // btnStop
            // 
            this.btnStop.Location = new System.Drawing.Point(677, 14);
            this.btnStop.Name = "btnStop";
            this.btnStop.Size = new System.Drawing.Size(75, 23);
            this.btnStop.TabIndex = 13;
            this.btnStop.Text = "Stop";
            this.btnStop.UseVisualStyleBackColor = true;
            this.btnStop.Click += new System.EventHandler(this.btnStop_Click);
            // 
            // chkIsLowMemoryMode
            // 
            this.chkIsLowMemoryMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.chkIsLowMemoryMode.AutoSize = true;
            this.chkIsLowMemoryMode.Checked = true;
            this.chkIsLowMemoryMode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIsLowMemoryMode.Location = new System.Drawing.Point(758, 494);
            this.chkIsLowMemoryMode.Name = "chkIsLowMemoryMode";
            this.chkIsLowMemoryMode.Size = new System.Drawing.Size(116, 17);
            this.chkIsLowMemoryMode.TabIndex = 14;
            this.chkIsLowMemoryMode.Text = "Low Memory Mode";
            this.chkIsLowMemoryMode.UseVisualStyleBackColor = true;
            this.chkIsLowMemoryMode.CheckedChanged += new System.EventHandler(this.chkIsLowMemoryMode_CheckedChanged);
            // 
            // Form1
            // 
            this.AcceptButton = this.btnRunStudies;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1153, 523);
            this.Controls.Add(this.chkIsLowMemoryMode);
            this.Controls.Add(this.btnStop);
            this.Controls.Add(this.lstTournamentStrategyMethods);
            this.Controls.Add(this.chkShowStudyTransformationMatrix);
            this.Controls.Add(this.chkShowStudyStatistics);
            this.Controls.Add(this.chkShowRunTime);
            this.Controls.Add(this.chkShowCompetitorInfo);
            this.Controls.Add(this.chkShowRawTournamentResults);
            this.Controls.Add(this.chkShowResultsFrequenciesGraph);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtCompetitors);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblCurrentIteration);
            this.Controls.Add(this.lblIterationsLabel);
            this.Controls.Add(this.txtIterations);
            this.Controls.Add(this.txtResults);
            this.Controls.Add(this.btnRunStudies);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnRunStudies;
        private System.Windows.Forms.TextBox txtResults;
        private System.Windows.Forms.TextBox txtIterations;
        private System.Windows.Forms.Label lblIterationsLabel;
        private System.Windows.Forms.Label lblCurrentIteration;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtCompetitors;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox chkShowResultsFrequenciesGraph;
        private System.Windows.Forms.CheckBox chkShowRawTournamentResults;
        private System.Windows.Forms.CheckBox chkShowCompetitorInfo;
        private System.Windows.Forms.CheckBox chkShowRunTime;
        private System.Windows.Forms.CheckBox chkShowStudyStatistics;
        private System.Windows.Forms.CheckBox chkShowStudyTransformationMatrix;
        private System.Windows.Forms.ListBox lstTournamentStrategyMethods;
        private System.Windows.Forms.Button btnStop;
        private System.Windows.Forms.CheckBox chkIsLowMemoryMode;
    }
}

