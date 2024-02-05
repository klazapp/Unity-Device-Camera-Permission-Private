using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEditor;
#if UNITY_IOS
using System.Runtime.InteropServices;
#endif
#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace com.Klazapp.Utility
{
    public class DeviceCameraPermissionHandler : MonoSingletonGlobal<DeviceCameraPermissionHandler>
    {
        #region Variables
        private Action<bool> onDeviceCameraPermissionCallback;
        
        private static readonly WaitForSeconds oneWfs = new(0.1f);
        
#if UNITY_IOS
        [DllImport("__Internal")]
        private static extern void RequestNativeIOSCameraPermission();
#endif
        #endregion
        
        #region Public Access
        public static bool GetDeviceCameraPermission()
        {
            if (Application.isEditor)
            {
                //Checking devices instead of Application/Permission HasAuthorization/Permission because in editor will always return true
                var devices = WebCamTexture.devices;
                return devices.Length > 0;
            }

            
#if UNITY_ANDROID
            return Permission.HasUserAuthorizedPermission(Permission.Camera);
#elif UNITY_IOS
            return Application.HasUserAuthorization(UserAuthorization.WebCam);
#elif UNITY_STANDALONE_WIN || UNITY_STANDALONE_OSX
            //On Windows and macOS (standalone builds), assume camera is available (no direct API to check)
            //TODO: Implement custom solution
            return true; 
#elif UNITY_WEBGL
            //For WebGL, camera access depends on browser permissions, which can't be checked directly from Unity
            //TODO: Implement custom javascript solution
            return false;
#endif
            return false;
        }
        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void RequestDeviceCameraPermission(Action<bool> deviceCameraPermissionCallback = null)
        {
            onDeviceCameraPermissionCallback = deviceCameraPermissionCallback;
            
            if (Application.isEditor)
            {
                var devices = WebCamTexture.devices;

                DeviceCameraPermissionCallback(devices.Length > 0);
            }
            else
            {
#if UNITY_ANDROID
                if (!Permission.HasUserAuthorizedPermission(Permission.Camera))
                {
                    //If permission is not granted, request it
                    var callbacks = new PermissionCallbacks();
                    callbacks.PermissionDenied += Android_CameraPermissionDeniedCallBack;
                    callbacks.PermissionGranted += Android_CameraPermissionGrantedCallBack;
                    callbacks.PermissionDeniedAndDontAskAgain += Android_CameraPermissionDeniedAndDontAskAgainCallBack;
                    Permission.RequestUserPermission(Permission.Camera, callbacks);
                }
                else
                {
                    DeviceCameraPermissionCallback(true);
                }
#elif UNITY_IOS
                if (!Application.HasUserAuthorization(UserAuthorization.WebCam))
                {
                    //Call native iOS function
                    RequestNativeIOSCameraPermission();
                }
                else
                {
                    DeviceCameraPermissionCallback(true);
                }
#endif
            }
        }

        #region Callbacks
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Android_CameraPermissionDeniedAndDontAskAgainCallBack(string _)
        {
            StartCoroutine(DelayAndroidDeviceCameraPermissionCallbackCo(false));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Android_CameraPermissionGrantedCallBack(string _)
        {
            StartCoroutine(DelayAndroidDeviceCameraPermissionCallbackCo(true));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Android_CameraPermissionDeniedCallBack(string _)
        {
            StartCoroutine(DelayAndroidDeviceCameraPermissionCallbackCo(false));
        }
        
        // Android camera textures cannot immediately load after being called so its delayed by 1 second
        // Only used for Android device callbacks
        private IEnumerator DelayAndroidDeviceCameraPermissionCallbackCo(bool isGranted)
        {
            yield return oneWfs;
            DeviceCameraPermissionCallback(isGranted);
        }
        
        public void IOS_CameraPermissionCallBack(string isGranted)
        {
            DeviceCameraPermissionCallback(bool.Parse(isGranted));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void DeviceCameraPermissionCallback(bool isGranted)
        {
            onDeviceCameraPermissionCallback?.Invoke(isGranted);
        }
        #endregion
    }
}
