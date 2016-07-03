using System;
using System.Threading.Tasks;
using Windows.Media.SpeechRecognition;

namespace Magellanic.Speech.Recognition
{
    public static class SpeechRecognitionExtensions
    {
        public static async Task Dispose(this SpeechRecognitionManager speechRecognitionManager)
        {
            await speechRecognitionManager.SpeechRecognizer.ContinuousRecognitionSession.StopAsync();

            speechRecognitionManager.SpeechRecognizer.Dispose();

            speechRecognitionManager.SpeechRecognizer = null;
        }

        public static string GetInterpretation(this SpeechRecognitionSemanticInterpretation interpretation, string key)
        {
            if (interpretation.Properties.ContainsKey(key))
            {
                return interpretation.Properties[key][0];
            }

            return null;
        }

        public static bool IsRecognisedWithHighConfidence(this SpeechRecognitionResult result)
        {
            return result.Confidence == SpeechRecognitionConfidence.High;
        }
    }
}
