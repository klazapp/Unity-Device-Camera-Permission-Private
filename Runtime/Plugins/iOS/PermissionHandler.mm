#import <AVFoundation/AVFoundation.h>
#import <UIKit/UIKit.h>
#import <Unity/UnityInterface.h>

extern "C" {
    void RequestNativeIOSCameraPermission() {
        [AVCaptureDevice requestAccessForMediaType:AVMediaTypeVideo completionHandler:^(BOOL granted) {
            // Handle the permission request result
            dispatch_async(dispatch_get_main_queue(), ^{
                if (granted) {
                    UnitySendMessage("DeviceCameraPermissionHandler", "IOS_CameraPermissionCallBack", "true");
                } else {
                    UnitySendMessage("DeviceCameraPermissionHandler", "IOS_CameraPermissionCallBack", "false");
                }
            });
        }];
    }
}
