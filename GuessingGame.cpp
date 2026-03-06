#include <iostream>
#include <string>
#include <cstdlib> // Required for system()
#include <ctime>

using namespace std;

// Helper Function for speech output
void speak(string text)
{
    // Build a command to be sent to the Windows Terminal
    string command = "powershell -Command \"Add-Type -AssemblyName System.Speech; (New-Object System.Speech.Synthesis.SpeechSynthesizer).Speak('" + text + "')\"";

    // system() sends this command to the Windows terminal
    system(command.c_str());
}

int main()
{
    srand(static_cast<unsigned int>(time(0)));
    int target = rand() % 100 + 1;
    int guess = 0;

    cout << "Accessible Number Guessing Game Started!" << endl;
    speak("Welcome. I have chosen a number between 1 and 100. What is your guess?");

    while (guess != target)
    {
        cout << "Enter your guess: ";
        cin >> guess;

        if (guess > target)
        {
            cout << "Too high!" << endl;
            speak("Too high. Try a lower number.");
        }
        else if (guess < target)
        {
            cout << "Too low!" << endl;
            speak("Too low. Try a higher number.");
        }
        else
        {
            cout << "Correct!" << endl;
            speak("Correct! You have won the game! Congratulations!");
        }
    }

    return 0;
}