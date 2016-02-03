using System;

namespace TournamentSimulation.Helpers
{
    /// <summary>
    /// Basic statistics functions
    /// </summary>
    /// <remarks>
    /// Constructor: public statistics(params double[] list)
    /// Update the design: public void update(params double[] list)
    /// Compute the mode: public double mode() - If there is more then one mode the NaN value will be returned.
    /// Compute the size of the design: public int length()
    /// Compute the minimum value of the design: public double min()
    /// Compute the maximum value of the design: public double max()
    /// Compute the first quarter : public double Q1
    /// Compute the median : public double Q2
    /// Compute the third quarter : public double Q3
    /// Compute the average: public double Mean
    /// Compute the range: public double range()
    /// Compute the inter quantile range: public double IQ()
    /// Compute middle of range: public double middle_of_range()
    /// Compute sample variance: public double var()
    /// Compute standard deviation: public double s()
    /// Compute the YULE index: public double YULE()
    /// compute the index standard of a given member of the design: public double Z(double member)
    /// compute the covariance: public double cov(statistics s), public static double cov(statistics s1,statistics s2)
    /// compute the correlation coefficient: public double r(statistics design), public static double r(statistics design1,statistics design2)
    /// compute the "a" factor of the linear function of design: public double a(statistics design)
    /// compute the "a" factor of the linear function of design2: public static double a(statistics design1,statistics design2)
    /// compute the "b" factor of the linear function of design: public double b(statistics design)
    /// compute the "b" factor of the linear function of design 2: public static double b(statistics design1,statistics design2)
    /// </remarks>
    public class StatisticsHelper
    {
        private double[] _list;

        public StatisticsHelper(params double[] list)
        {
            this._list = list;
        }

        public void Update(params double[] list)
        {
            this._list = list;
        }

        public double Mode
        {
            get
            {
                try
                {
                    var i = new double[_list.Length];
                    _list.CopyTo(i, 0);
                    Sort(i);
                    double valMode = i[0], helpValMode = i[0];
                    int oldCounter = 0, newCounter = 0;
                    var j = 0;
                    
                    for (; j <= i.Length - 1; j++)
                    {
                        if (i[j] == helpValMode) newCounter++;
                        else if (newCounter > oldCounter)
                        {
                            oldCounter = newCounter;
                            newCounter = 1;
                            helpValMode = i[j];
                            valMode = i[j - 1];
                        }
                        else if (newCounter == oldCounter)
                        {
                            valMode = double.NaN;
                            helpValMode = i[j];
                            newCounter = 1;
                        }
                        else
                        {
                            helpValMode = i[j];
                            newCounter = 1;
                        }
                    }

                    if (newCounter > oldCounter) valMode = i[j - 1];
                    else if (newCounter == oldCounter) valMode = double.NaN;
                    return valMode;
                }
                catch
                {
                    return double.NaN;
                }
            }
        }
        public int Length
        {
            get
            {
                return _list.Length;
            }
        }
        public double Min
        {
            get
            {
                var minimum = double.PositiveInfinity;
                for (var i = 0; i <= _list.Length - 1; i++)
                    if (_list[i] < minimum) minimum = _list[i];
                return minimum;
            }
        }
        public double Max
        {
            get
            {
                var maximum = double.NegativeInfinity;
                for (var i = 0; i <= _list.Length - 1; i++)
                    if (_list[i] > maximum) maximum = _list[i];
                return maximum;
            }
        }
        public double Quartile1
        {
            get
            {
                return Qi(0.25);
            }
        }
        public double Quartile2
        {
            get
            {
                return Qi(0.5);
            }
        }
        public double Quartile3
        {
            get
            {
                return Qi(0.75);
            }
        }
        public double Mean
        {
            get
            {
                try
                {
                    double sum = 0;
                    for (var i = 0; i <= _list.Length - 1; i++)
                    {
                        sum += _list[i];
                    }
                    
                    return sum / _list.Length;
                }
                catch
                {
                    return double.NaN;
                }
            }
        }
        public double Range
        {
            get
            {
                return (Max - Min);
            }
        }

        public double InterquartileRange
        {
            get
            {
                return Quartile3 - Quartile1;
            }
        }
        public double MiddleOfRange
        {
            get
            {
                return (Min + Max) / 2;
            }
        }
        public double Variance
        {
            get
            {
                try
                {
                    double s = 0;
                    for (var i = 0; i <= _list.Length - 1; i++)
                        s += Math.Pow(_list[i], 2);
                    return (s - _list.Length * Math.Pow(Mean, 2)) / (_list.Length - 1);
                }
                catch
                {
                    return double.NaN;
                }
            }
        }
        public double StandardDeviation
        {
            get
            {
                return Math.Sqrt(Variance);
            }
        }
        public double YuleIndex
        {
            get
            {
                try
                {
                    return ((Quartile3 - Quartile2) - (Quartile2 - Quartile1)) / (Quartile3 - Quartile1);
                }
                catch
                {
                    return double.NaN;
                }
            }
        }

