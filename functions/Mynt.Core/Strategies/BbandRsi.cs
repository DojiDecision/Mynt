﻿using System.Collections.Generic;
using System.Linq;
using Mynt.Core.Enums;
using Mynt.Core.Indicators;
using Mynt.Core.Interfaces;
using Mynt.Core.Models;

namespace Mynt.Core.Strategies
{
    /// <summary>
    /// https://www.tradingview.com/script/zopumZ8a-Bollinger-RSI-Double-Strategy-by-ChartArt/
    /// </summary>
    public class BbandRsi : ITradingStrategy
    {
        public string Name => "BBand RSI";
        
        public List<TradeAdvice> Prepare(List<Candle> candles)
        {
            var result = new List<TradeAdvice>();

            var currentPrices = candles.Select(x => x.Close).ToList();
            var bbands = candles.Bbands(20);
            var rsi = candles.Rsi(16);
            
            for (int i = 0; i < candles.Count; i++)
            {
                if (i == 0)
                    result.Add(TradeAdvice.Hold);
                else if (rsi[i] < 30 && currentPrices[i] < bbands.LowerBand[i])
                    result.Add(TradeAdvice.Buy);
                else if (rsi[i] > 70)
                    result.Add(TradeAdvice.Sell);
                else
                    result.Add(TradeAdvice.Hold);
            }

            return result;
        }
    }
}
