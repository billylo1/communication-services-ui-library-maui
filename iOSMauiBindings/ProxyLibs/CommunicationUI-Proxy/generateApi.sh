cd DeviceFramework/Release-iphoneos
sharpie bind -sdk iphoneos -output ./ -namespace iOS.CallingUI.Binding -scope ./CommunicationUI_Proxy.framework/Headers ./CommunicationUI_Proxy.framework/Headers/CommunicationUI_Proxy-Swift.h
echo Next step: Fix imports, remove Verify and change NSURL to NSUrl, then copy it to the iOS.CallingUI.Binding directory
