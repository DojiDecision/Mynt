﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mynt.Core.Enums;
using Mynt.Core.Indicators;
using Mynt.Core.Interfaces;
using Mynt.Core.Models;

namespace Mynt.Core.Strategies
{
    public class DerivativeOscillator : ITradingStrategy
    {
        public string Name => "Derivative Oscillator";

        public List<TradeAdvice> Prepare(List<Candle> candles)
        {
            var result = new List<TradeAdvice>();
            var derivativeOsc = candles.DerivativeOscillator();

            for (int i = 0; i < candles.Count; i++)
            {
                if (i == 0)
                    result.Add(TradeAdvice.Hold);
                else if (derivativeOsc[i - 1] < 0 && derivativeOsc[i] > 0)
                    result.Add(TradeAdvice.Buy);
                else if (derivativeOsc[i] >= 0 && derivativeOsc[i] <= derivativeOsc[i-1])
                    result.Add(TradeAdvice.Sell);
                else
                    result.Add(TradeAdvice.Hold);
            }

            return result;
        }
    }
}
