using System;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;

[assembly: System.Runtime.Versioning.SupportedOSPlatform("windows")]

namespace VoiceGame
{
	class Program
	{
		static SpeechSynthesizer speaker = new SpeechSynthesizer();
		static SpeechRecognitionEngine listener = new SpeechRecognitionEngine();
		static int targetNumber;
		static bool isGameOver = false;

		static void Main(string[] args)
		{
			targetNumber = new Random().Next(1, 101);

			Choices choices = new Choices();
			for (int i = 1; i <= 100; i++) choices.Add(i.ToString());
			choices.Add("restart");
			choices.Add("exit game");

			// GrammarBuilder creates a strict phonetic profile for higher accuracy
			GrammarBuilder gb = new GrammarBuilder(choices);
			listener.LoadGrammar(new Grammar(gb));

			listener.SetInputToDefaultAudioDevice();
			listener.SpeechRecognized += HandleSpeech;

			Console.WriteLine("Game Active. Speak your guess CLEARLY (1-100)...");
			Speak("Welcome to the Number Guessing Game. I have a number from 1 to 100. What is your guess?");

			listener.RecognizeAsync(RecognizeMode.Multiple);

			while (!isGameOver)
			{
				Thread.Sleep(100);
			}
			Speak("Thank you for playing.");
			Console.WriteLine("Exiting...");
			Thread.Sleep(1500);
		}

		static void HandleSpeech(object? sender, SpeechRecognizedEventArgs e)
		{
			// Ignore low-confidence background noise
			// This rating determines how confident the "listener" engine is with how it interpreted the spoken input. The higher the confidence, the better it heard your input, DOES NOT ACTUALLY DETERMINE THE ACCURACY OF THE INPUT.
			if (e.Result.Confidence < 0.5) return;

			string input = e.Result.Text;

			if (input == "restart")
			{
				targetNumber = new Random().Next(1, 101);
				Speak("New number generated. Guess again.");
				return;
			}

			if (input == "exit game")
			{
				Speak("Thank you for playing.");
				isGameOver = true;
				return;
			}

			int guess = int.Parse(input);
			Console.WriteLine($"Result: {guess} ({e.Result.Confidence:P0} confidence)");

			if (guess > targetNumber)
			{
				Speak($"{guess} is too high. Try a lower number.");
			}
			else if (guess < targetNumber)
			{
				Speak($"{guess} is too low. Try a higher number.");
			}
			else
			{
				listener.RecognizeAsyncStop();
				// Synchronous Speak prevents app closure before audio finishes
				speaker.Speak($"Correct! {guess} was the number. You win!");
				isGameOver = true;
			}
		}

		static void Speak(string text)
		{
			speaker.SpeakAsync(text);
		}
	}
}