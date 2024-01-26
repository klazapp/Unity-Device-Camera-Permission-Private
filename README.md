# LogMessage Utility for Unity

## Introduction
The `LogMessage` utility class is part of the `com.Klazapp.Utility` namespace, designed for Unity projects to simplify logging process. It allows for an easy on/off switch to enable or disable all log messages in the app, eliminating the need to manually comment out or add log statements in each script.

## Features
- Easy toggling of log messages with a single switch.
- Methods for standard, error, and warning messages, each color-coded for clarity.
- Conditional compilation to ensure log calls are only included in builds where logging is enabled.

## Usage
To use `LogMessage`, first ensure it is correctly added to your Unity project. You can then call its methods in your scripts as follows:

```csharp
LogMessage.Debug("This is a debug message");
LogMessage.DebugError("This is an error message");
LogMessage.DebugWarning("This is a warning message");
```

To enable logging, define `ENABLE_LOGS` in your project's compilation symbols. When `ENABLE_LOGS` is not defined, calls to `LogMessage` methods will be ignored, reducing overhead in production builds.

## Installation
To install the `LogMessage` utility via Unity's Package Manager, follow these steps:

1. **Open the Unity Package Manager**:
   - In Unity, go to `Window` > `Package Manager`.

2. **Add Package from Git URL**:
   - In the Package Manager, click the `+` icon at the top left corner and select `Add package from git URL...`.
   - Enter the Git URL for this repository. It usually looks like `https://github.com/klazapp/Unity-Logger-Public.git`.

3. **Import into Your Project**:
   - Unity will resolve and download the package.
   - Once the package is downloaded, it will be available in your project, and you can start using `LogMessage` in your scripts.

## License
This utility is released under the [MIT License](LICENSE).
