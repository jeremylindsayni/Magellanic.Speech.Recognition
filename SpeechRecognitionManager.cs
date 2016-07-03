using System;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Media.SpeechRecognition;

namespace Magellanic.Speech.Recognition
{
    public class SpeechRecognitionManager
    {
        public SpeechRecognitionManager(string grammarFile)
        {
            this.SpeechRecognizer = new SpeechRecognizer();

            this.GrammarFile = string.Format(grammarFile);
        }

        public SpeechRecognizer SpeechRecognizer { get; set; }

        private string GrammarFile { get; set; }

        public async Task CompileGrammar()
        {
            var grammarContentFile = await Package.Current.InstalledLocation.GetFileAsync(this.GrammarFile);

            var grammarConstraint = new SpeechRecognitionGrammarFileConstraint(grammarContentFile);

            this.SpeechRecognizer.Constraints.Add(grammarConstraint);

            var compilationResult = await this.SpeechRecognizer.CompileConstraintsAsync();

            if (compilationResult.Status == SpeechRecognitionResultStatus.Success)
            {
                await this.SpeechRecognizer.ContinuousRecognitionSession.StartAsync();
                return;
            }

            throw new Exception($"Grammar file was not compiled successfully: {compilationResult.Status}");
        }
    }
}