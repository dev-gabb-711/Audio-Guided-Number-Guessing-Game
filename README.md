# Audio-Guided-Number-Guessing-Game
This project contains two implementations of a number guessing game designed to showcase the evolution from a standard Command-Line Interface (CLI) to a fully accessible, voice-controlled system. There are 2 version of implementations here, one in C++ and one in C#

## Part 1: C++ Manual CLI Game
A lightweight, terminal-based version of the game that focuses on core logic and standard keyboard interaction.

### Key Features
- Manual Input: Uses standard C++ streams for keyboard-based guessing.
- Basic Audio: Employs simple system calls to provide vocal feedback for high/low guesses.
- Optimized Logic: Fast, low-latency execution using the C++17 standard.
- Minimalist UI: Clean, text-only interface for maximum compatibility with legacy systems.

### Technical Details
- Language: C++
- Compiler: MinGW / G++
- Interface: Windows Command Prompt / PowerShell

### Installation and Running
For both parts, clone the repository:

```
git clone https://github.com/dev-gabb-711/Audio-Guided-Number-Guessing-Game.git
```
Compile the source code:

```
g++ GuessingGame.cpp -o ManualGame.exe
```
Run the executable:

```
./ManualGame.exe
```

## Part 2: C# Voice-Controlled Game
A hands-free, fully accessible version of the game designed for users with visual or motor impairments who cannot use a keyboard.

### Key Features
- Full STT/TTS: Complete voice-in and voice-out loop using the Windows Speech API.
- Grammar Constraints: High-accuracy recognition engine limited to numbers 1-100 and specific game commands.
- Noise Suppression: Implements a 0.5 confidence threshold to filter out ambient room noise.
- Echo Protection: Automatically manages the audio buffer to prevent the computer from hearing its own voice.
- Note: The audio reading of this program is still very minimal so it might not pick up immediately. Please try and increase your intonation and your volume when speaking. Thank you :))

### How to Play (Voice Version)
The game is designed to be played entirely via a microphone. Once the game starts:
- Listen: The computer will introduce the game and ask for a guess.
- Speak: Say any number between 1 and 100.
- Navigate: Use the following voice commands to control the state of the game:
  - "Restart": Resets the secret number and starts a new round immediately.
  - "Exit Game": Safely closes the application.

### Technical Details
- Language: C#
- Framework: .NET 6.0+
- Library: System.Speech

Installation and Running
For both parts, clone the repository:

```
git clone https://github.com/dev-gabb-711/Audio-Guided-Number-Guessing-Game.git
```
Navigate into the project folder:

```
cd Final
```
Add the required speech package:

```
dotnet add package System.Speech
```
Run the application:

```
dotnet run
```

Please enjoy this little project I've made designed with accessibility in mind. Thank you :))
