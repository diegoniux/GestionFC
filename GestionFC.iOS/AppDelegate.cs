using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using GestionFC.Views;
using UIKit;
using UserNotifications;
using Firebase.InstanceID;
using Firebase.Core;
using Firebase.CloudMessaging;


namespace GestionFC.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate, IUNUserNotificationCenterDelegate, IMessagingDelegate
    {
        public event EventHandler<UserInfoEventArgs> MessageReceived;

        // class-level declarations

        //public override UIWindow Window
        //{
        //    get;
        //    set;
        //}

        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            global::Xamarin.Forms.Forms.Init();

            //(Window.RootViewController as UINavigationController).PushViewController(new UserInfoViewController(this), true);
            //UIApplication.SharedApplication.StatusBarStyle = UIStatusBarStyle.LightContent;

            //Firebase.Core.App.Configure();
            FFImageLoading.Forms.Platform.CachedImageRenderer.Init();
            LoadApplication(application: new App());

            //// Register your app for remote notifications.
            //if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            //{
            //    // For iOS 10 display notification (sent via APNS)
            //    UNUserNotificationCenter.Current.Delegate = this;

            //    var authOptions = UNAuthorizationOptions.Alert | UNAuthorizationOptions.Badge | UNAuthorizationOptions.Sound;
            //    UNUserNotificationCenter.Current.RequestAuthorization(authOptions, (granted, error) => {
            //        Console.WriteLine(granted);
            //    });
            //}
            //else
            //{
            //    // iOS 9 or before
            //    var allNotificationTypes = UIUserNotificationType.Alert | UIUserNotificationType.Badge | UIUserNotificationType.Sound;
            //    var settings = UIUserNotificationSettings.GetSettingsForTypes(allNotificationTypes, null);
            //    UIApplication.SharedApplication.RegisterUserNotificationSettings(settings);
            //}

            //UIApplication.SharedApplication.RegisterForRemoteNotifications();

            //Messaging.SharedInstance.Delegate = this;

            //InstanceId.SharedInstance.GetInstanceId(InstanceIdResultHandler);

            return base.FinishedLaunching(app, options);
        }

        //void InstanceIdResultHandler(InstanceIdResult result, NSError error)
        //{
        //    if (error != null)
        //    {
        //        LogInformation(nameof(InstanceIdResultHandler), $"Error: {error.LocalizedDescription}");
        //        return;
        //    }

        //    LogInformation(nameof(InstanceIdResultHandler), $"Remote Instance Id token: {result.Token}");
        //}

        //[Export("messaging:didReceiveRegistrationToken:")]
        //public void DidReceiveRegistrationToken(Messaging messaging, string fcmToken)
        //{
        //    LogInformation(nameof(DidReceiveRegistrationToken), $"Firebase registration token: {fcmToken}");
        //    //Dependency.DataManager.FCMToken = fcmToken;
        //}

        //public override void DidReceiveRemoteNotification(UIApplication application, NSDictionary userInfo, Action<UIBackgroundFetchResult> completionHandler)
        //{

        //    Messaging.SharedInstance.AppDidReceiveMessage(userInfo);

        //    // Generate custom event
        //    NSString[] keys = { new NSString("Event_type") };
        //    NSObject[] values = { new NSString("Recieve_Notification") };
        //    var parameters = NSDictionary<NSString, NSObject>.FromObjectsAndKeys(keys, values, keys.Length);

        //    // Send custom event
        //    //Firebase.Analytics.Analytics.LogEvent("CustomEvent", parameters);

        //    if (application.ApplicationState == UIApplicationState.Active)
        //    {
        //        System.Diagnostics.Debug.WriteLine(userInfo);
        //        var aps_d = userInfo["aps"] as NSDictionary;
        //        var alert_d = aps_d["alert"] as NSDictionary;
        //        var body = alert_d["body"] as NSString;
        //        var title = alert_d["title"] as NSString;
        //        var category_d = aps_d["category"] as NSDictionary;
        //    }
        //}

        //[Export("messaging:didReceiveMessage:")]
        //public void DidReceiveMessage(Messaging messaging, RemoteMessage remoteMessage)
        //{
        //    // Handle Data messages for iOS 10 and above.
        //    HandleMessage(remoteMessage.AppData);

        //    LogInformation(nameof(DidReceiveMessage), remoteMessage.AppData);
        //}

        //void HandleMessage(NSDictionary message)
        //{
        //    if (MessageReceived == null)
        //        return;

        //    MessageType messageType;
        //    if (message.ContainsKey(new NSString("aps")))
        //        messageType = MessageType.Notification;
        //    else
        //        messageType = MessageType.Data;

        //    var e = new UserInfoEventArgs(message, messageType);
        //    MessageReceived(this, e);
        //}

        //public static void ShowMessage(string title, string message, UIViewController fromViewController, Action actionForOk = null)
        //{
        //    var alert = UIAlertController.Create(title, message, UIAlertControllerStyle.Alert);
        //    alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, (obj) => actionForOk?.Invoke()));
        //    fromViewController.PresentViewController(alert, true, null);
        //}

        //public void DidRefreshRegistrationToken(Messaging messaging, string fcmToken)
        //{
        //    System.Diagnostics.Debug.WriteLine($"FCM Token: {fcmToken}");
        //}

        //void LogInformation(string methodName, object information) => Console.WriteLine($"\nMethod name: {methodName}\nInformation: {information}");
    }
}
