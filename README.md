# DeviceCameraPermissionHandler Utility for Unity

## Introduction
The `DeviceCameraPermissionHandler` utility, part of the `com.Klazapp.Utility` namespace, is specifically designed for Unity projects to manage camera permissions across multiple platforms. It offers a straightforward solution for requesting and checking camera permissions, ensuring a seamless integration of camera functionalities in Unity applications.

## Features
- **Unified Camera Permission Handling**: Simplifies camera permission requests and checks across iOS, Android, Windows, macOS, and WebGL.
- **Platform-Specific Implementation**: Tailored approaches for each supported platform to ensure optimal functionality.
- **Editor Compatibility**: Special handling in the Unity editor for testing and development purposes.

## Dependencies
To use `DeviceCameraPermissionHandler`, ensure your Unity project meets the following requirements:
- **Unity Version**: Requires Unity 2020.3 LTS or higher.
- **Repository**: Access the utility at [DeviceCameraPermissionHandler Unity Utility](https://github.com/klazapp/Unity-Device-Camera-Permission-Private.git).

## Compatibility
| Platform       | iOS | Android | Windows | macOS | WebGL |
|----------------|-----|---------|---------|-------|-------|
| Compatibility  | ✔️  | ✔️      | ✔️      | ✔️    | ❓     |

## Installation
1. In Unity, go to `Window` > `Package Manager`.
2. Click the `+` icon, select `Add package from git URL...`, and input `https://github.com/klazapp/Unity-DeviceCameraPermissionHandler-Public.git`.
3. The package will be automatically downloaded and integrated into your project.

## Usage
To request and check camera permissions, use the following methods:
```csharp
DeviceCameraPermissionHandler.Instance.RequestDeviceCameraPermission((isGranted) => {
    // Handle the result of the permission request
});

bool hasPermission = DeviceCameraPermissionHandler.GetDeviceCameraPermission();
```

## Planned Enhancements (To-Do List)
- **WebGL Support**: Implement a solution for camera permission handling in WebGL.
- **Advanced Permission Management**: Additional features for more intricate permission scenarios.
- **User-Friendly Prompts**: Enhanced UI/UX for permission requests.

## License
`DeviceCameraPermissionHandler` is available under the [MIT License](LICENSE).
