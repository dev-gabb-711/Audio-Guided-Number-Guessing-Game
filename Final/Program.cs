using System;
using System.Speech.Synthesis; // For Speaking
using System.Speech.Recognition; // For Listening
using System.Threading; // Keep the application running

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
			// Setup the number
			targetNumber = new Random().Next(1, 101);

			// Setup listening "grammar"
			// Tell computer to ONLY listen for gameChoices 1 to 100
			Choices gameChoices = new Choices();
			int i;
			for (i = 1; i <= 100; i++)
			{
				gameChoices.Add(i.ToString());
			}

			gameChoices.Add("restart");
			gameChoices.Add("exit game");

			GrammarBuilder gb = new GrammarBuilder(gameChoices);
			Grammar g = new Grammar(gb);

			listener.LoadGrammar(g);
			listener.SetInputToDefaultAudioDevice();
			listener.SpeechRecognized += HandleSpeech;

			// Start Game
			Console.WriteLine("Game Started! Speak your guess (1-100)...");
			Speak("Welcome to the voice guessing game. Speak a number from 1 to 100.");

			// Start listening in the background
			listener.RecognizeAsync(RecognizeMode.Multiple);

			// Kepp program running until game is over
			while (!isGameOver)
			{
				Thread.Sleep(100);
			}

			Speak("Game exiting...");
			Thread.Sleep(2000); // Give time to finish speaking
		}

		static void HandleSpeech(object? sender, SpeechRecognizedEventArgs e)
		{
			string speech = e.Result.Text;

			if (speech == "restart")
			{
				targetNumber = new Random().Next(1, 101);
				Speak("Game reset. I have a new number. What is your guess?");
				return;
			}

			if (speech == "exit game")
			{
				isGameOver = true;
				return;
			}

			int guess = int.Parse(e.Result.Text);
			Console.WriteLine($"You said: {guess}");

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
				speaker.Speak($"Correct! {guess} was the secret number. You Win!");
				isGameOver = true;
			}
		}

		static void Speak(string text)
		{
			// Use 'Immediate' to stop computer from talking over itself
			speaker.SpeakAsync(text);
		}
	}
}