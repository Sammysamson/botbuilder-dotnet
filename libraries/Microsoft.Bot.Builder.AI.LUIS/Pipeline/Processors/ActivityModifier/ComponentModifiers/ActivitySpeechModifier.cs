﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Bot.Builder.AI.LanguageGeneration.Helpers;
using Microsoft.Bot.Schema;

namespace Microsoft.Bot.Builder.AI.LanguageGeneration.Engine
{
    internal class ActivitySpeechModifier : IActivityComponentModifier
    {
        public void Modify(Activity activity, ICompositeResponse response)
        {
            if (!string.IsNullOrWhiteSpace(activity.Speak))
            {
                var recognizedPatterns = PatternRecognizer.Recognize(activity.Speak);
                foreach (var pattern in recognizedPatterns)
                {
                    activity.Speak = activity.Speak.Replace(pattern, response.TemplateResolutions[pattern]);
                }
            }
        }
    }
}