        public double Z(double member)
        {
            try
            {
                if (Exists(member)) return (member - Mean) / StandardDeviation;
                
                return double.NaN;
            }
            catch
            {
                return double.NaN;
            }
        }

        public static double Covariance(StatisticsHelper s1, StatisticsHelper s2)
        {
            try
            {
                if (s1.Length != s2.Length) return double.NaN;
                var len = s1.Length;
                double sumMul = 0;
                for (var i = 0; i <= len - 1; i++)
                    sumMul += (s1._list[i] * s2._list[i]);
                return (sumMul - len * s1.Mean * s2.Mean) / (len - 1);
            }
            catch
            {
                return double.NaN;
            }
        }
        public static double MeanAbsoluteDifference(StatisticsHelper s1, StatisticsHelper s2)
        {
            return s1.MeanAbsoluteDifference(s2);
        }
        public static double r(StatisticsHelper design1, StatisticsHelper design2)
        {
            try
            {
                return Covariance(design1, design2) / (design1.StandardDeviation * design2.StandardDeviation);
            }
            catch
            {
                return double.NaN;
            }
        }
        public static double a(StatisticsHelper design1, StatisticsHelper design2)
        {
            try
            {
                return Covariance(design1, design2) / (Math.Pow(design2.StandardDeviation, 2));
            }
            catch
            {
                return double.NaN;
            }
        }
        public static double b(StatisticsHelper design1, StatisticsHelper design2)
        {
            return design1.Mean - a(design1, design2) * design2.Mean;
        }

        #region Helper Methods

        private double Qi(double i)
        {
            try
            {
                var j = new double[_list.Length];
                _list.CopyTo(j, 0);
                Sort(j);
                if (Math.Ceiling(_list.Length * i) == _list.Length * i)
                    return (j[(int)(_list.Length * i - 1)] + j[(int)(_list.Length * i)]) / 2;
                
                return j[((int)(Math.Ceiling(this._list.Length * i))) - 1];
            }
            catch
            {
                return double.NaN;
            }
        }

        private void Sort(double[] i)
        {
            var temp = new double[i.Length];
            MergeSort(i, temp, 0, i.Length - 1);
        }

        private void MergeSort(double[] source, double[] temp, int left, int right)
        {
            if (left >= right) return;

            var mid = (left + right) / 2;
            this.MergeSort(source, temp, left, mid);
            this.MergeSort(source, temp, mid + 1, right);
            Merge(source, temp, left, mid + 1, right);
        }

        private static void Merge(double[] source, double[] temp, int left, int mid, int right)
        {
            int i;
            var leftEnd = mid - 1;
            var tmpPos = left;
            var numElements = right - left + 1;

            while ((left <= leftEnd) && (mid <= right))
            {
                if (source[left] <= source[mid])
                {
                    temp[tmpPos] = source[left];
                    tmpPos++;
                    left++;
                }
                else
                {
                    temp[tmpPos] = source[mid];
                    tmpPos++;
                    mid++;
                }
            }

            while (left <= leftEnd)
            {
                temp[tmpPos] = source[left];
                left++;
                tmpPos++;
            }

            while (mid <= right)
            {
                temp[tmpPos] = source[mid];
                mid++;
                tmpPos++;
            }

            for (i = 1; i <= numElements; i++)
            {
                source[right] = temp[right];
                right--;
            }
        }

        private bool Exists(double member)
        {
            var isExist = false;
            var i = 0;
            while (i <= _list.Length - 1 && !isExist)
            {
                isExist = (_list[i] == member);
                i++;
            }
            return isExist;
        }

        private double Covariance(StatisticsHelper s)
        {
            try
            {
                if (this.Length != s.Length) return double.NaN;
                var len = this.Length;
                double sumMul = 0;
                for (var i = 0; i <= len - 1; i++)
                    sumMul += (this._list[i] * s._list[i]);

                return (sumMul - len * this.Mean * s.Mean) / (len - 1);
            }
            catch
            {
                return double.NaN;
            }
        }

        private double MeanAbsoluteDifference(StatisticsHelper design)
        {
            try
            {
                if (this.Length != design.Length) return double.NaN;

                var len = this.Length;
                double sumDiff = 0;

                for (var i = 0; i < len; i++)
                    sumDiff += Math.Abs(this._list[i] - design._list[i]);

                return sumDiff / len;
            }
            catch
            {
                return double.NaN;
            }
        }

        private double r(StatisticsHelper design)
        {
            try
            {
                return this.Covariance(design) / (this.StandardDeviation * design.StandardDeviation);
            }
            catch
            {
                return double.NaN;
            }
        }

        private double a(StatisticsHelper design)
        {
            try
            {
                return this.Covariance(design) / (Math.Pow(design.StandardDeviation, 2));
            }
            catch
            {
                return double.NaN;
            }
        }

        private double b(StatisticsHelper design)
        {
            return this.Mean - this.a(design) * design.Mean;
        }

        #endregion
    }
}